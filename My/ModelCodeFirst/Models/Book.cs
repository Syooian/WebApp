using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ModelCodeFirst.Models
{
    /// <summary>
    /// 留言
    /// </summary>
    public partial class Book
    {
        public string ID { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string? Photo { get; set; } = null!;

        /// <summary>
        /// 每個回覆
        /// </summary>
        //public virtual ICollection<ReBook> ReBooks { get; set; } = new List<ReBook>();
        public virtual List<ReBook>? ReBooks { get; set; }//一個留言可以有多個回覆
    }
}
