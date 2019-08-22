using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookStore.Model.Enum;
using BookStore_Inspiration.ViewModels.Suppliers;

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
}