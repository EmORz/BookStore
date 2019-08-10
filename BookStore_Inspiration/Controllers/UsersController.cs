using BookStore.Data;
using BookStore.Model;
using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels.User.Edit;
using BookStore_Inspiration.ViewModels.User.Index;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BookStore_Inspiration.ViewModels.User;

namespace BookStore_Inspiration.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly BookStoreDbContext context;

        public UsersController(IUserServices userServices, BookStoreDbContext context)
        {
            _userServices = userServices;
            this.context = context;
        }

        public IActionResult Counter()
        {
           var users = _userServices.GetAllUsers().Count;
           var counter = new CounterOfUsers
           {
               CountOfUsers = users
           };

           return this.View(counter);


        }


        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            _userServices.DeleteUser(id);
            return this.Redirect("/Users/AllUsers");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditAdmin(string id)
        {
           var userFromDb= _userServices.GetUserById(id);
            if (userFromDb==null)
            {
                return new NotFoundResult();
            }

            var model = new EditUserFromDbView()
            {
                Email = userFromDb.Email,
                FirstName = userFromDb.FirstName,
                Lastname = userFromDb.LastName,
                Phonenumber = userFromDb.PhoneNumber
            };
            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditAdmin(EditUserFromDbView editUser)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
           
            var user = new BookStoreUser()
            {
                Email = editUser.Email,
                NormalizedEmail = editUser.Email,

                FirstName = editUser.FirstName,
                LastName = editUser.Lastname,
                PhoneNumber = editUser.Phonenumber
            };
            //_userServices.EditFirstName(user, editUser.FirstName);
            //_userServices.EditLastName(user, editUser.Lastname);
            //_userServices.EditEmail(user, editUser.Email);
            //_userServices.EditPhonenumber(user, editUser.Phonenumber);


            //_userServices.EditAdmin(user);

            return this.Redirect("/Users/AllUsers");

        }
    }
}