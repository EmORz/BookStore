using System.ComponentModel.DataAnnotations;

namespace BookStore.Model.Enum
{
    public enum PaymentStatus
    {
        [Display(Name = "Платено")]
        Paid = 1,

        [Display(Name = "Очаква плащане")]
        Unpaid = 2,

        [Display(Name = "Отказана")]
        Denied = 4

    }
}