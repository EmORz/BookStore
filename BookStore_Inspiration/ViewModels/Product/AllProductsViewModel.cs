using System.Collections.Generic;

namespace BookStore_Inspiration.ViewModels.Product
{
    public class AllProductsViewModel
    {
        public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
    }
}