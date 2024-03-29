﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Leerplatform.Models;
using Microsoft.AspNetCore.Authorization;

namespace Leerplatform.Controllers
{
    public class VaksController : Controller
    {
        private readonly LeerplatformDbContext _context;

        public VaksController(LeerplatformDbContext context)
        {
            _context = context;
        }

        // GET: Vaks
        [Authorize(Roles = "Admin, Docent, Student")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vakken.ToListAsync());
        }

        // GET: Vaks/Details/5
        [Authorize(Roles = "Admin, Docent, Student")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vak = await _context.Vakken
                .FirstOrDefaultAsync(m => m.VakId == id);
            if (vak == null)
            {
                return NotFound();
            }

            return View(vak);
        }

        // GET: Vaks/Create
        [Authorize(Roles = "Admin, Docent")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vaks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Docent")]
        public async Task<IActionResult> Create([Bind("VakId,Titel,Studiepunten")] Vak vak)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vak);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vak);
        }

        // GET: Vaks/Edit/5
        [Authorize(Roles = "Admin, Docent")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vak = await _context.Vakken.FindAsync(id);
            if (vak == null)
            {
                return NotFound();
            }
            return View(vak);
        }

        // POST: Vaks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Docent")]
        public async Task<IActionResult> Edit(string id, [Bind("VakId,Titel,Studiepunten")] Vak vak)
        {
            if (id != vak.VakId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vak);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VakExists(vak.VakId))
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
            return View(vak);
        }

        // GET: Vaks/Delete/5
        [Authorize(Roles = "Admin, Docent")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vak = await _context.Vakken
                .FirstOrDefaultAsync(m => m.VakId == id);
            if (vak == null)
            {
                return NotFound();
            }

            return View(vak);
        }

        // POST: Vaks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Docent")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vak = await _context.Vakken.FindAsync(id);
            _context.Vakken.Remove(vak);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Vaks/Planning/5
        [Authorize(Roles = "Admin, Docent")]
        public async Task<IActionResult> Planning(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vak = await _context.Vakken.FindAsync(id);
            var planningen = await _context.Planningen.ToListAsync();
            var lokalen = await _context.Lokalen.ToListAsync();
            VakPlanningLokaalVM vakVM = new VakPlanningLokaalVM();
            if (vak == null)
            {
                return NotFound();
            }
            vakVM.Vak = vak;
            vakVM.Planningen = new List<Planning>();
            vakVM.Lokalen = new List<Lokaal>();
            foreach (Planning planning in planningen)
            {
                vakVM.Planningen.Add(planning);
            }
            foreach (Lokaal lokaal in lokalen)
            {
                vakVM.Lokalen.Add(lokaal);
            }
            return View(vakVM);
        }

        // POST: Vaks/Planning/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Docent")]
        public async Task<IActionResult> Planning(string id, int planning, string lokaal, DateTime tijdstip)
        {
            var vak = await _context.Vakken.FindAsync(id);
            var plan = await _context.Planningen.Include(l => l.Lessen).FirstOrDefaultAsync(l => l.PlanningId == planning);
            var lok = await _context.Lokalen.FindAsync(lokaal);
            if (string.IsNullOrEmpty(id))
            {
                ModelState.AddModelError(nameof(id), "Vakcode verplicht");
            }
            if (string.IsNullOrEmpty(lokaal))
            {
                ModelState.AddModelError(nameof(lokaal), "Lokaalnummer verplicht");
            }
            var inGebruik = await _context.Lessen.Where(l => l.LokaalId == lokaal).FirstOrDefaultAsync();
            if (inGebruik != null)
            {
                ModelState.AddModelError(nameof(lokaal), "Dit lokaal is reeds in gebruik");
            }
            if (ModelState.IsValid)
            {
                Les les = new Les();
                les.Vak = vak;
                les.Lokaal = lok;
                les.Planning = plan;
                les.Tijdstip = tijdstip;
                try
                {
                    _context.Add(les);
                    plan.Lessen.Add(les);
                    _context.Update(plan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VakExists(vak.VakId))
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
            var planningen = await _context.Planningen.ToListAsync();
            var lokalen = await _context.Lokalen.ToListAsync();
            VakPlanningLokaalVM vakVM = new VakPlanningLokaalVM();
            vakVM.Vak = vak;
            vakVM.Planningen = new List<Planning>();
            vakVM.Lokalen = new List<Lokaal>();
            foreach (Planning p in planningen)
            {
                vakVM.Planningen.Add(p);
            }
            foreach (Lokaal l in lokalen)
            {
                vakVM.Lokalen.Add(l);
            }
            return View(vakVM);
        }

        private bool VakExists(string id)
        {
            return _context.Vakken.Any(e => e.VakId == id);
        }
    }
}
