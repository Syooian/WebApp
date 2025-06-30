using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ModelCodeFirst.Models
{
    public abstract partial class BookBase
    {
        /// <summary>
        /// 內文
        /// </summary>
        [Display(Name = "內文")]
        [Required(ErrorMessage = "請輸入內文")]
        [DataType(DataType.MultilineText)]//標註多行文字
        public string Description { get; set; } = null!;
        /// <summary>
        /// 作者
        /// </summary>
        [Display(Name = "作者")]
        [StringLength(20, ErrorMessage = "不能超過20個字")]
        public string Author { get; set; } = null!;
        /// <summary>
        /// 發表日期
        /// </summary>
        [Display(Name = "發表日期")]
        [DataType(DataType.Date)]//給View看的
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss")]//讀出來時的格式化日期
        //[DisplayFormat(DataFormatString = "{0})]//按照原始格式
        [HiddenInput]//隱藏在View中
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
