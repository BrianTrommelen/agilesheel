using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;

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

            if (!context.Cinemas.Any())
            {
                context.Cinemas.Add(
                    new Cinema
                    {
                        Name = "Agile's Heel Movies",
                        Location = "Breda"
                    });
            }

            context.SaveChanges();

            for (int i = 0; i < 6; i++)
            {
                if (!context.Theaters.Any(t => t.Name == $"Zaal { i + 1 }"))
                {
                    context.Theaters.Add(
                        new Theater
                        {
                            Name = $"Zaal { i + 1 }",
                            Cinema = context.Cinemas.First(),
                            Has3D = new[] { "Zaal 1", "Zaal 2" }.Contains($"Zaal { i + 1 }") // Zaal 1 en 2 zijn 3d
                        });

                    context.SaveChanges();
                }
            }

            // Zaal 1 - 3 (8*15)
            foreach (var t in context.Theaters.Where(t => new[] { "Zaal 1", "Zaal 2", "Zaal 3" }.Contains(t.Name)))
            {
                if (!context.SeatRows.Any(s => s.Theater.Name == t.Name))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        context.SeatRows.AddRange(
                            new SeatRow
                            {
                                Seats = 15,
                                Theater = t
                            });
                    }
                }
            }

            context.SaveChanges();

            // Zaal 4 (6*10)
            if (!context.SeatRows.Any(s => s.Theater.Name == "Zaal 4"))
            {
                for (int i = 0; i < 6; i++)
                {
                    context.SeatRows.AddRange(
                        new SeatRow
                        {
                            Seats = 10,
                            Theater = context.Theaters.First(t => t.Name == "Zaal 4")
                        });

                }
                context.SaveChanges();
            }

            // Zaal 5 - 6 (2*10+2*15)
            foreach (var t in context.Theaters.Where(t => new[] { "Zaal 5", "Zaal 6" }.Contains(t.Name)))
            {
                if (!context.SeatRows.Any(s => s.Theater.Name == t.Name))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        context.SeatRows.AddRange(
                            new SeatRow
                            {
                                Seats = 10,
                                Theater = t
                            });
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        context.SeatRows.AddRange(
                            new SeatRow
                            {
                                Seats = 15,
                                Theater = t
                            });
                    }
                }
            }
            context.SaveChanges();

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

            if (!context.Shows.Any(s => s.StartTime > DateTime.Now))
            {
                foreach (var t in context.Theaters)
                {
                    var moviestart = DateTime.Now.Date;

                    var show1 = moviestart.AddHours(18);
                    var show2 = show1.AddHours(3);
                    var show3 = show2.AddHours(3);

                    context.Shows.AddRange(
                        new Show
                        {
                            Movie = context.Movies.First(),
                            StartTime = show1,
                            Theater = t
                        },
                        new Show
                        {
                            Movie = context.Movies.First(),
                            StartTime = show2,
                            Theater = t
                        },
                        new Show
                        {
                            Movie = context.Movies.First(),
                            StartTime = show3,
                            Theater = t
                        });

                }
                context.SaveChanges();
            }
        }
    }
}
