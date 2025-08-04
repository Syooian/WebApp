using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Models;

public partial class ProcessingStatus
{
    [Key]
    [StringLength(1)]
    public string PSCode { get; set; } = null!;

    [Column("ProcessingStatus")]
    [StringLength(20)]
    public string ProcessingStatus1 { get; set; } = null!;

    [InverseProperty("PSCodeNavigation")]
    public virtual ICollection<RoomService> RoomService { get; set; } = new List<RoomService>();
}
