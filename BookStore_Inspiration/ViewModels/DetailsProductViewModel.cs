using System.ComponentModel.DataAnnotations;
using BookStore.Model.Enum;

namespace BookStore_Inspiration.ViewModels
{
    public class DetailsProductViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Вид на продукта")]
        public string ProductTypes { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Количество")]
        public int Quantity { get; set; }

    }
}