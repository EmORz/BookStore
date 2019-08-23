using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BookStore.Data;
using BookStore.Model;
using BookStore.Model.Address;
using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels.Orders.Create;
using BookStore_Inspiration.ViewModels.Suppliers;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_Inspiration.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IAddressesServices _addressesServices;
        private readonly IProductServices _productServices;
        private readonly BookStoreDbContext _db;
        private readonly ISuppliersService _suppliersService;
        private readonly IOrderServices _orderServices;
        private readonly IIncomeMoneyService _incomeMoneyService;


        public OrdersController(IUserServices userServices, IAddressesServices addressesServices, IProductServices productServices, BookStoreDbContext db,
            ISuppliersService suppliersService, IOrderServices orderServices, IIncomeMoneyService incomeMoneyService)
        {
            _userServices = userServices;
            _addressesServices = addressesServices;
            _productServices = productServices;
            _db = db;
            _suppliersService = suppliersService;
            _orderServices = orderServices;
            _incomeMoneyService = incomeMoneyService;
        }

        [HttpPost]
        public IActionResult Create(CreateOrderViewModel model)
        {
            var user = _userServices.GetUserByUsername(this.User.Identity.Name);

            var userId = user.Id;
            var totalMoney = 0.0m;
            var productIdForRecord = 0;
            var AddressDelivery = "";
            var quantityForRecord = 0;
            var paymentMethodRec = "";
         
            var dayOfPurchase = DateTime.Now;

            var order = this._orderServices.GetProcessingOrder(this.User.Identity.Name);
            if (order == null)
            {
                order = this._orderServices.CreateOrder(this.User.Identity.Name);
            }
            
            decimal deliveryPrice = _suppliersService.GetDiliveryPrice(model.SupplierId, model.DeliveryType);
            this._orderServices.SetOrderDetails(order, model.FullName, model.PhoneNumber, model.PaymentType, model.DeliveryAddressId.Value, deliveryPrice);


            var fullName = order.BookStoreUser.FirstName+ order.BookStoreUser.LastName;
            var adddress = _addressesServices.GetAllAddresses().Where(x => x.Id == order.DeliveryAddressId);

            var taxes = deliveryPrice;


            var typeOfPayment = order.PaymentType.ToString();
            paymentMethodRec = typeOfPayment;

            var productFromDb = _productServices.GetProductById(model.ProductOrderViewModel.ProductId);
            var temporalEnterQuantity = productFromDb.Quantity - model.ProductOrderViewModel.ClientsQuantity;
            var tempText = new List<string>();

            tempText.Add($"ClientName: {this.User.Identity.Name}");
            tempText.Add($"PaymentMethod: {typeOfPayment}");
            tempText.Add($"DeliverPrice: {taxes}");
            StringBuilder sb = new StringBuilder();
            foreach (var address in adddress)
            {

                sb.Append("Град: " + address.City.Name.ToString() + " " + address.City.Postcode);
                sb.Append("Адрес: " + address.Street + " ");
                sb.Append("№ " + address.BuildingNumber + " ");
                sb.AppendLine("Допълнително описание: " + address.Description);
            }

            productIdForRecord = productFromDb.Id;
            AddressDelivery = sb.ToString().Trim();
            tempText.Add($"AddressForDeliver: {AddressDelivery}");
            tempText.Add($"ProductId: {productIdForRecord}");

            if (temporalEnterQuantity > 0)
            {
                productFromDb.Quantity = temporalEnterQuantity;
                var priceWithAddTaxes = productFromDb.Price * 0.1M+productFromDb.Price;

                tempText.Add($"Quantity: {model.ProductOrderViewModel.ClientsQuantity}");
                quantityForRecord = model.ProductOrderViewModel.ClientsQuantity;
                totalMoney = model.ProductOrderViewModel.ClientsQuantity * priceWithAddTaxes;

            tempText.Add($"Price: {productFromDb.Price}");
                tempText.Add($"Price after added taxes from 10 %: {priceWithAddTaxes}");
                tempText.Add($"Total: {totalMoney}");
                tempText.Add($"DateTimeOfPurchase: {DateTime.Now}");
                tempText.Add($"***********************************");
            }
            else
            {
                tempText.Add($"Quantity: NO");
            }
            _incomeMoneyService.Create(userId, totalMoney, productIdForRecord, quantityForRecord, paymentMethodRec, AddressDelivery, dayOfPurchase);

            this._db.Products.Update(productFromDb);
            this._db.SaveChanges();

            System.IO.File.AppendAllLines($"C:\\Users\\User\\source\\repos\\BookStore_Inspiration\\BookStore\\BookStore_Inspiration\\Views\\Info\\OrderResult.txt", tempText);


            return Redirect("/");

        }

        public IActionResult Create(int id)
        {
            var user = this._userServices.GetUserByUsername(this.User.Identity.Name);

            var product = _productServices.GetProductById(id);

            var productsView = new ProductOrderViewModel()
            {
                ProductId = product.Id,
                Title = product.Title,
                Price = product.Price
            };

            var address = _addressesServices.GetAllUserAddresses(User.Identity.Name).ToList();
            var addressViewModel = address.Select(x => new OrderAdressViewModel()
            {
                CityName = x.City.Name,
                CityPostcode = x.City.Postcode,
                Description = x.Description,
                Street = x.Street,
                Id = x.Id
            }).ToList();

            var supplierViewModels = this._suppliersService.All().Select(x => new SupplierViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                IsDefault = x.IsDefault,
                PriceToHome = x.PriceToHome,
                PriceToOffice = x.PriceToOffice
            }).ToList();
         //   var supplierViewModels = _mapper.Map<IList<SupplierViewModel>>(suppliers);

            if (user.FirstName == null)
            {
                user.FirstName = "XXX";
            }
            if (user.LastName == null)
            {
                user.LastName = "XXX";
            }
            if (user.PhoneNumber == null)
            {
                user.PhoneNumber = "XXX";
            }

            var fullName = $"{user.FirstName} {user.LastName}";


            var createOrderViewModel = new CreateOrderViewModel
            {


                FullName = fullName,
                PhoneNumber = user.PhoneNumber,
                OrderAddressesViewModel = addressViewModel.ToList(),
     
                ProductOrderViewModel = productsView,
                SuppliersViewModel = supplierViewModels
               
            };

            return this.View(createOrderViewModel);
        }
    }
}