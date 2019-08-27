using System;
using BookStore.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BookStore.Services.Tests
{
    public class SuppliersServiceTests
    {
        [Fact]

        public void AllShouldReturnListWithAllSuppliers()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var supplierService = new SuppliersService(dbContext);


        }

        [Fact]

        public void CreateShouldCreateSupplierAndAddToDb()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var supplierService = new SuppliersService(dbContext);


        }


        [Fact]

        public void MakeDefaultShouldMakeUserDefault()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var supplierService = new SuppliersService(dbContext);
            

        }


        [Fact]

        public void GetDiliveryPriceShouldGetDeliveryPrice()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var supplierService = new SuppliersService(dbContext);

        }


        [Fact]

        public void DeleteShouldRemoveSupplierFromDb()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var supplierService = new SuppliersService(dbContext);

        }


        [Fact]

        public void EditShouldChangeSupplierInformationFromDb()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var supplierService = new SuppliersService(dbContext);

        }


        [Fact]

        public void GetSupplierByIdShouldGetSupplierFromDbById()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var supplierService = new SuppliersService(dbContext);

        }


        [Fact]

        public void GetDefaultSupplierShouldGetDefaultSupplier()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var supplierService = new SuppliersService(dbContext);

        }
    }
}