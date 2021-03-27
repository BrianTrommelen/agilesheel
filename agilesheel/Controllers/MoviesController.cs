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

        [AllowAnonymous]
        public async Task<IActionResult> IndexAsync(string genre, string _3d)
        {
            DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 0, 0, 0);

            IQueryable<Show> shows = _context.Shows
                            .Include(s => s.Movie)
                            .Include(s => s.Theater);

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

        [AllowAnonymous]
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
                    .ToListAsync(),

                FeaturedMovies = await _context.Movies
                .Where(s => s.IsFeatured == true)
                .ToListAsync()
            };


            return View(movieViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Length,Synopsis,Year,Genre,PosterUrl,ParentalRating,Is3D,IsFeatured")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Length,Synopsis,Year,Genre,PosterUrl,ParentalRating,Is3D,IsFeatured")] Movie movie)
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
