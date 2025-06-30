using System.ComponentModel.DataAnnotations;

namespace ModelCodeFirst.Models
{
    /// <summary>
    /// 留言
    /// </summary>
    public class Book
    {
        /// <summary>
        /// 留言編號
        /// <para>GUID</para>
        /// </summary>
        [Display(Name = "留言編號")]
        [StringLength(36, MinimumLength = 36)]
        [Key]
        public string ID { get; set; } = null!;
        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// 照片
        /// <para>檔名對應<see cref="ID"/></para>
        /// </summary>
        [Display(Name = "照片")]
        [StringLength(40)]//GUID+.jpg
        public string? Photo { get; set; } = null!;

        /// <summary>
        /// 每個回覆
        /// </summary>
        //public virtual ICollection<ReBook> ReBooks { get; set; } = new List<ReBook>();
        public virtual List<ReBook>? ReBooks { get; set; }//一個留言可以有多個回覆
    }
}
