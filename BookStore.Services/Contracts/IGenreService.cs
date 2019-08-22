using System.Collections.Generic;
using BookStore.Model;

namespace BookStore.Services.Contracts
{
    public interface IGenreService
    {
        void Create(string name);

        IEnumerable<Genre> All();

        bool Delete(int id);

        void Edit(int id, string name);

        Genre GetGenreById(int id);

    }
}