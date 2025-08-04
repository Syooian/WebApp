using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Models;

public partial class PayType
{
    [Key]
    [StringLength(2)]
    public string PayCode { get; set; } = null!;

    [Column("PayType")]
    [StringLength(10)]
    public string PayType1 { get; set; } = null!;

    [InverseProperty("PayCodeNavigation")]
    public virtual ICollection<Order> Order { get; set; } = new List<Order>();
}
