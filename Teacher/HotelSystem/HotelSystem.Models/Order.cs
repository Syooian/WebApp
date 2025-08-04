using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Models;

public partial class Order
{
    [Key]
    [StringLength(12)]
    public string OrderID { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ExpectedCheckInDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ExpectedCheckOutDate { get; set; }

    [StringLength(200)]
    public string? Note { get; set; }

    [StringLength(5)]
    public string MemberID { get; set; } = null!;

    [StringLength(4)]
    public string? EmployeeID { get; set; }

    [StringLength(2)]
    public string PayCode { get; set; } = null!;

    [StringLength(1)]
    public string StatusCode { get; set; } = null!;

    [ForeignKey("EmployeeID")]
    [InverseProperty("Order")]
    public virtual Employee? Employee { get; set; }

    [ForeignKey("MemberID")]
    [InverseProperty("Order")]
    public virtual Member Member { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();

    [ForeignKey("PayCode")]
    [InverseProperty("Order")]
    public virtual PayType PayCodeNavigation { get; set; } = null!;

    [ForeignKey("StatusCode")]
    [InverseProperty("Order")]
    public virtual OrderStatus StatusCodeNavigation { get; set; } = null!;
}
