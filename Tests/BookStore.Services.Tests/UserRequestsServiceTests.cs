using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Data;
using BookStore.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BookStore.Services.Tests
{
    public class UserRequestsServiceTests
    {
        [Fact]
        public void CreateShoulCreteUserRequestAndSaveInDb()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var userRequestService = new UserRequestsService(dbContext);

            var title = "Question";
            var email = "user@gmail.com";
            var content = "content";

            userRequestService.Create(title, email, content);

            var userRequestFronDb = dbContext.UserRequests.FirstOrDefault();

            Assert.True(userRequestFronDb != null);
            Assert.True(userRequestFronDb.Title == title);
            Assert.True(userRequestFronDb.Email == email);
            Assert.True(userRequestFronDb.Content == content);
        }

        [Fact]
        public void AllShouldReturnListWithRequestSaveInDB()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var userRequestService = new UserRequestsService(dbContext);

            var title = "Question";
            var email = "user@gmail.com";
            var content = "content";

            userRequestService.Create(title, email, content);
            userRequestService.Create(title+1, email, content);

            var isReurntallRequests = userRequestService.All().ToList();

            Assert.True(isReurntallRequests != null);
            Assert.True(isReurntallRequests.Count==2);
        }


        [Fact]
        public void GetRequestByIdShouldReturnRequestById()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var userRequestService = new UserRequestsService(dbContext);

            var listOfUserRequests = new List<UserRequest>
            {
                new UserRequest()
                {
                    Id = 2, Title = "Test"
                },
                new UserRequest()
                {
                    Id = 1, Title = "Test"
                }
            };

            dbContext.UserRequests.AddRange(listOfUserRequests);
            dbContext.SaveChanges();

            var isReurntRequestById= userRequestService.GetRequestById(1);

            Assert.True(isReurntRequestById != null);
            Assert.True(isReurntRequestById.Title=="Test");
        }

        [Fact]
        public void DeleteShouldDeleteRequestFromDb()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var userRequestService = new UserRequestsService(dbContext);
            var listOfUserRequests = new List<UserRequest>
            {
                new UserRequest()
                {
                    Id = 2, Title = "Test"
                },
                new UserRequest()
                {
                    Id = 1, Title = "Test"
                }
            };

            dbContext.UserRequests.AddRange(listOfUserRequests);
            dbContext.SaveChanges();

            var isReurntRequestById = userRequestService.Delete(1);
            var tryToGetDeleteElementFromDb = dbContext.UserRequests.Where(x => x.Id == 1).ToList();

            Assert.True(isReurntRequestById);
            Assert.True(tryToGetDeleteElementFromDb.Count==0);
        }

        [Fact]
        public void SeenShouldSerSeenOnTrue()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var userRequestService = new UserRequestsService(dbContext);

            var listOfUserRequests = new List<UserRequest>
            {
                new UserRequest()
                {
                    Id = 2, Title = "Test"
                },
                new UserRequest()
                {
                    Id = 1, Title = "Test"
                }
            };

            dbContext.UserRequests.AddRange(listOfUserRequests);
            dbContext.SaveChanges();

            userRequestService.Seen(1);
            var isSeenSetToTrue = dbContext.UserRequests.Where(x => x.Id == 1).FirstOrDefault();

            Assert.True(isSeenSetToTrue.Seen);
        }


        [Fact]
        public void UnSeenShouldSerSeenOnFalse()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var userRequestService = new UserRequestsService(dbContext);

            var listOfUserRequests = new List<UserRequest>
            {
                new UserRequest()
                {
                    Id = 2, Title = "Test"
                },
                new UserRequest()
                {
                    Id = 1, Title = "Test"
                }
            };

            dbContext.UserRequests.AddRange(listOfUserRequests);
            dbContext.SaveChanges();

            userRequestService.Unseen(1);
            var isSeenSetToTrue = dbContext.UserRequests.Where(x => x.Id == 1).FirstOrDefault();

            Assert.True(isSeenSetToTrue.Seen==false);
        }
        [Fact]
        public void GetUnseenRequestsShouldReturnListWithUnseenRequests()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var userRequestService = new UserRequestsService(dbContext);

            var title = "Question";
            var email = "user@gmail.com";
            var content = "content";

            userRequestService.Create(title, email, content);
            userRequestService.Create(title + 1, email, content);

            userRequestService.Unseen(1);
            userRequestService.Unseen(2);

            var unSeenListOfRequest = userRequestService.GetUnseenRequests().ToList();

            Assert.True(unSeenListOfRequest !=null);
            Assert.True(unSeenListOfRequest.Count==2);
        }
    }
}