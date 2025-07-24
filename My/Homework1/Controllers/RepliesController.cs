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
            ViewData["MainTextID"] = MainTextID;
            return View();
        }

        // POST: Replies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReplyID,Content,CreatedDate,UserName,MainTextID")] Reply reply)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reply);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MainTextID"] = new SelectList(_context.MainTexts, "MainTextID", "MainTextID", reply.MainTextID);
            return View(reply);
        }

        private bool ReplyExists(string id)
        {
            return _context.Replies.Any(e => e.ReplyID == id);
        }
    }
}
