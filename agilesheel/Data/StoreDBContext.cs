using Microsoft.EntityFrameworkCore;

namespace agilesheel.Models
{
    public class StoreDBContext : DbContext
    {
        public StoreDBContext(DbContextOptions<StoreDBContext> options)
            : base(options) { }

        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Show> Shows { get; set; }

        public DbSet<Theater> Theaters { get; set; }

        public DbSet<SeatRow> SeatRows { get; set; }

        //public DbSet<Patron> Patrons { get; set; }
        //public DbSet<Movie> Movies { get; set; }
        //public DbSet<Show> Shows { get; set; }
        //public DbSet<Theater> Theaters { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Theater>()
        //        .HasOne(bc => bc.Cinema)
        //        .WithMany(b => b.Theaters)
        //        .HasForeignKey(bc => bc.CinemaId);

        //    modelBuilder.Entity<Show>()
        //        .HasKey(bc => new { bc.MovieId, bc.TheaterId });

        //    modelBuilder.Entity<Show>()
        //        .HasOne(bc => bc.Movie)
        //        .WithMany(b => b.Shows)
        //        .HasForeignKey(bc => bc.MovieId);

        //    modelBuilder.Entity<Show>()
        //        .HasOne(bc => bc.Theater)
        //        .WithMany(c => c.Shows)
        //        .HasForeignKey(bc => bc.TheaterId);

        //    modelBuilder.Entity<SeatRow>()
        //        .HasOne(bc => bc.Theater)
        //        .WithMany(t => t.SeatRows)
        //        .HasForeignKey(f => f.TheaterId);

        //    modelBuilder.Entity<Ticket>()
        //        .HasKey(bc => new { bc.SeatRowId, bc.ShowId });

        //    modelBuilder.Entity<Ticket>()
        //        .HasOne(s => s.SeatRow)
        //        .WithMany(t => t.Tickets)
        //        .HasForeignKey(f => f.SeatRowId);

        //    modelBuilder.Entity<Ticket>()
        //        .HasOne(s => s.Show)
        //        .WithMany(t => t.Tickets)
        //        .HasForeignKey(f => f.ShowId);
        //}
    }
}
