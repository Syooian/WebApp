using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyModel_CodeFirst.Models;

namespace MyModel_CodeFirst.ViewComponents
{
    //2.3.3 VCReBooks class繼承ViewComponent(注意using Microsoft.AspNetCore.Mvc;)  
    public class VCReBooks : ViewComponent
    {
        private readonly GuestBookContext _context;

        public VCReBooks(GuestBookContext context)
        {
            _context = context;
        }

        //4.3.4 在VCRebooks ViewComponent中加入isDel參數判斷是否呈現Delete View
        //2.3.4 撰寫InvokeAsync()方法取得回覆留言資料
        public async Task<IViewComponentResult> InvokeAsync(string bookID, bool isDel=false)
        {
            //select*
            //from rebook
            //where BookID = @bookID

            var rebook = await _context.ReBook.Where(r=>r.BookID==bookID).OrderByDescending(r=>r.CreatedDate).ToListAsync();

            if(isDel)
                return View("Delete",rebook);

            return View(rebook);
        }

    }
}
