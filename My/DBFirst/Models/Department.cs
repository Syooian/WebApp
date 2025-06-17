using System.ComponentModel.DataAnnotations;

namespace DBFirst.Models
{
    public class Department
    {
        /// <summary>
        /// 科系ID
        /// </summary>
        [Display(Name = "科系ID")]
        [Key] // 標記為主鍵
        public string DeptID { get; set; } = null!;
        /// <summary>
        /// 科系
        /// </summary>
        [Display(Name = "科系")]
        public string DeptName { get; set; } = null!;
        /// <summary>
        /// 
        /// <para>描述此與tStudent是一對多的關聯</para>
        /// </summary>
        public virtual List<tStudent>? tStudents { get; set; }////virtual表示描述與tStudent的關聯(不重要)
    }
}
