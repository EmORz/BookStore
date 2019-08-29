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

        [Display(Name = "Доставчик №")]
        [Required(ErrorMessage = "Полето\"{0}\" e задължително.")]
        public int SupplierId { get; set; }

        [Display(Name = "Адрес за доставка №")]
        [Required(ErrorMessage = "Полето\"{0}\" e задължително.")]
        public int? DeliveryAddressId { get; set; }

        [Display(Name = "Метод на плащане")]
        [Required(ErrorMessage = "Полето\"{0}\" e задължително.")]

        public PaymentType PaymentType { get; set; }


        [Display(Name = "Пълно име")]
        public string FullName { get; set; }

        [Display(Name = "Първо име")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Тел. номер")]
        public string PhoneNumber { get; set; }
    }
}