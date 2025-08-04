using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Models;

public partial class RoomStatus
{
    [Key]
    [StringLength(1)]
    public string StatusCode { get; set; } = null!;

    [StringLength(10)]
    public string Status { get; set; } = null!;

    [InverseProperty("StatusCodeNavigation")]
    public virtual ICollection<Room> Room { get; set; } = new List<Room>();
}
