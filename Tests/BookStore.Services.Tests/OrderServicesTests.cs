using System;
using BookStore.Data;
using BookStore.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BookStore.Services.Tests
{
    public class OrderServicesTests
    {
        [Fact]
        public void GetProcessingOrderShouldGetOrder()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var _userServices = new Mock<IUserServices>();

            var orderService = new OrderServices(_userServices.Object, dbContext);

        }

        [Fact]
        public void CreateOrderShouldCreateOrder()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var _userServices = new Mock<IUserServices>();

            var orderService = new OrderServices(_userServices.Object, dbContext);

        }

        [Fact]
        public void SetOrderDetailsShouldSetDetailsToOrder()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var _userServices = new Mock<IUserServices>();

            var orderService = new OrderServices(_userServices.Object, dbContext);

        }

        [Fact]
        public void GetOrderByIdShouldReturnOrderById()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var _userServices = new Mock<IUserServices>();

            var orderService = new OrderServices(_userServices.Object, dbContext);

        }

        [Fact]
        public void GetUserOrderByIdShouldReturnOrderById()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var _userServices = new Mock<IUserServices>();

            var orderService = new OrderServices(_userServices.Object, dbContext);

        }
    }
}