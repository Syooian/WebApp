using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace MyModel_CodeFirst.Models
{
    //1.1.4 設計ReBook類別的各屬性，包括名稱、資料類型及其相關的驗證規則及顯示名稱(Display)
    public partial class ReBook
    {
        //屬性封裝
        public string ReBookID { get; set; } = null!; //留言編號, 採用GUID

        public string Description { get; set; } = null!;

        public string Author { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        //1.1.5 撰寫兩個類別間的關聯屬性做為未來資料表之間的關聯
        public string BookID { get; set; } = null!;
       
        public virtual Book? Book { get; set; } //一個ReBook只能屬於一個Book
    }
}
