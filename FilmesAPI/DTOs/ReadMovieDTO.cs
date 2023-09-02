using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.DTOs
{
    public class ReadMovieDTO
    {
       
        public string Title { get; set; }
        public string Gender { get; set; } //Genero
        public int Duration { get; set; }
        public DateTime ConsultationTime { get; set; } = DateTime.Now;
    }
}
