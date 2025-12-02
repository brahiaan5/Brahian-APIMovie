using API.Movies.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Movies.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Section to create the DbSet of entities/models
        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
