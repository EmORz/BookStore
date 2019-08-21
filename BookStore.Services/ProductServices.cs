using BookStore.Data;
using BookStore.Model;
using BookStore.Model.Enum;
using BookStore.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Services
{
    public class ProductServices : IProductServices
    {
        private const string PriceLowestToHighestProductOrderCriteria = "price-lowest-to-highest";

        private const string PriceHighestToLowestProductOrderCriteria = "price-highest-to-lowest";


        private readonly BookStoreDbContext context;

        public ProductServices(BookStoreDbContext context)
        {
            this.context = context;
        }


        private IQueryable<Product> GetAllProductsByPriceAscending()
        {
            return this.context.Products.OrderBy(product => product.Price);
        }

        private IQueryable<Product> GetAllProductsByPriceDescending()
        {
            return this.context.Products.OrderByDescending(product => product.Price);
        }

        //todo its not work
        public IQueryable<Product> GetAllProducts(string criteria = null)
        {
            //todo add viewModel!!!

            switch (criteria)
            {
                case PriceLowestToHighestProductOrderCriteria: return this.GetAllProductsByPriceAscending();
                case PriceHighestToLowestProductOrderCriteria: return this.GetAllProductsByPriceDescending();
            }

            return this.context.Products;
        }

        public bool Create(string Title, string productType, decimal price, int quantity, string description, string author, string publishng, string yearOfPublishing, string picture, string youTubeLink)
        {
            var productTypeTemp = Enum.TryParse<ProductTypes>(productType, true, out ProductTypes resultProductType);

            //https://youtu.be/TNofc-YY4Pc
            var youTubeRender = youTubeLink.Split("/");
            var tempLinkKey = "";
            if (youTubeRender.Length != 4)
            {
                tempLinkKey = "xx";
            }
            else
            {
                tempLinkKey = youTubeRender[3];
            }

            Product temp = new Product()
            {
                Title = Title,
                ProductTypes = resultProductType,
                Price = price,
                Quantity = quantity,
                Author = author,
                Description = description,
                Publishing = publishng,
                YearOfPublishing = yearOfPublishing,
                Picture = picture,
                YouTubeLink = tempLinkKey


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

        //todo search box service
        //заглавие, автор, ISBN или търсите книга от определено издателство
        public IEnumerable<SearchProductViewModel> GetProductsBySearch(string searchString = "0")
        {

            var tokens = searchString.Trim().ToLower();
            var productsAll = this.context.Products.Select(x => new SearchProductViewModel
            {
                Title = x.Title,
                Author = x.Author,
                ISBN = x.ISBN,
                Publishing = x.Publishing

            }).ToList();
            var searchResult = new List<SearchProductViewModel>();
            foreach (var product in productsAll)
            {
                if (product.Author == null)
                {
                    product.Author = "xxx";
                }
                if (product.ISBN == null)
                {
                    product.ISBN = "xxx";
                }
                if (product.Publishing == null)
                {
                    product.Publishing = "xxx";
                }
                if (product.Title == null)
                {
                    product.Title = "xxx";
                }

                var check = product?.Title.ToLower() == tokens
                            || product?.Author.ToLower() == tokens
                            || product?.ISBN.ToLower() == tokens
                            || product?.Publishing.ToLower() == tokens;
                if (check)
                {
                    searchResult.Add(product);
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

    public class SearchResultViewModels
    {
        public List<SearchProductViewModel> Result { get; set; } = new List<SearchProductViewModel>();
    }

    public class SearchProductViewModel
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Publishing { get; set; }

        public string ISBN { get; set; }

        public string Input { get; set; }
    }
}