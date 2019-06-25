using System.Collections.Generic;
using BookStore.Model.HelpModels;
using BookStore.Model.Orders;

namespace BookStore.Model
{
    public class User : EntityBase<string>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phonenumber { get; set; }

        public FullName FullName { get; set; }

        public string Address { get; set; }

        public ICollection<Order> Orders { get; set; }  
        /* - Username (string)
           - Password (string)
           - Email (string)
           - Phonenumber
           - Full Name (string)
           - Address (string)*/
    }
}