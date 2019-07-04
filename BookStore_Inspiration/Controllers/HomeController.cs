using BookStore_Inspiration.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStore_Inspiration.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

       
        
    }
}
