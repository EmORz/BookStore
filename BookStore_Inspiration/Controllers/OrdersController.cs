using System.Collections.Generic;
using System.Linq;
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

        public OrdersController(IUserServices userServices, IAddressesServices addressesServices, IProductServices productServices)
        {
            _userServices = userServices;
            _addressesServices = addressesServices;
            _productServices = productServices;
        }
        public IActionResult Create(int id)
        {
            var user = this._userServices.GetUserByUsername(this.User.Identity.Name);

            var product = _productServices.GetProductById(id);

            var productsView = new ProductOrderViewModel()
            {
                Title = product.Title,
                Price = product.Price,
                Quantity = 1
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

     

         



            if (user.FirstName==null)
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