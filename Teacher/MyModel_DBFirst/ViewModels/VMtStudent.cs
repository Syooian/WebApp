using MyModel_DBFirst.Models;

namespace MyModel_DBFirst.ViewModels
{

    //5.8.3 撰寫VMtStudent類別
    public class VMtStudent
    {
        public List<tStudent>? Students { get; set; } //tStudent資料表的資料
        public List<Department>? Departments { get; set; }
    }
}
