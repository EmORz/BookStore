using System;
using System.Collections.Generic;
using BookStore.Model;

namespace BookStore.Services.Contracts
{
    public interface ISearchService
    {
        void Create(int productId, string author, string isbn, string publishing, string title, string userId );

        IList<Search> AllSearchesResults();

        void DeleteHistotyOfSearch(string userId);
    }


    /*       public int ProductId { get; set; }

        public string ProductTypes { get; set; }

        public int GenreId { get; set; }


        public decimal Price { get; set; }

        public int Quantity { get; set; }


        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string Publishing { get; set; }

        public string ISBN { get; set; }

        public string YearOfPublishing { get; set; }


        public string Picture { get; set; }


        public string YouTubeLink { get; set; }*/
}