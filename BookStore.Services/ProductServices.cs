using BookStore.Data;
using BookStore.Model;
using BookStore.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Services
{
    public class ProductServices : IProductServices
    {
        private readonly BookStoreDbContext context;

        public ProductServices(BookStoreDbContext context)
        {
            this.context = context;
        }

        public bool Create(Product product)
        {
            Product temp = new Product()
            {
                Name = product.Name,
                ProductTypes = product.ProductTypes,
                Price = product.Price,
                Quantity = product.Quantity
                
            };

            context.Products.Add(product);
            int result = context.SaveChanges();

            return result > 0;
        }

        public void AddProduct(Product product)
        {
            if (product==null)
            {
                return;
            }

            this.context.Products.Add(product);
            this.context.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            var currentProduct = this.context.Products.FirstOrDefault(x => x.Id == id);
            return currentProduct;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var allProducts = this.context.Products.ToList();
            return allProducts;
        }

        public bool ProductExists(int id)
        {
            var isProductExist = this.context.Products.Any(x => x.Id == id);

            return isProductExist;

        }

        public bool EditProduct(Product product)
        {
            if (!this.ProductExists(product.Id))
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

        }
    }
}