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
        public IActionResult Uchitelq()
        {

            var children = GenreList.GenreNames[8];
            var genreId = _genreService.All().Where(x => x.Name.ToLower().Contains(children.ToLower())).Select(x => x.Id).FirstOrDefault();

            var allProductsN = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Book && type.GenreId == genreId)
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
        public IActionResult Hudojestvena()
        {

            var children = GenreList.GenreNames[7];
            var genreId = _genreService.All().Where(x => x.Name.ToLower().Contains(children.ToLower())).Select(x => x.Id).FirstOrDefault();

            var allProductsN = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Book && type.GenreId == genreId)
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
        public IActionResult Psihologiya()
        {

            var children = GenreList.GenreNames[6];
            var genreId = _genreService.All().Where(x => x.Name.ToLower().Contains(children.ToLower())).Select(x => x.Id).FirstOrDefault();

            var allProductsN = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Book && type.GenreId == genreId)
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
        public IActionResult Children()
        {

            var children = GenreList.GenreNames[0];
            var genreId = _genreService.All().Where(x => x.Name.ToLower().Contains(children.ToLower())).Select(x => x.Id).FirstOrDefault();

            var allProductsN = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Book && type.GenreId == genreId)
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

            var history = GenreList.GenreNames[1];
            var genreId = _genreService.All().Where(x => x.Name.ToLower().Contains(history.ToLower())).Select(x => x.Id).FirstOrDefault();

            var allProductsN = productServices.GetAllProducts()
                .Where(type => type.ProductTypes == ProductTypes.Book && type.GenreId == genreId)
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