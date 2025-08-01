using System.ComponentModel.DataAnnotations;
using MyWebAPI.Models;

namespace MyWebAPI.DTOs
{
    //5.3.6 建立CategoryPostDTO類別
    public class CategoryPostDTO
    {
        //5.3.7 在CategoryPostDTO.cs加入需要的內建驗證器(Validator)

        [Required]
        [RegularExpression("[A-Z][1-9]")]
        public string CateID { get; set; } = null!;

        [Required]
        [StringLength(20)]
        [CategoryNameDuplicateCheck] //5.3.9 在需要使用此驗證器的屬性上加入標籤
        public string CateName { get; set; } = null!;

    }

    //5.3.8 在CategoryPostDTO.cs加入自訂驗證器(使用ValidationAttribute物件)
    public class CategoryNameDuplicateCheck : ValidationAttribute
    {

        //不可以模仿Controller的寫法注入 Context
        //private readonly GoodStoreContextG2 _context;

        //public CategoryNameDuplicateCheck(GoodStoreContextG2 context)
        //{
        //    _context = context;
        //}


        //假設類別名稱不可以重複
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            string cateName = value.ToString();

            GoodStoreContextG2 _context= validationContext.GetService<GoodStoreContextG2>();


            var result = _context.Category.Where(c => c.CateName == cateName);

            if (result.Any())
            {
                return new ValidationResult("類別名稱不可以重複");
            }


            return ValidationResult.Success;
        }
    }

}
