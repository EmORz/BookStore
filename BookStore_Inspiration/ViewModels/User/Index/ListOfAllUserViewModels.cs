using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore_Inspiration.ViewModels.User.Index
{
    public class ListOfAllUserViewModels
    {
        public string Id { get; set; }

        [Display(Name = "Имейл")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Първо име")]

        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]

        public string Lastname { get; set; }

        [Display(Name = "Потребителско име")]

        public string UserName { get; set; }

        [Display(Name = "ЕГН")]

        public string UCN { get; set; }

        [Display(Name = "Телефонен номер")]

        public string Phonenumber { get; set; }



    }
}