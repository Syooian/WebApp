using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyModel_CodeFirst.Models;

namespace MyModel_CodeFirst.Controllers
{
    public class ReBooksController : Controller
    {
        private readonly GuestBookContext _context;

        public ReBooksController(GuestBookContext context)
        {
            _context = context;
        }



        //2.5.8 傳遞BookID參數
        public IActionResult Create(string BookID)
        {
            ViewData["BookID"] = BookID; //2.5.8 傳遞BookID參數
            return View();
        }

        // POST: ReBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReBookID,Description,Author,CreatedDate,BookID")] ReBook reBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookID"] = new SelectList(_context.Book, "BookID", "BookID", reBook.BookID);
            // 將原本的 return View("Create","ReBooks", reBook); 修正為只傳回 view 名稱與 model
            return View(reBook);
        }

       
    }
}
