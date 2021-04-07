using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using agilesheel.Models;
using agilesheel.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace agilesheel.Controllers
{
    public class LostAndFoundController : Controller
    {
        private readonly StoreDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public LostAndFoundController(StoreDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: LostAndFound
        public async Task<IActionResult> Index()
        {
            var objects = new List<LostAndFound>();

            if (User.IsInRole("Manager"))
            {
                objects = await _context.LostAndFound.Include(o => o.User).ToListAsync();
            }
            else
            {
                objects = await _context.LostAndFound
                    .Where(o => o.UserId == User.GetUserId())
                    .ToListAsync();
            }

            return View(objects);
        }

        // GET: LostAndFound/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lostAndFound = await _context.LostAndFound
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lostAndFound == null)
            {
                return NotFound();
            }

            return View(lostAndFound);
        }

        // GET: LostAndFound/Create
        [Authorize(Roles = "Basic")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: LostAndFound/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Basic")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,IsFound,UserId")] LostAndFound lostAndFound)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lostAndFound);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lostAndFound);
        }

        // GET: LostAndFound/Edit/5
        [Authorize(Roles = "Basic, Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lostAndFound = await _context.LostAndFound.FindAsync(id);
            if (lostAndFound == null)
            {
                return NotFound();
            }
            return View(lostAndFound);
        }

        // POST: LostAndFound/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Basic, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,IsFound,UserId")] LostAndFound lostAndFound)
        {
            if (id != lostAndFound.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lostAndFound);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LostAndFoundExists(lostAndFound.Id))
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
            return View(lostAndFound);
        }

        // GET: LostAndFound/Delete/5
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lostAndFound = await _context.LostAndFound
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lostAndFound == null)
            {
                return NotFound();
            }

            return View(lostAndFound);
        }

        // POST: LostAndFound/Delete/5
        [Authorize(Roles = "Basic")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lostAndFound = await _context.LostAndFound.FindAsync(id);
            _context.LostAndFound.Remove(lostAndFound);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LostAndFoundExists(int id)
        {
            return _context.LostAndFound.Any(e => e.Id == id);
        }
    }
}
