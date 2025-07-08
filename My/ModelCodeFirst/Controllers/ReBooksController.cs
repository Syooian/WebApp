using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModelCodeFirst.Models;

namespace ModelCodeFirst.Controllers
{
    public class ReBooksController : Controller
    {
        private readonly GuestBookContext _context;

        public ReBooksController(GuestBookContext context)
        {
            _context = context;
        }

        // GET: ReBooks/Create
        public IActionResult Create()
        {
            ViewData["ID"] = new SelectList(_context.Book, "ID", "ID");
            return View();
        }

        // POST: ReBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReID,Description,Author,CreatedDate,ID")] ReBook reBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reBook);
        }
    }
}
