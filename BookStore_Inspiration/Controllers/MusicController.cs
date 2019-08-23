using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Model.Enum;
using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels.Product.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_Inspiration.Controllers
{
    public class MusicController : Controller
    {
        private readonly IProductServices productServices;
        private readonly IUserServices _userServices;

        public MusicController(IProductServices productServices, IUserServices userServices)
        {
            this.productServices = productServices;
            _userServices = userServices;
        }


        [Authorize]
        public IActionResult Clasic()
        {
            var allProductsN = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Music && type.GenreId == 4)
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
            var allProductsN = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Music && type.GenreId == 5)
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