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
        private double NormalPrice = 0;
        private double TotalPrice = 0;
        private int AmountNormalPrice;
        private int AmountChildrenPrice;
        private int AmountStudentsPrice;
        private int Amount65Price;

        public TicketsController(StoreDbContext context)
        {
            _context = context;
        }

        // GET: Tickets/Price/{movie_id}
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
            else
            {
                NormalPrice = 9.00;
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
            NormalPrice = 9.00;

            ViewData["Price"] = NormalPrice;

            int.TryParse(HttpContext.Request.Form["AmountNormalPrice"], out AmountNormalPrice);
            int.TryParse(HttpContext.Request.Form["AmountChildrenPrice"], out AmountChildrenPrice);
            int.TryParse(HttpContext.Request.Form["AmountStudentsPrice"], out AmountStudentsPrice);
            int.TryParse(HttpContext.Request.Form["Amount65Price"], out Amount65Price);

            for (int i = 0; i < AmountNormalPrice; i++)
            {
                TotalPrice = NormalPrice * i;
            }

            // Check bouwen
            // Voor kinderen tot 11 jaar.
            // Voorstelling tot 18:00,
            // Alleen Nederlands gesproken films
            for (int i = 0; i < AmountChildrenPrice; i++)
            {
                TotalPrice = (NormalPrice - 1.50) * i;
            }

            // Check bouwen
            // Op vertoon van een studentenkaart
            // Alleen geldig op maandag t/m donderdag
            for (int i = 0; i < AmountStudentsPrice; i++)
            {
                TotalPrice = (NormalPrice - 1.50) * i;
            }
            
            // Check
            // Op vertoon van een 65+ plus
            // Niet geldig op vakantiedagen en / of feestdagen
            for (int i = 0; i < Amount65Price; i++)
            {
                TotalPrice = (NormalPrice - 1.50) * i;
            }
            ViewData["TotalPrice"] = TotalPrice;


            return View(movie);
        }
    }
}
