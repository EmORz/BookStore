using System.ComponentModel.DataAnnotations;
using BookStore.Model.Enum;
using Microsoft.AspNetCore.Http;

namespace BookStore_Inspiration.ViewModels.Product
{
    public class EditProductViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Име/Заглавие")]
        [Required(ErrorMessage = "Полето\"{0}\" e задължително.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]
        public string Title { get; set; }

        [Display(Name = "Жанр №")]
        [Required(ErrorMessage = "Полето\"{0}\" e задължително.")]
        public int GenreId { get; set; }

        [Display(Name = "Вид на продукта")]
        [Required(ErrorMessage = "Полето\"{0}\" e задължително.")]
        public string ProductTypes { get; set; }

        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Полето\"{0}\" e задължително.")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]
        public decimal Price { get; set; }

        [Display(Name = "Количество")]
        [Required(ErrorMessage = "Полето\"{0}\" e задължително.")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]
        public int Quantity { get; set; }

        [Display(Name = "Описание")]

        public string Description { get; set; }

        [Display(Name = "Автор")]

        public string Author { get; set; }

        [Display(Name = "Издателство")]

        public string Publishing { get; set; }

        [Display(Name = "ИСБН")]

        public string ISBN { get; set; }

        [Display(Name = "Година на издаване")]

        public string YearOfPublishing { get; set; }
        [Display(Name = "Снимки")]

        public IFormFile Picture { get; set; }
        [Display(Name = "Линк към видео")]

        public string youTubeLink { get; set; }   
    }
}