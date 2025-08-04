using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Models;

public partial class RoomPhoto
{
    [Key]
    public long SN { get; set; }

    [StringLength(50)]
    public string PhotoPath { get; set; } = null!;

    [StringLength(5)]
    public string RoomID { get; set; } = null!;

    [ForeignKey("RoomID")]
    [InverseProperty("RoomPhoto")]
    public virtual Room Room { get; set; } = null!;
}
