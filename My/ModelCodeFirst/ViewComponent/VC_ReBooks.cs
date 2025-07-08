using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelCodeFirst.Models;

namespace ModelCodeFirst.ViewComponent
{
    public class VC_ReBooks : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        readonly GuestBookContext _context;
        public VC_ReBooks(GuestBookContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string ID)
        {
            Console.WriteLine($"ID：{ID}");

            var Result = await _context.ReBook.Where(R => R.ID == ID).OrderByDescending(R => R.CreatedDate).ToListAsync();
            return View(Result);
            //return View(await Task.FromResult(new List<ReBook>()));
        }
    }
}
