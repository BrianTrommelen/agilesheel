using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace agilesheel.Models.Seeds
{
    public static class MovieSeed
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            StoreDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<StoreDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Movies.Any())
            {
                context.Movies.AddRange(
                    new MovieModel
                    {
                        Title = "Movie 1",
                        Length = 180,
                        Synopsis = "text",
                        Year = 2021,
                        Genre = "Comedy",
                        ParentalRating = "18",
                        Is3D = false
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
