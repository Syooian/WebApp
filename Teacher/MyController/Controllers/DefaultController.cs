using Microsoft.AspNetCore.Mvc;

namespace MyController.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult ShowPhotos()
        {
            for(int i = 1; i <= 8; i++)
            {
                ViewData["Photos"] += $"<a href='/Default/ShowPhoto?index={i}'><img src='/images/{i}.jpg' width='200'></a>";
            }
          

            return View();
        }

        public IActionResult ShowPhoto(int index)
        {
            string[] name = { "櫻桃鴨", "鴨油高麗菜", "鴨油麻婆豆腐", "櫻桃鴨握壽司", "片皮鴨捲三星蔥", "三杯鴨", "櫻桃鴨片肉", "慢火白菜燉鴨湯" };

            ViewData["Photo"] += $"<div style='text-align:center'><img src='/images/{index}.jpg' ><br><h3>{name[index-1]}</h3></div>";


            return View();
        }
    }
}
