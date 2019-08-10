using System.Collections.Generic;

namespace BookStore_Inspiration.ViewModels.Product.Home
{
    public class AllProductsHomeViewModel
    {
        public List<ProductHomeViewModel> Products { get; set; } = new List<ProductHomeViewModel>();
    }
}