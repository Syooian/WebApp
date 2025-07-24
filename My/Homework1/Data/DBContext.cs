using Homework1.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework1.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MainText> MainTexts { get; set; }
        public virtual DbSet<Reply> Replies { get; set; }
    }
}
