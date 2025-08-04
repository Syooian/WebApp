using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Models;

public partial class Room
{
    [Key]
    [StringLength(5)]
    public string RoomID { get; set; } = null!;

    [StringLength(40)]
    public string RoomName { get; set; } = null!;

    public byte PeopleNum { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [StringLength(1)]
    public string Area { get; set; } = null!;

    public byte Floor { get; set; }

    [StringLength(400)]
    public string Introduction { get; set; } = null!;

    public string? Note { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [StringLength(1)]
    public string StatusCode { get; set; } = null!;

    [InverseProperty("Room")]
    public virtual ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();

    [InverseProperty("Room")]
    public virtual ICollection<RoomPhoto> RoomPhoto { get; set; } = new List<RoomPhoto>();

    [ForeignKey("StatusCode")]
    [InverseProperty("Room")]
    public virtual RoomStatus StatusCodeNavigation { get; set; } = null!;
}
