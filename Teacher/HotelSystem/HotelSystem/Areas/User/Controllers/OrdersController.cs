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
using System.Security.Claims;
using Humanizer;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace HotelSystem.Areas.User.Controllers
{
    [Area("User")]
    //[Authorize(Roles = "Member")]
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
        public IActionResult Create(DateTime ExpectedCheckInDate, DateTime ExpectedCheckOutDate)
        {
            ViewData["ExpectedCheckInDate"] = ExpectedCheckInDate;

            ViewData["ExpectedCheckOutDate"] = ExpectedCheckOutDate;

            ViewData["PayCode"] = new SelectList(_context.PayType, "PayCode", "PayType1");
         
            return View();
        }

        // POST: User/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order,string Cart)
        {
            //OrderID要系統自動產生
            //OrderDate等寫入資料庫時再抓
            order.MemberID = User.FindFirst(ClaimTypes.Sid)?.Value;
            order.EmployeeID = null;
            order.StatusCode = "0";


            ModelState.Remove("OrderID");
            ModelState.Remove("StatusCode");
            ModelState.Remove("MemberID");
            ModelState.Remove("Member");
            ModelState.Remove("PayCodeNavigation");
            ModelState.Remove("StatusCodeNavigation");


            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ExpectedCheckInDate"] = order.ExpectedCheckInDate;

            ViewData["ExpectedCheckOutDate"] = order.ExpectedCheckOutDate;

            ViewData["PayCode"] = new SelectList(_context.PayType, "PayCode", "PayType1");
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



//CREATE FUNCTION[dbo].[GenerateOrderID] ()
//RETURNS NCHAR(12)
//AS
//BEGIN
//    DECLARE @prefix CHAR(1) = 'R';
//DECLARE @datePart CHAR(8) = CONVERT(CHAR(8), GETDATE(), 112); --yyyyMMdd
//    DECLARE @lastNumber INT = 0;
//DECLARE @newNumber VARCHAR(3);
//DECLARE @newOrderID VARCHAR(20);

//--取得當天最後一個OrderID流水號
//    SELECT @lastNumber =
//        ISNULL(MAX(CAST(RIGHT(OrderID, 3) AS INT)), 0)
//    FROM [Order]
//WHERE OrderID LIKE @prefix + @datePart + '%';

//--流水號 + 1
//    SET @lastNumber = @lastNumber + 1;

//--格式化為三位數，不足補0
//SET @newNumber = RIGHT('000' + CAST(@lastNumber AS VARCHAR(3)), 3);

//--組成新的OrderID
//    SET @newOrderID = @prefix + @datePart + @newNumber;

//RETURN @newOrderID;
//END
