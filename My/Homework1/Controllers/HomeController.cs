using Homework1.Data;
using Homework1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Homework1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly DBContext Context;

        public HomeController(ILogger<HomeController> logger, DBContext Context)
        {
            _logger = logger;
            this.Context = Context;
        }

        /// <summary>
        /// 主文清單
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var Result = await Context.MainTexts.Include(R => R.Replies).OrderByDescending(T => T.CreatedDate).ToListAsync();

            return View(Result);
        }

        /// <summary>
        /// 新增主文
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MainText"></param>
        /// <param name="MainTextPhoto">圖片上傳</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainTextID,Title,Content,UserName")] MainText MainText, IFormFile? MainTextPhoto)
        {
            if (ModelState.IsValid)
            {
                MainText.CreatedDate = DateTime.Now;

                //圖片上傳
                if (MainTextPhoto != null && MainTextPhoto.Length != 0)
                {
                    //取新檔名
                    var FileName = MainText.MainTextID + Path.GetExtension(MainTextPhoto.FileName);

                    var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MainTextPhotos", FileName);

                    using (FileStream FS = new FileStream(FilePath, FileMode.Create))
                    {
                        MainTextPhoto.CopyTo(FS);
                    }

                    MainText.Photo = MainText.MainTextID;
                    MainText.PhotoType = Path.GetExtension(MainTextPhoto.FileName);
                }

                Context.Add(MainText);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //Console.WriteLine("ModelState is invalid. Errors: " + string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));

            return View(MainText);
        }

        /// <summary>
        /// 詳細檢視
        /// </summary>
        /// <param name="MainTextID">主文編號</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(string MainTextID)
        {
            if (string.IsNullOrEmpty(MainTextID))
            {
                return NotFound();
            }

            var MainText = await Context.MainTexts
                .Include(M => M.Replies)
                .FirstOrDefaultAsync(M => M.MainTextID == MainTextID);

            if (MainText == null)
            {
                return NotFound();
            }

            return View(MainText);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
