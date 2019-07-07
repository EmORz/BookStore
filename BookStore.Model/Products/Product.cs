using BookStore.Model.Enum;
using System.Collections.Generic;

namespace BookStore.Model
{
    public class Product 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ProductTypes ProductTypes { get; set; }


        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Music> Musics { get; set; }
        public virtual ICollection<Film> Films { get; set; }
        public virtual ICollection<Other> Others { get; set; }

     

    }
}