﻿using BookStore.Data;
using BookStore.Model;
using BookStore.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using BookStore_Inspiration.ViewModels;
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

        public InfoController(IUserServices userServices, IProductServices productServices, BookStoreDbContext db, IAddressesServices addressesServices, IOrderServices orderServices)
        {
            this.userServices = userServices;
            _productServices = productServices;
            _db = db;
            _addressesServices = addressesServices;
            _orderServices = orderServices;
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
            var search = _productServices.GetProductsBySearch(input.InputStr).ToList();

       
            List<string> temp = new List<string>();
            foreach (var model in search)
            {
                var author = model.Author;
                var title = model.Title;
                var isbn = model.ISBN;
                var publishing = model.Publishing;
                temp.Add(author);
                temp.Add(title);
                temp.Add(isbn);
                temp.Add(publishing);
            }
            temp.Add("Търсенето бе извършено на "+DateTime.Now);
            System.IO.File.AppendAllLines("C:\\Users\\User\\source\\repos\\BookStore_Inspiration\\BookStore\\BookStore_Inspiration\\Views\\Info\\SearchResult.txt", temp);


            return Redirect("Result");
        }

        [HttpGet]
        //todo search box Controller
        public IActionResult Result()
        {

           

           var result = System.IO.File.ReadAllLines("C:\\Users\\User\\source\\repos\\BookStore_Inspiration\\BookStore\\BookStore_Inspiration\\Views\\Info\\SearchResult.txt");

            return View(result);
        }

        [HttpGet]
        //todo search box Controller
        public IActionResult DeleteHistoty()
        {


            System.IO.File.Delete("C:\\Users\\User\\source\\repos\\BookStore_Inspiration\\BookStore\\BookStore_Inspiration\\Views\\Info\\SearchResult.txt");

            return Redirect("/");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Receipts()
        {
            var result = System.IO.File.ReadAllLines($"C:\\Users\\User\\source\\repos\\BookStore_Inspiration\\BookStore\\BookStore_Inspiration\\Views\\Info\\OrderResult.txt");
            var total = 0.0;
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Contains("Total:"))
                {
                    var tokens = result[i].Split(" ")[1];
                    total += Convert.ToDouble(tokens);
                }

            }

            var resultList = result.ToList();
            resultList.Add("Общо постъпления от продадени артикули: " + total.ToString() + "\n");
            var last = resultList.FindLast(x => x.Contains("Общо постъпления от продадени артикули:"));
            resultList.Insert(0, last);

            return View(resultList);
        }

        [HttpGet]
        public IActionResult ReceiptsClient()
        {
            var result = System.IO.File.ReadAllLines($"C:\\Users\\User\\source\\repos\\BookStore_Inspiration\\BookStore\\BookStore_Inspiration\\Views\\Info\\OrderResult.txt");

            var userListReceipts = new List<string>();

            for (int i = 0; i < result.Length; i++)
            {

                if (result[i].Contains($"ClientName: {this.User.Identity.Name}") )
                {
                    for (int j = 0; j < 8; j++)
                    {
                        userListReceipts.Add(result[i+j]);
                    }
                    userListReceipts.Add("***********************************");

                }
            }
            return View(userListReceipts);
        }

        [Authorize(Roles = ("Admin"))]
        [HttpGet]
        public IActionResult ReceiptsClientA(string id)
        {
            var result = System.IO.File.ReadAllLines($"C:\\Users\\User\\source\\repos\\BookStore_Inspiration\\BookStore\\BookStore_Inspiration\\Views\\Info\\OrderResult.txt");


            var userListReceipts = new List<string>();

            for (int i = 0; i < result.Length; i++)
            {

                if (result[i].Contains($"ClientName: "))
                {
                    var tempUsername = result[i].Split(" ")[1];
                    var tempUser = userServices.GetUserByUsername(tempUsername);
                    if (tempUser!=null && tempUser.Id == id)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            userListReceipts.Add(result[i + j]);
                        }
                        userListReceipts.Add("***********************************");
                    }

                }
            }
            return View(userListReceipts);
        }

    }
}