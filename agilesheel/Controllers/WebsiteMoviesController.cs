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
    public class WebsiteMoviesController : Controller
    {
        private readonly StoreDbContext _context;

        public WebsiteMoviesController(StoreDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: WebsiteMoviesController
        public async Task<IActionResult> Index()
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

            MovieViewModel movieViewModel = new MovieViewModel()
            {
                Shows = await _context.Shows
                .Include(s => s.Movie)
                .Include(s => s.Theater)
                .Where(s => (s.StartTime > DateTime.Now) && (s.StartTime < lastMovieDay))
               .ToListAsync(),

                Movies = new List<Movie>(),
            };

            List<int> movie_ids = movieViewModel.Shows.Select(m => m.MovieId).Distinct().ToList();

            foreach (int movie_id in movie_ids)
            {
                movieViewModel.Movies.Add(await _context.Movies.FirstOrDefaultAsync(m => m.Id == movie_id));
            }

            return View(movieViewModel);
        }


        // GET: WebsiteMoviesController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 0, 0, 0);

            MovieViewModel movieViewModel = new MovieViewModel()
            {
                Movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id),
                Shows = await _context.Shows
                    .Where(s => ((s.StartTime > DateTime.Now) && (s.StartTime < end) && s.Movie.Id == id))
                    .Include(s => s.Theater)
                    .OrderBy(s => s.StartTime)
                    .ToListAsync()
            };

            return View(movieViewModel);
        }

        // GET: WebsiteMoviesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WebsiteMoviesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WebsiteMoviesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WebsiteMoviesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WebsiteMoviesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WebsiteMoviesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
