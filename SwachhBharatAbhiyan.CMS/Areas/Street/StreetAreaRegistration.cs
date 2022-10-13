using System.Web.Mvc;

namespace SwachhBharatAbhiyan.CMS.Areas.Street
{
    public class StreetAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Street";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Street_default",
                "Street/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new { controller = "StreetHome|StreetAccount|StreetAttendence|StreetSweeping|StreetEmployee|StreetGarbage|StreetLocation|StreetMainMaster|StreetDumpYard|StreetReport" },
               new[] { "SwachhBharatAbhiyan.CMS.Areas.Street.Controllers" }
            );
        }
    }
}