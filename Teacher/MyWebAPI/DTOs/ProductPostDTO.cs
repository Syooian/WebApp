using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.DTOs
{
    //5.2.3 建立一個ProductPostDTO給Post利用DTO傳遞資料
    public class ProductPostDTO
    {
        [Required]
        [RegularExpression("[A-Z][1-9][0-9]{3}")]
        public string ProductID { get; set; } = null!;

        [Required]
        [StringLength(40)]

        public string ProductName { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        [Required]
        public IFormFile Picture { get; set; } = null!;

        [Required]
        [RegularExpression("[A-Z][1-9]")]
        public string CateID { get; set; } = null!;

    }
}
