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

        // GET: BooksManage/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: BooksManage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BooksManage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Photo,Description,Author,CreatedDate")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: BooksManage/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: BooksManage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Title,Photo,Description,Author,CreatedDate")] Book book)
        {
            if (id != book.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.ID))
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
            return View(book);
        }

        // GET: BooksManage/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.ID == id);
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

        /// <summary>
        /// 刪除回覆留言
        /// </summary>
        /// <param name="ReID"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReBook(string ReID)
        {
            var ReBook = await _context.ReBook.FindAsync(ReID);
            if (ReBook != null)
            {
                _context.ReBook.Remove(ReBook);
            }

            await _context.SaveChangesAsync();

            //不要整頁重新整理
            //return RedirectToAction(nameof(Index));
            return Json(ReBook);
        }

        //4.4.4
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        public IActionResult GetReBookByViewComponent(string BookID)
        {
            return ViewComponent("VC_ReBooks", new { BookID = BookID, IsDelete=true});
        }
        
        private bool BookExists(string id)
        {
            return _context.Book.Any(e => e.ID == id);
        }
    }
}
