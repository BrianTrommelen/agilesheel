using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using agilesheel.Models;

namespace agilesheel.Controllers
{
    public class TicketsController : Controller
    {
        private readonly StoreDbContext _context;
        private double NormalPrice = 9.00;
        private double TotalPrice = 0;
        private int AmountNormalPrice;
        private int AmountChildrenPrice;
        private int AmountStudentsPrice;
        private int Amount65Price;
        private int TotalAmount = 0;

        public TicketsController(StoreDbContext context)
        {
            _context = context;
        }

        // GET: Tickets/Price/{movie_id}
        [HttpGet]
        public async Task<IActionResult> Price(int? id)
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

            if (movie.Length <= 120)
            {
                NormalPrice = 8.50;
            }

            ViewData["Price"] = NormalPrice;

            return View(movie);
        }

        // POST: Tickets/Price/{movie_id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PriceAsync(int id)
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

            if (movie.Length <= 120)
            {
                NormalPrice = 8.50;
            }

            ViewData["Price"] = NormalPrice;

            int.TryParse(HttpContext.Request.Form["AmountNormalPrice"], out AmountNormalPrice);
            int.TryParse(HttpContext.Request.Form["AmountChildrenPrice"], out AmountChildrenPrice);
            int.TryParse(HttpContext.Request.Form["AmountStudentsPrice"], out AmountStudentsPrice);
            int.TryParse(HttpContext.Request.Form["Amount65Price"], out Amount65Price);

            // TODO : Voor kinderen tot 11 jaar.
            // TODO : Alleen Nederlands gesproken films. Op Genre?
            if (this.CheckTime())
            {
                TotalPrice = TotalPrice + ((NormalPrice - 1.50) * AmountChildrenPrice);
            }
            else
            {
                TotalPrice = TotalPrice + (NormalPrice * AmountChildrenPrice);
            }

            // TODO : Op vertoon van een studentenkaart
            if (this.CheckDay())
            {
                TotalPrice = TotalPrice + ((NormalPrice - 1.50) * AmountStudentsPrice);
            }
            else
            {
                TotalPrice = TotalPrice + (NormalPrice * AmountStudentsPrice);
            }

            // TODO : Op vertoon van een 65+ plus kaart
            // TODO : Niet geldig op vakantiedagen en / of feestdagen
            TotalPrice = TotalPrice + ((NormalPrice - 1.50) * Amount65Price);

            TotalPrice = TotalPrice + (NormalPrice * AmountNormalPrice);

            ViewData["TotalPrice"] = TotalPrice;
            return View(movie);
        }

        private bool CheckDay()
        {
            var today = DateTime.Today;
            if (DayOfWeek.Friday == today.DayOfWeek
                && DayOfWeek.Saturday == today.DayOfWeek
                && DayOfWeek.Sunday == today.DayOfWeek)
            {
                return true;
            }

            return false;
        }

        private bool CheckTime()
        {
            var start = new TimeSpan(06, 0, 0);
            var end = new TimeSpan(18, 0, 0);
            var now = DateTime.Now.TimeOfDay;

            if (start >= now && end <= now)
            {
                return false;
            }

            return true;
        }
    }
}
