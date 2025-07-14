using Microsoft.EntityFrameworkCore;
using ModelCodeFirst.Models;

namespace Microsoft.AspNetCore.Mvc;

public class VC_BooksTopThree : ViewComponent
{
    /// <summary>
    /// 
    /// </summary>
    readonly GuestBookContext Context;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Context"></param>
    public VC_BooksTopThree(GuestBookContext Context)
    {
        this.Context = Context;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var Books = await Context.Book.OrderByDescending(B => B.CreatedDate).Take(3).ToListAsync();//取前3筆

        return View(Books);
    }
}
