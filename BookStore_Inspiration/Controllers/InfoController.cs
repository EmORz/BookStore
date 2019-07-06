using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_Inspiration.Controllers
{
    public class InfoController : BaseController
    {
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