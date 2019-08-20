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
using BookStore.Services;

namespace BookStore_Inspiration.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices productServices;
        private readonly IImagesService imagesService;
        private readonly IUserServices _userServices;
        private readonly ICloudinaryServices _cloudinaryService;

        public ProductController(IProductServices productServices,
            IImagesService imagesService, IUserServices userServices, ICloudinaryServices cloudinaryService)
        {
            this.productServices = productServices;
            this.imagesService = imagesService;
            _userServices = userServices;
            _cloudinaryService = cloudinaryService;
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
                Title = product.Title,
                Price = product.Price,
                Quantity = product.Quantity,
                ProductTypes = product.ProductTypes.ToString(),
                Author = product.Author,
                Description = product.Description,
                ISBN = product.ISBN,
                Picture = product.Picture,
                Publishing = product.Publishing
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
                YearOfPublishing = product.YearOfPublishing,
   
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
            string pictureUrl = this._cloudinaryService.UploadPictureAsync(
                model.Picture,
                model.Title);
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
                ISBN = model.ISBN,
                Picture = pictureUrl
            };
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
            string pictureUrl = this._cloudinaryService.UploadPictureAsync(
                createProduct.Picture,
                createProduct.Title);

            productServices.Create(createProduct.Title, createProduct.ProductTypes, createProduct.Price,
                createProduct.Quantity, createProduct.Description, createProduct.Author, createProduct.Publishing,
                createProduct.YearOfPublishing, pictureUrl);
            return this.Redirect("/");
        }


        [Authorize()]
        public IActionResult Book()
        {
            var allProductsN = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Book)
                .Select(x => new ProductIndexHomeViewModel
            {
                Id = x.Id,
                Description = x.Description,
                Price = x.Price,
                Picture = x.Picture,
                Publishing = x.Publishing,
                Title = x.Title,
                UsersCount = _userServices.GetAllUsers().Count
            }).ToList();

            AllProductIndex allP = new AllProductIndex()
            {
                Products = allProductsN
            };

            return View(allP);
        }

        [Authorize]
        public IActionResult Film()
        {
            var allProductsN = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Film)
                .Select(x => new ProductIndexHomeViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Price = x.Price,
                    Picture = x.Picture,
                    Publishing = x.Publishing,
                    Title = x.Title,
                    UsersCount = _userServices.GetAllUsers().Count
                }).ToList();

            AllProductIndex allP = new AllProductIndex()
            {
                Products = allProductsN
            };

            return View(allP);
        }

        [Authorize]
        public IActionResult Music()
        {
            var allProductsN = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Music)
                .Select(x => new ProductIndexHomeViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Price = x.Price,
                    Picture = x.Picture,
                    Publishing = x.Publishing,
                    Title = x.Title,
                    UsersCount = _userServices.GetAllUsers().Count
                }).ToList();

            AllProductIndex allP = new AllProductIndex()
            {
                Products = allProductsN
            };

            return View(allP);
        }

        [Authorize]
        public IActionResult Other()
        {
            var allProductsN = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Other)
                .Select(x => new ProductIndexHomeViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Price = x.Price,
                    Picture = x.Picture,
                    Publishing = x.Publishing,
                    Title = x.Title,
                    UsersCount = _userServices.GetAllUsers().Count
                }).ToList();

            AllProductIndex allP = new AllProductIndex()
            {
                Products = allProductsN
            };

            return View(allP);
        }
    }
}