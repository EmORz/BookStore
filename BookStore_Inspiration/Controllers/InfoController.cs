using BookStore.Data;
using BookStore.Model;
using BookStore.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using BookStore_Inspiration.ViewModels;
using BookStore_Inspiration.ViewModels.Recipient;
using BookStore_Inspiration.ViewModels.Search;

namespace BookStore_Inspiration.Controllers
{
    public class InfoController : Controller
    {
        private readonly IUserServices userServices;
        private readonly IProductServices _productServices;
        private readonly BookStoreDbContext _db;
        private readonly IAddressesServices _addressesServices;
        private readonly IOrderServices _orderServices;
        private readonly ISearchService _searchService;
        private readonly IIncomeMoneyService _incomeMoneyService;

        public InfoController(IUserServices userServices, IProductServices productServices, BookStoreDbContext db, 
            IAddressesServices addressesServices, IOrderServices orderServices, IIncomeMoneyService incomeMoneyService, ISearchService searchService)
        {
            this.userServices = userServices;
            _productServices = productServices;
            _db = db;
            _addressesServices = addressesServices;
            _orderServices = orderServices;
            _searchService = searchService;
            _incomeMoneyService = incomeMoneyService;
        }

        public IActionResult UserDetails(string username)
        {
            var userData = this.userServices.GetUserByUsername(username);
            return View(userData);
        }
        public IActionResult ForUs()
        {
            return View();
        }

        public IActionResult Contact()
        {
            GetAddress addressViewMOdel = new GetAddress();
            var street = "ьь";
            var town = "ьь";
            var fullAddress = "в Системата няма данни за Вашият адрес.";

            var user = userServices.GetUserByUsername(User.Identity.Name);
            var order = this._orderServices.GetProcessingOrder(this.User.Identity.Name);
            if (order !=null)
            {
                var adddress = _addressesServices.GetAllAddresses().Where(x => x.Id == order.DeliveryAddressId);
                foreach (var address in adddress)
                {
                    street = address.Street + " " + address.BuildingNumber;
                    town = address.City.Name + " " + address.City.Postcode;
                    fullAddress = street + " " + town;
                    addressViewMOdel.Address = fullAddress;
                }
            }
            else
            {
                addressViewMOdel.Address = fullAddress;
            }

            return View(addressViewMOdel);
        }

        public IActionResult PaymentMethods()
        {
            return View();
        }

        public IActionResult Deliver()
        {
            return View();
        }

        [HttpGet]

        public IActionResult SearchBox()
        {

            return View();
        }

        [HttpPost]
        public IActionResult SearchBox(Input input)
        {
           
        
            var userId = userServices.GetUserByUsername(User.Identity.Name).Id;
            var search = _productServices.GetProductsBySearch(input.InputStr).ToList();

            foreach (var searchProduct in search)
            {
                _searchService.Create(
                    searchProduct.Id,
                    searchProduct.Author,
                    searchProduct.ISBN,
                    searchProduct.Publishing,
                    searchProduct.Title,
                    userId);
            }

       
            


            return Redirect("Result");
        }

        [HttpGet]
        //todo search box Controller
        public IActionResult Result()
        {

            var userId = userServices.GetUserByUsername(User.Identity.Name).Id;
            var resultFromSearch = _searchService.AllSearchesResults().Where(x => x.UserId == userId && x.DateTime.Day == DateTime.Now.Day).Select(x => new SearchProductViewModel()
            {
                Author = x.Author,
                ISBN = x.ISBN,
                Publishing = x.Publishing,
                Title = x.Title,
                DateTimeOfSearch = x.DateTime
            }).ToList();

            return View(resultFromSearch);
        }

        [HttpGet]
        //todo search box Controller
        public IActionResult DeleteHistoty()
        {

            var userId = userServices.GetUserByUsername(User.Identity.Name).Id;
            _searchService.DeleteHistotyOfSearch(userId);

         

            return Redirect("/");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Receipts()
        {
            var clientReceipts = _incomeMoneyService.AllPurchase().Select(x => new Receiptclient
            {
                UserId = x.UserId,
                Address = x.AddressDelivery,
                paymentMethod = x.PaymentMethod,
                TimeOfPurchase = x.DateTimeOfPurchase,
                productId = x.ProductId,
                Totalmoney = x.TotalMoney
            }).ToList();



            return View(clientReceipts);
        }

        [HttpGet]
        public IActionResult ReceiptsClient()
        {
            var user = userServices.GetUserByUsername(this.User.Identity.Name);
            var clientReceipts = _incomeMoneyService.AllPurchase().Where(x => x.UserId == user.Id).Select(x => new Receiptclient
            {
                UserId = x.UserId,
                Address = x.AddressDelivery,
                paymentMethod = x.PaymentMethod,
                TimeOfPurchase = x.DateTimeOfPurchase,
                productId = x.ProductId,
                Totalmoney = x.TotalMoney
            }).ToList();
       
            return View(clientReceipts);
        }

        [Authorize(Roles = ("Admin"))]
        [HttpGet]
        public IActionResult ReceiptsClientA(string id)
        {
         
            var clientReceipts = _incomeMoneyService.AllPurchase().Where(x => x.UserId == id).Select(x => new Receiptclient
            {
                UserId = x.UserId,
                Address = x.AddressDelivery,
                paymentMethod = x.PaymentMethod,
                TimeOfPurchase = x.DateTimeOfPurchase,
                productId = x.ProductId,
                Totalmoney = x.TotalMoney
            }).ToList();

            return View(clientReceipts);
        }

    }
}