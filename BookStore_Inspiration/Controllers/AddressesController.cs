using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_Inspiration.Controllers
{
    public class AddressesController : Controller
    {
        private readonly IAddressesServices _addressesServices;

        public AddressesController(IAddressesServices addressesServices)
        {
            _addressesServices = addressesServices;
        }
        public IActionResult Add(AddAddressesViewModel addressesModel)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction("Create", "Orders");
            }

            var address = this._addressesServices.CreateAddress(addressesModel.Street, addressesModel.Description, addressesModel.CityName, addressesModel.CityPostcode);
            this._addressesServices.AddAddressToUser(this.User.Identity.Name, address);

            return this.RedirectToAction("Create", "Orders");
        }
    }
    public class AddAddressesViewModel
    {
        public int Id { get; set; }

  
        public string Street { get; set; }


        public string Description { get; set; }

        public string CityName { get; set; }


        public string CityPostcode { get; set; }
    }
}