using System;
using System.Linq;
using BookStore.Data;
using BookStore.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BookStore.Services.Tests
{
    public class GenreServicesTest
    {
        [Fact]
        public void CreateShoulCreateGenre()
        {

            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var genreServices = new GenreService(dbContext);

            genreServices.Create("History");

            var isGenreCreated = dbContext.Genres.ToList();

            Assert.True(isGenreCreated.Count==1);
            Assert.True(isGenreCreated[0].Name=="History");
        }

        [Fact]
        public void AllShouldReturnCollectionOfGenres()
        {

            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var genreServices = new GenreService(dbContext);
            var history = new Genre
            {
                Name = "History"
            };

            dbContext.Genres.Add(history);

            var music = new Genre
            {
                Name = "Music"
            };

            dbContext.Genres.Add(music);

            dbContext.SaveChanges();

            var genreCollection = genreServices.All().ToList();

       

            Assert.True(genreCollection.Count == 2);
            Assert.True(genreCollection[0].Name == "History");
            Assert.True(genreCollection[1].Name == "Music");
        }

        [Fact]
        public void DeleteShouldRemoveGenreFromDb()
        {

            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var genreServices = new GenreService(dbContext);
            var history = new Genre
            {
                Name = "History"
            };

            dbContext.Genres.Add(history);

            var music = new Genre
            {
                Name = "Music"
            };

            dbContext.Genres.Add(music);

            dbContext.SaveChanges();

            var genreRemove1 = genreServices.Delete(history.Id);
            var genreRemove2 = genreServices.Delete(music.Id);



            Assert.True(genreRemove1);
            Assert.True(genreRemove2);
        }

        [Fact]
        public void EditShouldChangeGenreName()
        {

            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var genreServices = new GenreService(dbContext);
            var history = new Genre
            {
                Name = "History"
            };

            dbContext.Genres.Add(history);

            var music = new Genre
            {
                Name = "Music"
            };

            dbContext.Genres.Add(music);

            dbContext.SaveChanges();

             genreServices.Edit(history.Id, "History and politic");
             genreServices.Edit(music.Id, "Music hard rock :)");



            Assert.True(history.Name=="History and politic");
            Assert.True(music.Name== "Music hard rock :)");
        }

        [Fact]
        public void GetGenreByIdShouldReturnCurrentGenre()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var genreServices = new GenreService(dbContext);
            var history = new Genre
            {
                Name = "History"
            };

            dbContext.Genres.Add(history);

            var music = new Genre
            {
                Name = "Music"
            };

            dbContext.Genres.Add(music);

            dbContext.SaveChanges();

            var getHistoryFromDbbyId = genreServices.GetGenreById(history.Id);

            Assert.True(getHistoryFromDbbyId.Name=="History");
        }

        [Fact]
        public void GetGenreByNameShouldReturnCurrentGenre()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            //

            var dbContext = new BookStoreDbContext(options);

            var genreServices = new GenreService(dbContext);
            var history = new Genre
            {
                Name = "History"
            };

            dbContext.Genres.Add(history);

            var music = new Genre
            {
                Name = "Music"
            };

            dbContext.Genres.Add(music);

            dbContext.SaveChanges();

            var getHistoryFromDbbyId = genreServices.GetGenreByName(history.Name);

            Assert.True(getHistoryFromDbbyId.Name == "History" && getHistoryFromDbbyId.Id==1);
        }
    }
}