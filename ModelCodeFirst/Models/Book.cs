namespace ModelCodeFirst.Models
{
    /// <summary>
    /// 留言
    /// </summary>
    public class Book
    {
        /// <summary>
        /// 留言編號
        /// </summary>
        public string ID { get; set; } = null!;
        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        public string? Photo { get; set; } = null!;
    }
}
