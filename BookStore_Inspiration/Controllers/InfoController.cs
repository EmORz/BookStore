using BookStore.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Authorization = System.Net.Authorization;

namespace BookStore_Inspiration.Controllers
{
    public class InfoController : BaseController
    {
        private readonly IUserServices userServices;

        public InfoController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        public IActionResult UserDetails(string username)
        {
            var userData = this.userServices.GetUserByUsername(username);
            return View(userData);
        }
        public IActionResult ForUs()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult PaymentMethods()
        {
            return View();
        }

        public IActionResult Deliver()
        {
            return View();
        }
        public IActionResult SearchBox()
        {
            return View();
        }
    }
}