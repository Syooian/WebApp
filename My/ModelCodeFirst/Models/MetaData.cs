using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelCodeFirst.Models;

/// <summary>
/// 
/// </summary>
public class BookData : BookBase
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
public class ReBookData : BookBase
{
    /// <summary>
    /// 回覆留言編號
    /// </summary>
    [Display(Name = "回覆留言編號")]
    [Key]
    public string ReID { get; set; } = null!;
}
/// <summary>
/// 
/// </summary>
[ModelMetadataType(typeof(ReBookData))]
public partial class ReBook
{

}