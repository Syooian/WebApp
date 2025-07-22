using System;
using System.Collections.Generic;

namespace MyWebAPI.Models;

public partial class Order
{
    public string OrderID { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public string MemberID { get; set; } = null!;

    public string ContactName { get; set; } = null!;

    public string ContactAddress { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();
}
