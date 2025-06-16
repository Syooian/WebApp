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


        //4.3.7 加入Token驗證標籤
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(tStudent student)
        {
            //4.3.6 加入檢查主鍵是否重覆的程式
            var result = db.tStudent.Find(student.fStuId); //使用Find方法查詢學號是否存在

            if (result != null)
            {
                ViewData["ErrorMessage"] = "學號已存在，請重新輸入！"; //將錯誤訊息傳遞到View
                return View(student);
            }


            //把表單送來的資料存入資料庫
            if (ModelState.IsValid)
            {
                //1.在模型新增一筆資料
                db.tStudent.Add(student);
                //2.回寫資料庫
                db.SaveChanges(); //轉譯SQL 執行 INSERT INTO tStudent(fStuId, fName, fEmail, fScore) VALUES(...)

                return RedirectToAction("Index"); //新增完成後，導向到Index Action
            }


            //如果模型驗證失敗，則回到Create View
            return View(student); //將表單資料傳回Create View，讓使用者可以修正錯誤

        }




        //4.4.1 撰寫Edit Action程式碼(需有兩個Edit Action)
        public ActionResult Edit(string id)
        {
            ViewData["Now"] = DateTime.Now;

            var result = db.tStudent.Find(id); //使用Find方法查詢學號是否存在

            if (result == null)
            {
                return NotFound(); //如果找不到資料，回傳404 Not Found
            }


            return View(result);

        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(string id, tStudent student)
        {
            if (id != student.fStuId) //檢查傳入的學號是否與表單資料的學號一致
            {
                return View(student); //如果不一致，則回到Edit View
            }

            if (ModelState.IsValid)
            {
                db.tStudent.Update(student);
                db.SaveChanges();
                return RedirectToAction("Index"); //編輯完成後，導向到Index Action

            }


            return View(student);

        }
    }
}
