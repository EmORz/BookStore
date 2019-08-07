using System.ComponentModel.DataAnnotations;
using BookStore.Model.Enum;

namespace BookStore_Inspiration.ViewModels.Product
{
    public class EditProductViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ProductTypes { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string Publishing { get; set; }

        public string ISBN { get; set; }

        public string YearOfPublishing { get; set; }
    }
}