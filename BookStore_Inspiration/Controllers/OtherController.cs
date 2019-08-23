using BookStore.Model.Enum;
using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels.Product.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStore_Inspiration.Controllers
{
    public class OtherController : Controller
    {
        private readonly IProductServices productServices;
        private readonly IUserServices _userServices;

        public OtherController(IProductServices productServices, IUserServices userServices)
        {
            this.productServices = productServices;
            _userServices = userServices;
        }


        [Authorize]
        public IActionResult Headphones()
        {
            var allProductsN = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Other && type.GenreId == 7)
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