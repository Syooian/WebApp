using Homework1.Data;
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

                //var MainTexts = new List<MainText>();
                //var Replies = new List<Reply>();

                for (int a = 0; a < 5; a++)
                {
                    var MainText = new MainText()
                    {
                        MainTextID = Guid.NewGuid().ToString(),
                        Title = $"MainText Title Post {a + 1}",
                        Content = $"This is the content of sample post {a + 1}.",
                        CreatedDate = DateTime.Now.AddMinutes(-10 * (a + 1)),
                        UserName = $"MainText User{a + 1}"
                    };

                    context.MainTexts.Add(MainText);

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
