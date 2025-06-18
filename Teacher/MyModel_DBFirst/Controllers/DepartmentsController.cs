using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyModel_DBFirst.Models;

namespace MyModel_DBFirst.Controllers
{
    public class DepartmentsController : Controller
    {
        //private readonly dbStudentsContext _context;

        //public DepartmentsController(dbStudentsContext context)
        //{
        //    _context = context;
        //}


        //5.6.4 參考2 .2.1修改建立DbContext物件的程式
        dbStudentsContext _context = new dbStudentsContext(); //直接new物件的寫法

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Department.ToListAsync());
        }

       
        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeptID,DeptName")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

       
        private bool DepartmentExists(string id)
        {
            return _context.Department.Any(e => e.DeptID == id);
        }
    }
}
