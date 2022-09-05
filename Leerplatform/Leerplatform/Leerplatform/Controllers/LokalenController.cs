using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Leerplatform.Models;
using Microsoft.AspNetCore.Authorization;
using Leerplatform.Services;

namespace Leerplatform.Controllers
{
    public class LokalenController : Controller
    {
        private readonly LokalenService _service;

        public LokalenController(LokalenService service)
        {
            _service = service;
        }

        // GET: Lokalen
        [Authorize(Roles = "Admin, Docent, Student")]
        public async Task<IActionResult> Index()
        {
            return View(_service.GetLokalen()) ;
        }

        // GET: Lokalen/Details/5
        [Authorize(Roles = "Admin, Docent, Student")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokaal = _service.GetLokaalById(id);
            if (lokaal == null)
            {
                return NotFound();
            }
            return View(lokaal);
        }

        // GET: Lokalen/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            Lokaal lokaal = new Lokaal();
            lokaal.Middelen = new List<Middel>();
            var middelen = await _service.GetMiddelen();
            foreach (Middel middel in middelen)
            {
                    lokaal.Middelen.Add(middel);
            }
            return View(lokaal);
        }

        // POST: Lokalen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("LokaalId,Naam,Plaats,Capaciteit")] Lokaal lokaal, List<int> middelen)
        {

            var allMiddelen = await _service.GetMiddelen();
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
                await _service.AddLokaal(lokaal);
                return RedirectToAction(nameof(Index));
            }
            return View(lokaal);
        }

        // GET: Lokalen/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokaal = _service.GetLokaalById(id);
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
        [Authorize(Roles = "Admin")]
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
                    _service.UpdateLokaal(lokaal);
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokaal = await _service.GetLokaalById(id);
            if (lokaal == null)
            {
                return NotFound();
            }

            return View(lokaal);
        }

        // POST: Lokalen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var lokaal = await _service.GetLokaalById(id);
            await _service.DeleteLokaal(lokaal);
            return RedirectToAction(nameof(Index));
        }

        private bool LokaalExists(string id)
        {
            return _service.LokaalExists(id);
        }
    }
}
