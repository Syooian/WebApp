using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;

namespace MyController.Controllers
{
    public class FileUploadController : Controller
    {
        public IActionResult showPhotos()
        {

            string[] files=  Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Photos")); //  /wwwroot/Photos/

            foreach(string item in files)
            {
                string fileName = Path.GetFileName(item);
                ViewData["Photos"] += $"<img src='/Photos/{fileName}'>";
            }


            return View();
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
            {
                ViewData["ErrMessage"] = "別開玩笑了!!你根本沒上傳檔案!!";
                return View();
            }

            //只允許上傳圖片
            if (photo.ContentType != "image/jpeg" && photo.ContentType != "image/png")
            {
                ViewData["ErrMessage"] = "只允許上傳.jpg或.png的圖片檔案!!";
                return View();
            }


            //取得檔案名稱
            string fileName = Path.GetFileName(photo.FileName);

            //取得檔案的完整路徑
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Photos", fileName);
            // /wwwroot/Photos/xxx.jpg

            //將檔案上傳並儲存於指定的路徑

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                photo.CopyTo(fs);
            }

            ViewData["Result"] = "檔案上傳成功!!";
            return View();
        }

       
    }
}
