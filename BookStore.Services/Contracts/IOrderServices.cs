using BookStore.Model.Enum;
using BookStore.Model.Orders;

namespace BookStore.Services.Contracts
{
    public interface IOrderServices
    {
        Order GetProcessingOrder(string username);
        Order CreateOrder(string username);

        void SetOrderDetails(Order order, string fullName, string phoneNumber, PaymentType paymentType, int deliveryAddressId, decimal deliveryPrice);


        Order GetOrderById(int orderId);

        Order GetUserOrderById(int orderId, string username);


    }
}