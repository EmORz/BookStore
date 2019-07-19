﻿using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Data;
using BookStore.Model;
using BookStore.Model.Enum;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BookStore.Services.Tests
{
    public class ProductServicesTests
    {
        [Fact]
        public void TestCreateProductAndSaveItInDB()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //
            var dbContext = new BookStoreDbContext(options);
            var productServices = new ProductServices(dbContext);

            var testProduct = new Product()
            {
                Name = "Под игото",
                ProductTypes = ProductTypes.Book,
                Price = 153.03M,
                Quantity = 1

            };
            bool temp = productServices.Create(testProduct);

            Assert.True(temp);

        }

        [Fact]
        public void TestAddProductShouldAddProductInDb()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //
            var dbContext = new BookStoreDbContext(options);
            var productServices = new ProductServices(dbContext);
            var title = "Под игото";
            var testProduct = new Product()
            {
                Name = title,
                ProductTypes = ProductTypes.Book,
                Price = 153.03M,
                Quantity = 1

            };
            productServices.AddProduct(testProduct);

            var testIsInDb = dbContext.Products.SingleOrDefault(x => x.Name == title)?.Name;

            Assert.True(testIsInDb == title);

        }

        [Fact]
        public void TestAddProductShouldReturnNullReferenceException()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //
            var dbContext = new BookStoreDbContext(options);
            var productServices = new ProductServices(dbContext);
            //
            var title = "Под игото";
            var testProduct = new Product()
            {
                Name = "Бай Ганьо",
                ProductTypes = ProductTypes.Book,
                Price = 153.03M,
                Quantity = 1

            };
            productServices.AddProduct(testProduct);

            var testIsInDb = dbContext.Products.SingleOrDefault(x => x.Name == title);

            Assert.Null(testIsInDb);

        }

        [Fact]
        public void TestGetProductByIdShouldReturnProduct()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //
            var dbContext = new BookStoreDbContext(options);
            var productServices = new ProductServices(dbContext);
            //
            var testProduct = new Product()
            {
                Id = 1,
                Name = "Бай Ганьо",
                ProductTypes = ProductTypes.Book,
                Price = 153.03M,
                Quantity = 1

            };
            productServices.AddProduct(testProduct);

            var productFromDB = productServices.GetProductById(1).Id==1;


            Assert.True(productFromDB);

        }
        //GetAllProducts

        [Fact]
        public void GetAllProductsShouldReturnListOfProducts()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //
            var dbContext = new BookStoreDbContext(options);
            var productServices = new ProductServices(dbContext);
            //
            var testProduct1 = new Product()
            {
                Id = 1,
                Name = "Бай Ганьо1",
                ProductTypes = ProductTypes.Book,
                Price = 153.03M,
                Quantity = 1

            };
            var testProduct2 = new Product()
            {
                Id = 2,
                Name = "Бай Ганьо2",
                ProductTypes = ProductTypes.Book,
                Price = 153.03M,
                Quantity = 1

            };
            var listOfProducts = new List<Product>()
            {
                testProduct2,
                testProduct1
            };
            dbContext.Products.AddRange(listOfProducts);
            dbContext.SaveChanges();


            var productFromDB = productServices.GetAllProducts().Count()==2;

            Assert.True(productFromDB);

        }

        [Fact]
        public void ProductExistsReturnIsProductExistInDB()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //
            var dbContext = new BookStoreDbContext(options);
            var productServices = new ProductServices(dbContext);
            //
            var testProduct1 = new Product()
            {
                Id = 1,
                Name = "Бай Ганьо1",
                ProductTypes = ProductTypes.Book,
                Price = 153.03M,
                Quantity = 1

            };
     
        
            dbContext.Products.AddRange(testProduct1);
            dbContext.SaveChanges();


            var productFromDB = productServices.ProductExists(1);

            Assert.True(productFromDB);

        }

        [Fact]
        public void ProductEditShouldEditInformationForProduct()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //
            var dbContext = new BookStoreDbContext(options);
            var productServices = new ProductServices(dbContext);
            //
            var testProduct1 = new Product()
            {
                Id = 1,
                Name = "Бай Ганьо1",
                ProductTypes = ProductTypes.Book,
                Price = 153.03M,
                Quantity = 1

            };

            var dataP1 = testProduct1.Name;
            dbContext.Products.Add(testProduct1);
            dbContext.SaveChanges();
            testProduct1.Name = "Бай Ганьо от Алеко";

            var productFromDB = productServices.EditProduct(testProduct1);

            var isNameOfEditProductIsEdited = productServices.GetProductById(1).Name == "Бай Ганьо от Алеко";

            Assert.True(isNameOfEditProductIsEdited);

        }


        /*
           
           public bool EditProduct(Product product)
           {
           if (this.ProductExists(product.Id))
           {
           return false;
           }
           
           try
           {
           this.context.Products.Update(product);
           this.context.SaveChanges();
           }
           catch 
           {
           return false;
           }
           
           return true;
           }
           
           public IEnumerable<Product> GetProductsBySearch(string searchString)
           {
           var tokens = searchString.Split(new string[] {",", ".", " "}, StringSplitOptions.RemoveEmptyEntries);
           
           var products = this.context.Products.Where(x => tokens.All(c => x.Name.ToLower().Contains(c.ToLower())));
           //todo its not good
           
           return products;
           
           }*/
    }
}