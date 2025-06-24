using DBFirst_MySql2.Models;

namespace DBFirst_MySql2.ViewModels
{
    public class VM_tStudent2_Department
    {
        /// <summary>
        /// 科系
        /// </summary>
        public List<department>? Departments { get; set; }
        /// <summary>
        /// 學生
        /// </summary>
        public List<tstudent2>? Students { get; set; }
    }
}
