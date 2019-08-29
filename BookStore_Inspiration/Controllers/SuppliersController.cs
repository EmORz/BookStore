using BookStore.Model;
using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels.Suppliers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStore_Inspiration.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly ISuppliersService suppliersService;


        public SuppliersController(ISuppliersService suppliersService)
        {
            this.suppliersService = suppliersService;

        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public IActionResult All()
        {
            var supplierViewModels = this.suppliersService.All().Select(x => new SupplierViewModel()
            {

               Name = x.Name,
               PriceToOffice = x.PriceToOffice,
               PriceToHome = x.PriceToHome,
               IsDefault = x.IsDefault,
               Id = x.Id
                
            }).ToList();


            return View(supplierViewModels);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult MakeDafault(int id)
        {
            this.suppliersService.MakeDafault(id);

            return this.RedirectToAction(nameof(All));
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            this.suppliersService.Delete(id);

            return this.RedirectToAction(nameof(All));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Supplier supplier = this.suppliersService.GetSupplierById(id);
            
            var editViewModel = new EditSupplierViewModel()
            {
                Id = supplier.Id,
                Name = supplier.Name,
                PriceToOffice = supplier.PriceToOffice,
                PriceToHome = supplier.PriceToHome
            };

            return this.View(editViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(EditSupplierViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            this.suppliersService.Edit(model.Id, model.Name, model.PriceToHome, model.PriceToOffice);

            return this.RedirectToAction(nameof(All));
        }
    }
}