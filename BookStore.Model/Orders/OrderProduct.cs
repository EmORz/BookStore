using System;
using BookStore.Model.HelpModels;

namespace BookStore.Model.Orders
{
    public class OrderProduct : EntityBase<string>
    {
    
        public DateTime IssuedOn { get; set; }

        public string OrderId { get; set; }
        public virtual Order Order { get; set; }


        public int ProductId { get; set; }
        public virtual Product Product { get; set; }


        public decimal Price { get; set; }

        public int Quantity { get; set; }


    }
}