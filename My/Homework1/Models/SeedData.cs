using Homework1.Data;
using Homework1.Views.Shared;
using Microsoft.EntityFrameworkCore;

namespace Homework1.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider ServiceProvider)
        {
            using (var context = new DBContext(
                ServiceProvider.GetRequiredService<DbContextOptions<DBContext>>()))
            {
                // Look for any MainTexts.
                if (context.MainTexts.Any())
                {
                    return;   // DB has been seeded
                }

                //圖片路徑檢查
                var PathCheck = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", Shared.MainTextPhotosPath);
                if (!Directory.Exists(PathCheck)) Directory.CreateDirectory(PathCheck);

                for (int a = 0; a < 5; a++)
                {
                    var MainTextID = Guid.NewGuid().ToString();

                    var MainText = new MainText()
                    {
                        MainTextID = MainTextID,
                        Title = $"MainText Title Post {a + 1}",
                        Content = $"This is the content of sample post {a + 1}.",
                        CreatedDate = DateTime.Now.AddMinutes(-10 * (a + 1)),
                        UserName = $"MainText User{a + 1}",
                        Photo = MainTextID,
                        PhotoType = ".jpg"
                    };

                    context.MainTexts.Add(MainText);

                    #region 圖片處理
                    File.Copy(
                        Path.Combine(Directory.GetCurrentDirectory(), "SeedPhotos", $"{a + 1}{MainText.PhotoType}"),
                        Path.Combine(PathCheck, $"{MainText.Photo}{MainText.PhotoType}")
                        );
                    #endregion

                    for (int b = 0; b < 5; b++)
                    {
                        var Reply = new Reply()
                        {
                            ReplyID = Guid.NewGuid().ToString(),
                            Content = $"This is a reply to post {a + 1}, reply {b + 1}.",
                            CreatedDate = DateTime.Now.AddMinutes(-5 * (b + 1)),
                            UserName = $"Reply User{b + 1}",
                            MainTextID = MainText.MainTextID
                        };

                        context.Replies.Add(Reply);
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
