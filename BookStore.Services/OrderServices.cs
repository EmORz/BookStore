using BookStore.Model.Enum;
using BookStore.Model.Orders;
using BookStore.Services.Contracts;

namespace BookStore.Services
{
    public class OrderServices : IOrderServices
    {
        public Order CreateOrder(string username)
        {
            throw new System.NotImplementedException();
        }

        public void SetOrderDetails(Order order, string fullName, string phoneNumber, PaymentType paymentType, int deliveryAddressId,
            decimal deliveryPrice)
        {
            throw new System.NotImplementedException();
        }

        public Order GetOrderById(int orderId)
        {
            throw new System.NotImplementedException();
        }

        public Order GetUserOrderById(int orderId, string username)
        {
            throw new System.NotImplementedException();
        }
    }
}