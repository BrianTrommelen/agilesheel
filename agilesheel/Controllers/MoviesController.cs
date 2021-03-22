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
    [AllowAnonymous]
    public class MoviesController : Controller
    {
        private readonly StoreDbContext _context;

        public MoviesController(StoreDbContext context)
        {
            _context = context;
        }

        // GET: MovieController
        public async Task<IActionResult> Index()
        {
            DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 0, 0, 0);
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

        // GET: Movies/Movie/5
        public async Task<IActionResult> Movie(int? id)
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
    }
}
