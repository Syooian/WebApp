namespace Homework2.Model
{
    /// <summary>
    /// 航班
    /// </summary>
    public class FlightData
    {
        public DateTime FlightDate { get; set; }
        public string FlightNumber { get; set; }
        public int AirRouteType { get; set; }
        public string AirlineID { get; set; }
        public string DepartureAirportID { get; set; }
        public string ArrivalAirportID { get; set; }
        public DateTime ScheduleDepartureTime { get; set; }
        public DateTime ActualDepartureTime { get; set; }
        public DateTime EstimatedDepartureTime { get; set; }
        public DateTime ScheduleArrivalTime { get; set; }
        public DateTime ActualArrivalTime { get; set; }
        public DateTime EstimatedArrivalTime { get; set; }
        public string DepartureRemark { get; set; }
        public string DepartureRemarkEn { get; set; }
        public string ArrivalRemark { get; set; }
        public string ArrivalRemarkEn { get; set; }
        public string FlightStatus { get; set; }
        public string FlightStatusEn { get; set; }
        public string FlightStatusPC { get; set; }
        public string FlightRemark { get; set; }
        public string ArrivalTerminal { get; set; }
        public string DepartureTerminal { get; set; }
        public string ArrivalGate { get; set; }
        public string DepartureGate { get; set; }
        public string ArrivalApron { get; set; }
        public string DepartureApron { get; set; }
        public string CodeShare { get; set; }
        public bool IsCargo { get; set; }
        public string AcType { get; set; }
        public string BaggageClaim { get; set; }
        public string CheckCounter { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
