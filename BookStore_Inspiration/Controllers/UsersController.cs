using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.Common;

namespace BookStore_Inspiration.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        public IActionResult AllUsers()
        {
            var listOfUsers = _userServices.GetAllUsers().Select(x => new ListOfAllUserViewModels
            {
                Id = x.Id,
                Email = x.Email,
                UserName = x.UserName,
                FirstName = x.FirstName,
                Lastname = x.LastName,
                Phonenumber = x.PhoneNumber,
                UCN = x.UCN

            }).ToList();
            CollectionOfUsers allUsers = new CollectionOfUsers();
            allUsers.Users.AddRange(listOfUsers);
            return View(allUsers);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            _userServices.DeleteUser(id);
            return this.Redirect("/Users/AllUsers");
        }
    }
}