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
    public class tstudents2Controller : Controller
    {
        //private readonly dbstudentsContext _context;

        //public tstudents2Controller(dbstudentsContext context)
        //{
        //    _context = context;
        //}
        dbstudentsContext _context = new dbstudentsContext();

        // GET: tstudents2
        public async Task<IActionResult> Index()
        {
            var dbstudentsContext = _context.tstudent2.Include(t => t.Dept);
            return View(await dbstudentsContext.ToListAsync());
        }

        // GET: tstudents2/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tstudent2 = await _context.tstudent2
                .Include(t => t.Dept)
                .FirstOrDefaultAsync(m => m.fStuId == id);
            if (tstudent2 == null)
            {
                return NotFound();
            }

            return View(tstudent2);
        }

        // GET: tstudents2/Create
        public IActionResult Create()
        {
            ViewData["DeptID"] = new SelectList(_context.department, "DeptID", "DeptID");
            return View();
        }

        // POST: tstudents2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("fStuId,fName,fEmail,fScore,DeptID")] tstudent2 tstudent2)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tstudent2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptID"] = new SelectList(_context.department, "DeptID", "DeptID", tstudent2.DeptID);
            return View(tstudent2);
        }

        // GET: tstudents2/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tstudent2 = await _context.tstudent2.FindAsync(id);
            if (tstudent2 == null)
            {
                return NotFound();
            }
            ViewData["DeptID"] = new SelectList(_context.department, "DeptID", "DeptID", tstudent2.DeptID);
            return View(tstudent2);
        }

        // POST: tstudents2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("fStuId,fName,fEmail,fScore,DeptID")] tstudent2 tstudent2)
        {
            if (id != tstudent2.fStuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tstudent2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tstudent2Exists(tstudent2.fStuId))
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
            ViewData["DeptID"] = new SelectList(_context.department, "DeptID", "DeptID", tstudent2.DeptID);
            return View(tstudent2);
        }

        // GET: tstudents2/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tstudent2 = await _context.tstudent2
                .Include(t => t.Dept)
                .FirstOrDefaultAsync(m => m.fStuId == id);
            if (tstudent2 == null)
            {
                return NotFound();
            }

            return View(tstudent2);
        }

        // POST: tstudents2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tstudent2 = await _context.tstudent2.FindAsync(id);
            if (tstudent2 != null)
            {
                _context.tstudent2.Remove(tstudent2);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tstudent2Exists(string id)
        {
            return _context.tstudent2.Any(e => e.fStuId == id);
        }
    }
}
