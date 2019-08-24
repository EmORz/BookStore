using System;

namespace BookStore.Model
{
    public class Search
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
        public string UserId { get; set; }

        public int ProductId { get; set; }

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


        public string YouTubeLink { get; set; }
    }
}