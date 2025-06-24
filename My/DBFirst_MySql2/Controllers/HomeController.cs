using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBFirst_MySql2.Models;
using DBFirst_MySql2.ViewModels;

namespace DBFirst_MySql2.Controllers
{
    public class HomeController : Controller
    {
        private readonly dbstudentsContext _context;

        public HomeController(dbstudentsContext context)
        {
            _context = context;
        }

        // GET: Home
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 學生資料管理
        /// </summary>
        /// <param name="DeptID">科系ID<para>未帶入值時顯示全部科系</para></param>
        /// <returns></returns>
        public IActionResult Students(string DeptID = "")
        {
            var VM = new VM_tStudent2_Department()
            {
                Departments = _context.department.ToList(),
                Students = string.IsNullOrEmpty(DeptID) ? _context.tstudent2.ToList() : _context.tstudent2.Where(S => S.DeptID == DeptID).ToList()
            };

            return View(VM);
        }

        // GET: Home/Details/5
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

        // GET: Home/Create
        public IActionResult Create()
        {
            ViewData["DeptID"] = new SelectList(_context.department, "DeptID", "DeptID");
            return View();
        }

        // POST: Home/Create
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

        // GET: Home/Edit/5
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

        // POST: Home/Edit/5
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

        // GET: Home/Delete/5
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

        // POST: Home/Delete/5
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
