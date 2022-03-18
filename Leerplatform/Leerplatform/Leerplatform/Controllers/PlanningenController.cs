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
    public class PlanningenController : Controller
    {
        private readonly LeerplatformDbContext _context;

        public PlanningenController(LeerplatformDbContext context)
        {
            _context = context;
        }

        // GET: Planningen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Planningen.ToListAsync());
        }

        // GET: Planningen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planning = await _context.Planningen
                .Include(m => m.Lessen).FirstOrDefaultAsync(m => m.PlanningId == id);
            if (planning == null)
            {
                return NotFound();
            }

            return View(planning);
        }

        // GET: Planningen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Planningen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanningId, Naam")] Planning planning)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planning);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(planning);
        }

        // GET: Planningen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planning = await _context.Planningen.FindAsync(id);
            if (planning == null)
            {
                return NotFound();
            }
            return View(planning);
        }

        // POST: Planningen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanningId")] Planning planning)
        {
            if (id != planning.PlanningId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planning);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanningExists(planning.PlanningId))
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
            return View(planning);
        }

        // GET: Planningen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planning = await _context.Planningen
                .FirstOrDefaultAsync(m => m.PlanningId == id);
            if (planning == null)
            {
                return NotFound();
            }

            return View(planning);
        }

        // POST: Planningen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planning = await _context.Planningen.FindAsync(id);
            _context.Planningen.Remove(planning);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanningExists(int id)
        {
            return _context.Planningen.Any(e => e.PlanningId == id);
        }
    }
}
