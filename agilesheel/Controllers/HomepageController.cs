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

        public async Task<IActionResult> IndexAsync()
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


        //// GET: HomepageController
        //public async Task<IActionResult> Index()
        //{
        //    DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 0, 0, 0);
        //    MovieViewModel movieViewModel = new MovieViewModel()
        //    {
        //        Movies = await _context.Movies.ToListAsync(),
        //        Shows = await _context.Shows
        //        .Include(s => s.Movie)
        //        .Include(s => s.Theater)
        //        .Where(s => ((s.StartTime > DateTime.Now) && (s.StartTime < end)))
        //        .OrderBy(s => s.StartTime)
        //       .ToListAsync()
        //    };

        //    return View(movieViewModel);
        //}

        // GET: HomepageController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomepageController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomepageController/Create
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

        // GET: HomepageController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomepageController/Edit/5
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

        // GET: HomepageController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomepageController/Delete/5
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
