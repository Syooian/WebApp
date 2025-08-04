using System.ComponentModel.DataAnnotations;
using WebAPITest.Data;

namespace WebAPITest
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductNameCheck : ValidationAttribute//自訂驗證器
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="ValidationContext"></param>
        /// <returns></returns>
        protected override ValidationResult? IsValid(object Value, ValidationContext ValidationContext)
        {
            var ProductName = Value.ToString();

            if (ProductName.Length < 3)
            {
                return new ValidationResult("產品名稱長度不足");
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CateNameCheck : ValidationAttribute//自訂驗證器
    {
        //無法模仿Controller注入Context (注入Context需要特殊方法)
        //readonly GoodStoreContext Context;
        //public CateNameCheck(GoodStoreContext Context)
        //{
        //    this.Context = Context;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="ValidationContext"></param>
        /// <returns></returns>
        protected override ValidationResult? IsValid(object? Value, ValidationContext ValidationContext)
        {
            var CateName = Value?.ToString();
            Console.WriteLine("New CateName : " + CateName);

            //var Context = ValidationContext.GetService(typeof(GoodStoreContext)) as GoodStoreContext;
            var Context = ValidationContext.GetService<GoodStoreContext>();
            var Result = Context.Category.Where(C => C.CateName == CateName).FirstOrDefault();

            if (Result != null)
            {
                Console.WriteLine($"R CateID : {Result.CateID} CateName : {Result.CateName}");

                return new ValidationResult("類別名稱重複");
            }

            Console.WriteLine("R null");

            return ValidationResult.Success;
        }
    }
}
