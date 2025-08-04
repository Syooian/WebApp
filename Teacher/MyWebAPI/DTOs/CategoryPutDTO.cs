using System.ComponentModel.DataAnnotations;
using static MyWebAPI.ValidationAttributes.MyValidator; //8.5.4 在用到自訂驗證器的DTO中using static的 MyValidator類別

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
