using Microsoft.EntityFrameworkCore;

namespace agilesheel.Models
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) 
            : base(options) { }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Show> Shows { get; set; }
    }
}
