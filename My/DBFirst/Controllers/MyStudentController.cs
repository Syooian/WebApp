using DBFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DBFirst.Controllers
{
    public class MyStudentController : Controller
    {
        //建立DB物件 (4.1.4)
        dbStudentsContext Context = new dbStudentsContext();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult IndexViewModel()
        {
            var VM = new ViewModels.VM_tStudent
            {
                Departments = Context.Department.ToList(),
                Students = Context.tStudent.ToList()
            };

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
        /// <returns></returns>
        public IActionResult Create()
        {
            SetDeptData();
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
            return RedirectToAction(nameof(Index));
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

            SetDeptData();

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
                return RedirectToAction(nameof(Index));
            }

            return View(Student); //如果模型驗證失敗，回傳原本的View
        }

        /// <summary>
        /// 建立給科系的下拉式選單的資料來源
        /// </summary>
        void SetDeptData()
        {
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

            return RedirectToAction(nameof(Index));
        }
    }
}
