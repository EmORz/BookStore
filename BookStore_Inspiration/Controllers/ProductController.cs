using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_Inspiration.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductServices productServices;

        public ProductController(IProductServices productServices)
        {
            this.productServices = productServices;
        }

        public IActionResult Details(string id)
        {
            var product = this.productServices.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            var DetailsProductViewModel = new DetailsProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                ProductTypes = product.ProductTypes.ToString()
            };

            return View(DetailsProductViewModel);
        }


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