using Microsoft.EntityFrameworkCore;

namespace ModelCodeFirst.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class GuestBookContext : DbContext
    {
        /// <summary>
        /// 依賴注入用的建構子
        /// </summary>
        /// <param name="options"></param>
        public GuestBookContext(DbContextOptions<GuestBookContext> options)
            : base(options)
        {

        }

        #region 描述資料庫裡面的資料表
        public virtual DbSet<Book> Book { get; set; } //Book資料表
        public virtual DbSet<ReBook> ReBook { get; set; } //ReBook資料表
        #endregion
    }
}
