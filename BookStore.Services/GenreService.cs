using System.Collections.Generic;
using System.Linq;
using BookStore.Data;
using BookStore.Model;
using BookStore.Services.Contracts;

namespace BookStore.Services
{
    public class GenreService : IGenreService
    {
        private readonly BookStoreDbContext db;

        public GenreService(BookStoreDbContext db)
        {
            this.db = db;
        }
        public void Create(string name)
        {
            var genre = new  Genre()
            {
                Name = name
            };
            this.db.Genres.Add(genre);
            this.db.SaveChanges();
        }

        public IEnumerable<Genre> All()
        {
            return this.db.Genres.ToList();
        }

        public bool Delete(int id)
        {
            var genre = this.db.Genres.FirstOrDefault(x => x.Id == id);

            if (genre == null)
            {
                return false;
            }

            this.db.Genres.Remove(genre);
            this.db.SaveChanges();

            return true;
        }

        public void Edit(int id, string name)
        {
            var genre = this.db.Genres.FirstOrDefault(x => x.Id == id);

            if (genre == null)
            {
                return ;
            }

            genre.Name = name;

            this.db.SaveChanges();
        }

        public Genre GetGenreById(int id)
        {
            var genre = this.db.Genres.FirstOrDefault(x => x.Id == id);
            return genre;
        }
    }
}