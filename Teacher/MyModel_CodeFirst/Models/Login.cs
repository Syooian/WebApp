using System.ComponentModel.DataAnnotations;

namespace MyModel_CodeFirst.Models
{
    //5.1.4 設計Login類別的各屬性，包括名稱、資料類型及其相關的驗證規則及顯示名稱(DisplayName)
    public class Login
    {
        [Display(Name ="帳號")]
        [StringLength(10,MinimumLength =5,ErrorMessage ="帳號為5-10碼")]
        [Key]
        [Required(ErrorMessage ="必填")]
        [RegularExpression("[A-Za-z][A-Za-z0-9_]{4,9}",ErrorMessage ="帳號格式有誤")]
        public string Account { get; set; } = null!;


        [Display(Name = "密碼")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "密碼為8-20碼")]
        [Required(ErrorMessage = "必填")]
        [MaxLength(20)]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
