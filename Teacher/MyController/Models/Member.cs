namespace MyController.Models
{
    public class Member
    {
        //1.撰寫會員資料表的資料模型(複雜繫結法必須要有模型Model)
        //properties
        public string MemberID { get; set; } = null!;
        public string MemberName { get; set; } = null!;
        public string? Address { get; set; }
        public string Phone { get; set; } = null!;
        public bool Gender { get; set; }
    }
}
