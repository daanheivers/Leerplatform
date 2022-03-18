using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Leerplatform.Models;

namespace Leerplatform.Controllers
{
    public class MiddelsController : Controller
    {
        private readonly LeerplatformDbContext _context;

        public MiddelsController(LeerplatformDbContext context)
        {
            _context = context;
        }

        // GET: Middels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Middelen.ToListAsync());
        }

        // GET: Middels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var middel = await _context.Middelen
                .FirstOrDefaultAsync(m => m.MiddelId == id);
            if (middel == null)
            {
                return NotFound();
            }

            return View(middel);
        }

        // GET: Middels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Middels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MiddelId,Naam")] Middel middel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(middel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(middel);
        }

        // GET: Middels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var middel = await _context.Middelen.FindAsync(id);
            if (middel == null)
            {
                return NotFound();
            }
            return View(middel);
        }

        // POST: Middels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MiddelId,Naam")] Middel middel)
        {
            if (id != middel.MiddelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(middel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MiddelExists(middel.MiddelId))
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
            return View(middel);
        }

        // GET: Middels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var middel = await _context.Middelen
                .FirstOrDefaultAsync(m => m.MiddelId == id);
            if (middel == null)
            {
                return NotFound();
            }

            return View(middel);
        }

        // POST: Middels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var middel = await _context.Middelen.FindAsync(id);
            _context.Middelen.Remove(middel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MiddelExists(int id)
        {
            return _context.Middelen.Any(e => e.MiddelId == id);
        }
    }
}
