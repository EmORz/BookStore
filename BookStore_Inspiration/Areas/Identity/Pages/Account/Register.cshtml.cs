using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BookStore.Model;
using BookStore.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace BookStore_Inspiration.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
      
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<BookStoreUser> _signInManager;
        private readonly IUserServices _userServices;
        private readonly UserManager<BookStoreUser> _userManager;
        private readonly IEmailSender _emailSender;



        public RegisterModel(
            UserManager<BookStoreUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<BookStoreUser> signInManager,
            IUserServices userServices
           )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userServices = userServices;
            _roleManager = roleManager;


        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {


            [Display(Name = "Username")]
            public string Username { get; set; }

            [Display(Name = "FirstName")]
            public string Firstname { get; set; }

            [Display(Name = "LastName")]
            public string Lastname { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            //[DataType(DataType.Password)]
            [Display(Name = "Парола")]
            public string Password { get; set; }

            //[DataType(DataType.Password)]
            [Display(Name = "Потвърди парола")]
            [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Телефонен номер")]
            public string Phonenumber { get; set; }

            //[Required]
            //[Display(Name = "ЕГН")]
            //[StringLength(10, ErrorMessage = "ЕГН трябва да бъде точно 10 цифри!", MinimumLength = 10)]
            //public string UCN { get; set; }

        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            //if (!IsValidUCN(Input.UCN))
            //{
            //    return BadRequest("ЕГН което въвеждаш е невалидно! Опитай пак с валидни данни.");
            //}
            if (ModelState.IsValid)
            {
                var isRoot = !_userManager.Users.Any();
                var user = new BookStoreUser
                {
                    UserName = Input.Username,
                    Email = Input.Email,
                    FirstName = Input.Firstname,
                    LastName = Input.Lastname,
                    PhoneNumber = Input.Phonenumber,
                    //UCN = _userServices.EncryptData(Input.UCN)

                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (isRoot)
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private bool IsValidUCN(string input)
        {
            var total = 0;
            var last = input[input.Length - 1];
            int[] numChec = { 2, 4, 8, 5, 10, 9, 7, 3, 6 };
            for (int r = 0; r < input.Length - 1; r++)
            {
                var temp = int.Parse(input[r].ToString()) * numChec[r];
                total += temp;
            }
            var rem = total % 11;
            if (rem.ToString() == last.ToString())
            {
                return true;
            }

            return false;
        }
    }
}
