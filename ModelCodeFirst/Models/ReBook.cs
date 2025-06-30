using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelCodeFirst.Models
{
    /// <summary>
    /// 留言回覆
    /// </summary>
    public class ReBook : BookBase
    {
        /// <summary>
        /// 回覆留言編號
        /// </summary>
        [Display(Name = "回覆留言編號")]
        [Key]
        public string ReID { get; set; } = null!;

        /// <summary>
        /// 外來鍵屬性
        /// </summary>
        [ForeignKey("Book")]
        public string ID { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        public virtual Book? Book { get; set; }//一個回覆只能屬於一個留言
    }
}
