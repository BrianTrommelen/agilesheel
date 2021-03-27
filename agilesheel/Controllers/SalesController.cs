using agilesheel.Constants;
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
    [Authorize(Roles = "Manager")]
    public class SalesController : Controller
    {
        private readonly StoreDbContext _context;

        public SalesController(StoreDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckSales(DateTime startTime, DateTime endTime)
        {
            MovieViewModel movieViewModel = new MovieViewModel()
            {
                Shows = await _context.Shows
                .Where(s => s.StartTime >= startTime && s.StartTime <= endTime)
                .Include(s => s.Tickets)
                .ToListAsync()
            };

            TempData["Error message"] = "No results for selection";
            return View("SalesOverview", movieViewModel);
        }
    }
}
