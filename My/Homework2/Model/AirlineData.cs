namespace Homework2.Model
{
    /// <summary>
    /// 航空公司
    /// </summary>
    public class Airline
    {
        public string AirlineID { get; set; }
        public AirlineName AirlineName { get; set; }
        public AirlineNameAlias AirlineNameAlias { get; set; }
        public string AirlineIATA { get; set; }
        public string AirlineICAO { get; set; }
        public string AirlineEmail { get; set; }
        public string AirlineAddress { get; set; }
        public string AirlinePhone { get; set; }
        public string AirlineNationality { get; set; }
        public DateTime UpdateTime { get; set; }
        public int VersionID { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AirlineName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AirlineNameAlias
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }
}

