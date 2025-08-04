using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Models;

[Index("Account", Name = "UQ__Employee__B0C3AC46E22D3BC9", IsUnique = true)]
public partial class Employee
{
    [Key]
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

    [InverseProperty("Employee")]
    public virtual ICollection<Order> Order { get; set; } = new List<Order>();

    [ForeignKey("RoleCode")]
    [InverseProperty("Employee")]
    public virtual EmployeeRole RoleCodeNavigation { get; set; } = null!;

    [InverseProperty("Employee")]
    public virtual ICollection<RoomService> RoomService { get; set; } = new List<RoomService>();
}
