using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using agilesheel.Models;
using agilesheel.ViewModels;
using agilesheel.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace agilesheel.Controllers
{
    public class TicketsController : Controller
    {
        private readonly StoreDbContext _context;
        private IStoreRepository _repo;

        public TicketsController(StoreDbContext context, IStoreRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            List<Ticket> tickets;

            if (User.IsInRole("Cashier"))
            {
                tickets = await _context.Tickets
                    .Include(t => t.Show.Movie)
                    .ToListAsync();
            }
            else
            {
                tickets = await _context.Tickets
                    .Where(t => t.UserId == User.GetUserId())
                    .Include(t => t.Show.Movie)
                    .ToListAsync();
            }

            return View(tickets);
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
                .Include(s => s.Theater)
                .FirstOrDefaultAsync(m => m.Id == ticketViewModel.Ticket.ShowId);
            ticketViewModel.Movie = await _repo.Movies
                .FirstOrDefaultAsync(m => m.Id == ticketViewModel.Show.MovieId);

            ViewBag.Price = string.Format("{0:0.00}", ticketViewModel.Ticket.Price);

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

            TicketsViewModel ticketViewModel = new TicketsViewModel(_repo);

            ticketViewModel.Shows = _repo.Shows.ToList();
            ticketViewModel.Show = _repo.Shows.FirstOrDefault(x => x.Id == id);
            ticketViewModel.Movie = _repo.Movies.FirstOrDefault(x => x.Id == ticketViewModel.Show.MovieId);
            ticketViewModel.Seat = ticketViewModel.GetSeatNumber(currentShow);
            ticketViewModel.Rates = _repo.Rates.ToList();

            return View(ticketViewModel);
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShowId,Name,Code,Price,SeatRowId,SeatNumber,UserId,VerkoperId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var rateId = int.Parse(ticket.Name);
                var rate = _repo.Rates.FirstOrDefault(r => r.Id == rateId);

                ticket.Name = rate.Name;
                ticket.Price = rate.Price;
                ticket.Code = TicketHelper.GenerateRandomCode();

                var shows = _repo.Shows.Where(s => s.Id == ticket.ShowId)
                            .Include(s => s.Movie);

                if (shows.FirstOrDefault().Movie.Length > 120)
                {
                    ticket.Price += .50;
                }

                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = ticket.Id });
            }

            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);

            var shows = _repo.Shows.Where(s => s.Id == ticket.ShowId)
                .Include(s => s.Theater);

            var theaterid = shows.FirstOrDefault().TheaterId;

            ViewBag.SeatRows = new SelectList(_context.SeatRows.Where(sr => (sr.TheaterId == theaterid)), "Id", "RowNumber", ticket.SeatRowId);

            var seats = _context.SeatRows.FindAsync(ticket.SeatRowId).Result.Seats;
            var availableSeats = new List<int>();

            for (int i = 1; i < seats + 1; i++)
            {
                if (!_context.Tickets.Where(t => t.SeatRowId == ticket.SeatRowId && t.ShowId == ticket.ShowId && t.SeatNumber == i).Any())
                {
                    availableSeats.Add(i);
                }
            }

            ViewBag.Seats = new SelectList(availableSeats);

            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        public JsonResult GetSeats(int seatRowid, int showid)
        {
            var seats = _context.SeatRows.FindAsync(seatRowid).Result.Seats;
            var availableSeats = new List<int>();

            for (int i = 1; i < seats + 1; i++)
            {
                if (!_context.Tickets.Where(t => t.SeatRowId == seatRowid && t.ShowId == showid && t.SeatNumber == i).Any())
                {
                    availableSeats.Add(i);
                }
            }

            return Json(new SelectList(availableSeats));
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SeatRowId,Name,Code,Price,SeatNumber")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            var t = _context.Tickets.AsNoTracking().Where(t => t.Id == id).FirstOrDefault();

            ticket.ShowId = t.ShowId;
            ticket.UserId = t.UserId;
            ticket.Price = t.Price;

            if (User.IsInRole("Cashier"))
            {
                ticket.VerkoperId = User.Identity.Name;
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
    }
}
