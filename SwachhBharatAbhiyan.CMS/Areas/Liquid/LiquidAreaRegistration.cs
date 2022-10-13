using System.Web.Mvc;

namespace SwachhBharatAbhiyan.CMS.Areas.Liquid
{
    public class LiquidAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Liquid";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Liquid_default",
                "Liquid/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional },
                new { controller = "LiquidHome|LiquidWaste|LiquidEmployee|LiquidAttendence|LiquidGarbage|LiquidAndroid|LiquidMainMaster|LiquidAccount|LiquidLocation|LiquidDumpYard|LiquidReport" },
               new[] { "SwachhBharatAbhiyan.CMS.Areas.Liquid.Controllers" }
            );
        }
    }
}