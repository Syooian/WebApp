using System.ComponentModel.DataAnnotations;

namespace Test2
{
    public class NightMarket
    {
        /// <summary>
        /// 夜市編號
        /// </summary>
        [Display(Name = "編號")]
        public string ID { get; set; } = null!;
        /// <summary>
        /// 夜市名稱
        /// </summary>
        [Display(Name = "名稱")]
        public string Name { get; set; } = null!;
        /// <summary>
        /// 夜市地址
        /// </summary>
        [Display(Name = "地址")]
        public string Address { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <param name="Address"></param>
        public NightMarket(string ID, string Name, string Address)
        {
            this.ID = ID;
            this.Name = Name;
            this.Address = Address;
        }
    }
}
