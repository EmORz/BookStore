using System.ComponentModel.DataAnnotations;

namespace BookStore_Inspiration.ViewModels.Genre
{
    public class CreateGenreViewModel
    {
        [Display(Name = "Име на жанра")]
        [Required(ErrorMessage = "Полето\"{0}\" e задължително.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]

        public string Name { get; set; }
    }
}