using BookStore.Model;
using BookStore.Model.Enum;
using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels;
using BookStore_Inspiration.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using BookStore_Inspiration.ViewModels.Product.Home;

namespace BookStore_Inspiration.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices productServices;
        private readonly IImagesService imagesService;
        private readonly IUserServices _userServices;

        public ProductController(IProductServices productServices,
            IImagesService imagesService, IUserServices userServices)
        {
            this.productServices = productServices;
            this.imagesService = imagesService;
            _userServices = userServices;
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
                Name = product.Title,
                Price = product.Price,
                Quantity = product.Quantity,
                ProductTypes = product.ProductTypes.ToString()
            };

            return View(DetailsProductViewModel);
        }
        //
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
            product.Quantity--;
            this.productServices.EditProduct(product);

            return this.Redirect("/Product/All");

        }
        //





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

        //[Authorize(Roles = "Admin")]
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


        [Authorize()]
        public IActionResult Book()
        {
            var allProducts = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Book)
                .Select(product => new ProductHomeViewModel
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

        [Authorize]
        public IActionResult Film()
        {
            var allProducts = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Film)
                .Select(product => new ProductHomeViewModel
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

        [Authorize]
        public IActionResult Music()
        {
            var allProducts = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Music)
                .Select(product => new ProductHomeViewModel
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

        [Authorize]
        public IActionResult Other()
        {
            var allProducts = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Other)
                .Select(product => new ProductHomeViewModel
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