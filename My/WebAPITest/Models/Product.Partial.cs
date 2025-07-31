using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPITest.Models
{
    public partial class Product
    {
        [JsonIgnore]
        public virtual Category? Cate { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<OrderDetail>? OrderDetail { get; set; } = new List<OrderDetail>();
    }
}
