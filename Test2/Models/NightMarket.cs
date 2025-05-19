namespace Test2
{
    public struct NightMarket
    {
        /// <summary>
        /// 夜市編號
        /// </summary>
        public string ID { get; set; } = null!;
        /// <summary>
        /// 夜市名稱
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 夜市地址
        /// </summary>
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
