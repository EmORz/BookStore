using System.ComponentModel.DataAnnotations;

namespace BookStore_Inspiration.ViewModels
{
    public class GetAddress
    {
        public string Address { get; set; }

        [Required(ErrorMessage = "Полето за \"Заглавие\" е задължително!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Полето за \"Емайл\" е задължително!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полето за \"Съдържание\" е задължително!")]
        public string Content { get; set; }
    }
}