using System.Collections.Generic;
using BookStore.Model.Address;

namespace BookStore.Services.Contracts
{
    public interface IAddressesServices
    {
        Address CreateAddress(string street, string description, string city, string postcode);

        void AddAddressToUser(string username, Address address);

        IEnumerable<Address> GetAllUserAddresses(string name);
    }
}