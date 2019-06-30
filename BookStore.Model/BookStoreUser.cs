using System.Collections.Generic;
using BookStore.Model.HelpModels;
using BookStore.Model.Orders;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Model
{
    public class BookStoreUser : IdentityUser
    {
        //public BookStoreUser()
        //{
        //    this.Orders = new List<Order>();
        //}


        public string FirstName { get; set; }

        public string LastName { get; set; }


        //public FullName FullName { get; set; }

        public string Address { get; set; }

        //public ICollection<Order> Orders { get; set; }  
        /* - Username (string)
           - Password (string)
           - Email (string)
           - Phonenumber
           - Full Name (string)
           - Address (string)*/
    }
}