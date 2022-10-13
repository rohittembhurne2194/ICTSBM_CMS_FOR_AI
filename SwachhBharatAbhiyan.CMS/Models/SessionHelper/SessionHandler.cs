using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SwachhBharatAbhiyan.CMS.Models.SessionHelper
{
    public class SessionHandler
    {
        public string DB_Name { get; set; }
        public string DB_Source { get; set; }
        public string Property1 { get; set; }
        public string UserId { get; set; }
        public string UserRole { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public int AppId { get; set; }
        public string AppName { get; set; }
        public bool IsLoggedIn { get; set; }
        public string Latitude { get; set; }
        public string Logitude { get; set; }
        public string Type { get; set; }
        public string sessionType { get; set; }

        public Nullable<int> YoccClientID { get; set; }

        public Nullable<int> GramPanchyatAppID { get; set; }

        public string YoccFeddbackLink { get; set; }
        public string YoccDndLink { get; set; }

        //private constructor
        public SessionHandler()
        {
            Property1 = "default value";
        }

        // Gets the current session.
        public static SessionHandler Current
        {
            get
            {
                SessionHandler session =
                (SessionHandler)HttpContext.Current.Session["__MySession__"];
                if (session == null)
                {
                    session = new SessionHandler();
                    HttpContext.Current.Session["__MySession__"] = session;
                }
                return session;
            }
        }


    }
}