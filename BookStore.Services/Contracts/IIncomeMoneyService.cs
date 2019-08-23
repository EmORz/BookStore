using System;
using System.Collections.Generic;
using BookStore.Model;

namespace BookStore.Services.Contracts
{
    public interface IIncomeMoneyService
    {
        void Create(string userId, decimal totalMoney, int productId, int quantity, string paymentMethod, string addressDelivery, DateTime dateOfPurchase);

        IList<IncomeMoney> AllPurchase();

        IncomeMoney GetById(int Id);



        /*  public string UserId { get; set; }

        public decimal TotalMoney { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string PaymentMethod { get; set; }

        public string AddressDelivery { get; set; }

        public DateTime DateTimeOfPurchase { get; set; }*/
    }
}