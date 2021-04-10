using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using agilesheel.Models;
using Microsoft.AspNetCore.Authorization;

namespace agilesheel.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class ShowsController : Controller
    {
        private readonly StoreDbContext _context;

        public ShowsController(StoreDbContext context)
        {
            _context = context;
        }

        // GET: Shows/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id");
            ViewData["TheaterId"] = new SelectList(_context.Theaters, "Id", "Id");
            return View();
        }

        // POST: Shows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,TheaterId,StartTime,Duration")] Show show)
        {
            _context.Add(show);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create");
        }
    }
}
