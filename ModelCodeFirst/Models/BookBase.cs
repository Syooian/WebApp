namespace ModelCodeFirst.Models
{
    public abstract class BookBase
    {
        /// <summary>
        /// 內文
        /// </summary>
        public string Description { get; set; } = null!;
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; } = null!;
        /// <summary>
        /// 發表日期
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
