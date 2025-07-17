using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModelCodeFirst.Models
{
    public class Login
    {
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "帳號")]
        [Required(ErrorMessage = "請輸入帳號")]
        [Key]
        public string Account { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        public string Password { get; set; }
    }
}
