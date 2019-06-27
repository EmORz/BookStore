using System.ComponentModel.DataAnnotations;

namespace BookStore.Model.Enum
{
    public enum PaymentType
    {
        [Display(Name = "Наложен платеж")]
        CashОnDelivery = 1,

        [Display(Name = "Visa, MasterCard и др.")]
        Card = 2
    }
}