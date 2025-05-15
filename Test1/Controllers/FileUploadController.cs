using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Test1.Controllers
{
    public class FileUploadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult UploadFile()
        {

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="File"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadFile(IFormFile Photo)
        {
            if (Photo == null || Photo.Length == 0)
            {
                ViewData["ErrorMsg"] = "無檔案";

                return View();
            }
            else if (Photo.ContentType != "image/jpeg" && Photo.ContentType != "image/png")//驗證MIME Type
            {
                ViewData["ErrorMsg"] = "僅限上傳圖片";

                return View();
            }

            //取檔案名稱
            var FileName = Path.GetFileName(Photo.FileName);
            Debug.WriteLine("FileName : " + FileName);

            //檔案上傳後要儲存的完整路徑
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Photos", FileName);
            Debug.WriteLine("FilePath : " + FilePath);

            //檢查資料夾路徑
            var DirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Photos");
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }

            //將檔案上傳至指定路徑
            using (FileStream FS = new FileStream(FilePath, FileMode.Create))
            {
                Photo.CopyTo(FS);
            }


            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowPhotos()
        {
            var Files = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Photos"));

            foreach (var File in Files)
            {
                var FileName = Path.GetFileName(File);
                ViewData["Photos"] += $"<img src='/Photos/{FileName}' style='width:200px;height:200px;'><br>";
            }

            return View();
        }
    }
}
