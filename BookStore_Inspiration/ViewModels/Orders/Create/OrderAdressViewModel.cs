using System.ComponentModel.DataAnnotations;

namespace BookStore_Inspiration.ViewModels.Orders.Create
{
    public class OrderAdressViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Улица")]
        [Required(ErrorMessage = "Полето\"{0}\" e задължително.")]
        public string Street { get; set; }

        [Display(Name = "Допълнително описание")]
        public string Description { get; set; }

        [Display(Name = "Град")]
        [Required(ErrorMessage = "Полето\"{0}\" e задължително.")]
        public string CityName { get; set; }

        [Display(Name = "Пощенски код")]
        [Required(ErrorMessage = "Полето\"{0}\" e задължително.")]
        public string CityPostcode { get; set; }
    }
}