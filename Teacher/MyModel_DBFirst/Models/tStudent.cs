using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyModel_DBFirst.Models;

public partial class tStudent
{
    //3.2 撰寫在View上顯示的欄位內容(需using System.ComponentModel.DataAnnotations)
    //3.3 撰寫在表單上的欄位驗證規則

    //假設學號規則:
    //1. 學號為必填欄位，且長度為6個字元
    //2. 學號第一碼為1-9的數字,第二碼到第六碼均為0-9的數字
    [Display(Name ="學號")]
    [Required(ErrorMessage ="必填")]
    [RegularExpression("[1-9][0-9]{5}",ErrorMessage ="格式有誤(ex:114025)")]
    public string fStuId { get; set; } = null!;

    [Display(Name = "姓名")]
    [Required(ErrorMessage = "必填")]
    [StringLength(30,ErrorMessage ="姓名最多30個字元")]
    public string fName { get; set; } = null!;

    [Display(Name = "電子郵件")]
    [StringLength(40, ErrorMessage = "電子郵件最多40個字元")]
    [EmailAddress(ErrorMessage = "電子郵件格式錯誤")]
    public string? fEmail { get; set; }

    [Display(Name = "成績")]
    [Range(0, 100, ErrorMessage = "成績必須介於0到100之間")]
    public int? fScore { get; set; }
}
