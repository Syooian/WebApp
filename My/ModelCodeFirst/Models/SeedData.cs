using Microsoft.EntityFrameworkCore;

namespace ModelCodeFirst.Models
{
    public class SeedData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ServiceProvider"></param>
        public static void Initialize(IServiceProvider ServiceProvider)
        {
            using (var context = new GuestBookContext(
                ServiceProvider.GetRequiredService<DbContextOptions<GuestBookContext>>()))
            {
                // Look for any books.
                if (context.Book.Any())
                {
                    return;   // DB has been seeded
                }

                //var BookData = new Book[5];
                //for (int a = 0; a < BookData.Length; a++)
                //{
                //    var NewGUID = Guid.NewGuid().ToString();

                //    BookData[a] = new Book
                //    {
                //        ID = NewGUID,
                //        Title = $"Book {a + 1}",
                //        Description = $"This is book number {a + 1}.",
                //        Author = $"Author {a + 1}",
                //        CreatedDate = DateTime.Now.AddMinutes(10),
                //        Photo = $"{NewGUID}{(a + 1).ToString("0000")}.jpg"
                //    };
                //}

                //context.Book.AddRange(BookData);
                //==============================================
                for (int a = 0; a < 5; a++)
                {
                    var NewGUID = Guid.NewGuid().ToString();

                    var BookData = new Book
                    {
                        ID = NewGUID,
                        Title = $"Book {a + 1}",
                        Description = $"This is book number {a + 1}.",
                        Author = $"Author {a + 1}",
                        CreatedDate = DateTime.Now.AddMinutes(10),
                        Photo = $"{NewGUID}{(a + 1).ToString("0000")}.jpg"
                    };

                    context.Book.Add(BookData);

                    #region 上傳圖片
                    File.Copy(
                        Path.Combine(Directory.GetCurrentDirectory(), "SeedPhotos", $"{a + 1}.jpg"), //From
                        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", BookData.Photo)); //To
                    #endregion

                    #region 留言回覆
                    for (int b = 0; b < 3; b++)
                    {
                        var ReBookData = new ReBook
                        {
                            ID = BookData.ID,
                            ReID = Guid.NewGuid().ToString(), //關聯到留言的ID
                            Author = $"ReAuthor {a + 1}-{b + 1}",
                            CreatedDate = DateTime.Now.AddMinutes(10),
                            Description = $"This is reply number {b + 1} for book {a + 1}."
                        };

                        context.ReBook.Add(ReBookData);
                    }
                    #endregion
                }
                context.SaveChanges();
            }
        }
    }
}
