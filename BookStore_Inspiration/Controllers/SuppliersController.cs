using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Model;
using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels.Orders.Create;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_Inspiration.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly ISuppliersService suppliersService;


        public SuppliersController(ISuppliersService suppliersService)
        {
            this.suppliersService = suppliersService;

        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateSupplierViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            this.suppliersService.Create(model.Name, model.PriceToHome, model.PriceToOffice);

            return this.RedirectToAction(nameof(All));
        }

        public IActionResult All()
        {
            var supplierViewModels = this.suppliersService.All().Select(x => new SupplierViewModel()
            {
               Name = x.Name,
               PriceToOffice = x.PriceToOffice,
               PriceToHome = x.PriceToHome,
               IsDefault = x.IsDefault
                
            }).ToList();

            //var supplierViewModels = mapper.Map<IList<SupplierViewModel>>(suppliers);

            return View(supplierViewModels);
        }

        public IActionResult MakeDafault(int id)
        {
            this.suppliersService.MakeDafault(id);

            return this.RedirectToAction(nameof(All));
        }

        //public IActionResult Delete(int id)
        //{
        //    this.suppliersService.Delete(id);

        //    return this.RedirectToAction(nameof(All));
        //}

        //public IActionResult Edit(int id)
        //{
        //    Supplier supplier = this.suppliersService.GetSupplierById(id);

        //    var editViewModel = mapper.Map<EditSupplierViewModel>(supplier);

        //    return this.View(editViewModel);
        //}

        //[HttpPost]
        //public IActionResult Edit(EditSupplierViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return this.View(model);
        //    }

        //    this.suppliersService.Edit(model.Id, model.Name, model.PriceToHome, model.PriceToOffice);

        //    return this.RedirectToAction(nameof(All));
        //}
    }

    public class CreateSupplierViewModel
    {

        public string Name { get; set; }

        public decimal PriceToHome { get; set; }


        public decimal PriceToOffice { get; set; }

        public bool IsDefault { get; set; }
    }
}