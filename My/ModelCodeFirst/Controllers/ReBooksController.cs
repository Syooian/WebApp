using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModelCodeFirst.Models;

namespace ModelCodeFirst.Controllers
{
    public class ReBooksController : Controller
    {
        private readonly GuestBookContext _context;

        public ReBooksController(GuestBookContext context)
        {
            _context = context;
        }

        // GET: ReBooks/Create
        public IActionResult Create(string ID)
        {
            //給下拉式選單的值
            ViewData["ID"] = ID;
            return View();
        }

        // POST: ReBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReID,Description,Author,CreatedDate,ID")] ReBook reBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reBook);
                await _context.SaveChangesAsync();

                //2.5.14 修改ReBooksController中的Create Action，使其Return JSON資料
                //使用局部更新，局部更新只能使用Ajax
                return Json(reBook);

                //return RedirectToAction("Display", "PostBooks", new { ID = reBook.ID });//("View, "Controller", 參數)，此動作就是重新Request，造成畫面重新刷新，因此是不合理的作法
            }

            // 將原本的 return View("Create","ReBooks", reBook); 修正為只傳回 view 名稱與 model
            ViewData["ID"] = new SelectList(_context.Book, "ID", "ID", reBook.ID);

            return View(reBook);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IActionResult GetReBookByViewComponent(string ID)
        {
            return ViewComponent("VC_ReBooks", new { ID = ID });
        }
    }
}
