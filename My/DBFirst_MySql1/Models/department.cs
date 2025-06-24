using System;
using System.Collections.Generic;

namespace DBFirst_Test.Models;

/// <summary>
/// 科系
/// </summary>
public partial class department
{
    /// <summary>
    /// 科系ID
    /// </summary>
    public string DeptID { get; set; } = null!;

    /// <summary>
    /// 科系名稱
    /// </summary>
    public string DeptName { get; set; } = null!;

    public virtual ICollection<tstudent2> tstudent2 { get; set; } = new List<tstudent2>();
}
