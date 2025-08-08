using Microsoft.AspNetCore.Mvc.Filters;

namespace HotelSystem.Filters
{
    public class LogFilter:IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
         
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.RouteData.Values["controller"];
            var action = context.RouteData.Values["action"];
            var id=context.RouteData.Values["id"];

            var agent = context.HttpContext.Request.Headers["User-Agent"].ToString();
            var ip = context.HttpContext.Connection.RemoteIpAddress?.ToString();

            string user = "Guest";
            var time = DateTime.Now;


            string logMessage = $"{time} - {user} - {ip} - {agent} - {controller}/{action}/{id}";


            // 寫入日誌系統
            string filePath = "LogFiles/ActionLog.txt";

            if (!Directory.Exists("LogFiles"))
            {
                Directory.CreateDirectory("LogFiles");
            }

            //寫檔
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(logMessage);
            }
        }
    }
}
