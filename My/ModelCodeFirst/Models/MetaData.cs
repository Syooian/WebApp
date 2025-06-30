using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelCodeFirst.Models;

/// <summary>
/// 
/// </summary>
public class BookData
{
    /// <summary>
    /// 留言編號
    /// <para>GUID</para>
    /// </summary>
    [Display(Name = "留言編號")]
    [StringLength(36, MinimumLength = 36)]
    [Key]
    public string ID { get; set; } = null!;
    /// <summary>
    /// 標題
    /// </summary>
    [Display(Name = "標題")]
    public string Title { get; set; } = null!;
    /// <summary>
    /// 照片
    /// <para>檔名對應<see cref="ID"/></para>
    /// </summary>
    [Display(Name = "照片")]
    [StringLength(40)]//GUID+.jpg
    public string? Photo { get; set; } = null!;

    #region 共用項目
    /// <summary>
    /// 內文
    /// </summary>
    [Display(Name = "內文")]
    [Required(ErrorMessage = "請輸入內文")]
    [DataType(DataType.MultilineText)]//標註多行文字
    public string Description { get; set; } = null!;
    /// <summary>
    /// 作者
    /// </summary>
    [Display(Name = "作者")]
    [StringLength(20, ErrorMessage = "不能超過20個字")]
    public string Author { get; set; } = null!;
    /// <summary>
    /// 發表日期
    /// </summary>
    [Display(Name = "發表日期")]
    [DataType(DataType.Date)]//給View看的
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss")]//讀出來時的格式化日期
    //[DisplayFormat(DataFormatString = "{0})]//按照原始格式
    [HiddenInput]//隱藏在View中
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    #endregion
}
/// <summary>
/// 
/// </summary>
[ModelMetadataType(typeof(BookData))]
public partial class Book
{

}

/// <summary>
/// 
/// </summary>
public class ReBookData
{
    /// <summary>
    /// 回覆留言編號
    /// </summary>
    [Display(Name = "回覆留言編號")]
    [Key]
    public string ReID { get; set; } = null!;

    #region 共用項目
    /// <summary>
    /// 內文
    /// </summary>
    [Display(Name = "內文")]
    [Required(ErrorMessage = "請輸入內文")]
    [DataType(DataType.MultilineText)]//標註多行文字
    public string Description { get; set; } = null!;
    /// <summary>
    /// 作者
    /// </summary>
    [Display(Name = "作者")]
    [StringLength(20, ErrorMessage = "不能超過20個字")]
    public string Author { get; set; } = null!;
    /// <summary>
    /// 發表日期
    /// </summary>
    [Display(Name = "發表日期")]
    [DataType(DataType.Date)]//給View看的
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss")]//讀出來時的格式化日期
    //[DisplayFormat(DataFormatString = "{0})]//按照原始格式
    [HiddenInput]//隱藏在View中
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    #endregion
}
/// <summary>
/// 
/// </summary>
[ModelMetadataType(typeof(ReBookData))]
public partial class ReBook
{

}