using System.Collections.Generic;
using System.Linq;
using BookStore.Data;
using BookStore.Model.Address;
using BookStore.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class AddressesServices :  IAddressesServices
    {
        private readonly BookStoreDbContext Db;
        private readonly IUserServices _userServices;

        public AddressesServices(BookStoreDbContext db, IUserServices userServices)
        {
            this.Db = db;
            _userServices = userServices;
        }
        public Address CreateAddress(string street, string description, string city, string postcode)
        {
            var getCity = this.GetOrCreateCity(city, postcode);
            var address = new Address()
            {
                City = getCity,
                Street = street,
                Description = description
            };
            this.Db.Addresses.Add(address);
            this.Db.SaveChanges();

            return address;
        }

        public City GetOrCreateCity(string name, string postCode)
        {
            var city = this.Db.Cities.FirstOrDefault(c => c.Name == name && c.Postcode == postCode);

            if (city==null)
            {
                city = new City()
                {
                    Name = name,
                    Postcode = postCode
                };

                this.Db.Cities.Add(city);
                this.Db.SaveChanges();
            }

            return city;
        }

        public void AddAddressToUser(string username, Address address)
        {
            var user = this._userServices.GetUserByUsername(username);
            user.Addresses.Add(address);

            this.Db.SaveChanges();
        }

        public IEnumerable<Address> GetAllUserAddresses(string userName)
        {
            var allAddresses = this.Db.Addresses.Include(x => x.City).Where(x => x.BookStoreUser.UserName == userName).ToList();
            return allAddresses;
        }

        public IEnumerable<Address> GetAllAddresses()
        {
            var allAddresses = Db.Addresses.Include(x => x.City).ToList();
            return allAddresses;
        }
    }
}