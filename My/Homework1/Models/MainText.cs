using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework1.Models
{
    public class MainText
    {
        /// <summary>
        /// 主文編號
        /// </summary>
        [Key]
        [HiddenInput]
        [StringLength(36, MinimumLength = 36)]
        [Column(TypeName = "char")]
        public string MainTextID { get; set; } = null!;
        /// <summary>
        /// 主題
        /// </summary>
        [Display(Name = "主題")]
        [Required(ErrorMessage = "請輸入主題")]
        [StringLength(50)]
        public string Title { get; set; } = null!;
        /// <summary>
        /// 發表內容
        /// </summary>
        [Display(Name = "發表內容")]
        [Required(ErrorMessage = "請輸入發表內容")]
        [StringLength(500)]
        public string Content { get; set; } = null!;
        /// <summary>
        /// 照片
        /// </summary>
        [Display(Name = "照片")]
        [StringLength(36, MinimumLength = 36)]
        [Column(TypeName = "char")]
        public string? Photo { get; set; }
        /// <summary>
        /// 照片類型
        /// </summary>
        [HiddenInput]
        [StringLength(5)]
        [Column(TypeName = "varchar")]
        public string? PhotoType { get; set; }
        /// <summary>
        /// 張貼時間
        /// </summary>
        [HiddenInput]
        [Display(Name = "張貼時間")]
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// 發表人
        /// </summary>
        [Display(Name = "發表人")]
        [StringLength(20)]
        [Required(ErrorMessage = "請輸入發表人")]
        public string UserName { get; set; } = null!;

        #region 關聯屬性
        /// <summary>
        /// 
        /// </summary>
        public virtual List<Reply> Replies { get; set; } = new List<Reply>();
        #endregion
    }
}
