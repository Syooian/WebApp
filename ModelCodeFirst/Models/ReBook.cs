namespace ModelCodeFirst.Models
{
    /// <summary>
    /// 留言回覆
    /// </summary>
    public class ReBook : BookBase
    {
        /// <summary>
        /// 回覆留言編號
        /// </summary>
        public string ReID { get; set; } = null!;
    }
}
