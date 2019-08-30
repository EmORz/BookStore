using BookStore.Model.Enum;
using BookStore.Services.Contracts;
using BookStore_Inspiration.Helper;
using BookStore_Inspiration.ViewModels;
using BookStore_Inspiration.ViewModels.Product.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStore_Inspiration.Controllers
{
    public class FilmController : Controller
    {
        private readonly IProductServices productServices;
        private readonly IUserServices _userServices;
        private readonly IGenreService _genreService;

        public FilmController(IProductServices productServices, IUserServices userServices, IGenreService genreService)
        {
            this.productServices = productServices;
            _userServices = userServices;
            _genreService = genreService;
        }

        [Authorize]
        public IActionResult Dvd()
        {
            var headphones = GenreList.GenreNames[11];
            var genreId = _genreService.All().Where(x => x.Name.ToLower().Contains(headphones.ToLower())).Select(x => x.Id).FirstOrDefault();

            var allProductsN = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Film && type.GenreId == genreId)
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
        public IActionResult StValentin()
        {
            var stValent = GenreList.GenreNames[2];
            var genreId = _genreService.All().Where(x => x.Name.ToLower().Contains(stValent.ToLower())).Select(x => x.Id).FirstOrDefault();

            var allProductsN = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Film && type.GenreId == genreId)
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