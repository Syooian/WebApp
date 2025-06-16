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

        //4.3.1 撰寫Create Action程式碼(需有兩個Create Action)
        //4.3.2 建立Create View

        public IActionResult Create()
        {
         
            return View();
        }


        [HttpPost]
        public IActionResult Create(tStudent student)
        {
            //把表單送來的資料存入資料庫

            //1.在模型新增一筆資料
            db.tStudent.Add(student);
            //2.回寫資料庫
            db.SaveChanges(); //轉譯SQL 執行 INSERT INTO tStudent(fStuId, fName, fEmail, fScore) VALUES(...)

            return RedirectToAction("Index"); //新增完成後，導向到Index Action
        }


    }
}
