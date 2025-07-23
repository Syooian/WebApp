using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace MyWebAPI.Models
{
    public class ProductData
    {

        [JsonIgnore]
        public virtual Category? Cate { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();
    }

    [ModelMetadataType(typeof(ProductData))]
    public partial class Product
    { }
}
