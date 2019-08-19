using System.Collections.Generic;
using BookStore.Model.Address;

namespace BookStore.Services.Contracts
{
    public interface IAddressesServices
    {
        Address CreateAddress(string street, string description, string city, string postcode);

        City GetOrCreateCity(string name, string postCode);

        void AddAddressToUser(string username, Address address);

        IEnumerable<Address> GetAllUserAddresses(string userName);
        IEnumerable<Address> GetAllAddresses();
    }
}