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
        public string MainTextID { get; set; }
        /// <summary>
        /// 主題
        /// </summary>
        [Display(Name = "主題")]
        [Required(ErrorMessage = "請輸入主題")]
        [StringLength(20)]
        public string Title { get; set; }
        /// <summary>
        /// 發表內容
        /// </summary>
        [Display(Name = "發表內容")]
        [Required(ErrorMessage = "請輸入發表內容")]
        [StringLength(100)]
        public string Context { get; set; }
        照片、照片類型、發表人、張貼時間)
    }
}
