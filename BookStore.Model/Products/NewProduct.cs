using BookStore.Model.Enum;
using BookStore.Model.HelpModels;

namespace BookStore.Model
{
    public class NewProduct : EntityBase<int>
    {
        public string Name { get; set; }

        public ProductTypes ProductTypes { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
        /*### Product New
  - Id (int)
  - Name (string)
  - Type (enum) (books/music/film etc . . .)
  - Price (decimal)
  - In Stock - (int:: quantity in stock)*/

    }
}