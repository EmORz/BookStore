using System.ComponentModel.DataAnnotations;

namespace BookStore_Inspiration.ViewModels.Product.Home
{
    public class ProductIndexHomeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Заглавие/Име")]
        public string Title { get; set; }

        [Display(Name = "Цена")]

        public decimal Price { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Издателство")]

        public string Publishing { get; set; }

        [Display(Name = "Снимка")]

        public string Picture { get; set; }

        [Display(Name = "Бр. регистрирани потребители")]

        public int UsersCount { get; set; }
    }
}