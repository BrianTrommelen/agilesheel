using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using agilesheel.Models;
using agilesheel.ViewModels;

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

        private IStoreRepository _repo;

        public TicketsController(StoreDbContext context, IStoreRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: Tickets
        //public IActionResult Index() => View(_repo.Tickets);
        public async Task<IActionResult> Index()
        {
            //if(id == null)
            //{
            //    return NotFound();
            //}

            //var isShow = await _context.Shows
            //    .Include(s => s.Tickets).ThenInclude(s => s.SeatRow)
            //     .Where(s => s.ShowId == id)
            // .FirstOrDefaultAsync();

            //var currentShow = _repo.Tickets.Where(p => p.Id == id);


            return View(await _context.Tickets.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);

            TicketsViewModel ticketViewModel = new TicketsViewModel()
            {
                Ticket = await _context.Tickets
               .FirstOrDefaultAsync(m => m.Id == id),
            };

            ticketViewModel.Show = await _repo.Shows
                .FirstOrDefaultAsync(m => m.Id == ticketViewModel.Ticket.ShowId);
            ticketViewModel.Movie = await _repo.Movies
                .FirstOrDefaultAsync(m => m.Id == ticketViewModel.Show.MovieId);

            //var movie = await _repo.Movies
            //    .FirstOrDefaultAsync(m => m.Id == show.MovieId);
            //if (movie == null)
            //{
            //    return NotFound();
            //}


            //ViewBag.Show = show.StartTime;
            //ViewBag.Movie = movie.Title;

            // ViewBags need to change to viewmodel

            return View(ticketViewModel);
        }

        // GET: Tickets/Create
        public IActionResult Create(int? id)
        {
            int currentShow;
            if (id != null)
            {
                currentShow = (int)id;
            }
            else
            {
                return NotFound();
            }

            ViewData["Price"] = NormalPrice;

            TicketsViewModel ticketViewModel = new TicketsViewModel(_repo);

            ticketViewModel.Shows = _repo.Shows.ToList();
            ticketViewModel.Show = _repo.Shows.FirstOrDefault(x => x.Id == id);
            ticketViewModel.Movie = _repo.Movies.FirstOrDefault(x => x.Id == ticketViewModel.Show.MovieId);

            ticketViewModel.Seat = ticketViewModel.GetSeatNumber(currentShow);


            //ticketViewModel.Tickets = _repo.Tickets.Select(m => m.ShowId == currentShow).ToList();

            return View(ticketViewModel);
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShowId,Name,Code,Price,SeatRowId,SeatNumber")] Ticket ticket)
        {

            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }

            return View(RedirectToAction(nameof(Create)));
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Code,Price,SeatNumber")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
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
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }

        // GET: Tickets/Price/{movie_id}
        [HttpGet]
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
