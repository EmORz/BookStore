using System;
using BookStore.Model.HelpModels;

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

        public int Quantity { get; set; }

        public NewProduct Product { get; set; }

        public User User { get; set; }
        /*  - IssuedOn (dateTime)
           - Quantity (int)
           - Product new (Product new)
           - Issuer (User)*/

    }
}