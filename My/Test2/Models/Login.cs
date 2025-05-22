using Microsoft.AspNetCore.Cors;
using System.ComponentModel.DataAnnotations;

namespace Test2.Models
{
    public class Login
    {
        /// <summary>
        /// 使用者名稱
        /// </summary>
        [Display(Name = "帳號")]
        [Required(ErrorMessage = "請輸入帳號")]
        public string UserName { get; set; } = null!;
        /// <summary>
        /// 密碼
        /// </summary>
        [Display(Name = "密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        public Login()
        {

        }
    }
}
