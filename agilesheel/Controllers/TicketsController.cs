﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using agilesheel.Models;
using agilesheel.ViewModels;
using agilesheel.Helpers;

namespace agilesheel.Controllers
{
    public class TicketsController : Controller
    {
        private readonly StoreDbContext _context;
        private double NormalPrice = 9.00;

        private IStoreRepository _repo;

        public TicketsController(StoreDbContext context, IStoreRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Tickets
                .Where(t => t.UserId == User.GetUserId())
                .Include(t => t.Show.Movie)
                .ToListAsync();
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

            ViewBag.Price = String.Format("{0:0.00}", (ticketViewModel.Ticket.Price));

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

            // Set the normal price if movie is lower than 120 minutes
            if (ticketViewModel.Movie.Length <= 120)
            {
                NormalPrice = 8.50;
            }

            ViewBag.NormalPrice = String.Format("{0:0.00}", NormalPrice);

            // Discount for children if time is between 6AM and 6PM
            if (ticketViewModel.IsShowInTimeSpan(TimeSpan.FromHours(6), TimeSpan.FromHours(18)))
            {
                ViewBag.ChildrenPrice = String.Format("{0:0.00}", (NormalPrice - 1.50));
            }
            else
            {
                ViewBag.ChildrenPrice = ViewBag.NormalPrice;
            }

            // Discount for student if day is not friday, saturday or sunday
            if (ticketViewModel.IsShowInWeekDay(DateTime.Now))
            {
                ViewBag.StudentPrice = String.Format("{0:0.00}", (NormalPrice - 1.50));
            }
            else
            {
                ViewBag.StudentPrice = ViewBag.NormalPrice;
            }

            ViewBag.ElderyPrice = String.Format("{0:0.00}", (NormalPrice - 1.50));

            return View(ticketViewModel);
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShowId,Name,Code,Price,SeatRowId,SeatNumber,UserId")] Ticket ticket)
        {

            if (ModelState.IsValid)
            {
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
