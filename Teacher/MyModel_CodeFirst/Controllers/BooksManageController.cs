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
    public class BooksManageController : Controller
    {
        private readonly GuestBookContext _context;

        public BooksManageController(GuestBookContext context)
        {
            _context = context;
        }

        // GET: BooksManage
        public async Task<IActionResult> Index()
        {
            return View(await _context.Book.ToListAsync());
        }



        // GET: BooksManage/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: BooksManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //4.4.1 在BooksManageController加人中的DeleteReBook Action
        [HttpPost]
        public async Task<IActionResult> DeleteReBook(string id)
        {
            var reBook = await _context.ReBook.FindAsync(id);

            if (string.IsNullOrEmpty(id))
            {
                return Json(reBook);
            }

            _context.ReBook.Remove(reBook);
            await _context.SaveChangesAsync();




            return Json(reBook);

        }
    }
}
