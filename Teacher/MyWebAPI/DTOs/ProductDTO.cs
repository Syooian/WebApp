using System.Text.Json.Serialization;
using MyWebAPI.Models;

namespace MyWebAPI.DTOs
{
    //4.2.2 建立ProductDTO類別
    public class ProductDTO
    {
        public string ProductID { get; set; } = null!;

        public string ProductName { get; set; } = null!;

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string Picture { get; set; } = null!;

        public string CateID { get; set; } = null!;

        //在這裡補上CateName屬性
        public string CateName { get; set; } = null!;

        //4.5.10 在ProductDTO裡也加入一些統計資料的屬性
        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();

        public int BuyCount
        {
            get
            {
                return OrderDetail.Count;
            }
        }

        public int TotalQty
        {
            get
            {
                return OrderDetail.Sum(p=>p.Qty);
            }
        }

        public decimal SumOfBusiness
        {
            get
            {
                return OrderDetail.Sum(p => p.Qty*p.Price);
            }
        }


    }
}
