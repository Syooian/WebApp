using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelCodeFirst.Models
{
    /// <summary>
    /// 留言回覆
    /// </summary>
    public partial class ReBook
    {
        public string ReID { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Author { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string ID { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        public virtual Book? Book { get; set; }//一個回覆只能屬於一個留言
    }
}
