using BookStore.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BookStore_Inspiration.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<BookStoreUser> _signInManager;
        private readonly UserManager<BookStoreUser> _userManager;
      


        public RegisterModel(
            UserManager<BookStoreUser> userManager,
            SignInManager<BookStoreUser> signInManager
           )
        {
            _userManager = userManager;
            _signInManager = signInManager;

      
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
            [DataType(DataType.Password)]
            [Display(Name = "Парола")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Потвърди парола")]
            [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Телефонен номер")]
            public string Phonenumber { get; set; }

            [Display(Name = "Адрес")]
            public string Address { get; set; }
     
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
              
                var user = new BookStoreUser
                {
                    UserName = Input.Username,
                    Email = Input.Email,
                    FirstName = Input.Firstname,
                    LastName = Input.Lastname,
                    PhoneNumber = Input.Phonenumber
            
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
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
    }
}
