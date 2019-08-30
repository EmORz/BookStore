using System.Collections.Generic;
using BookStore.Data;
using BookStore.Model;
using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels.User;
using BookStore_Inspiration.ViewModels.User.Edit;
using BookStore_Inspiration.ViewModels.User.Index;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BookStore.Services.Helper;
using Microsoft.VisualStudio.Services.Common;

namespace BookStore_Inspiration.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly BookStoreDbContext context;
        private readonly IProductServices _productServices;


        public UsersController(IUserServices userServices, BookStoreDbContext context, IProductServices productServices)
        {
            _userServices = userServices;
            this.context = context;
            _productServices = productServices;
        }


        [Authorize(Roles = "Admin")]
        public IActionResult UsersWithUCN()
        {
            var allUsers = _userServices.GetAllUsers().Where(x => x.UCN!=null);
            var allProducts = _productServices.GetAllProducts().ToList();
            
            //var potencialProducts = new List<string>();

            foreach (var product in allProducts)
            {
                var t = product.Title.ToLower();
            }
            var personalInfo = new PersonalInfo();
            foreach (var user in allUsers)
            {

                var decriptUcn =_userServices.DecryptData(user.UCN);
               
                var result = _userServices.ClientMetric(decriptUcn);
                foreach (var ofert in result)
                {
                    ofert.UserName = user.UserName;
                    var regT = ofert.Region.ToLower();
                    foreach (var product in allProducts)
                    {
                        var isHaveMatch = product.Title.ToLower().Contains(regT) || product.Description.ToLower().Contains(regT);
                        if (isHaveMatch)
                        {
                            ofert.potencialProductOfInteres.Add(product.Title);
                        }
                    }
                }
                personalInfo.PersonaInfo.AddRange(result);
                //personalInfo.PersonaInfo.Add();
            }

            return View(personalInfo);
        }
        [Authorize(Roles = "Admin")]

        public IActionResult EditClientData(EditClientFirstLastNamePhoneNumber edit)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction("Create", "Orders");
            }

            var tempUser = _userServices.GetUserByUsername(this.User.Identity.Name);
            _userServices.EditFirstName(tempUser, edit.Firstname);
            _userServices.EditLastName(tempUser, edit.Lastname);
            _userServices.EditPhonenumber(tempUser, edit.Phonenumber);

            return this.RedirectToAction("Create", "Orders");

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
           

            return this.Redirect("/Users/AllUsers");

        }
    }

    public class PersonalInfo
    {
        public IList<PersonalUserDataForSpecialOfert> PersonaInfo = new List<PersonalUserDataForSpecialOfert>();
    }
}