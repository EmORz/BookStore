using System.Collections.Generic;
using BookStore_Inspiration.ViewModels.Product.Home;

namespace BookStore_Inspiration.ViewModels
{
    public class AllProductIndex
    {
        public List<ProductIndexHomeViewModel> Products { get; set; } = new List<ProductIndexHomeViewModel>();
    }
}