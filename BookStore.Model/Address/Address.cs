using BookStore.Model.Orders;
using System.Collections.Generic;

namespace BookStore.Model.Address
{
    public class Address 
    {
        /*string street, string description, string city, string postcode*/
        public string Id { get; set; }

        public string Street { get; set; }

        public string Description { get; set; }

        public string Country { get; set; }

        public int CityId { get; set; }
        public virtual City City { get; set; }

        public string UserId { get; set; }
        public virtual BookStoreUser BookStoreUser { get; set; }

    

        public string BuildingNumber { get; set; }

        public ICollection<Order> Addresses { get; set; }
    }
}