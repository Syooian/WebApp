using System.ComponentModel.DataAnnotations;

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
    }
}
