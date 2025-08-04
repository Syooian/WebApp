using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Models;

[Keyless]
public partial class view_EmployeeWithRole
{
    [StringLength(4)]
    public string EmployeeID { get; set; } = null!;

    [StringLength(40)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime HireDate { get; set; }

    [StringLength(50)]
    public string Address { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime Birthday { get; set; }

    [StringLength(20)]
    public string Tel { get; set; } = null!;

    [StringLength(30)]
    public string Account { get; set; } = null!;

    [StringLength(200)]
    public string Password { get; set; } = null!;

    [StringLength(1)]
    public string RoleCode { get; set; } = null!;

    [StringLength(15)]
    public string RoleName { get; set; } = null!;
}
