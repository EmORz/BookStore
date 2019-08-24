using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Data;
using BookStore.Model;
using BookStore.Services.Contracts;

namespace BookStore.Services
{
    public class SearchService : ISearchService
    {
        private readonly BookStoreDbContext _db;

        public SearchService(BookStoreDbContext db)
        {
            _db = db;
        }
        public void Create(int productId, string author, string isbn, string publishing, string title, string userId)
        {
            var search = new Search()
            {
                ProductId = productId,
                Author = author,
                ISBN = isbn,
                Publishing = publishing,
                Title = title,
                UserId = userId,
                DateTime = DateTime.Now

            };

            _db.Searches.Add(search);
            _db.SaveChanges();
        }

        public IList<Search> AllSearchesResults()
        {
            var searchesResults = _db.Searches.ToList();
            return searchesResults;
        }

        public void DeleteHistotyOfSearch(string userId)
        {
            var searchForDelete = _db.Searches.Where(x => x.UserId == userId);

            _db.Searches.RemoveRange(searchForDelete);
            _db.SaveChanges();
        }
    }
}