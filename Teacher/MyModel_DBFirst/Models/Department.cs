using System.ComponentModel.DataAnnotations;

namespace MyModel_DBFirst.Models
{
    public class Department
    {
        //5.6.6 編輯Department 模型
        [Key]
        [Display(Name ="科系代碼")]
        [StringLength(2,ErrorMessage ="最多2碼")]
        [Required(ErrorMessage = "必填")]
        public string DeptID { get; set; } = null!;

        [Display(Name = "科系名稱")]
        [StringLength(30, ErrorMessage = "最多30個字")]
        [Required(ErrorMessage = "必填")]
        public string DeptName { get; set; } = null!;

        public virtual List<tStudent>? tStudents { get; set; }//這個屬性是在描述它與tStudnet是一對多的關聯

    }
}
