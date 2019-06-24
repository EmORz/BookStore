using System.Collections.Generic;

namespace BookStore.Model.Address
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Postcode { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}