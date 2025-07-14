using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyModel_CodeFirst.Models;

namespace MyModel_CodeFirst.ViewComponents
{
    //3.2.5 撰寫VCBooksTopThree Class使其能讀出最新n筆留言
    public class VCBooksTopThree:ViewComponent
    {

        private readonly GuestBookContext _context;

        public VCBooksTopThree(GuestBookContext context)
        {
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var books =  await _context.Book.OrderByDescending(b => b.CreatedDate).Take(4).ToListAsync();
            return View(books);
        }


    }
}
