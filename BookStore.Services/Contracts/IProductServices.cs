using System.Collections.Generic;
using BookStore.Model;

namespace BookStore.Services.Contracts
{
    public interface IProductServices
    {
        bool Create(string Title, string productType, decimal price, int quantity, string description, string author, string publishng, string yearOfPublishing);

        /*        public string Title { get; set; }
           
           public ProductTypes ProductTypes { get; set; } = ProductTypes.Book;
           
           public decimal Price { get; set; }
           
           public int Quantity { get; set; }
           
           public string Description { get; set; }
           
           public string Author { get; set; }
           
           public string Publishing { get; set; }
           
           public string ISBN { get; set; }
           
           public string YearOfPublishing { get; set; }
           */
        void AddProduct(Product product);

        Product GetProductById(int id);

        IEnumerable<Product> GetAllProducts();

        bool ProductExists(int id);

        bool EditProduct(Product product);

        IEnumerable<Product> GetProductsBySearch(string searchString);

    }
}