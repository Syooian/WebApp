using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyModel_DBFirst.Models;

public partial class tStudent
{
    //3.2 撰寫在View上顯示的欄位內容(Display)

    [Display(Name ="學號")]
    public string fStuId { get; set; } = null!;

    [Display(Name = "姓名")]
    public string fName { get; set; } = null!;

    [Display(Name = "電子郵件")]
    public string? fEmail { get; set; }

    [Display(Name = "成績")]
    public int? fScore { get; set; }
}
