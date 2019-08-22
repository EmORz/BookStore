using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Model;
using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels.Genre;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_Inspiration.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(CreateGenreViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            this._genreService.Create(model.Name);

            return this.RedirectToAction(nameof(All));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult All()
        {
            var genreViewModels = this._genreService.All().Select(x => new GenreViewModel()
            {

                Name = x.Name,
     
                Id = x.Id

            }).ToList();


            return View(genreViewModels);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            this._genreService.Delete(id);

            return this.RedirectToAction(nameof(All));
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Genre supplier = this._genreService.GetGenreById(id);

            var editViewModel = new EditGenreViewModel()
            {
                Id = supplier.Id,
                Name = supplier.Name,
            };

            return this.View(editViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(EditGenreViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            this._genreService.Edit(model.Id, model.Name);

            return this.RedirectToAction(nameof(All));
        }


    }
}