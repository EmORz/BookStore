using System;
using BookStore.Model.HelpModels;
using BookStore.Model.Address;

namespace BookStore.Model
{
    public class OrderNew : EntityBase<string>
    {
        public OrderNew()
        {
            this.User= new User();
            this.Product = new NewProduct();
        }

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