using System.ComponentModel.DataAnnotations;

namespace BookStore_Inspiration.ViewModels.Product.Home
{
    public class ProductHomeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Залавие/Име")]
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

        [Display(Name = "Издателствоь")]

        public string Publishing { get; set; }

        [Display(Name = "ИСБН")]

        public string ISBN { get; set; }

        [Display(Name = "Година на публикуване")]

        public string YearOfPublishing { get; set; }

        [Display(Name = "бр. потребители")]

        public int UsersCount { get; set; }

        [Display(Name = "Снимка")]

        public string Picture { get; set; }
    }
}