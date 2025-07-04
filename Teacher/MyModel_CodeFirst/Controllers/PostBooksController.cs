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
    public class PostBooksController : Controller
    {
        private readonly GuestBookContext _context;

        public PostBooksController(GuestBookContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            //2.1.6 修改Index Action的寫法
            var result = await _context.Book.OrderByDescending(s=>s.CreatedDate).ToListAsync();

            return View(result);
        }


        //2.2.2 將PostBooksController中Details Action改名為Display(View也要改名字)
        public async Task<IActionResult> Display(string id)
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

        // GET: Books/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookID,Title,Description,Author,Photo,CreatedDate")] Book book, IFormFile? newPhoto)
        {
 
            book.CreatedDate = DateTime.Now; //設定建立時間為目前時間

            //2.4.6 修改Post Create Action，加上處理上傳照片的功能
            if (newPhoto != null && newPhoto.Length != 0)
            {
                //只允許上傳圖片
                if (newPhoto.ContentType != "image/jpeg" && newPhoto.ContentType != "image/png")
                {
                    ViewData["ErrMessage"] = "只允許上傳.jpg或.png的圖片檔案!!";
                    return View();
                }


                //取得檔案名稱
                string fileName = book.BookID + Path.GetExtension(newPhoto.FileName);

                //取得檔案的完整路徑
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BookPhotos", fileName);
                // /wwwroot/Photos/xxx.jpg

                //將檔案上傳並儲存於指定的路徑

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    newPhoto.CopyTo(fs);
                }

                book.Photo = fileName;

            }

            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

     

        private bool BookExists(string id)
        {
            return _context.Book.Any(e => e.BookID == id);
        }
    }
}
