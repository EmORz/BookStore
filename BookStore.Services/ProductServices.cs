using System.Collections.Generic;
using BookStore.Model;
using BookStore.Services.Contracts;

namespace BookStore.Services
{
    public class ProductServices : IProductServices
    {
        public void AddProduct(Product product)
        {
            throw new System.NotImplementedException();
        }

        public Product GetProductById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            throw new System.NotImplementedException();
        }

        public bool ProductExists(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool EditProduct(Product product)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Product> GetProductsBySearch(string searchString)
        {
            throw new System.NotImplementedException();
        }
    }
}