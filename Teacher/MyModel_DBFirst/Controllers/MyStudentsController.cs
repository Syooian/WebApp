using Microsoft.AspNetCore.Mvc;
using MyModel_DBFirst.Models;

namespace MyModel_DBFirst.Controllers
{
    public class MyStudentsController : Controller
    {
        //4.1.4 撰寫建立DbContext物件的程式
        dbStudentsContext db = new dbStudentsContext();


        //讀出tStudents資料表的資料
        public IActionResult Index()
        {
            //4.2.1 撰寫Index Action程式碼

            //select * from tStudents

            //Linq
            //var result = from s in db.tStudent
            //             select s;

            var result = db.tStudent.ToList();  //select * from tStudents

            //將查詢結果傳給View
            return View(result);
        }
    }
}
