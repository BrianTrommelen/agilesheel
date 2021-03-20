using agilesheel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using agilesheel.Helpers;
using agilesheel.ViewModels;

namespace agilesheel.Controllers
{
    [Authorize(Roles = "Basic")]
    public class WebsiteTicketsController : Controller
    {
        private readonly StoreDbContext _context;
        private IStoreRepository _repo;
        private double NormalPrice = 9.00;

        public WebsiteTicketsController(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var tickets = await _context.Tickets.Where(t => t.UserId == User.GetUserId()).ToListAsync();
            return View(tickets);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TicketsViewModel ticketViewModel = new TicketsViewModel()
            {
                Ticket = await _context.Tickets
                .Include(t => t.Show)
                .Include(t => t.Show.Movie)
                .Include(t => t.Show.Theater)
                .Include(t => t.Show.Theater.Cinema)
                .FirstOrDefaultAsync(t => t.Id == id),
            };

            ViewBag.Price = String.Format("{0:0.00}", (ticketViewModel.Ticket.Price));

            return View(ticketViewModel);
        }

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

            if (ticketViewModel.Movie.Length <= 120)
            {
                NormalPrice = 8.50;
            }

            ViewBag.NormalPrice = String.Format("{0:0.00}", NormalPrice);

            if (ticketViewModel.IsShowInTimeSpan(TimeSpan.FromHours(6), TimeSpan.FromHours(18)))
            {
                ViewBag.ChildrenPrice = String.Format("{0:0.00}", (NormalPrice - 1.50));
            }
            else
            {
                ViewBag.ChildrenPrice = ViewBag.NormalPrice;
            }

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
