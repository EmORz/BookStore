using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Services;
using BookStore.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Authorization = System.Net.Authorization;
using SearchProductViewModel = BookStore_Inspiration.ViewModels.Product.SearchBox.SearchProductViewModel;

namespace BookStore_Inspiration.Controllers
{
    public class InfoController : Controller
    {
        private readonly IUserServices userServices;
        private readonly IProductServices _productServices;

        public InfoController(IUserServices userServices, IProductServices productServices)
        {
            this.userServices = userServices;
            _productServices = productServices;
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
            return View();
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
        public IActionResult SearchBox(SearchProductViewModel inputSearch)
        {
            var search = _productServices.GetProductsBySearch(inputSearch.Input).ToList();

            SearchResultViewModels result = new SearchResultViewModels()
            {
                Result = search
            };

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
                    for (int j = 0; j < 7; j++)
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
                        for (int j = 0; j < 7; j++)
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