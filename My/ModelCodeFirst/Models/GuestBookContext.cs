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
        /// <summary>
        /// 留言資料表
        /// </summary>
        public virtual DbSet<Book> Book { get; set; }
        /// <summary>
        /// 留言回覆資料表
        /// </summary>
        public virtual DbSet<ReBook> ReBook { get; set; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ModelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            //Fluent API設定
            //如果驗證規則是寫在原模型類別檔中，則不需要寫OnModelCreating，Entity Framework Core 會自動根據這些屬性產生對應的驗證規則與資料表結構
            //但如果你有複雜的關聯、索引、複合主鍵、或 Fluent API 才能描述的規則，就還是需要在 OnModelCreating 裡設定。

            //base.OnModelCreating(modelBuilder);

            ModelBuilder.Entity<Book>(Entity =>
            {
                Entity.HasKey(E => E.ID).HasName("PK_BookID");//設定主鍵

                Entity.Property(E => E.ID)
                    .HasMaxLength(36)
                    .IsUnicode(false)//不要使用Unicode編碼
                    .HasDefaultValue(false);//預設值為false，表示不允許空值
                                            //newid()//MSSQL產生GUID的函式

                Entity.Property(E => E.Title)
                    .IsRequired()//必填
                    .HasMaxLength(50)//最大長度50個字元
                    .IsUnicode(true);

                Entity.Property(E => E.Photo)
                    .HasMaxLength(40)//最大長度40個字元
                    .IsUnicode(false);//不要使用Unicode編碼

                Entity.Property(E => E.Description)
                    .IsRequired()//必填
                    .IsUnicode(true);//使用Unicode編碼

                Entity.Property(E => E.Author)
                    .IsRequired(true)
                    .HasMaxLength(20)
                    .IsUnicode(true)
                    .HasColumnType("datetime");
            });
        }
    }
}
