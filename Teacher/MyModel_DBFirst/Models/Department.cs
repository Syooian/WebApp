using System.ComponentModel.DataAnnotations;

namespace MyModel_DBFirst.Models
{
    public class Department
    {
        [Key]
        public string DeptID { get; set; } = null!;

        public string DeptName { get; set; } = null!;

        public virtual List<tStudent>? tStudents { get; set; }//這個屬性是在描述它與tStudnet是一對多的關聯

    }
}
