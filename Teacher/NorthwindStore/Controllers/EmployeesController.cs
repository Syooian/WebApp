using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthwindStore.Models;

namespace NorthwindStore.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly NorthwindContext _context;

        public EmployeesController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var northwindContext = _context.Employees.Include(e => e.ReportsToNavigation);
            return View(await northwindContext.ToListAsync());
        }


        public async Task<IActionResult> GetPhoto(int id)
        {
            //取得某個商品分類的圖片
            var emp = await _context.Employees.FindAsync(id);

            if (emp == null)
            {
                return NotFound(); //如果找不到分類或圖片長度小於78，則回傳NotFound
            }


            var photoBytes = emp.Photo.Skip(78).ToArray();//跳過前78個byte，取得圖片的實際內容後，轉成byte[]陣列  

            return File(photoBytes, "image/bmp");
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(e => e.ReportsToNavigation)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["ReportsTo"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeID");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeID,LastName,FirstName,Title,TitleOfCourtesy,BirthDate,HireDate,Address,PostalCode,HomePhone,Extension,Photo,Notes,ReportsTo")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReportsTo"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeID", employees.ReportsTo);
            return View(employees);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }
            ViewData["ReportsTo"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeID", employees.ReportsTo);
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeID,LastName,FirstName,Title,TitleOfCourtesy,BirthDate,HireDate,Address,PostalCode,HomePhone,Extension,Photo,Notes,ReportsTo")] Employees employees)
        {
            if (id != employees.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(employees.EmployeeID))
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
            ViewData["ReportsTo"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeID", employees.ReportsTo);
            return View(employees);
        }

      

        private bool EmployeesExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeID == id);
        }
    }
}
