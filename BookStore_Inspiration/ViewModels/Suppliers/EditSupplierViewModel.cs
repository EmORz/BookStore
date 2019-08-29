using System.ComponentModel.DataAnnotations;

namespace BookStore_Inspiration.ViewModels.Suppliers
{
    public class EditSupplierViewModel
    {
   
        public int Id { get; set; }

        [Display(Name = "Име на доставчика")]
        [Required(ErrorMessage = "Полето\"{0}\" e задължително.")]
        public string Name { get; set; }

        [Display(Name = "Цена на доставката до дома")]
        [Range(typeof(decimal), "0.01", "10000000", ErrorMessage = "Цената \"{0}\" трябва да бъде текст с минимална стойност {2} и максимална стойност {1}.")]
        [Required(ErrorMessage = "Полето\"{0}\" e задължително.")]
        public decimal PriceToHome { get; set; }

        [Display(Name = "Цена на доставката до офиса")]
        [Required(ErrorMessage = "Полето\"{0}\" e задължително.")]
        [Range(typeof(decimal), "0.01", "10000000", ErrorMessage = "Цената \"{0}\" трябва да бъде текст с минимална стойност {2} и максимална стойност {1}.")]
        public decimal PriceToOffice { get; set; }
    }
}