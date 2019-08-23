using System;

namespace BookStore.Model
{
    public class IncomeMoney
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public decimal TotalMoney { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string PaymentMethod { get; set; }

        public string AddressDelivery { get; set; }

        public DateTime DateTimeOfPurchase { get; set; }
    }
}