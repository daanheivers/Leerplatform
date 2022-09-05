using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Leerplatform.Models;
using Microsoft.AspNetCore.Authorization;

namespace Leerplatform.Controllers
{
    public class InschrijvingenController : Controller
    {
        private readonly LeerplatformDbContext _context;

        public InschrijvingenController(LeerplatformDbContext context)
        {
            _context = context;
        }

        // GET: Inschrijvingen
        [Authorize(Roles = "Admin, Docent, Student")]
        public async Task<IActionResult> Index(string username)
        {
            if(username == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            var userRole = await _context.UserRoles.FirstOrDefaultAsync(u => u.UserId == user.Id);

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == userRole.RoleId);

            if (role.Name.Equals("Docent") || role.Name.Equals("Admin"))
            {
                return View(await _context.Inschrijvingen.Include(i => i.User).Include(i => i.Vak).ToListAsync());
            }

            return View(await _context.Inschrijvingen.Include(i => i.User).Include(i => i.Vak).Where(i => i.User == user).ToListAsync());
        }

        [Authorize(Roles = "Admin, Docent, Student")]
        public async Task<IActionResult> Ingepland(string username)
        {
            if (username == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(i => i.UserName == username);

            var aanvaardeVakken = await _context.Inschrijvingen.Include(i => i.User).Include(i => i.Vak).Where(i => i.Aanvaard && i.User.UserName == username).ToListAsync();

            var planningen = _context.Planningen.Include(i => i.Studenten).Where(i => i.Studenten.Contains(user)).ToList();

            var lessen = new List<Les>();

            foreach (Planning planning in planningen)
            {
                foreach (Les les in planning.Lessen)
                {
                    foreach (Inschrijving inschrijving in aanvaardeVakken)
                    {
                        if (les.VakId.Equals(inschrijving.VakId))
                        {
                            lessen.Add(les);
                        }
                    }
                }
            }

            lessen.Distinct().OrderBy(les => les.Tijdstip).Reverse();

            return View(lessen);
        }

        // GET: Inschrijvingen/Details/5
        [Authorize(Roles = "Admin, Docent, Student")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inschrijving = await _context.Inschrijvingen
                .Include(i => i.User)
                .Include(i => i.Vak)
                .FirstOrDefaultAsync(m => m.InschrijvingId == id);
            if (inschrijving == null)
            {
                return NotFound();
            }

            return View(inschrijving);
        }

        [Authorize(Roles = "Admin, Docent, Student")]
        public async Task<IActionResult> Create(string username, string vakId)
        {
            Inschrijving inschrijving = new Inschrijving();
            if (username == null)
            {
                return NotFound();
            }
            if (vakId == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserName == username);
            var vak = await _context.Vakken.FirstOrDefaultAsync(m => m.VakId == vakId);

            inschrijving.User = user;
            inschrijving.Vak = vak;
            inschrijving.Aanvaard = false;

            _context.Add(inschrijving);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { username = username });
        }

        [Authorize(Roles = "Admin, Docent, Student")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inschrijving = await _context.Inschrijvingen
                .Include(v => v.Vak)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.InschrijvingId == id);
            if (inschrijving == null)
            {
                return NotFound();
            }

            return View(inschrijving);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Docent, Student")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inschrijving = await _context.Inschrijvingen.Include(u => u.User).FirstOrDefaultAsync(m => m.InschrijvingId == id);
            var user = inschrijving.User;
            _context.Inschrijvingen.Remove(inschrijving);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { user.UserName });
        }

        [Authorize(Roles = "Admin, Docent")]
        public async Task<IActionResult> SwapStatus(int? id, string username)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (username == null)
            {
                return NotFound();
            }

            var inschrijving = await _context.Inschrijvingen
                .Include(v => v.Vak)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.InschrijvingId == id);

            if (inschrijving == null)
            {
                return NotFound();
            }

            inschrijving.Aanvaard = !inschrijving.Aanvaard;
            _context.Update(inschrijving);
            await _context.SaveChangesAsync();

            if (inschrijving.Aanvaard)
            {
                return RedirectToAction(nameof(Planning), new { username });
            }

            return RedirectToAction(nameof(Index), new { username });
        }

        [Authorize(Roles = "Admin, Docent")]
        public async Task<IActionResult> Planning(string username)
        {
            if (username == null)
            {
                return NotFound();
            }

            var student = await _context.Users.FirstOrDefaultAsync(i => i.UserName == username);
            var planningen = await _context.Planningen.ToListAsync();
            PlanningStudentVM planningVM = new PlanningStudentVM();
            planningVM.Planningen = new List<Planning>();
            planningVM.Student = student;
            foreach (Planning planning in planningen)
            {
                planningVM.Planningen.Add(planning);
            }
            return View(planningVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Docent")]
        public async Task<IActionResult> Planning(string id, int planning, string username)
        {
            var student = await _context.Users.FirstOrDefaultAsync(i => i.Id == id);
            var plan = await _context.Planningen.Include(l => l.Lessen).FirstOrDefaultAsync(l => l.PlanningId == planning);
            if (string.IsNullOrEmpty(id))
            {
                ModelState.AddModelError(nameof(id), "Student verplicht");
            }
            if (ModelState.IsValid)
            {
                if(plan.Studenten == null)
                {
                    plan.Studenten = new List<User>();
                }
                plan.Studenten.Add(student);
                try
                {
                    _context.Update(plan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index), new { username });
            }
            var planningen = await _context.Planningen.ToListAsync();
            var lokalen = await _context.Lokalen.ToListAsync();
            PlanningStudentVM planningVM = new PlanningStudentVM();
            planningVM.Planningen = new List<Planning>();
            planningVM.Student = student;
            foreach (Planning p in planningen)
            {
                planningVM.Planningen.Add(p);
            }
            return View(planningVM);
        }

        private bool InschrijvingExists(int id)
        {
            return _context.Inschrijvingen.Any(e => e.InschrijvingId == id);
        }
    }
}
