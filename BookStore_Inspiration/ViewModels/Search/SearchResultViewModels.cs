using System.Collections.Generic;

namespace BookStore_Inspiration.ViewModels.Search
{
    public class SearchResultViewModels
    {
        public List<BookStore.Model.Product> Result { get; set; } = new List<BookStore.Model.Product>();
    }
}