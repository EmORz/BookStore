using System.Linq;
using BookStore.Data;
using BookStore.Model.Enum;
using BookStore.Model.Orders;
using BookStore.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IUserServices _userService;
        private readonly BookStoreDbContext _db;

        public OrderServices(IUserServices userService, BookStoreDbContext db)
        {
            _userService = userService;
            _db = db;
        }

        public Order GetProcessingOrder(string username)
        {
            var user = this._userService.GetUserByUsername(username);

            if (user == null)
            {
                return null;
            }

            var order = this._db.Orders.Include(x => x.DeliveryAddress)
                .ThenInclude(x => x.City)
                .Include(x => x.OrderProducts)
                .FirstOrDefault(x => x.BookStoreUser.UserName == username && x.Status == OrderStatus.Processing);

            return order;
        }

        public Order CreateOrder(string username)
        {
            var user = this._userService.GetUserByUsername(username);

            var order = new Order
            {
                Status = OrderStatus.Processing,
                BookStoreUser = user,
            };

            this._db.Orders.Add(order);
            this._db.SaveChanges();

            return order;
        }

        public void SetOrderDetails(Order order, string fullName, string phoneNumber, PaymentType paymentType, int deliveryAddressId,
            decimal deliveryPrice)
        {
            order.Recipient = fullName;
            order.RecipientPhoneNumber = phoneNumber;
            order.PaymentType = paymentType;
            order.DeliveryAddressId = deliveryAddressId;
            order.DeliveryPrice = deliveryPrice;

            this._db.Update(order);
            this._db.SaveChanges();
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