using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Data;
using BookStore.Model;
using BookStore.Model.Enum;
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

            var suppliers = new List<Supplier>
            {
                new Supplier()
                {
                    Name = "Speedy",
                    PriceToOffice = 4.99M,
                    PriceToHome = 5.99M

                },
                new Supplier()
                {
                    Name = "Eccont",
                    PriceToOffice = 5.99M,
                    PriceToHome = 6.99M

                },
            };

            dbContext.Suppliers.AddRange(suppliers);

            dbContext.SaveChanges();

            var listWithSuppliers = supplierService.All().ToList();

            Assert.True(listWithSuppliers.Count==2);


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

            supplierService.Create("Speedy", 5, 4);

            var isSupplierIsCreate = dbContext.Suppliers.ToList();

            Assert.True(isSupplierIsCreate.Count==1);


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

            var suppliers = new List<Supplier>
            {
                new Supplier()
                {
                    Name = "Speedy",
                    PriceToOffice = 4.99M,
                    PriceToHome = 5.99M,
                    IsDefault = true
                },
                new Supplier()
                {
                    Name = "Eccont",
                    PriceToOffice = 5.99M,
                    PriceToHome = 6.99M,
                    IsDefault = false
                },
            };
            dbContext.Suppliers.AddRange(suppliers);
            dbContext.SaveChanges();

            var changeSupplierToDefault = supplierService.MakeDafault(suppliers[1].Id);

            var checkIsEccontIsDefault = dbContext.Suppliers.FirstOrDefault(x => x.Id == 2).IsDefault;


            Assert.True(changeSupplierToDefault);

            Assert.True(checkIsEccontIsDefault);
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

            var suppliers = new List<Supplier>
            {
                new Supplier()
                {
                    Name = "Speedy",
                    PriceToOffice = 4.99M,
                    PriceToHome = 5.99M,
                    IsDefault = true
                },
                new Supplier()
                {
                    Name = "Eccont",
                    PriceToOffice = 5.99M,
                    PriceToHome = 6.99M,
                    IsDefault = false
                },
            };
            dbContext.Suppliers.AddRange(suppliers);
            dbContext.SaveChanges();

            var getDeliveryPriceHome = supplierService.GetDiliveryPrice(suppliers[0].Id, DeliveryType.Home);
            var getDeliveryPriceOffice = supplierService.GetDiliveryPrice(suppliers[0].Id, DeliveryType.Office);

            Assert.True(getDeliveryPriceOffice==4.99M);
            Assert.True(getDeliveryPriceHome==5.99M);

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

            var suppliers = new List<Supplier>
            {
                new Supplier()
                {
                    Name = "Speedy",
                    PriceToOffice = 4.99M,
                    PriceToHome = 5.99M,
                    IsDefault = false
                },
                new Supplier()
                {
                    Name = "Eccont",
                    PriceToOffice = 5.99M,
                    PriceToHome = 6.99M,
                    IsDefault = true
                },
            };
            dbContext.Suppliers.AddRange(suppliers);
            dbContext.SaveChanges();

            var isDeleteSupplierFromDb = supplierService.Delete(suppliers[0].Id);

            Assert.True(isDeleteSupplierFromDb);
            Assert.True(dbContext.Suppliers.ToList().Count==1);

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

            var suppliers = new List<Supplier>
            {
                new Supplier()
                {
                    Name = "Speedy",
                    PriceToOffice = 4.99M,
                    PriceToHome = 5.99M,
                    IsDefault = false
                },
                new Supplier()
                {
                    Name = "Eccont",
                    PriceToOffice = 5.99M,
                    PriceToHome = 6.99M,
                    IsDefault = true
                },
            };
            dbContext.Suppliers.AddRange(suppliers);
            dbContext.SaveChanges();

           supplierService.Edit(suppliers[0].Id, "SpeedyUpgrade", 4M, 5M);

           var isNameChange = suppliers[0].Name == "SpeedyUpgrade";
           var isPriceToHomeChange = suppliers[0].PriceToHome == 4;
           var isPriceToOfficeChange = suppliers[0].PriceToOffice == 5;

           Assert.True(isNameChange);
           Assert.True(isPriceToOfficeChange);
           Assert.True(isPriceToHomeChange);
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


            var suppliers = new List<Supplier>
            {
                new Supplier()
                {
                    Name = "Speedy",
                    PriceToOffice = 4.99M,
                    PriceToHome = 5.99M,
                    IsDefault = false
                },
                new Supplier()
                {
                    Name = "Eccont",
                    PriceToOffice = 5.99M,
                    PriceToHome = 6.99M,
                    IsDefault = true
                },
            };
            dbContext.Suppliers.AddRange(suppliers);
            dbContext.SaveChanges();

            var isGetSupplierById = supplierService.GetSupplierById(suppliers[0].Id);

            Assert.True(isGetSupplierById!=null);
            Assert.True(isGetSupplierById.Name=="Speedy");
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

            var suppliers = new List<Supplier>
            {
                new Supplier()
                {
                    Name = "Speedy",
                    PriceToOffice = 4.99M,
                    PriceToHome = 5.99M,
                    IsDefault = false
                },
                new Supplier()
                {
                    Name = "Eccont",
                    PriceToOffice = 5.99M,
                    PriceToHome = 6.99M,
                    IsDefault = true
                },
            };
            dbContext.Suppliers.AddRange(suppliers);
            dbContext.SaveChanges();

            var isGetDefauultSupplier = supplierService.GetDefaultSupplier();

            Assert.True(isGetDefauultSupplier!=null);
            Assert.True(isGetDefauultSupplier.Name=="Eccont");


        }
    }
}