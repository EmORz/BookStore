using System.Net;
using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

        public IActionResult Details(int id)
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

        [Authorize()]
        public IActionResult Book()
        {
            return View();
        }

        [Authorize]
        public IActionResult Film()
        {
            return View();
        }

        [Authorize]
        public IActionResult Music()
        {
            return View();
        }

        [Authorize]
        public IActionResult Other()
        {
            return View();
        }
    }
}