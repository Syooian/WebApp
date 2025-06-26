using System;
using System.Collections.Generic;

namespace NorthwindStore.Models;

public partial class Categories
{
    public int CategoryID { get; set; }

    public string CategoryName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public byte[] Picture { get; set; } = null!;

    public virtual ICollection<Products> Products { get; set; } = new List<Products>();
}
