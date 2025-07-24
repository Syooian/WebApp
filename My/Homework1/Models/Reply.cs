using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework1.Models
{
    public class Reply
    {
        /// <summary>
        /// 回覆編號
        /// </summary>
        [Key]
        [HiddenInput]
        [StringLength(36, MinimumLength = 36)]
        [Column(TypeName = "char")]
        public string ReplyID { get; set; } = null!;
        /// <summary>
        /// 回覆內容
        /// </summary>
        [Display(Name = "回覆內容")]
        [Required(ErrorMessage = "請輸入回覆內容")]
        [StringLength(500)]
        public string Content { get; set; } = null!;
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
        [HiddenInput]
        [ForeignKey(nameof(MainText))]

        public string MainTextID { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        public MainText? MainText { get; set; }
        #endregion
    }
}
