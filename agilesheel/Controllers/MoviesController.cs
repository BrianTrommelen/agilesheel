using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using agilesheel.Models;
using agilesheel.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace agilesheel.Controllers
{
    public class MoviesController : Controller
    {
        private readonly StoreDbContext _context;

        public MoviesController(StoreDbContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day +1 , 0, 0, 0);
            MovieViewModel movieViewModel = new MovieViewModel()
            {
                Movies = await _context.Movies.ToListAsync(),
                Shows = await _context.Shows
                .Include(s => s.Movie)
                .Include(s => s.Theater)
                .Where(s => ((s.StartTime > DateTime.Now) && (s.StartTime < end)))
                .OrderBy(s => s.StartTime)
               .ToListAsync()
            };

            return View(movieViewModel);
        }

        [Authorize(Roles = "Touchscreen")]
        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 0, 0, 0);

            MovieViewModel movieViewModel = new MovieViewModel()
            {
                Movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id),
                Shows = await _context.Shows
                    .Where(s => ((s.StartTime > DateTime.Now) && (s.StartTime < end) && s.Movie.Id == id))
                    .Include(s => s.Theater)
                    .OrderBy(s => s.StartTime)
                    .ToListAsync()
        };


            return View(movieViewModel);
        }

        [Authorize(Roles = "Touchscreen")]
        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Touchscreen")]
        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Length,Synopsis,Year,Genre,PosterUrl,ParentalRating,Is3D")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        [Authorize(Roles = "Touchscreen")]
        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [Authorize(Roles = "Touchscreen")]
        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Length,Synopsis,Year,Genre,PosterUrl,ParentalRating,Is3D")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        [Authorize(Roles = "Touchscreen")]
        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [Authorize(Roles = "Touchscreen")]
        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
