using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Models;

public partial class RoomService
{
    [Key]
    [StringLength(8)]
    public string RoomServiceID { get; set; } = null!;

    [StringLength(5)]
    public string RoomID { get; set; } = null!;

    [StringLength(20)]
    public string Subject { get; set; } = null!;

    [StringLength(500)]
    public string ServiceContent { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedTime { get; set; }

    [StringLength(1000)]
    public string? ProcessingDiscription { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CompletionTime { get; set; }

    [StringLength(5)]
    public string MemberID { get; set; } = null!;

    [StringLength(4)]
    public string? EmployeeID { get; set; }

    [StringLength(1)]
    public string PSCode { get; set; } = null!;

    [ForeignKey("EmployeeID")]
    [InverseProperty("RoomService")]
    public virtual Employee? Employee { get; set; }

    [ForeignKey("MemberID")]
    [InverseProperty("RoomService")]
    public virtual Member Member { get; set; } = null!;

    [ForeignKey("PSCode")]
    [InverseProperty("RoomService")]
    public virtual ProcessingStatus PSCodeNavigation { get; set; } = null!;
}
