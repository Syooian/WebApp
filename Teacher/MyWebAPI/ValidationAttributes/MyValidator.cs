using MyWebAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.ValidationAttributes
{
    //8.0.2 將原本寫在ProductPostDTO及CategoryPostDTO內的自訂驗證類別(ValidationAttributes)貼進MyValidator.cs檔案中
    public class MyValidator
    {

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

                GoodStoreContextG2 _context = validationContext.GetService<GoodStoreContextG2>();


                var result = _context.Category.Where(c => c.CateName == cateName);

                if (result.Any())
                {
                    return new ValidationResult("類別名稱不可以重複");
                }


                return ValidationResult.Success;
            }
        }


        public class ProudctNameCheck : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                string PName = value.ToString();

                if (PName.Length < 3)
                {
                    return new ValidationResult("商品名稱至少三個字");
                }

                return ValidationResult.Success;
            }

        }


    }
}
