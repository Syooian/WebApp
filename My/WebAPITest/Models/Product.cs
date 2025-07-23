using System;
using System.Collections.Generic;

namespace WebAPITest.Models;

public partial class Product
{
    public string ProductID { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public string Picture { get; set; } = null!;

    //public DateTime CreatedDate { get; set; }

    public string CateID { get; set; } = null!;
}
