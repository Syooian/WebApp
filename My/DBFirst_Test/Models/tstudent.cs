using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBFirst_Test.Models;

public partial class tstudent
{
    [Display(Name = "學號")]
    public string fStuId { get; set; } = null!;
    [Display(Name = "姓名")]
    public string fName { get; set; } = null!;

    public string fEmail { get; set; } = null!;

    public int? fScore { get; set; }
}
