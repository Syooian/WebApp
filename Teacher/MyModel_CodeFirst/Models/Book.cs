using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MyModel_CodeFirst.Models
{
    //1.1.2 設計Book類別的各屬性，包括名稱、資料類型及其相關的驗證規則及顯示名稱(Display)
    public class Book
    {
        //屬性封裝
        [Display(Name = "編號")]
        [StringLength(36,MinimumLength = 36)]
        [Key]
        public string BookID { get; set; }=null!; //留言編號, 採用GUID


        [Display(Name = "主題")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "主題2~30個字")]
        [Required(ErrorMessage = "必填")]
        public string Title { get; set; } = null!;


        [Display(Name = "內容")]
        [Required(ErrorMessage = "必填")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = null!;


        [Display(Name = "發表人")]
        [StringLength(20, ErrorMessage = "最多20字")]
        [Required(ErrorMessage = "必填")]
        public string Author { get; set; } = null!;

        [Display(Name = "照片")]
        [StringLength(40)]
        public string? Photo { get; set; } = null!;  //照片的檔名規則:GUID+.jpg


        [Display(Name = "發布時間")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString ="{0:yyyy/MM/dd hh:mm:ss}")]
        [HiddenInput]
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        //1.1.5 撰寫兩個類別間的關聯屬性做為未來資料表之間的關聯
        public virtual List<ReBook>? ReBooks { get; set; }

    }
}
