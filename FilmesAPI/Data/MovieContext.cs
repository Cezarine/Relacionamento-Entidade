using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> opts)
            : base(opts)
        {
            
        }

        public DbSet<MovieVM> Movies { get; set; }
    }
}
