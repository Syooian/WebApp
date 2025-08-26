using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Models;

public partial class MemberAccount
{
    [Key]
    [StringLength(30)]
    public string Account { get; set; } = null!;

    [StringLength(200)]
    public string Password { get; set; } = null!;

    [StringLength(5)]
    public string MemberID { get; set; } = null!;

    [ForeignKey("MemberID")]
    [InverseProperty("MemberAccount")]
    public virtual Member Member { get; set; } = null!;
}
