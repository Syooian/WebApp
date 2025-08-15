namespace Homework2.Model
{
    /// <summary>
    /// 航空公司
    /// </summary>
    public class Airline
    {
        public string AirlineID { get; set; }
        public Name AirlineName { get; set; }
        public Name AirlineNameAlias { get; set; }
        public string AirlineIATA { get; set; }
        public string AirlineICAO { get; set; }
        public string AirlineEmail { get; set; }
        public string AirlineAddress { get; set; }
        public string AirlinePhone { get; set; }
        public string AirlineNationality { get; set; }
        public DateTime UpdateTime { get; set; }
        public int VersionID { get; set; }
    }
}

