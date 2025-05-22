using System.ComponentModel.DataAnnotations;

namespace MyView.Models
{
    public class Login
    {
        [Display(Name="帳號")]
        [Required(ErrorMessage="必填")]
        [StringLength(20)]
        public string UserName { get; set; } = null!;

        [Display(Name = "密碼")]
        [Required(ErrorMessage = "必填")]
        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage ="最少要8碼")]
        [MaxLength(12, ErrorMessage = "最多只能12碼")]
        public string Password { get; set; } = null!;
    }
}
