using System.Collections.Generic;
using BookStore.Model.Enum;
using BookStore.Model.HelpModels;

namespace BookStore.Model
{
    public class Product : EntityBase<int>
    {
        public string Name { get; set; }

        public ProductTypes ProductTypes { get; set; }


        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Music> Musics { get; set; }
        public virtual ICollection<Film> Films { get; set; }
        public virtual ICollection<Other> Others { get; set; }

        /*### Product New
  - Id (int)
  - Name (string)
  - Type (enum) (books/music/film etc . . .)
  - Price (decimal)
  - In Stock - (int:: quantity in stock)*/

    }
}