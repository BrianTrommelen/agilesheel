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
                Shows = await _context.Shows
                .Include(t => t.Tickets)
                .Where(s => s.StartTime >= startTime && s.StartTime <= endTime && s.Tickets.Count() > 0)
                .ToListAsync(),

                Tickets = new List<Ticket>(),
            };

            List<int> show_ids = movieViewModel.Shows.Select(s => s.Id).ToList();

            foreach (int show_id in show_ids)
            {
                var ticket = await _context.Tickets.Where(t => t.ShowId == show_id)
                    .Include(s => s.Show)
                    .Include(m => m.Show.Movie)
                    .Include(m => m.Show.Theater)
                    .Include(m => m.Show.Theater.Cinema)
                    .FirstOrDefaultAsync();
                movieViewModel.Tickets.Add(ticket);
            }

            foreach(Ticket ticket in movieViewModel.Tickets)
            {
                saleOverviewPrice = saleOverviewPrice + ticket.Price;
            }

            ViewBag.SalesOverviewPrice = String.Format("{0:0.00}", saleOverviewPrice); ;
            ViewBag.TicketsSold = movieViewModel.Tickets.Count();

            return View(movieViewModel);
        }
    }
}
