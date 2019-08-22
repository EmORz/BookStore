namespace BookStore_Inspiration.ViewModels.Suppliers
{
    public class CreateSupplierViewModel
    {

        public string Name { get; set; }

        public decimal PriceToHome { get; set; }


        public decimal PriceToOffice { get; set; }

        public bool IsDefault { get; set; }
    }
}