using BookStore.Model.Orders;
using BookStore.Model.Address;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BookStore.Model
{
    public class BookStoreUser : IdentityUser
    {
        public BookStoreUser()
        {
            this.Addresses = new List<Address.Address>();
        }


        public string FirstName { get; set; }

        public string LastName { get; set; }

    

        public virtual ICollection<Address.Address> Addresses { get; set; }

        public ICollection<Order> Orders { get; set; }  
    
    }
}