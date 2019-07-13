using System.ComponentModel.DataAnnotations;
using BookStore.Model.Enum;

namespace BookStore_Inspiration.Areas.Administration.ViewModels
{
    public class CreateProductView
    {
        public int Id { get; set; }

        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Тип на продукта")]
        public ProductTypes ProductTypes { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Количество")]
        public int Quantity { get; set; }
    }
}