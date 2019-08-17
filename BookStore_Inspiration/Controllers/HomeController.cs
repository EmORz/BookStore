using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels.Product.Home;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
        public IActionResult Index()
        {
            var allProducts = _productServices.GetAllProducts().Select(product => new ProductHomeViewModel
            {
                Author = product.Author,
                Description = product.Description,
                Id = product.Id,
                ISBN = product.ISBN,
                Price = product.Price,
                ProductTypes = product.ProductTypes.ToString(),
                Publishing = product.Publishing,
                Quantity = product.Quantity,
                Title = product.Title,
                YearOfPublishing = product.YearOfPublishing,
                UsersCount = _userServices.GetAllUsers().Count,
                Picture = product.Picture
            }).ToList();

            AllProductsHomeViewModel all = new AllProductsHomeViewModel()
            {
                Products = allProducts
            };

            return View(all);
        }

       
        
    }
}
