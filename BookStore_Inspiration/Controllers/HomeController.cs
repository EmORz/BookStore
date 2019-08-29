using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels.Product.Home;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BookStore_Inspiration.ViewModels;

namespace BookStore_Inspiration.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductServices _productServices;
        private readonly IUserServices _userServices;

        public HomeController(IProductServices productServices, IUserServices userServices)
        {
            _productServices = productServices;
            _userServices = userServices;
        }
        public IActionResult Index([FromQuery]string criteria = null)
        {
   
            var allProductsN =_productServices.GetAllProducts(criteria).Select(x => new ProductIndexHomeViewModel
            {
                Id = x.Id,
                Description = x.Description,
                Price = x.Price,
                Picture = x.Picture,
                Publishing = x.Publishing,
                Title = x.Title,
                UsersCount = _userServices.GetAllUsers().Count
            }).ToList();

            this.ViewData["criteria"] = criteria;

            AllProductIndex allP = new AllProductIndex()
            {
                Products = allProductsN
            };
            return View(allP);
        }

       
        
    }
}
