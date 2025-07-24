using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Homework1.Data;
using Homework1.Models;

namespace Homework1.Controllers
{
    public class RepliesController : Controller
    {
        private readonly DBContext _context;

        public RepliesController(DBContext context)
        {
            _context = context;
        }

        // GET: Replies/Create
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MainTextID"></param>
        /// <returns></returns>
        public IActionResult Create(string MainTextID)
        {
            Console.WriteLine($"Create MainTextID : {MainTextID}");

            ViewData["MainTextID"] = MainTextID;
            return View();
        }

        // POST: Replies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReplyID,Content,CreatedDate,UserName,MainTextID")] Reply Reply)
        {
            if (ModelState.IsValid)
            {
                Reply.CreatedDate = DateTime.Now;

                _context.Add(Reply);
                await _context.SaveChangesAsync();
                return Json(Reply);
            }

            //Model驗證失敗
            Console.WriteLine("ModelState is invalid. Errors: " + string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));

            return Json(Reply);
        }

        /// <summary>
        /// 取得回覆留言資料的Action
        /// </summary>
        /// <param name="MainTextID"></param>
        /// <returns></returns>
        public IActionResult GetViewComponent(string MainTextID) => ViewComponent("VC_Replies", new { MainTextID = MainTextID });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ReplyExists(string id)
        {
            return _context.Replies.Any(e => e.ReplyID == id);
        }
    }
}
