using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.DTOs
{
    public class CreateMovieDTO
    {
        [Required(ErrorMessage = "The title is requerid")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "The gender is requerid")]
        [StringLength(30, ErrorMessage = "Length cannot exceed 30 characters")]
        public string Gender { get; set; } //Genero

        [Required(ErrorMessage = "The duration is requerid")]
        [Range(70, 600, ErrorMessage = "The duration must be between 70 and 600 minutes")]
        public int Duration { get; set; }
    }
}
