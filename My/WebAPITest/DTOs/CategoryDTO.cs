using WebAPITest.Models;

namespace WebAPITest.DTOs
{
    public class CategoryDTO
    {
        public string CateID { get; set; } = null!;

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
}
