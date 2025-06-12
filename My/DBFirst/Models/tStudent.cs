using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBFirst.Models;

public partial class tStudent
{
    /// <summary>
    /// 學號
    /// </summary>
    [Display(Name = "學號")]
    public string fStuId { get; set; } = null!;
    /// <summary>
    /// 姓名
    /// </summary>
    [Display(Name = "姓名")]
    public string fName { get; set; } = null!;
    /// <summary>
    /// 電子郵件
    /// </summary>
    [Display(Name = "電子郵件")]
    public string? fEmail { get; set; }
    /// <summary>
    /// 成績
    /// </summary>
    [Display(Name = "成績")]
    public int? fScore { get; set; }
}
