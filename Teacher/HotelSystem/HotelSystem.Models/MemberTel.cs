using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Models;

public partial class MemberTel
{
    [Key]
    public long SN { get; set; }

    [StringLength(20)]
    public string Tel { get; set; } = null!;

    [StringLength(5)]
    public string MemberID { get; set; } = null!;

    [ForeignKey("MemberID")]
    [InverseProperty("MemberTel")]
    public virtual Member Member { get; set; } = null!;
}
