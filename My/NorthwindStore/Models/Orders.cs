using System;
using System.Collections.Generic;

namespace NorthwindStore.Models;

public partial class Orders
{
    public int OrderID { get; set; }

    public string CustomerID { get; set; } = null!;

    public int EmployeeID { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime? RequiredDate { get; set; }

    public DateTime? ShippedDate { get; set; }

    public int ShipVia { get; set; }

    public decimal? Freight { get; set; }

    public string ShipName { get; set; } = null!;

    public string ShipAddress { get; set; } = null!;

    public string? ShipPostalCode { get; set; }

    public virtual Customers Customer { get; set; } = null!;

    public virtual Employees Employee { get; set; } = null!;

    public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();

    public virtual Shippers ShipViaNavigation { get; set; } = null!;
}
