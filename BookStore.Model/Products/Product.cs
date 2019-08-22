using BookStore.Model.Enum;
using System.Collections.Generic;

namespace BookStore.Model
{
    public class Product 
    {
        public int Id { get; set; }
       // public string Name { get; set; }

        public ProductTypes ProductTypes { get; set; }

        public int GenreId { get; set; }


        public decimal Price { get; set; }

        public int Quantity { get; set; }

        //
        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string Publishing { get; set; }

        public string ISBN { get; set; }

        public string YearOfPublishing { get; set; }


        public string Picture { get; set; }


        public string YouTubeLink { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Music> Musics { get; set; }
        public virtual ICollection<Film> Films { get; set; }
        public virtual ICollection<Other> Others { get; set; }

     

    }
}