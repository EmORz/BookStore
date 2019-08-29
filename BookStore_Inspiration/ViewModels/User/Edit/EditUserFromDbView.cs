using System.ComponentModel.DataAnnotations;

namespace BookStore_Inspiration.ViewModels.User.Edit
{
    public class EditUserFromDbView
    {
        [Display(Name = "Имейл")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Първо име")]

        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]

        public string Lastname { get; set; }



        [Display(Name = "Телефонен номер")]

        public string Phonenumber { get; set; }
    }
}