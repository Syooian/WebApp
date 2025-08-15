namespace Homework2.Services
{
    public class FlightInformationServices
    {
        /// <summary>
        /// 
        /// </summary>
        HttpClient Client;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Client"></param>
        public FlightInformationServices(HttpClient Client)
        {
            this.Client = Client;
        }
    }
}
