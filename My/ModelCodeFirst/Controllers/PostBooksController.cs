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
    public class PostBooksController : Controller
    {
        private readonly GuestBookContext _context;

        public PostBooksController(GuestBookContext context)
        {
            _context = context;
        }

        // GET: PostBooks
        public async Task<IActionResult> Index()
        {
            #region 在資料庫執行排序
            //var Result = await _context.Book.OrderByDescending(R => R.CreatedDate).ToListAsync();
            #endregion
            #region 在本機記憶體執行排序
            //var Result = await _context.Book.ToListAsync();
            //Result.OrderByDescending(R => R.CreatedDate);
            #endregion

            return View(await _context.Book.ToListAsync());
        }

        // GET: PostBooks/Details/5
        public async Task<IActionResult> Display(string id)
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

        // GET: PostBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PostBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Photo,Description,Author,CreatedDate")] Book book, IFormFile? NewPhoto)
        {
            var CreatedDatetime = DateTime.Now;

            #region 上傳照片
            if (NewPhoto != null && NewPhoto.Length != 0)
            {
                switch (NewPhoto.ContentType)
                {
                    case "image/jpeg":
                    case "image/png":
                        {
                            //要複製過去的圖片，取新檔名(Book的ID+原檔附檔名)
                            var FileName = book.ID + Path.GetExtension(NewPhoto.FileName);
                            //要複製過去的路徑
                            var UploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BookPhotos", FileName);

                            using (FileStream FS = new FileStream(UploadPath, FileMode.Create))
                            {
                                NewPhoto.CopyTo(FS);
                            }

                            book.Photo = FileName;//儲存新檔名到資料庫

                            break;
                        }
                    default:
                        {
                            ViewData["ErrorMessage"] = "僅限上傳JPG或PNG格式圖片。";
                            return View();
                        }
                }
            }
            #endregion

            //Model驗證
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
            return _context.Book.Any(e => e.ID == id);
        }
    }
}
