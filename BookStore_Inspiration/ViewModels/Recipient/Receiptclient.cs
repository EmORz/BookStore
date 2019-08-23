using System;

namespace BookStore_Inspiration.ViewModels.Recipient
{
    public class Receiptclient
    {
        public string UserId { get; set; }

        public DateTime TimeOfPurchase { get; set; }

        public string Address { get; set; }

        public int productId { get; set; }

        public string paymentMethod { get; set; }

        public decimal Totalmoney { get; set; }
        /*      UserId = userId,
                AddressDelivery = addressDelivery,
                DateTimeOfPurchase = dateOfPurchase,
                ProductId = productId,
                PaymentMethod = paymentMethod,
                Quantity = quantity,
                TotalMoney = totalMoney*/
    }
}