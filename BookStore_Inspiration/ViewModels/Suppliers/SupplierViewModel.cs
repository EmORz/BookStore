using System.ComponentModel.DataAnnotations;

namespace BookStore_Inspiration.ViewModels.Suppliers
{
    public class SupplierViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Име")]
        [Required(ErrorMessage = "Полето\"{0}\" e задължително.")]
        
        public string Name { get; set; }

        [Display(Name = "Доставка до дома")]
        [Required(ErrorMessage = "Полето\"{0}\" e задължително.")]

        public decimal PriceToHome { get; set; }

        [Display(Name = "Доставка до офиса")]
        [Required(ErrorMessage = "Полето\"{0}\" e задължително.")]

        public decimal PriceToOffice { get; set; }

        [Display(Name = "Доставчик по подразбиране")]

        public bool IsDefault { get; set; }
    }
}