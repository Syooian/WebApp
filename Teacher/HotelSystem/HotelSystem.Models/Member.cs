using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Models;

public partial class Member
{
    [Key]
    [StringLength(5)]
    public string MemberID { get; set; } = null!;

    [StringLength(40)]
    public string Name { get; set; } = null!;

    [StringLength(10)]
    public string City { get; set; } = null!;

    [StringLength(50)]
    public string Address { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime Birthday { get; set; }

    [InverseProperty("Member")]
    public virtual ICollection<MemberAccount> MemberAccount { get; set; } = new List<MemberAccount>();

    [InverseProperty("Member")]
    public virtual ICollection<MemberTel> MemberTel { get; set; } = new List<MemberTel>();

    [InverseProperty("Member")]
    public virtual ICollection<Order> Order { get; set; } = new List<Order>();

    [InverseProperty("Member")]
    public virtual ICollection<RoomService> RoomService { get; set; } = new List<RoomService>();
}
