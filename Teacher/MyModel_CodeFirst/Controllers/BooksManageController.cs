using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyModel_CodeFirst.Models;

namespace MyModel_CodeFirst.Controllers
{
    //5.2.7 在需要驗證的 Controller 或 Action 加上 [Authorize]
    [Authorize(Roles ="Manager")]
    public class BooksManageController : Controller
    {
        private readonly GuestBookContext _context;

        public BooksManageController(GuestBookContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {

            return View(await _context.Book.OrderByDescending(b => b.CreatedDate).ToListAsync());
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return View();
            }

            //4.4.7 在BooksManageController中的Delete Action加入刪除圖片的程式

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BookPhotos", book.Photo ?? string.Empty);

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath); //刪除圖片檔案



            _context.Book.Remove(book);
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


        //4.4.4 在BooksManageController中加入GetRebookByViewComponent Action
        public IActionResult GetRebookByViewComponent(string id)
        {
            //呼叫ViewComponent
            return ViewComponent("VCReBooks", new { bookID = id, isDel = true });
        }
    }
}
