using System.ComponentModel.DataAnnotations;

namespace DBFirst_Test.Models
{
    /// <summary>
    /// 
    /// </summary>
    [MetadataType(typeof(tstudent2Metadata))]
    public partial class tstudent2
    {
        // 保持模型類別本身的清潔，所有顯示名稱和驗證規則都在元數據類別
    }

    public class tstudent2Metadata
    {
        /// <summary>
        /// 學號
        /// </summary>
        [Display(Name = "學號")]
        [Required(ErrorMessage = "必填")]
        [RegularExpression("[1-9][0-9]{5}", ErrorMessage = "格式有誤(ex:114025)")]
        public string fStuId { get; set; } = null!;

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

        /// <summary>
        /// 成績
        /// </summary>
        [Display(Name = "成績")]
        [Range(0, 100, ErrorMessage = "成績必須介於0到100之間")]
        public int? fScore { get; set; }
    }
}
