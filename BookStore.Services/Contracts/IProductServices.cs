using System.Collections.Generic;
using BookStore.Model;

namespace BookStore.Services.Contracts
{
    public interface IProductServices
    {
        bool Create(string Title, string productType, decimal price, int quantity, string description, string author, string publishng, string yearOfPublishing, string picture);

        void AddProduct(Product product);

        Product GetProductById(int id);

        IEnumerable<Product> GetAllProducts();

        bool ProductExists(int id);

        bool EditProduct(Product product);

        IEnumerable<SearchProductViewModel> GetProductsBySearch(string searchString);

        void Delete(int id);

    }
}