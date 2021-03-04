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
                    new Movie
                    {
                        Title = "Tenet",
                        Length = 150,
                        Synopsis = "Armed with only one word, Tenet, and fighting for the survival of the entire world, a Protagonist journeys through a twilight world of international espionage on a mission that will unfold in something beyond real time.",
                        Year = 2020,
                        Genre = "Action",
                        ParentalRating = "12",
                        PosterUrl = "https://m.media-amazon.com/images/M/MV5BYzg0NGM2NjAtNmIxOC00MDJmLTg5ZmYtYzM0MTE4NWE2NzlhXkEyXkFqcGdeQXVyMTA4NjE0NjEy._V1_UX182_CR0,0,182,268_AL_.jpg",
                        Is3D = true
                    },
                    new Movie
                    {
                        Title = "The Blind Side",
                        Length = 129,
                        Synopsis = "The story of Michael Oher, a homeless and traumatized boy who became an All-American football player and first-round NFL draft pick with the help of a caring woman and her family.",
                        Year = 2009,
                        Genre = "Drama",
                        ParentalRating = "9",
                        PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjEzOTE3ODM3OF5BMl5BanBnXkFtZTcwMzYyODI4Mg@@._V1_UX182_CR0,0,182,268_AL_.jpg",
                        Is3D = false
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
