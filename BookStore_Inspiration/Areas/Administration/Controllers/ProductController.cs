using BookStore.Model;
using BookStore.Services.Contracts;
using BookStore_Inspiration.Areas.Administration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStore_Inspiration.Areas.Administration.Controllers
{
    public class ProductController : AdminController
    {
        private readonly IProductServices productServices;

        public ProductController(IProductServices productServices)
        {
            this.productServices = productServices;
        }


        //create
        public IActionResult Create()
        {
            return this.View();
        }
        [HttpPost]
        public IActionResult Create(CreateProductView createProductView)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var product = new Product()
            {
                Id = createProductView.Id,
                Name = createProductView.Name,
                Quantity = createProductView.Quantity,
                Price = createProductView.Price,
                ProductTypes = createProductView.ProductTypes
            };
            this.productServices.AddProduct(product);

            return RedirectToAction(nameof(All));
        }
        //edit
        public IActionResult Edit(int id)
        {
            var product = this.productServices.GetProductById(id);
            if (product==null)
            {
                return NotFound();
            }
            var model = new EditProductViewModel()
            {
                Name = product.Name,
                Price = product.Price,
                ProductTypes = product.ProductTypes,
                Quantity = product.Quantity
            };
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var product = new Product()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Quantity = model.Quantity,
                ProductTypes = model.ProductTypes
            };
            this.productServices.EditProduct(product);

            return RedirectToAction(nameof(All));
        }
        //all
        public IActionResult All()
        {
            var products = this.productServices.GetAllProducts().OrderByDescending(x => x.Price);

            return View(products);
        }

        //
    }
}