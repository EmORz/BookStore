using Microsoft.AspNetCore.Mvc;

namespace BookStore_Inspiration.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Book()
        {
            return View();
        }

        public IActionResult Film()
        {
            return View();
        }

        public IActionResult Music()
        {
            return View();
        }

        public IActionResult Other()
        {
            return View();
        }
    }
}