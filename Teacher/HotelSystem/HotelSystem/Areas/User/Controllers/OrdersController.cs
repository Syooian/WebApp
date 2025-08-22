using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelSystem.Access.Data;
using HotelSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotelSystem.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "Member")]
    public class OrdersController : Controller
    {
        private readonly HotelSysDBContext2 _context;

        public OrdersController(HotelSysDBContext2 context)
        {
            _context = context;
        }

        // GET: User/Orders
        public async Task<IActionResult> Index()
        {
            // 取得 MemberID（存在 ClaimTypes.Sid）
            var MemberID = User.FindFirst(System.Security.Claims.ClaimTypes.Sid)?.Value;


            var hotelSysDBContext2 = _context.Order.Include(o => o.Employee).Include(o => o.Member).Include(o => o.PayCodeNavigation)
                .Include(o => o.StatusCodeNavigation).Where(o => o.MemberID == MemberID);
            return View(await hotelSysDBContext2.ToListAsync());
        }

        // GET: User/Orders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Employee)
                .Include(o => o.Member)
                .Include(o => o.PayCodeNavigation)
                .Include(o => o.StatusCodeNavigation)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: User/Orders/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID");
            ViewData["MemberID"] = new SelectList(_context.Member, "MemberID", "MemberID");
            ViewData["PayCode"] = new SelectList(_context.PayType, "PayCode", "PayCode");
            ViewData["StatusCode"] = new SelectList(_context.OrderStatus, "StatusCode", "StatusCode");
            return View();
        }

        // POST: User/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,OrderDate,ExpectedCheckInDate,ExpectedCheckOutDate,Note,MemberID,EmployeeID,PayCode,StatusCode")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", order.EmployeeID);
            ViewData["MemberID"] = new SelectList(_context.Member, "MemberID", "MemberID", order.MemberID);
            ViewData["PayCode"] = new SelectList(_context.PayType, "PayCode", "PayCode", order.PayCode);
            ViewData["StatusCode"] = new SelectList(_context.OrderStatus, "StatusCode", "StatusCode", order.StatusCode);
            return View(order);
        }

        // GET: User/Orders/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", order.EmployeeID);
            ViewData["MemberID"] = new SelectList(_context.Member, "MemberID", "MemberID", order.MemberID);
            ViewData["PayCode"] = new SelectList(_context.PayType, "PayCode", "PayCode", order.PayCode);
            ViewData["StatusCode"] = new SelectList(_context.OrderStatus, "StatusCode", "StatusCode", order.StatusCode);
            return View(order);
        }

        // POST: User/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OrderID,OrderDate,ExpectedCheckInDate,ExpectedCheckOutDate,Note,MemberID,EmployeeID,PayCode,StatusCode")] Order order)
        {
            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", order.EmployeeID);
            ViewData["MemberID"] = new SelectList(_context.Member, "MemberID", "MemberID", order.MemberID);
            ViewData["PayCode"] = new SelectList(_context.PayType, "PayCode", "PayCode", order.PayCode);
            ViewData["StatusCode"] = new SelectList(_context.OrderStatus, "StatusCode", "StatusCode", order.StatusCode);
            return View(order);
        }

        // GET: User/Orders/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Employee)
                .Include(o => o.Member)
                .Include(o => o.PayCodeNavigation)
                .Include(o => o.StatusCodeNavigation)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: User/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(string id)
        {
            return _context.Order.Any(e => e.OrderID == id);
        }
    }
}
