using System.ComponentModel.DataAnnotations;

namespace BookStore_Inspiration.ViewModels.Product
{
    public class ProductViewModel
    {
        [Display(Name = "Номер на продукта")]
        public int ProductId { get; set; }

        public int Id { get; set; }

        [Display(Name = "Заглавие/Име")]

        public string Title { get; set; }

        [Display(Name = "Тип на продукта")]

        public string ProductTypes { get; set; }

        [Display(Name = "Цена")]

        public decimal Price { get; set; }

        [Display(Name = "Количество")]

        public int Quantity { get; set; }

        [Display(Name = "Описание")]

        public string Description { get; set; }

        [Display(Name = "Автор")]

        public string Author { get; set; }

        [Display(Name = "Издателство")]

        public string Publishing { get; set; }

        [Display(Name = "Издателство")]

        public string ISBN { get; set; }

        [Display(Name = "Година на публикуване")]

        public string YearOfPublishing { get; set; }

       
    }
}