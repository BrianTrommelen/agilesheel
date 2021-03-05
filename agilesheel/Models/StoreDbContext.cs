using Microsoft.EntityFrameworkCore;

namespace agilesheel.Models
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) 
            : base(options) { }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Theater> Theaters { get; set; }

        public DbSet<Show> Shows { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Show>()
			.HasKey(bc => new { bc.MovieId, bc.TheaterId });

			modelBuilder.Entity<Show>()
			.HasOne(bc => bc.Movie)
			.WithMany(b => b.Shows)
			.HasForeignKey(bc => bc.MovieId);

			modelBuilder.Entity<Show>()
			.HasOne(bc => bc.Theather)
			.WithMany(c => c.Shows)
			.HasForeignKey(bc => bc.TheaterId);
		}
	}
}
