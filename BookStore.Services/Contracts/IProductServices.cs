using System.Collections.Generic;
using BookStore.Model;

namespace BookStore.Services.Contracts
{
    public interface IProductServices
    {
        void AddProduct(Product product);

        Product GetProductById(int id);

        IEnumerable<Product> GetAllProducts();

        bool ProductExists(int id);

        bool EditProduct(Product product);

        IEnumerable<Product> GetProductsBySearch(string searchString);

    }
}