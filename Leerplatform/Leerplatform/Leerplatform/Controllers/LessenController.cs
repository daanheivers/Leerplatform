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
    public class LessenController : Controller
    {
        private readonly LeerplatformDbContext _context;

        public LessenController(LeerplatformDbContext context)
        {
            _context = context;
        }

        // GET: Lessen
        public async Task<IActionResult> Index()
        {
            var leerplatformDbContext = _context.Lessen.Include(l => l.Lokaal).Include(l => l.Planning).Include(l => l.Vak);
            return View(await leerplatformDbContext.ToListAsync());
        }

        // GET: Lessen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var les = await _context.Lessen
                .Include(l => l.Lokaal)
                .Include(l => l.Planning)
                .Include(l => l.Vak)
                .FirstOrDefaultAsync(m => m.LesId == id);
            if (les == null)
            {
                return NotFound();
            }

            return View(les);
        }

        // GET: Lessen/Create
        public IActionResult Create()
        {
            ViewData["LokaalId"] = new SelectList(_context.Lokalen, "LokaalId", "LokaalId");
            ViewData["PlanningId"] = new SelectList(_context.Planningen, "PlanningId", "PlanningId");
            ViewData["VakId"] = new SelectList(_context.Vakken, "VakId", "VakId");
            return View();
        }

        // POST: Lessen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LesId,Tijdstip,VakId,LokaalId,PlanningId")] Les les)
        {
            if (ModelState.IsValid)
            {
                _context.Add(les);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LokaalId"] = new SelectList(_context.Lokalen, "LokaalId", "LokaalId", les.LokaalId);
            ViewData["PlanningId"] = new SelectList(_context.Planningen, "PlanningId", "PlanningId", les.PlanningId);
            ViewData["VakId"] = new SelectList(_context.Vakken, "VakId", "VakId", les.VakId);
            return View(les);
        }

        // GET: Lessen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var les = await _context.Lessen.FindAsync(id);
            if (les == null)
            {
                return NotFound();
            }
            ViewData["LokaalId"] = new SelectList(_context.Lokalen, "LokaalId", "LokaalId", les.LokaalId);
            ViewData["PlanningId"] = new SelectList(_context.Planningen, "PlanningId", "PlanningId", les.PlanningId);
            ViewData["VakId"] = new SelectList(_context.Vakken, "VakId", "VakId", les.VakId);
            return View(les);
        }

        // POST: Lessen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LesId,Tijdstip,VakId,LokaalId,PlanningId")] Les les)
        {
            if (id != les.LesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(les);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LesExists(les.LesId))
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
            ViewData["LokaalId"] = new SelectList(_context.Lokalen, "LokaalId", "LokaalId", les.LokaalId);
            ViewData["PlanningId"] = new SelectList(_context.Planningen, "PlanningId", "PlanningId", les.PlanningId);
            ViewData["VakId"] = new SelectList(_context.Vakken, "VakId", "VakId", les.VakId);
            return View(les);
        }

        // GET: Lessen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var les = await _context.Lessen
                .Include(l => l.Lokaal)
                .Include(l => l.Planning)
                .Include(l => l.Vak)
                .FirstOrDefaultAsync(m => m.LesId == id);
            if (les == null)
            {
                return NotFound();
            }

            return View(les);
        }

        // POST: Lessen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var les = await _context.Lessen.FindAsync(id);
            _context.Lessen.Remove(les);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LesExists(int id)
        {
            return _context.Lessen.Any(e => e.LesId == id);
        }
    }
}
