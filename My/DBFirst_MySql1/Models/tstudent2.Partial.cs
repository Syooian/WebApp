using System.ComponentModel.DataAnnotations;

namespace DBFirst_MySql1.Models
{
    public partial class tstudent2
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "必填")]
        [StringLength(30, ErrorMessage = "姓名最多30個字元")]
        public string fName { get; set; } = null!;

        /// <summary>
        /// 電子郵件
        /// </summary>
        [Display(Name = "電子郵件")]
        [StringLength(40, ErrorMessage = "電子郵件最多40個字元")]
        [EmailAddress(ErrorMessage = "電子郵件格式錯誤")]
        public string fEmail { get; set; } = null!;
    }
}
