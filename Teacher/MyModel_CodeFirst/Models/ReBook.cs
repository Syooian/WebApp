using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MyModel_CodeFirst.Models
{
    //1.1.4 設計ReBook類別的各屬性，包括名稱、資料類型及其相關的驗證規則及顯示名稱(Display)
    public class ReBook
    {
        //屬性封裝
        [Display(Name = "編號")]
        [StringLength(36, MinimumLength = 36)]
        [Key]
        public string ReBookID { get; set; } = null!; //留言編號, 採用GUID

        [Display(Name = "回覆內容")]
        [Required(ErrorMessage = "必填")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = null!;

        [Display(Name = "回覆人")]
        [StringLength(10, ErrorMessage = "最多10字")]
        [Required(ErrorMessage = "必填")]
        public string Author { get; set; } = null!;

        [Display(Name = "回覆時間")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd  hh:mm:ss}")]
        [HiddenInput]
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        //1.1.5 撰寫兩個類別間的關聯屬性做為未來資料表之間的關聯
    }
}
