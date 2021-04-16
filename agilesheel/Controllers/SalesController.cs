using agilesheel.Models;
using agilesheel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace agilesheel.Controllers
{
    [Authorize(Roles = "Manager")]
    public class SalesController : Controller
    {
        private readonly StoreDbContext _context;
        private double saleOverviewPrice = 0;

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
        public async Task<IActionResult> SalesOverview(DateTime startTime, DateTime endTime)
        {
            startTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, 0, 0, 0);
            endTime = new DateTime(endTime.Year, endTime.Month, endTime.Day, 23, 59, 59);

            MovieViewModel movieViewModel = new MovieViewModel()
            {
                Tickets = await _context.Tickets
                .Where(s => s.Show.StartTime >= startTime && s.Show.StartTime <= endTime)
                .Include(s => s.Show)
                .Include(m => m.Show.Movie)
                .Include(m => m.Show.Theater)
                .Include(m => m.Show.Theater.Cinema)
                .ToListAsync()
            };

            foreach (Ticket ticket in movieViewModel.Tickets)
            {
                saleOverviewPrice = saleOverviewPrice + ticket.Price;
            }

            ViewBag.SalesOverviewPrice = String.Format("€ {0:0.00}", saleOverviewPrice);
            ViewBag.TicketsSold = movieViewModel.Tickets.Count();

            return View(movieViewModel);
        }
    }
}
