using System.Text.Json.Serialization;
using MyWebAPI.Models;

namespace MyWebAPI.DTOs
{
    //4.5.2 建立CategoryDTO類別
    public class CategoryDTO
    {
        public string CateID { get; set; } = null!;

        public string CateName { get; set; } = null!;

        //4.5.9 在CategoryDTO裡加入統計該類別有幾種商品的屬性
        public int ProductCount 
        { 
            get {
                return Product.Count;
            } 
        }

        public virtual ICollection<Product> Product { get; set; } = new List<Product>();

       

    }
}

