using System;
using System.Collections.Generic;

namespace DBFirst_MySql2.Models;

public partial class tstudent
{
    public string fStuId { get; set; } = null!;

    public string fName { get; set; } = null!;

    public string fEmail { get; set; } = null!;

    public int? fScore { get; set; }
}
