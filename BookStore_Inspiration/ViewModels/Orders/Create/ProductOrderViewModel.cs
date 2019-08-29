using System.ComponentModel.DataAnnotations;

namespace BookStore_Inspiration.ViewModels.Orders.Create
{
    public class ProductOrderViewModel
    {
        [Display(Name = "Продук №")]
        public int ProductId { get; set; }

        [Display(Name = "Заглавие/Име на продукта")]
        public string Title { get; set; }

        [Display(Name = "Цена")]

        public decimal Price { get; set; }

        [Display(Name = "Количество - клиент")]
        [Required(ErrorMessage = "Полето\"{0}\" e задължително.")]

        public int ClientsQuantity { get; set; } = 1;

        [Display(Name = "Количество - база")]
        public int QuantityFromDb { get; set; }
    }
}