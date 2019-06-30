﻿using System;
using System.Collections.Generic;
using BookStore.Model.Enum;
using BookStore.Model.HelpModels;

namespace BookStore.Model.Orders
{
    public class Order 
    {
        public string Id { get; set; }

        public OrderStatus Status { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public int? DeliveryAddressId { get; set; }
        public virtual Address.Address DeliveryAddress { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DeliveryPrice { get; set; }

        public PaymentType PaymentType { get; set; }

        public string UserId { get; set; }
       // public BookStoreUser BookStoreUser { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

    }
}