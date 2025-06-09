using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBFirst_Test.Models;

namespace DBFirst_Test.Controllers
{
    public class tstudentsController : Controller
    {
        private readonly dbstudentsContext _context;

        public tstudentsController(dbstudentsContext context)
        {
            _context = context;
        }

        // GET: tstudents
        public async Task<IActionResult> Index()
        {
            return View(await _context.tstudent.ToListAsync());
        }

        // GET: tstudents/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tstudent = await _context.tstudent
                .FirstOrDefaultAsync(m => m.fStuId == id);
            if (tstudent == null)
            {
                return NotFound();
            }

            return View(tstudent);
        }

        // GET: tstudents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: tstudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("fStuId,fName,fEmail,fScore")] tstudent tstudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tstudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tstudent);
        }

        // GET: tstudents/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tstudent = await _context.tstudent.FindAsync(id);
            if (tstudent == null)
            {
                return NotFound();
            }
            return View(tstudent);
        }

        // POST: tstudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("fStuId,fName,fEmail,fScore")] tstudent tstudent)
        {
            if (id != tstudent.fStuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tstudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tstudentExists(tstudent.fStuId))
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
            return View(tstudent);
        }

        // GET: tstudents/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tstudent = await _context.tstudent
                .FirstOrDefaultAsync(m => m.fStuId == id);
            if (tstudent == null)
            {
                return NotFound();
            }

            return View(tstudent);
        }

        // POST: tstudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tstudent = await _context.tstudent.FindAsync(id);
            if (tstudent != null)
            {
                _context.tstudent.Remove(tstudent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tstudentExists(string id)
        {
            return _context.tstudent.Any(e => e.fStuId == id);
        }
    }
}
