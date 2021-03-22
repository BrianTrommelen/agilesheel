using agilesheel.Models;
using agilesheel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agilesheel.Controllers
{
    [AllowAnonymous]
    public class HomepageController : Controller
    {
        private readonly StoreDbContext _context;

        public HomepageController(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync(string genre, string _3d)
        {
            var offset = 0;
            var date = DateTime.Now;
            var currentWeekday = (int)date.DayOfWeek;
            const int endDay = 4; // Thursday

            if (currentWeekday < endDay)
            {
                offset = endDay - currentWeekday;
            }
            if (currentWeekday == endDay)
            {
                offset = 7;
            }
            if (currentWeekday > endDay)
            {
                offset = endDay - currentWeekday + 7;
            }

            var lastMovieDay = date.AddDays(offset);

            IQueryable<Show> shows = _context.Shows
                            .Include(s => s.Movie)
                            .Include(s => s.Theater)
                            .Where(s => (s.StartTime > DateTime.Now) && (s.StartTime < lastMovieDay));

            //Genre filter
            if (!string.IsNullOrEmpty(genre))
            {
                shows = shows.Where(s => s.Movie.Genre.ToLower().Equals(genre.ToLower()));
            }

            // 3D filter
            if (!string.IsNullOrEmpty(_3d))
            {
                shows = shows.Where(s => _3d == "true" ? s.Movie.Is3D : !s.Movie.Is3D);
            }

            MovieViewModel movieViewModel = new MovieViewModel()
            {
                Shows = await shows.ToListAsync(),
                Movies = new List<Movie>(),
            };

            List<int> movie_ids = movieViewModel.Shows.Select(m => m.MovieId).Distinct().ToList();

            foreach (int movie_id in movie_ids)
            {
                movieViewModel.Movies.Add(await _context.Movies.FirstOrDefaultAsync(m => m.Id == movie_id));
            }

            return View(movieViewModel);
        }

        public IActionResult Contact()
        {
            return View(_context.Cinemas.FirstOrDefault());
        }
    }
}
