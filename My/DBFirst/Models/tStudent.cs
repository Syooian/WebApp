using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBFirst.Models;

public partial class tStudent
{
    /// <summary>
    /// 學號
    /// </summary>
    [Display(Name = "學號")]
    //[StringLength(6, ErrorMessage = "學號長度需6個字元。", MinimumLength = 6)]
    [RegularExpression(@"^[0-9]{6}$", ErrorMessage = "學號只能包含數字，且長度為6個字元。")]
    public string fStuId { get; set; } = null!;
    /// <summary>
    /// 姓名
    /// </summary>
    [Display(Name = "姓名")]
    [Required(ErrorMessage = "姓名為必填欄位。")]
    public string fName { get; set; } = null!;
    /// <summary>
    /// 電子郵件
    /// </summary>
    [Display(Name = "電子郵件")]
    [EmailAddress(ErrorMessage = "請輸入有效的電子郵件地址。")]
    public string? fEmail { get; set; }
    /// <summary>
    /// 成績
    /// </summary>
    [Display(Name = "成績")]
    [Range(0, 100, ErrorMessage = "成績必須介於0到100之間。")]
    public int? fScore { get; set; }
    /// <summary>
    /// 科系ID
    /// </summary>
    [Display(Name = "科系")]
    [ForeignKey("Department")]//用標籤方式來表示關聯
    public string DeptID { get; set; } = null!;
    /// <summary>
    /// 
    /// <para>描述此與tStudent是一對多的關聯</para>
    /// </summary>
    public virtual Department? Department { get; set; }//virtual表示描述與Department的關聯(不重要)
}
