using System;
using BookStore.Model.HelpModels;

namespace BookStore.Model
{
    public class OrderOld : EntityBase<string>
    {
        public DateTime IssuedOn { get; set; }

        public int Quantity { get; set; }

        public OldProduct Product { get; set; }

        public User Client { get; set; }



        /*### Order old
           - Id (string)
           - IssuedOn (dateTime)
           - Quantity (int)
           - Product old (Product old)
           - Issuer (User)*/

    }
}