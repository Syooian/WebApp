using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.DTOs
{
    //6.1.6 新增ProductPutDTO類別
    public class ProductPutDTO
    {
        [Required]
        [StringLength(40)]
        [ProudctNameCheck]
        public string ProductName { get; set; } = null!;

        [Required]
        [Range(0, 1000000)]
        public decimal Price { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }


        public IFormFile? Picture { get; set; }

    }
}
