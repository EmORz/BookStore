using System.Collections.Generic;
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
            var allProductsN = _productServices.GetAllProducts().Select(x => new ProductIndexHomeViewModel
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


            //var allProducts = _productServices.GetAllProducts().Select(product => new ProductHomeViewModel
            //{
            //    Author = product.Author,
            //    Description = product.Description,
            //    Id = product.Id,
            //    ISBN = product.ISBN,
            //    Price = product.Price,
            //    ProductTypes = product.ProductTypes.ToString(),
            //    Publishing = product.Publishing,
            //    Quantity = product.Quantity,
            //    Title = product.Title,
            //    YearOfPublishing = product.YearOfPublishing,
            //    UsersCount = _userServices.GetAllUsers().Count,
            //    Picture = product.Picture
            //}).ToList();

            //AllProductsHomeViewModel all = new AllProductsHomeViewModel()
            //{
            //    Products = allProducts
            //};

            return View(allP);
        }

       
        
    }

    public class AllProductIndex
    {
        public List<ProductIndexHomeViewModel> Products { get; set; } = new List<ProductIndexHomeViewModel>();
    }

    public class ProductIndexHomeViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }


        public decimal Price { get; set; }

        //???
        public string Description { get; set; }


        public string Publishing { get; set; }

        public string Picture { get; set; }

        public int UsersCount { get; set; }
    }
}
