using MyWebAPI.Models;

namespace MyWebAPI.DTOs
{
    //4.5.2 建立CategoryDTO類別
    public class CategoryDTO
    {
        public string CateID { get; set; } = null!;

        public string CateName { get; set; } = null!;


        public virtual ICollection<Product> Product { get; set; } = new List<Product>();


     
    }
}

