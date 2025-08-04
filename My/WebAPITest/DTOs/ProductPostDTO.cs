using System.ComponentModel.DataAnnotations;

namespace WebAPITest.DTOs
{
    public class ProductPostDTO
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [RegularExpression("[A-Z][1-9][0-9]{3}")]//格式驗證，第一碼A~Z，第二碼1~9，後面三碼0~9，e.g. A2003
        public string ProductID { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        [ProductNameCheck]
        public string ProductName { get; set; } = null!;

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public IFormFile Picture { get; set; } = null!;

        [Required]
        [RegularExpression("[A-Z][1-9]")]
        public string CateID { get; set; } = null!;
    }
}
