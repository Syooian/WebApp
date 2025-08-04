using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Models;

[PrimaryKey("OrderID", "RoomID")]
public partial class OrderDetail
{
    [Key]
    [StringLength(12)]
    public string OrderID { get; set; } = null!;

    [Key]
    [StringLength(5)]
    public string RoomID { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CheckInTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CheckOutTime { get; set; }

    [ForeignKey("OrderID")]
    [InverseProperty("OrderDetail")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("RoomID")]
    [InverseProperty("OrderDetail")]
    public virtual Room Room { get; set; } = null!;
}
