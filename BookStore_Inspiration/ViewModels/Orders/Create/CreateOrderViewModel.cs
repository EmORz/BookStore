using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookStore.Model.Enum;

namespace BookStore_Inspiration.ViewModels.Orders.Create
{
    public class CreateOrderViewModel
    {

        public IList<OrderAdressViewModel> OrderAddressesViewModel { get; set; }

        public OrderAdressViewModel OrderAdressViewModel { get; set; }

        public ProductOrderViewModel ProductOrderViewModel { get; set; }


        public IList<SupplierViewModel> SuppliersViewModel { get; set; }


        public DeliveryType DeliveryType { get; set; }

        public int SupplierId { get; set; }

        public int? DeliveryAddressId { get; set; }

        public PaymentType PaymentType { get; set; }



        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }

    public class ProductOrderViewModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }

        public decimal Price { get; set; }

        public int ClientsQuantity { get; set; } = 1;
    }

    public class OrderAdressViewModel
    {
        public int Id { get; set; }

        public string Street { get; set; }

        public string Description { get; set; }

        public string CityName { get; set; }

        public string CityPostcode { get; set; }
    }
    public class SupplierViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal PriceToHome { get; set; }

        public decimal PriceToOffice { get; set; }

        public bool IsDefault { get; set; }
    }
}