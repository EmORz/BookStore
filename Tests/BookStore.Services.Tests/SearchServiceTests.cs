using System;
using System.Linq;
using BookStore.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BookStore.Services.Tests
{
    public class SearchServiceTests
    {
        [Fact]
        public void CreateShoulCreateIncomeMoneyOrder()
        {

            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var searchService = new SearchService(dbContext);

            searchService.Create(
                1, "xx", "123456789", "Zora", "Magazine Universe", "D220E7A5-D7A8-465F-A923-066873664B4F"
                );

            var isSearchServiceCreate = dbContext.Searches.ToList();

            Assert.True(isSearchServiceCreate.Count == 1);
        }

        [Fact]
        public void AllSearchesResultsMustReturn()
        {

            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var searchService = new SearchService(dbContext);

            searchService.Create(
                1, "xx", "123456789", "Zora", "Magazine Universe", "D220E7A5-D7A8-465F-A923-066873664B4F"
            );


            searchService.Create(
                1, "xx", "123456789", "Zora", "Magazine Universe", "D220E7A5-D7A8-465F-A923-066873664B4P"
            );

            var searchResults = searchService.AllSearchesResults().ToList();

            Assert.True(searchResults.Count==2);


        }

        [Fact]
        public void DeleteHistoryOfSearchMustRemoveUserByIdSearchResults()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var searchService = new SearchService(dbContext);

            searchService.Create(
                1, "xx", "123456789", "Zora", "Magazine Universe", "D220E7A5-D7A8-465F-A923-066873664B4F"
            );

            searchService.Create(
                2, "Пушкин", "123456789", "Zora", "Андрей Шенье", "D220E7A5-D7A8-465F-A923-066873664B4F"
            );


            searchService.Create(
                3, "xx", "123456789", "Zora", "Magazine Universe", "D220E7A5-D7A8-465F-A923-066873664B4P"
            );

            var currentSearches = searchService.AllSearchesResults().ToList().Count==3;
            searchService.DeleteHistotyOfSearch("D220E7A5-D7A8-465F-A923-066873664B4F");

            var searchesAfterDeleteHistory = searchService.AllSearchesResults().ToList().Count==1;

            Assert.True(currentSearches);
            Assert.True(searchesAfterDeleteHistory);
        }
    }
}