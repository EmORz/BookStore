using BookStore.Model;
using BookStore.Model.Enum;
using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels;
using BookStore_Inspiration.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore_Inspiration.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices productServices;
        private readonly IImagesService imagesService;

        public ProductController(IProductServices productServices, IImagesService imagesService)
        {
            this.productServices = productServices;
            this.imagesService = imagesService;
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
                //Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                ProductTypes = product.ProductTypes.ToString()
            };

            return View(DetailsProductViewModel);
        }
        //
        public IActionResult Edit(int id)
        {
            var product = this.productServices.GetProductById(id);
            
            if (product == null)
            {
                return new NotFoundResult();
            }
            var model = new EditProductViewModel()
            {
                Title = product.Title,
                Author = product.Author,
                Description = product.Description,
                ISBN = product.ISBN,
                Price = product.Price,
                ProductTypes = product.ProductTypes.ToString(),
                Publishing = product.Publishing,
                Quantity = product.Quantity,
                YearOfPublishing = product.YearOfPublishing
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

            Enum.TryParse<ProductTypes>(model.ProductTypes, true, out ProductTypes result);
            var product = new Product()
            {
                Id = model.Id,
                Title = model.Title,
                Price = model.Price,
                Quantity = model.Quantity,
                ProductTypes = result,
                Author = model.Author,
                Description = model.Description,
                ISBN = model.ISBN
            };
            this.productServices.EditProduct(product);

            return this.Redirect("/Product/All");

        }
        //




        [Authorize()]
        public IActionResult Book()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            this.productServices.Delete(id);

            return this.Redirect("/");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult All()
        {
            var product = this.productServices.GetAllProducts().Select(x => new ProductViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                Description = x.Description,
                ISBN = x.ISBN,
                Price = x.Price,
                ProductTypes = x.ProductTypes.ToString(),
                Publishing = x.Publishing,
                Quantity = x.Quantity,
                YearOfPublishing = x.YearOfPublishing
            }).ToList();
            AllProductsViewModel products = new AllProductsViewModel();
            products.Products.AddRange(product);
            return View(products);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(CreateProductBindingModel createProduct)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/");
            }

            productServices.Create(createProduct.Title, createProduct.ProductTypes, createProduct.Price,
                createProduct.Quantity, createProduct.Description, createProduct.Author, createProduct.Publishing,
                createProduct.YearOfPublishing);
            return this.Redirect("/");
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