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
        public IActionResult Create()
        {
            // TODO: get showId from URL
            int currentShow = 1;

            TicketsViewModel ticketViewModel = new TicketsViewModel(_repo);

            ticketViewModel.Shows =  _repo.Shows.ToList();

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
                return RedirectToAction(nameof(Index));
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
    }
}
