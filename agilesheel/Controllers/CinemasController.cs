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
    public class CinemasController : Controller
    {
        private readonly StoreDbContext _context;

        public CinemasController(StoreDbContext context)
        {
            _context = context;
        }

        // GET: CinemasController
        public async Task<IActionResult> Index()
        {
            CinemaViewModel cinemaViewModel = new CinemaViewModel()
            {
                Cinemas = await _context.Cinemas.ToListAsync()
            };

            return View(cinemaViewModel);
        }

        // GET: CinemasController/Details/5
        public async Task<IActionResult> Cinema(int? id)
        {
            CinemaViewModel cinemaViewModel = new CinemaViewModel()
            {
                Cinema = await _context.Cinemas.FirstOrDefaultAsync(c => c.Id == id)
            };

            
            return View(cinemaViewModel);
        }
    }
}
