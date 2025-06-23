using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyModel_DBFirst.Models;
using MyModel_DBFirst.ViewModels;

namespace MyModel_DBFirst.Controllers
{
    public class MyStudentsController : Controller
    {
        //4.1.4 撰寫建立DbContext物件的程式
        //dbStudentsContext db = new dbStudentsContext();

        private readonly dbStudentsContext db;


        public MyStudentsController(dbStudentsContext context)
        {
            db = context; //使用依賴注入的方式建立dbStudentsContext物件
        }
     


        //5.8.4 撰寫MyStudnetsController裡新的IndexViewModel Action
        public IActionResult IndexViewModel(string id = "01")
        {

            VMtStudent students = new VMtStudent()
            {
                Students = db.tStudent.Where(s => s.DeptID == id).ToList(),
                Departments = db.Department.ToList()
            };

            ViewData["DeptName"] = db.Department.Find(id).DeptName;

            ViewData["DeptID"] = id;

            return View(students);
        }


        //讀出tStudents資料表的資料
        public IActionResult Index()
        {
            //4.2.1 撰寫Index Action程式碼

            //select * from tStudents

            //Linq
            //var result = from s in db.tStudent
            //             select s;
            //var result = db.tStudent.ToList();  //select * from tStudents

            //5.5.1 修改 Index Action
            var result = db.tStudent.Include(t => t.Department).ToList();


            //將查詢結果傳給View
            return View(result);
        }

        //4.3.1 撰寫Create Action程式碼(需有兩個Create Action)
        //4.3.2 建立Create View
        public IActionResult Create(string deptid)
        {
            //5.5.3 修改 Create Action
            ViewData["Dept"] = new SelectList(db.Department, "DeptID", "DeptName"); //建立給下拉選單的資料來源
            
            //5.9.2 修改Get Create Action進行參數傳遞
            ViewData["DeptID"] = deptid;

            return View();
        }


        //4.3.7 加入Token驗證標籤
        //5.9.3 修改Post Create Action進行參數傳遞
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

                                                            //5.9.3 修改Post Create Action進行參數傳遞
                return RedirectToAction("IndexViewModel", new { id= student.DeptID }); //新增完成後，導向到Index Action
            }


            //如果模型驗證失敗，則回到Create View
            return View(student); //將表單資料傳回Create View，讓使用者可以修正錯誤

        }




        //4.4.1 撰寫Edit Action程式碼(需有兩個Edit Action)
        //5.9.4 修改Get Edit Action進行參數傳遞
        public ActionResult Edit(string id, string deptid)
        {
            ViewData["Now"] = DateTime.Now;

            var result = db.tStudent.Find(id); //使用Find方法查詢學號是否存在

            if (result == null)
            {
                return NotFound(); //如果找不到資料，回傳404 Not Found
            }

            //5.5.5 修改 Edit Action
            ViewData["Dept"] = new SelectList(db.Department, "DeptID", "DeptName"); //建立給下拉選單的資料來源

            //5.9.4 修改Get Edit Action進行參數傳遞
            ViewData["DeptID"] = deptid;

            return View(result);

        }

        //5.9.5 修改Post Edit Action進行參數傳遞
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
                                                        //5.9.5 修改Post Edit Action進行參數傳遞
                return RedirectToAction("IndexViewModel", new { id= student.DeptID }); //編輯完成後，導向到Index Action

            }


            return View(student);

        }


        //4.5.1 撰寫Delete Action程式碼
        //4.5.4 執行Delete功能測試
        //5.9.6 修改Post Delete Action進行參數傳遞
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            //delete from tStudents where fStuId = id;

            var result = db.tStudent.Find(id);

            if (result == null)
            {
                return NotFound(); //如果找不到資料，回傳404 Not Found
            }

            db.tStudent.Remove(result); //將找到的資料從模型資料裡移除
            db.SaveChanges(); //回寫資料庫，執行 DELETE FROM tStudents WHERE fStuId = id;

                                                       //5.9.6 修改Post Delete Action進行參數傳遞      
            return RedirectToAction("IndexViewModel", new { id = result.DeptID }); //刪除完成後，導向到Index Action
        }

    }
}
