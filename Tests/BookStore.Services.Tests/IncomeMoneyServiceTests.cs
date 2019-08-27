using System;
using System.Linq;
using BookStore.Data;
using BookStore.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BookStore.Services.Tests
{
    public class IncomeMoneyServiceTests
    {
        [Fact]
        public void CreateShoulCreateIncomeMoneyOrder()
        {

            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var incomeMoneyService = new IncomeMoneyService(dbContext);

            incomeMoneyService.Create("D220E7A5-D7A8-465F-A923-066873664B4F", 12.6M,6, 2, "Card", "Razgrad bul.Bulgaria 13 A", DateTime.Now);

            var isIncomeMonetCreated = dbContext.IcIncomeMonies.ToList();

            Assert.True(isIncomeMonetCreated.Count == 1);
        }

        [Fact]
        public void AllPurchaseShoulReturnIncomeMoneyOrders()
        {

            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var incomeMoneyService = new IncomeMoneyService(dbContext);

            incomeMoneyService.Create("D220E7A5-D7A8-465F-A923-066873664B4F", 12.6M, 6, 2, "Card", "Razgrad bul.Bulgaria 13 A", DateTime.Now);
            incomeMoneyService.Create("2C1D4E9A-EB8A-4ED7-8E67-9B4CE19FFDBD", 112.6M, 6, 3, "Card", "Razgrad bul.Bulgaria 13 B", DateTime.Now);

            var listOfPurchase = incomeMoneyService.AllPurchase().ToList();

            Assert.True(listOfPurchase.Count == 2);
        }

        [Fact]
        public void GetByIdShouldReturnOrderById()
        {

            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var incomeMoneyService = new IncomeMoneyService(dbContext);

            var purchase = new IncomeMoney
            {
                UserId = "D220E7A5-D7A8-465F-A923-066873664B4F",
                TotalMoney = 12.6M,
                ProductId = 6,
                Quantity = 2,
                PaymentMethod = "Card",
                AddressDelivery = "Razgrad bul.Bulgaria 13 A",
                DateTimeOfPurchase = DateTime.Now

            };
            dbContext.IcIncomeMonies.Add(purchase);
            dbContext.SaveChanges();


            var returnPurchaseFromDb = incomeMoneyService.GetById(purchase.Id);

            Assert.True(returnPurchaseFromDb.UserId== "D220E7A5-D7A8-465F-A923-066873664B4F");
        }
    }
}