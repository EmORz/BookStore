using System.Collections.Generic;
using System.Linq;
using BookStore.Model;

namespace BookStore.Services.Contracts
{
    public interface IProductServices
    {
        IQueryable<Product> GetAllProducts(string criteria = null);

        bool Create(string Title, string productType, decimal price, int quantity, string description, string author, string publishng, string yearOfPublishing, string picture, string youTubeLink, int genreId);

        void AddProduct(Product product);

        Product GetProductById(int id);

        IEnumerable<Product> GetAllProducts();

        bool ProductExists(int id);

        bool EditProduct(Product product);

        IEnumerable<Product> GetProductsBySearch(string searchString);

        void Delete(int id);

    }
}