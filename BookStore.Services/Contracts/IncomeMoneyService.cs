using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Data;
using BookStore.Model;

namespace BookStore.Services.Contracts
{
    public class IncomeMoneyService : IIncomeMoneyService
    {
        private readonly BookStoreDbContext _db;

        public IncomeMoneyService(BookStoreDbContext Db)
        {
            _db = Db;
        }

        public void Create(string userId, decimal totalMoney, int productId, int quantity, string paymentMethod,
            string addressDelivery, DateTime dateOfPurchase)
        {

            var income = new IncomeMoney()
            {
                UserId = userId,
                AddressDelivery = addressDelivery,
                DateTimeOfPurchase = dateOfPurchase,
                ProductId = productId,
                PaymentMethod = paymentMethod,
                Quantity = quantity,
                TotalMoney = totalMoney
            };

            _db.IcIncomeMonies.Add(income);

            _db.SaveChanges();
        }

        public IList<IncomeMoney> AllPurchase()
        {

            var allPurchase = _db.IcIncomeMonies.ToList();
            return allPurchase;
        }

        public IncomeMoney GetById(int Id)
        {
            var curentPurchase = _db.IcIncomeMonies.Where(x => x.Id == Id).FirstOrDefault();

            return curentPurchase;
        }
    }
}