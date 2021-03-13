using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;

namespace agilesheel.Models.Seeds
{
    public static class MovieSeed
    {
        //Function to get random number
        private static readonly Random getrandom = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }

        public static void EnsurePopulated(IApplicationBuilder app)
        {
            AgilesheelContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<AgilesheelContext>();

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
                if (!context.Theaters.Any(t => t.Name == $"Theater { i + 1 }"))
                {
                    context.Theaters.Add(
                        new Theater
                        {
                            Name = $"Theater { i + 1 }",
                            Cinema = context.Cinemas.First(),
                            Has3D = new[] { "Theater 1", "Theater 2" }.Contains($"Theater { i + 1 }") // Theater 1 en 2 zijn 3d
                        });

                    context.SaveChanges();
                }
            }

            // Theater 1 - 3 (8*15)
            foreach (var t in context.Theaters.Where(t => new[] { "Theater 1", "Theater 2", "Theater 3" }.Contains(t.Name)))
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

            // Theater 4 (6*10)
            if (!context.SeatRows.Any(s => s.Theater.Name == "Theater 4"))
            {
                for (int i = 0; i < 6; i++)
                {
                    context.SeatRows.AddRange(
                        new SeatRow
                        {
                            Seats = 10,
                            Theater = context.Theaters.First(t => t.Name == "Theater 4")
                        });

                }
                context.SaveChanges();
            }

            // Theater 5 - 6 (2*10+2*15)
            foreach (var t in context.Theaters.Where(t => new[] { "Theater 5", "Theater 6" }.Contains(t.Name)))
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
                    },
                    new Movie
                    {
                        Title = "WALL-E",
                        Length = 98,
                        Synopsis = "In the distant future, a small waste-collecting robot inadvertently embarks on a space journey that will ultimately decide the fate of mankind.",
                        Year = 2008,
                        Genre = "Adventure",
                        ParentalRating = "AL",
                        PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjExMTg5OTU0NF5BMl5BanBnXkFtZTcwMjMxMzMzMw@@._V1_UX182_CR0,0,182,268_AL_.jpg",
                        Is3D = false
                    }
                );
                context.SaveChanges();
            }

            if (!context.Shows.Any(s => s.StartTime > DateTime.Now))
            {
                foreach (var t in context.Theaters)
                {
                    int rando = GetRandomNumber(1, context.Movies.Count() + 1);
                    var moviestart = DateTime.Now.Date;

                    var show1 = moviestart.AddHours(18);
                    var show2 = show1.AddHours(3);
                    var show3 = show2.AddHours(3);
                    

                    context.Shows.AddRange(
                        new Show
                        {
                            Movie = context.Movies.FirstOrDefault(x => x.Id == rando),
                            StartTime = show1,
                            Theater = t
                        },
                        new Show
                        {
                            Movie = context.Movies.FirstOrDefault(x => x.Id == rando),
                            StartTime = show2,
                            Theater = t
                        },
                        new Show
                        {
                            Movie = context.Movies.FirstOrDefault(x => x.Id == rando),
                            StartTime = show3,
                            Theater = t
                        });

                }
                context.SaveChanges();
            }
        }
    }
}
