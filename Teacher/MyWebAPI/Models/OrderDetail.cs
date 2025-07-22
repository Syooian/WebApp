using System;
using System.Collections.Generic;

namespace MyWebAPI.Models;

public partial class OrderDetail
{
    public string OrderID { get; set; } = null!;

    public string ProductID { get; set; } = null!;

    public int Qty { get; set; }

    public decimal Price { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
