using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebAPITest.Data;
using WebAPITest.Models;

namespace WebAPITest.DTOs
{
    public class CategoryPostDTO
    {
        public string CateID { get; set; } = null!;

        [CateNameCheck]
        public string CateName { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        //public int ProductCount { get; set; }
        /// <summary>
        /// 計算產品數量
        /// </summary>
        public int ProductCount => Products.Count;
        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();//雖然可以使用原本的模型，但是實務上還是習慣把要Return的東西一律另外做成新的DTO
    }

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
