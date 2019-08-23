using BookStore.Model.Enum;
using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels.Product.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStore_Inspiration.Controllers
{
    public class BookController : Controller
    {
        private readonly IProductServices productServices;
        private readonly IUserServices _userServices;
        private readonly IGenreService _genreService;

        public BookController(IProductServices productServices, IUserServices userServices, IGenreService genreService)
        {
            this.productServices = productServices;
            _userServices = userServices;
            _genreService = genreService;
        }


        [Authorize]
        public IActionResult Children()
        {
            var allProductsN = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Book && type.GenreId == 1)
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
        public IActionResult History()
        {
            var allProductsN = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Book && type.GenreId == 2)
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