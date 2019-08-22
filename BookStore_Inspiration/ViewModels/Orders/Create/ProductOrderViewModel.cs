namespace BookStore_Inspiration.ViewModels.Orders.Create
{
    public class ProductOrderViewModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }

        public decimal Price { get; set; }

        public int ClientsQuantity { get; set; } = 1;
    }
}