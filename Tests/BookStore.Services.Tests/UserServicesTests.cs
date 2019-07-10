using BookStore.Data;
using BookStore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace BookStore.Services.Tests
{
    public class UserServicesTests
    {
        [Fact]
        public void EditFirstNameOfUserShouldChangeFirstName()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new BookStoreDbContext(options);

            var testUser = new BookStoreUser()
            {
                UserName = "DesiUser",
                FirstName = "Desislava"
            };

            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();

            //var store = new Mock<IUserStore<BookStoreUser>>();
          

            var userServices = new UserServices(dbContext);
            var editFirstName = "Desi";

            userServices.EditFirstName(testUser, editFirstName);

            Assert.Equal(editFirstName, testUser.FirstName);


        }
    }
}