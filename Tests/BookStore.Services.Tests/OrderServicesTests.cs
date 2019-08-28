using System;
using System.Linq;
using BookStore.Data;
using BookStore.Model;
using BookStore.Model.Enum;
using BookStore.Model.Orders;
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


            var user = new BookStoreUser
            {
                Id = "2200A9B3-2622-44C3-A42F-6EBE640FFE74",
                UserName = "Test1"
            };
            _userServices.Setup(x => x.GetUserByUsername(user.UserName)).Returns(user);

            dbContext.BookStoreUsers.Add(user);
            dbContext.SaveChanges();

            var orderService = new OrderServices(_userServices.Object, dbContext);
            var isCreateOrder = orderService.CreateOrder(user.UserName);

            var getProcessingOrder = orderService.GetProcessingOrder(user.UserName);

            Assert.True(getProcessingOrder !=null);
            Assert.True(getProcessingOrder.Status == OrderStatus.Processing);




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


            var user = new BookStoreUser
            {
                Id = "2200A9B3-2622-44C3-A42F-6EBE640FFE74",
                UserName = "Test1"
            };

            _userServices.Setup(x => x.GetUserByUsername(user.UserName)).Returns(user);

            var orderService = new OrderServices(_userServices.Object, dbContext);

            var isCreateOrder = orderService.CreateOrder(user.UserName);

            Assert.True(isCreateOrder !=null);
            Assert.True(isCreateOrder.Status == OrderStatus.Processing);


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

            var order = new Order
            {
                Id = 1
            };

            dbContext.Orders.Add(order);
            dbContext.SaveChanges();

            var temp = PaymentType.Card;

            orderService.SetOrderDetails(order, "Tester", "084/618180", temp, 1, 159);

            var getOredrFromDb = dbContext.Orders.FirstOrDefault(x => x.Id == 1);

            var fullName = getOredrFromDb.Recipient == "Tester";
            var phonenumber = getOredrFromDb.RecipientPhoneNumber == "084/618180";
            var deliveryAddressId = getOredrFromDb.DeliveryAddressId == 1;
            var deliveryPrice = getOredrFromDb.DeliveryPrice == 159;

            Assert.True(fullName);
            Assert.True(deliveryAddressId);
            Assert.True(deliveryPrice);
            Assert.True(phonenumber);

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

            var order = new Order
            {
                Id = 1
            };

            dbContext.Orders.Add(order);
            dbContext.SaveChanges();

            var isReturnOrderById = orderService.GetOrderById(order.Id);

            Assert.True(isReturnOrderById !=null);
            Assert.True(isReturnOrderById.Id==order.Id);

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

            var user = new BookStoreUser
            {
                Id = "2200A9B3-2622-44C3-A42F-6EBE640FFE74",
                UserName = "Test1"
            };
            dbContext.BookStoreUsers.Add(user);
            dbContext.SaveChanges();

            _userServices.Setup(x => x.GetUserByUsername(user.UserName)).Returns(user);
            var orderService = new OrderServices(_userServices.Object, dbContext);


           var createdOrder =  orderService.CreateOrder(user.UserName);

           var getOrderByUsername = orderService.GetUserOrderById(createdOrder.Id, user.UserName);

           Assert.True(getOrderByUsername !=null);
           Assert.True(getOrderByUsername.BookStoreUser.UserName == user.UserName);
        }
    }
}