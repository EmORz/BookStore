using System.Collections.Generic;
using BookStore.Model;

namespace BookStore.Services.Contracts
{
    public interface IProductServices
    {
        void AddProduct(Product product);

        Product GetProductById(string id);

        IEnumerable<Product> GetAllProducts();

        bool ProductExists(string id);

        bool EditProduct(Product product);

        IEnumerable<Product> GetProductsBySearch(string searchString);

    }
}