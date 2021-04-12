using agilesheel.Models;
using agilesheel.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
            // Check if user has the right role
            if (User.IsInRole("Touchscreen") && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "TouchscreenMovies");
            }

            // How many days to be added
            var offset = 0;

            var date = DateTime.Now;
            var currentWeekday = (int)date.DayOfWeek;
            const int endDay = 4; // Thursday

            // If current day is before thursday
            // Count down till thursday
            if (currentWeekday < endDay)
            {
                offset = endDay - currentWeekday;
            }

            // If current day is equal thursday
            // Start a new week
            if (currentWeekday == endDay)
            {
                offset = 7;
            }

            // If current day is after thursday
            // Count thursday down to the current day, plus the amount of days that's left
            if (currentWeekday > endDay)
            {
                offset = endDay - currentWeekday + 7;
            }

            // Add the defined days
            var lastMovieDay = date.AddDays(offset);

            IQueryable<Show> shows = _context.Shows
                            .Include(s => s.Movie)
                            .Include(s => s.Theater)
                            .Where(s => (s.StartTime > date) && (s.StartTime < lastMovieDay));

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
                FeaturedMovies = await _context.Movies
                .Where(s => s.IsFeatured == true)
                .ToListAsync(),

                TextBar = await _context.TextBar.FirstOrDefaultAsync(),
            };

            List<int> movie_ids = movieViewModel.Shows.Select(m => m.MovieId).Distinct().ToList();

            foreach (int movie_id in movie_ids)
            {
                movieViewModel.Movies.Add(await _context.Movies.FirstOrDefaultAsync(m => m.Id == movie_id));
            }

            return View(movieViewModel);
        }

        [Authorize(Roles = "Admin, Manager")]
        // GET: HomepageController/Details/5
        public async Task<IActionResult> EditTextBar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var textBar = await _context.TextBar.FindAsync(id);
            if (textBar == null)
            {
                return NotFound();
            }
            return View(textBar);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTextBar(int id, [Bind("Id,Content,Hide")] TextBar textBar)
        {
            if (id != textBar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(textBar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(textBar);
        }
    }
}
