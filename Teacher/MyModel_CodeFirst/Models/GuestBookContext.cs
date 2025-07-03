using Microsoft.EntityFrameworkCore;

namespace MyModel_CodeFirst.Models
{
    //1.2.2 撰寫GuestBookContext類別的內容
    //(1)須繼承DbContext類別
    public class GuestBookContext : DbContext
    {
        //(2)撰寫依賴注入用的建構子
        public GuestBookContext(DbContextOptions<GuestBookContext> options)
            : base(options)
        {
        }

        //(3)描述資料庫裡面的資料表
        public virtual DbSet<Book> Book { get; set; } //Book資料表
        public virtual DbSet<ReBook> ReBook { get; set; } //ReBook資料表


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //1.2.6 在DbContext中使用Fluent API在GuestBookContext覆寫 OnModelCreating 方法

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.BookID).HasName("PK_BookID"); //設定主鍵

                entity.Property(e => e.BookID)
                .HasMaxLength(36)  //BookID長度為36
                .IsUnicode(false); //不使用Unicode編碼 

                entity.Property(e => e.Title)
               .HasMaxLength(30);

                entity.Property(e => e.Author)
               .HasMaxLength(20);

                entity.Property(e => e.Photo)
               .HasMaxLength(40);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime"); // 設定為datetime型別
            });

            modelBuilder.Entity<ReBook>(entity =>
            {
                entity.HasKey(e => e.ReBookID); //設定主鍵
                entity.Property(e => e.ReBookID).HasMaxLength(36)  //長度為36
                .IsUnicode(false);  //ReBookID預設值為GUID

                entity.Property(e => e.Author)
                .HasMaxLength(20);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime"); // 設定為datetime型別

            });
        }


    }
}
