using System.Collections.Generic;
using BookStore.Model.Address;
using BookStore.Services.Contracts;

namespace BookStore.Services
{
    public class AddressesServices : IAddressesServices
    {
        public Address CreateAddress(string street, string description, string city, string postcode)
        {
            throw new System.NotImplementedException();
        }

        public void AddAddressToUser(string username, Address address)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Address> GetAllUserAddresses(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}