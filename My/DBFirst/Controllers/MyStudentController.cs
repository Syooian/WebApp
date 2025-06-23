using DBFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DBFirst.Controllers
{
    public class MyStudentController : Controller
    {
        readonly dbStudentsContext Context;

        public MyStudentController(dbStudentsContext Context)
        {
            this.Context = Context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">科系ID<para>不帶入值時表示全部科系</para></param>
        /// <returns></returns>
        public IActionResult IndexViewModel(string id = "")
        {
            var VM = new ViewModels.VM_tStudent
            {
                //Where : 帶入條件

                Departments = Context.Department.ToList(),
                Students = string.IsNullOrEmpty(id) ? Context.tStudent.ToList() : Context.tStudent.Where(S => S.DeptID == id).ToList()
            };

            if (!string.IsNullOrEmpty(id))
                ViewData["DeptName"] = Context.Department.Find(id).DeptName;
            ViewData["DeptID"] = id;

            return View(VM);
        }

        /// <summary>
        /// 
        /// <para>讀取tStudnets資料表資料</para>
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //Index Action 4.2.1
            //Linq寫法
            //var Result = from Student in Context.tStudent select Student;
            var Result = Context.tStudent.Include(t => t.Department).ToList();

            //將結果傳給View 4.2.2
            return View(Result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Create(string DeptID)
        {
            SetDeptData(DeptID);

            Console.WriteLine("DeptID : " + DeptID);

            return View();
        }
        /// <summary>
        /// 將表單的資料傳回資料庫
        /// </summary>
        /// <param name="Student"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(tStudent Student)
        {
            //檢查學號重複值
            var Result = Context.tStudent.Find(Student.fStuId);
            if (Result != null)
            {
                //學號重複，回傳錯誤訊息
                ViewData["ErrorMessage"] = "學號已存在，請重新輸入。";
                return View(Student);
            }

            //在模型新增資料
            Context.tStudent.Add(Student);

            //傳回資料庫
            Context.SaveChanges();

            //回首頁
            return RedirectToAction("IndexViewModel", new { id = Student.DeptID });
        }

        /// <summary>
        /// 編輯學生資料
        /// </summary>
        /// <param name="id">學號</param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            Debug.WriteLine("fStuId : " + id);//需配合指定的參數名稱，在Program.cs內 (app.MapControllerRoute)

            var Student = Context.tStudent.Find(id);

            if (Student == null)
            {
                //找不到學生資料，回傳404 Not Found結果
                return NotFound();
            }

            SetDeptData(Student.DeptID);

            return View(Student);
        }
        /// <summary>
        /// 回傳資料庫
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Student"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(string id, tStudent Student)
        {
            if (id != Student.fStuId)//檢查學號是否一致
            {
                return NotFound();//回傳404 Not Found結果
            }

            if (ModelState.IsValid)//模型驗證
            {
                //更新資料
                Context.tStudent.Update(Student);

                //傳回資料庫
                Context.SaveChanges();

                //回首頁
                return RedirectToAction("IndexViewModel", new { id = Student.DeptID });
            }

            return View(Student); //如果模型驗證失敗，回傳原本的View
        }

        /// <summary>
        /// 建立給科系的下拉式選單的資料來源
        /// </summary>
        /// <param name="DeptID">科系ID</param>
        void SetDeptData(string DeptID)
        {
            //5.9.2 修改Get Create Action進行參數傳遞
            ViewData["DeptID"] = DeptID;

            ViewData["DeptData"] = new SelectList(Context.Department, "DeptID", "DeptName");
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            var Student = Context.tStudent.Find(id);

            if (Student == null)
            {
                //找不到學生資料，回傳404 Not Found結果
                return NotFound();
            }

            //從資料庫中刪除學生資料
            Context.tStudent.Remove(Student);

            Context.SaveChanges();

            return RedirectToAction("IndexViewModel", new { id = Student.DeptID });
        }
    }
}
