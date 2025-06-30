namespace MyModel_CodeFirst.Models
{
    //1.1.2 設計Book類別的各屬性，包括名稱、資料類型及其相關的驗證規則及顯示名稱(Display)
    public class Book
    {
        //屬性封裝
        public string BookID { get; set; }=null!; //留言編號, 採用GUID

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string? Photo { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
