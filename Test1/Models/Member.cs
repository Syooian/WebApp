namespace Test1.Models
{
    public class Member
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// <para>不可為Null</para>
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 
        /// <para>可為Null</para>
        /// </summary>
        public string? Phone { get; set; }
    }
}
