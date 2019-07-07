namespace BookStore.Model.Orders
{
    public class OrderProduct 
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }


        public int ProductId { get; set; }
        public virtual Product Product { get; set; }


        public decimal Price { get; set; }

        public int Quantity { get; set; }


    }
}