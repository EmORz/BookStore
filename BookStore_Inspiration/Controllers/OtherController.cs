using BookStore.Model.Enum;
using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels.Product.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BookStore_Inspiration.Helper;
using BookStore_Inspiration.ViewModels;

namespace BookStore_Inspiration.Controllers
{
    public class OtherController : Controller
    {
        private readonly IProductServices productServices;
        private readonly IUserServices _userServices;
        private readonly IGenreService _genreService;

        public OtherController(IProductServices productServices, IUserServices userServices, IGenreService genreService)
        {
            this.productServices = productServices;
            _userServices = userServices;
            _genreService = genreService;
        }

        [Authorize]
        public IActionResult Kancelariq()
        {
            var headphones = GenreList.GenreNames[12];
            var genreId = _genreService.All().Where(x => x.Name.ToLower().Contains(headphones.ToLower())).Select(x => x.Id).FirstOrDefault();

            var allProductsN = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Other && type.GenreId == genreId)
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
        public IActionResult Headphones()
        {
            var headphones = GenreList.GenreNames[5];
            var genreId = _genreService.All().Where(x => x.Name.ToLower().Contains(headphones.ToLower())).Select(x => x.Id).FirstOrDefault();

            var allProductsN = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Other && type.GenreId == genreId)
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