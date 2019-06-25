﻿using System.Collections.Generic;
using BookStore.Model.HelpModels;
using BookStore.Model.Orders;

namespace BookStore.Model.Address
{
    public class Address : EntityBase<int>
    {

        public string Description { get; set; }

        public string Country { get; set; }

        public int CityId { get; set; }
        public virtual City City { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string Street { get; set; }

        public string BuildingNumber { get; set; }

        public ICollection<OrderNewProduct> Addresses { get; set; }
    }
}