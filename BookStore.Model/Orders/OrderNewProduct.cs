using System;
using BookStore.Model.HelpModels;

namespace BookStore.Model.Orders
{
    public class OrderNewProduct : EntityBase<string>
    {
    
        public DateTime IssuedOn { get; set; }


        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public virtual NewProduct Product { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }



        public int? DeliveryAddressId { get; set; }
        public virtual Address.Address DeliveryAddress { get; set; }
        /*  - IssuedOn (dateTime)
           - Quantity (int)
           - Product new (Product new)
           - Issuer (User)*/

      
    }
}