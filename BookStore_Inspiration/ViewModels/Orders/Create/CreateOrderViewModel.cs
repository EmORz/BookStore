using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore_Inspiration.ViewModels.Orders.Create
{
    public class CreateOrderViewModel
    {

        public IList<OrderAdressViewModel> OrderAddressesViewModel { get; set; }

        public OrderAdressViewModel OrderAdressViewModel { get; set; }

        public ProductOrderViewModel ProductOrderViewModel { get; set; }

        public int? DeliveryAddressId { get; set; }



        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }

    public class ProductOrderViewModel
    {
        public string Title { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }

    public class OrderAdressViewModel
    {
        public int Id { get; set; }

        public string Street { get; set; }

        public string Description { get; set; }

        public string CityName { get; set; }

        public string CityPostcode { get; set; }
    }
}