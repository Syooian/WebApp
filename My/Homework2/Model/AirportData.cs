namespace Homework2.Model
{
    /// <summary>
    /// 機場
    /// </summary>
    public class Airport
    {
        public string AirportID { get; set; }
        public AirportName AirportName { get; set; }
        public string AirportIATA { get; set; }
        public string AirportICAO { get; set; }
        public AirportPosition AirportPosition { get; set; }
        public AirportCityName AirportCityName { get; set; }
        public string AirportAddress { get; set; }
        public string AirportPhone { get; set; }
        public string AirportNationality { get; set; }
        public string AuthorityID { get; set; }
        public DateTime UpdateTime { get; set; }
        public int VersionID { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AirportName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AirportPosition
    {
        public int PositionLon { get; set; }
        public int PositionLat { get; set; }
        public string GeoHash { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AirportCityName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }
}
