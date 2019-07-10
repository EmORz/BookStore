using BookStore.Data;
using BookStore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

          

            var userServices = new UserServices(dbContext);
            var editFirstName = "Desi";

            userServices.EditFirstName(testUser, editFirstName);

            Assert.Equal(editFirstName, testUser.FirstName);
        }

        [Fact]
        public void EditLastNameOfUserShouldChangeLastName()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new BookStoreDbContext(options);

            var testUser = new BookStoreUser()
            {
                UserName = "DesiUser",
                LastName = "Simeonova"
            };

            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();



            var userServices = new UserServices(dbContext);
            var editLastName = "miss Simeonova";

            userServices.EditLastName(testUser, editLastName);

            Assert.Equal(editLastName, testUser.LastName);
        }

        [Fact]
        public void EditEmailOfUserShouldChangeEmail()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new BookStoreDbContext(options);

            var testUser = new BookStoreUser()
            {
                UserName = "DesiUser",
                Email = "desi@abv.bg"
            };

            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();



            var userServices = new UserServices(dbContext);
            var editEmail = "desiSimeonova@abv.bg";

            userServices.EditEmail(testUser, editEmail);

            Assert.Equal(editEmail, testUser.Email);
        }


        [Fact]
        public void EditPhonenumberOfUserShouldChangePhonenumber()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new BookStoreDbContext(options);

            var testUser = new BookStoreUser()
            {
                UserName = "DesiUser",
                PhoneNumber = "0897000607"
            };

            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();



            var userServices = new UserServices(dbContext);
            var editPhonenumber = "0898247000";

            userServices.EditPhonenumber(testUser, editPhonenumber);

            Assert.Equal(editPhonenumber, testUser.PhoneNumber);
        }

        [Fact]
        public void EditUsernameOfUserShouldChangeUsername()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new BookStoreDbContext(options);

            var testUser = new BookStoreUser()
            {
                UserName = "DesiUser",
                PhoneNumber = "0897000607"
            };

            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();



            var userServices = new UserServices(dbContext);
            var editUsername = "DesislavaUser";

            userServices.EditUsername(testUser, editUsername);

            Assert.Equal(editUsername, testUser.UserName);
        }

        [Fact]
        public void GetUserByUsernameShouldReturnCurrentUser()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new BookStoreDbContext(options);

            var testUser = new BookStoreUser()
            {
                UserName = "DesiUser",
                PhoneNumber = "0897000607"
            };

            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();



            var userServices = new UserServices(dbContext);
            var testUsername = "DesiUser";

            var currentUser = userServices.GetUserByUsername(testUsername).UserName;

            Assert.Equal(testUsername, currentUser);
        }

        [Fact]
        public void GetUserByUsernameShouldReturnNull()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new BookStoreDbContext(options);

            var testUser = new BookStoreUser()
            {
                UserName = "DesiUser",
                PhoneNumber = "0897000607"
            };

            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();



            var userServices = new UserServices(dbContext);
            var testUsername = "EmoUser";

            var currentUser = userServices.GetUserByUsername(testUsername);

            Assert.Null(currentUser);
        }

        [Fact]
        public void GetUsersShouldReturnCollectionOfBookStoreUsers()
        {
            var userServices = new UserServices(SeedBoolStoreUsers());
          

            var currentUsersCollection = userServices.GetAllUsers();

            Assert.Equal(2, currentUsersCollection.Count);
        }

        //todo seed method for data
        private BookStoreDbContext SeedBoolStoreUsers()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new BookStoreDbContext(options);

            IList<BookStoreUser> users = new List<BookStoreUser>()
            {
                new BookStoreUser()
                {
                    UserName = "DesiUser1",
                    PhoneNumber = "0897000607"
                },
                new BookStoreUser()
                {
                    UserName = "DesiUser2",
                    PhoneNumber = "0897000607"
                },

            };

            dbContext.Users.AddRangeAsync(users);
            dbContext.SaveChanges();

            return dbContext;

        }
    }
}