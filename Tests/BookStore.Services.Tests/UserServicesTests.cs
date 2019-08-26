using BookStore.Data;
using BookStore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Services.Contracts;
using Xunit;

namespace BookStore.Services.Tests
{
    public class UserServicesTests
    {
        //For tests are use automate generate UCN from this site => https://georgi.unixsol.org/programs/egn.php?a=gen&s=0&d=0&m=0&y=0&n=5&r=0

        [Fact]
        public void TestClientMetricIsReturnCorrectNotValidUcnData()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var userServices = new UserServices(dbContext);

            //insert user in Db and Encrypt his data
            var testUser = new BookStoreUser()
            {
                UserName = "DesiUser",
                //For tests are use automate generate UCN from this site => https://georgi.unixsol.org/programs/egn.php?a=gen&s=0&d=0&m=0&y=0&n=5&r=0
                //insert not valid ucn
                UCN = userServices.EncryptData("3008015552")
            };
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            //Get User from Db, special her encrypt UCN
            var userFromDbUcn = dbContext.BookStoreUsers.SingleOrDefault(x => x.UserName == "DesiUser")?.UCN;
            //Decrypt ucn
            var clearUcn = userServices.DecryptData(userFromDbUcn);
            //
            var clientInfo = userServices.ClientMetric(clearUcn);

            var isValidUcn = clientInfo[0].IsValidUCN == false;
            var gender = clientInfo[0].Gender == "n";
            var year = clientInfo[0].Year == "n";
            var month = clientInfo[0].Month == "n";
            var region = clientInfo[0].Region == "n";

            Assert.True(isValidUcn);
            Assert.True(gender);
            Assert.True(year);
            Assert.True(month);
            Assert.True(region);
        }

        [Fact]
        public void TestClientMetricIsReturnCorrectValidData()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var userServices = new UserServices(dbContext);

            //insert user in Db and Encrypt his data
            var testUser = new BookStoreUser()
            {
                UserName = "DesiUser",
                //For tests are use automate generate UCN from this site => https://georgi.unixsol.org/programs/egn.php?a=gen&s=0&d=0&m=0&y=0&n=5&r=0
                UCN = userServices.EncryptData("3008085036")
            };
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            //Get User from Db, special her encrypt UCN
            var userFromDbUcn = dbContext.BookStoreUsers.SingleOrDefault(x => x.UserName == "DesiUser")?.UCN;
            //Decrypt ucn
            var clearUcn = userServices.DecryptData(userFromDbUcn);
            //
            var clientInfo = userServices.ClientMetric(clearUcn);

            var isValidUcn = clientInfo[0].IsValidUCN;
            var gender = clientInfo[0].Gender == "Woman";
            var year = clientInfo[0].Year == "1930";
            var month = clientInfo[0].Month == "8";
            var region = clientInfo[0].Region == "Разград";
     
            Assert.True(isValidUcn);
            Assert.True(gender);
            Assert.True(year);
            Assert.True(month);
            Assert.True(region);
        }

        [Fact]
        public void TestDecriptDataShoulDecriptUcn()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var userServices = new UserServices(dbContext);

            var testUser = new BookStoreUser()
            {
                UserName = "DesiUser",
                //For tests are use automate generate UCN from this site => https://georgi.unixsol.org/programs/egn.php?a=gen&s=0&d=0&m=0&y=0&n=5&r=0
                UCN = userServices.EncryptData("6602100531")
            };

            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var userFromDbUcn = dbContext.BookStoreUsers.SingleOrDefault(x => x.UserName == "DesiUser")?.UCN;

            var isMatchrUcn = userServices.DecryptData(userFromDbUcn) == "6602100531";
           

            Assert.True(isMatchrUcn);
        }
        [Fact]
        public void TestToGetUserByEncryptUcn()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var userServices = new UserServices(dbContext);

            var testUser = new BookStoreUser()
            {
                UserName = "DesiUser",
                //For tests are use automate generate UCN from this site => https://georgi.unixsol.org/programs/egn.php?a=gen&s=0&d=0&m=0&y=0&n=5&r=0
                UCN = userServices.EncryptData("6602100531")
            };

            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();

            var testEncrUcn = "VAkZJfsiMFxQGlolWk7hBA==";
            var tempUcnB = testUser.UCN;

            var temp = userServices.GetUserByUcn(tempUcnB);

            Assert.Equal(tempUcnB, testEncrUcn);
            Assert.True(temp != null && temp.UserName.Contains("DesiUser"));
        }

        [Fact]
        public void TestToDeleteUcnShouldSetUcnToNull()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var userServices = new UserServices(dbContext);

            var testUser = new BookStoreUser()
            {
                UserName = "DesiUser",
                //For tests are use automate generate UCN from this site => https://georgi.unixsol.org/programs/egn.php?a=gen&s=0&d=0&m=0&y=0&n=5&r=0
                UCN = userServices.EncryptData("6602100531")
            };

            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();

            userServices.DeleteUCN(testUser);

            var isUcnOnUserIsSetToNull = testUser.UCN == null;

            Assert.True(isUcnOnUserIsSetToNull);
        }

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
        public void GetUserByIdShouldReturnCurrentUser()
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

            var currentUser = userServices.GetUserById(testUser.Id);

            var isReturnCorrectUser = currentUser.UserName == testUsername;

            Assert.True(isReturnCorrectUser);
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
        public void DeleteUserShouldSetUsernameFirstNameLastNameUcnPhonenumberEmailToXXX()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new BookStoreDbContext(options);

            var testUser = new BookStoreUser()
            {
                UserName = "DesiUser",
                PhoneNumber = "0897000607",
                FirstName = "Desi",
                LastName = "Simeonova",
                UCN = "VAkZJfsiMFxQGlolWk7hBA==",
                Email = "desi@abv.bg"
            };

            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();


            var userServices = new UserServices(dbContext);
            

            userServices.DeleteUser(testUser.Id);

            var username = testUser.UserName == "xxx";
            var firstName = testUser.FirstName == "xxx";
            var lastName = testUser.LastName == "xxx";
            var phonenumberName = testUser.PhoneNumber == "xxx";
            var ucn = testUser.UCN == "xxx";
            var email = testUser.Email == "xxx";

            Assert.True(username);
            Assert.True(firstName);
            Assert.True(lastName);
            Assert.True(phonenumberName);
            Assert.True(ucn);
            Assert.True(email);
        }

        [Fact]
        public void GetUsersShouldReturnCollectionOfBookStoreUsers()
        {
            var userServices = new UserServices(SeedBoolStoreUsers());
          

            var currentUsersCollection = userServices.GetAllUsers();

            Assert.Equal(2, currentUsersCollection.Count);
        }




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