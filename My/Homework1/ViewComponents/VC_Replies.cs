using Homework1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homework1.ViewComponents
{
    public class VC_Replies : ViewComponent
    {
        /// <summary>
        /// 
        /// </summary>
        readonly DBContext Context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Context"></param>
        public VC_Replies(DBContext Context)
        {
            this.Context = Context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MainTextID"></param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(string MainTextID)
        {
            //取得主文的回覆
            var Result = await Context.Replies.Where(R => R.MainTextID == MainTextID).OrderByDescending(R => R.CreatedDate).ToListAsync();
            return View(Result);
        }
    }
}
