﻿using agilesheel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agilesheel.Controllers
{
    public class ShowsController : Controller
    {
        private readonly StoreDBContext _context;

        public ShowsController(StoreDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetUpcomingShows()
        {
            var shows = await _context.Shows
                .Include(s => s.Movie)
                .Where(s => ((s.StartTime > DateTime.Now) && (s.StartTime < DateTime.Now.AddHours(15))))
                .OrderBy(s => s.StartTime)
               .ToListAsync();

            return View(shows);
        }
    }
}
