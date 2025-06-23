using DBFirst.Models;

namespace DBFirst.ViewModels
{
    public class VM_tStudent
    {
        /// <summary>
        /// 科系
        /// </summary>
        public List<Department> Departments { get; set; }
        /// <summary>
        /// 學生
        /// </summary>
        public List<tStudent> Students { get; set; }
    }
}
