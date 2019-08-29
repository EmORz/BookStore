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
        private readonly IUserServices _userServices;
        private readonly ICloudinaryServices _cloudinaryService;
        private readonly IGenreService _genreService;

        private const string  youTubeEmbed = "https://www.youtube.com/embed/";

        public ProductController(IProductServices productServices,
            IImagesService imagesService, IUserServices userServices, ICloudinaryServices cloudinaryService, IGenreService genreService)
        {
            this.productServices = productServices;
            this.imagesService = imagesService;
            _userServices = userServices;
            _cloudinaryService = cloudinaryService;
            _genreService = genreService;
        }

        public IActionResult Details(int id)
        {
            var product = this.productServices.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            var link = "";

            if (product.YouTubeLink=="xx")
            {
                link = product.YouTubeLink;
            }
            else
            {
                link = youTubeEmbed + product.YouTubeLink;
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
                Publishing = product.Publishing,
                YouTubeLink = link
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
                youTubeLink = product.YouTubeLink,
                GenreId = product.GenreId
   
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

            var youTubeRender = model.youTubeLink.Split("/");
            var tempLinkKey = "";
            if (youTubeRender.Length != 4)
            {
                tempLinkKey = "xx";
            }
            else
            {
                tempLinkKey = youTubeRender[3];
            }


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
                Picture = pictureUrl,
                YouTubeLink = tempLinkKey,
                GenreId = model.GenreId
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
        [HttpGet]
        public IActionResult Create()
        {
            var genres = _genreService.All().Select(x => new GenreViewModels()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            CreateProductBindingModel createProduct = new CreateProductBindingModel()
            {

                GenresViewModel = genres
            };
            return View(createProduct);
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
                createProduct.YearOfPublishing, pictureUrl, createProduct.YouTubeLink, createProduct.GenreId);
            return this.Redirect("/");
        }



    }
}