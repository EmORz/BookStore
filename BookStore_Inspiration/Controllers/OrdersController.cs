using System.Collections.Generic;
using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels.Orders.Create;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_Inspiration.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IUserServices _userServices;

        public OrdersController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        public IActionResult Create()
        {
            var user = this._userServices.GetUserByUsername(this.User.Identity.Name);

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
                PhoneNumber = user.PhoneNumber
            };

            return this.View(createOrderViewModel);
        }
    }
}