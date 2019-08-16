using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Data;
using BookStore.Model;
using BookStore.Model.Address;
using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels.Orders.Create;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_Inspiration.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IAddressesServices _addressesServices;
        private readonly IProductServices _productServices;
        private readonly BookStoreDbContext _db;

        public OrdersController(IUserServices userServices, IAddressesServices addressesServices, IProductServices productServices, BookStoreDbContext db)
        {
            _userServices = userServices;
            _addressesServices = addressesServices;
            _productServices = productServices;
            _db = db;
        }

        [HttpPost]
        public IActionResult Create(CreateOrderViewModel order)
        {
            var productFromDb = _productServices.GetProductById(order.ProductOrderViewModel.ProductId);
            var temporalEnterQuantity = productFromDb.Quantity - order.ProductOrderViewModel.ClientsQuantity;
            var tempText = new List<string>();
            tempText.Add($"ClientName: {this.User.Identity.Name}");
            tempText.Add($"ProductId: {productFromDb.Id}");

            if (temporalEnterQuantity > 0)
            {
                productFromDb.Quantity = temporalEnterQuantity;
                tempText.Add($"Quantity: {order.ProductOrderViewModel.ClientsQuantity}");
                tempText.Add($"Price: {productFromDb.Price}");
                tempText.Add($"Total: {order.ProductOrderViewModel.ClientsQuantity * productFromDb.Price}");
                tempText.Add($"DateTimeOfPurchase: {DateTime.Now}");
                tempText.Add($"***********************************");
            }
            else
            {
              tempText.Add($"Quantity: NO");
            }

            this._db.Products.Update(productFromDb);
            this._db.SaveChanges();

            System.IO.File.AppendAllLines($"C:\\Users\\User\\source\\repos\\BookStore_Inspiration\\BookStore\\BookStore_Inspiration\\Views\\Info\\OrderResult.txt", tempText );


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
                OrderAddressesViewModel = addressViewModel,
                ProductOrderViewModel = productsView
            };

            return this.View(createOrderViewModel);
        }
    }
}