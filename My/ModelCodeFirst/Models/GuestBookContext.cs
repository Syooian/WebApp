using Microsoft.EntityFrameworkCore;

namespace MyModel_CodeFirst.Models
{
    //1.2.2 撰寫GuestBookContext類別的內容
    //(1)須繼承DbContext類別
    public class GuestBookContext:DbContext
    {
        //(2)撰寫依賴注入用的建構子
        public GuestBookContext(DbContextOptions<GuestBookContext> options)
            : base(options)
        {
        }

        //(3)描述資料庫裡面的資料表
        public virtual DbSet<Book> Book { get; set; } //Book資料表
        public virtual DbSet<ReBook> ReBook { get; set; } //ReBook資料表

    }
}
