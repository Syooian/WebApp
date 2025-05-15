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
            try
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

                Debug.WriteLine("Uploading");

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
            }
            catch(Exception ex)
            {
                Debug.WriteLine("上傳失敗 : " + ex.Message);
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
                Debug.WriteLine("F1 : " + File);

                var FileName = Path.GetFileName(File);
                Debug.WriteLine("F2 : " + FileName);
                ViewData["Photos"] += $"<img src='/Photos/{FileName}' style='width:200px;height:200px;'><br>";
            }

            return View();
        }
    }
}
