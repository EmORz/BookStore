using BookStore.Data;
using BookStore.Model;
using BookStore.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Model.Enum;

namespace BookStore.Services
{
    public class ProductServices : IProductServices
    {
        private readonly BookStoreDbContext context;

        public ProductServices(BookStoreDbContext context)
        {
            this.context = context;
        }

        public bool Create(string Title, string productType, decimal price, int quantity, string description, string author, string publishng, string yearOfPublishing)
        {
            var productTypeTemp = Enum.TryParse<ProductTypes>(productType, true, out ProductTypes resultProductType);

            Product temp = new Product()
            {
                Title = Title,
                ProductTypes = resultProductType,
                Price = price,
                Quantity = quantity,
                Author = author,
                Description = description,
                Publishing = publishng,
                YearOfPublishing = yearOfPublishing

            };

            context.Products.Add(temp);
            int result = context.SaveChanges();

            return result > 0;
        }

        public void AddProduct(Product product)
        {
            if (product == null)
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
            var tokens = searchString.Split(new string[] { ",", ".", " " }, StringSplitOptions.RemoveEmptyEntries);
            var productsAll = this.context.Products.ToList();
            var searchResult = new List<Product>();
            foreach (var product in productsAll)
            {
                var productName = product.Title.ToLower().Split(new string[] { ",", ".", " " }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < tokens.Length; i++)
                {
                    var test = productName.Contains(tokens[i].ToLower());
                    if (test)
                    {
                        searchResult.Add(product);
                    }
                }

            }

            return searchResult;

        }

        public void Delete(int id)
        {
            var productFromDb = this.context.Products.FirstOrDefault(x => x.Id == id);

            if (productFromDb == null)
            {
                throw new ArgumentNullException(nameof(productFromDb));

            }

            this.context.Products.Remove(productFromDb);
            this.context.SaveChanges();
        }
    }
}