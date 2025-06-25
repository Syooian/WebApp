using System;
using System.Collections.Generic;

namespace NorthwindStore.Models;

public partial class Suppliers
{
    public int SupplierID { get; set; }

    public string CompanyName { get; set; } = null!;

    public string ContactName { get; set; } = null!;

    public string ContactTitle { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? PostalCode { get; set; }

    public string Phone { get; set; } = null!;

    public string? Fax { get; set; }

    public virtual ICollection<Products> Products { get; set; } = new List<Products>();
}
