using System;
using System.Diagnostics;
using System.Linq;
using BookStore.Data;
using BookStore.Model;
using BookStore.Model.Address;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BookStore.Services.Tests
{
    public class AddressesServicesTests
    {
        [Fact]
        public void CreateAddressShoudCreateAddress()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //
            var dbContext = new BookStoreDbContext(options);

            var addressServices = new AddressesServices(dbContext, null);

            addressServices.CreateAddress("Beli lom", "Some text", "Razgrad", "7200");
            addressServices.CreateAddress("Beli lom", "Some text", "Razgrad", "7201");

            var countAddress = dbContext.Addresses.ToArray().Length;

            Assert.Equal(2, countAddress);
        }

        [Fact]
        public void GetOrCreateCityShouldCreateCity()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //
            var dbContext = new BookStoreDbContext(options);
            var cityName = "Razgrad";
            var postCode = "7200";
            var addressServices = new AddressesServices(dbContext, null);

            addressServices.GetOrCreateCity(cityName, postCode);



            var countCities = dbContext.Cities.ToArray().Length;
            var citiesFromDb = dbContext.Cities.FirstOrDefault(x => x.Name == cityName && x.Postcode == postCode);
            var name = citiesFromDb.Name;
            var postCodeFromDb = citiesFromDb.Postcode;

            Assert.Equal(1, countCities);
            Assert.Equal(cityName, name);
            Assert.Equal(postCodeFromDb, postCode);
        }

        [Fact]
        public void AddAddressToUserShouldAddData()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //
            var dbContext = new BookStoreDbContext(options);

            var user = new BookStoreUser(){FirstName = "Emo", UserName = "Jaivant"};

            dbContext.BookStoreUsers.Add(user);
            dbContext.SaveChanges();

            var userService = new UserServices(dbContext);

            var addressServices = new AddressesServices(dbContext, userService);

            var address = new Address()
            {
                Street = "Beli Lom",
                Description = "13A",
                BuildingNumber = "AA",
                City = new City()
                {
                    Name = "Razgrad",
                    Postcode = "7200"
                },
                Country = "BG",
            };
            addressServices.AddAddressToUser("Jaivant", address);

            var userFromDb = dbContext.BookStoreUsers.FirstOrDefault(x => x.UserName == "Jaivant")?.Addresses;
            var cityName = userFromDb?.Select(x => x.City).ToList()[0].Name;
           
            Debug.Assert(cityName != null, nameof(cityName) + " != null");
            Assert.Equal(userFromDb.Count, 1);
            Assert.Equal(cityName, "Razgrad");
        }

        [Fact]
        public void GetOrCreateCityShouldGetInfoForCity()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //
            var dbContext = new BookStoreDbContext(options);
            var cityName = "Razgrad";
            var postCode = "7200";
            var addressServices = new AddressesServices(dbContext, null);

            addressServices.GetOrCreateCity(cityName, postCode);


            var citiesFromDb = dbContext.Cities.FirstOrDefault(x => x.Name == cityName && x.Postcode == postCode);
            var name = citiesFromDb.Name;
            var postCodeFromDb = citiesFromDb.Postcode;

            Assert.Equal(cityName, name);
            Assert.Equal(postCodeFromDb, postCode);
        }
    }
}