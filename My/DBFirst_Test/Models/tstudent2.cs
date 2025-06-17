using System;
using System.Collections.Generic;

namespace DBFirst_Test.Models;

public partial class tstudent2
{
    /// <summary>
    /// 學號
    /// </summary>
    public string fStuId { get; set; } = null!;

    /// <summary>
    /// 姓名
    /// </summary>
    public string fName { get; set; } = null!;

    /// <summary>
    /// 電子郵件
    /// </summary>
    public string fEmail { get; set; } = null!;

    /// <summary>
    /// 分數
    /// </summary>
    public int? fScore { get; set; }

    /// <summary>
    /// 科系ID
    /// </summary>
    public string DeptID { get; set; } = null!;

    public virtual department Dept { get; set; } = null!;
}
