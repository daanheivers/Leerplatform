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
    public class LokalenController : Controller
    {
        private readonly LeerplatformDbContext _context;

        public LokalenController(LeerplatformDbContext context)
        {
            _context = context;
        }

        // GET: Lokalen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lokalen.ToListAsync());
        }

        // GET: Lokalen/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokaal = await _context.Lokalen
                .Include(m => m.Middelen).FirstOrDefaultAsync(m => m.LokaalId == id);
            if (lokaal == null)
            {
                return NotFound();
            }
            System.Diagnostics.Debug.WriteLine(lokaal);
            return View(lokaal);
        }

        // GET: Lokalen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lokalen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LokaalId,Naam,Plaats,Capaciteit")] Lokaal lokaal, List<int> middelen)
        {

            var allMiddelen = await _context.Middelen.ToListAsync();
            foreach (Middel middel in allMiddelen)
            {
                if (middelen.Contains(middel.MiddelId))
                {
                    if(lokaal.Middelen == null)
                    {
                        lokaal.Middelen = new List<Middel>();
                    }
                    lokaal.Middelen.Add(middel);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(lokaal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lokaal);
        }

        // GET: Lokalen/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokaal = await _context.Lokalen.FindAsync(id);
            if (lokaal == null)
            {
                return NotFound();
            }
            return View(lokaal);
        }

        // POST: Lokalen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LokaalId,Naam,Plaats,Capaciteit")] Lokaal lokaal)
        {
            if (id != lokaal.LokaalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lokaal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LokaalExists(lokaal.LokaalId))
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
            return View(lokaal);
        }

        // GET: Lokalen/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokaal = await _context.Lokalen
                .FirstOrDefaultAsync(m => m.LokaalId == id);
            if (lokaal == null)
            {
                return NotFound();
            }

            return View(lokaal);
        }

        // POST: Lokalen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var lokaal = await _context.Lokalen.FindAsync(id);
            _context.Lokalen.Remove(lokaal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LokaalExists(string id)
        {
            return _context.Lokalen.Any(e => e.LokaalId == id);
        }
    }
}
