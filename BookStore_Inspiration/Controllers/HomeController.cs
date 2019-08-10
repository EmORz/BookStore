using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels.Product.Home;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStore_Inspiration.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductServices _productServices;

        public HomeController(IProductServices productServices)
        {
            _productServices = productServices;
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
                YearOfPublishing = product.YearOfPublishing
            }).ToList();

            AllProductsHomeViewModel all = new AllProductsHomeViewModel()
            {
                Products = allProducts
            };

            return View(all);
        }

       
        
    }
}
