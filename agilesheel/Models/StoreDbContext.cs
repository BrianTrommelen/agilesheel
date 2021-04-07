using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace agilesheel.Models
{
    public class StoreDbContext : IdentityDbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options) { }

        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Show> Shows { get; set; }

        public DbSet<Theater> Theaters { get; set; }

        public DbSet<SeatRow> SeatRows { get; set; }

        public DbSet<TextBar> TextBar { get; set; }

        public DbSet<LostAndFound> LostAndFound { get; set; }

        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
