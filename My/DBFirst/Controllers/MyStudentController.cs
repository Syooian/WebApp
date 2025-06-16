using DBFirst.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBFirst.Controllers
{
    public class MyStudentController : Controller
    {
        //建立DB物件 (4.1.4)
        dbStudentsContext Context = new dbStudentsContext();

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
            var Result = Context.tStudent.ToList();

            //將結果傳給View 4.2.2
            return View(Result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
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
        /// <param name="fStuId">學號</param>
        /// <returns></returns>
        public ActionResult Edit(string fStuId)
        {
            var Student = Context.tStudent.Find(fStuId);

            if (Student == null)
            {
                //找不到學生資料，回傳404 Not Found結果
                return NotFound();
            }

            return View(Student);
        }
    }
}
