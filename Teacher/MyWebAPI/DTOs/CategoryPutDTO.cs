using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.DTOs
{
    //6.1.3 新增CategoryPutDTO類別
    public class CategoryPutDTO
    {
        [Required]
        [StringLength(20)]
        [CategoryNameDuplicateCheck]
        public string CateName { get; set; } = null!;
    }
}
