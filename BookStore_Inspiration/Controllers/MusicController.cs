using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Model.Enum;
using BookStore.Services.Contracts;
using BookStore_Inspiration.Helper;
using BookStore_Inspiration.ViewModels;
using BookStore_Inspiration.ViewModels.Product.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_Inspiration.Controllers
{
    public class MusicController : Controller
    {
        private readonly IProductServices productServices;
        private readonly IUserServices _userServices;
        private readonly IGenreService _genreService;

        public MusicController(IProductServices productServices, IUserServices userServices, IGenreService genreService)
        {
            this.productServices = productServices;
            _userServices = userServices;
            _genreService = genreService;
        }


        [Authorize]
        public IActionResult Clasic()
        {

            var clasic = GenreList.GenreNames[3];
            var genreId = _genreService.All().Where(x => x.Name.ToLower().Contains(clasic.ToLower())).Select(x => x.Id).FirstOrDefault();

            var allProductsN = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Music && type.GenreId == genreId)
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
        public IActionResult Estrada()
        {
            var estrada = GenreList.GenreNames[4];
            var genreId = _genreService.All().Where(x => x.Name.ToLower().Contains(estrada.ToLower())).Select(x => x.Id).FirstOrDefault();

            var allProductsN = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Music && type.GenreId == genreId)
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