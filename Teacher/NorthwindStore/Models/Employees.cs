using System;
using System.Collections.Generic;

namespace NorthwindStore.Models;

public partial class Employees
{
    public int EmployeeID { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string TitleOfCourtesy { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public DateTime HireDate { get; set; }

    public string Address { get; set; } = null!;

    public string? PostalCode { get; set; }

    public string HomePhone { get; set; } = null!;

    public string? Extension { get; set; }

    public byte[] Photo { get; set; } = null!;

    public string? Notes { get; set; }

    public int? ReportsTo { get; set; }

    public virtual ICollection<Employees> InverseReportsToNavigation { get; set; } = new List<Employees>();

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();

    public virtual Employees? ReportsToNavigation { get; set; }
}
