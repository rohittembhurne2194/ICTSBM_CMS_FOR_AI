using System.Web;
using System.Web.Mvc;

namespace SwachhBharatAbhiyan.CMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
