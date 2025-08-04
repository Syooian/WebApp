using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelSystem.Access.Data;
using HotelSystem.Models;

namespace HotelSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("ER/{action=Index}")]
    public class EmployeeRolesController : Controller
    {
        private readonly HotelSysDBContext2 _context;

        public EmployeeRolesController(HotelSysDBContext2 context)
        {
            _context = context;
        }

        [Route("/aaa")]
        public async Task<IActionResult> Index()
        {
            _context.GetRoomServiceCount();
            return View(await _context.EmployeeRole.ToListAsync());
        }

        [Route("/Guest/{ERName}-{id}.html")]
        public async Task<IActionResult> Details(string id, string ERName)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeRole = await _context.EmployeeRole
                .FirstOrDefaultAsync(m => m.RoleCode == id);
            if (employeeRole == null)
            {
                return NotFound();
            }

            return View(employeeRole);
        }

        // GET: EmployeeRoles/Create
        public IActionResult Create()
        {
            
            return View();
        }

        public IActionResult GetSomething()
        {
           
            return View(_context.CallTest222Async());
        }

        // POST: EmployeeRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleCode,RoleName")] EmployeeRole employeeRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeRole);
        }

        // GET: EmployeeRoles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeRole = await _context.EmployeeRole.FindAsync(id);
            if (employeeRole == null)
            {
                return NotFound();
            }
            return View(employeeRole);
        }

        // POST: EmployeeRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RoleCode,RoleName")] EmployeeRole employeeRole)
        {
            if (id != employeeRole.RoleCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeRoleExists(employeeRole.RoleCode))
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
            return View(employeeRole);
        }

        // GET: EmployeeRoles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeRole = await _context.EmployeeRole
                .FirstOrDefaultAsync(m => m.RoleCode == id);
            if (employeeRole == null)
            {
                return NotFound();
            }

            return View(employeeRole);
        }

        // POST: EmployeeRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employeeRole = await _context.EmployeeRole.FindAsync(id);
            if (employeeRole != null)
            {
                _context.EmployeeRole.Remove(employeeRole);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeRoleExists(string id)
        {
            return _context.EmployeeRole.Any(e => e.RoleCode == id);
        }
    }
}
