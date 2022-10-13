 
using SwachBharat.CMS.Dal.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.Services
{
    public abstract class AppService
    {
        protected DevChildSwachhBharatNagpurEntities db;
        protected DevSwachhBharatMainEntities dbMain;
        public AppService(int AppId)
        {
            db = new DevChildSwachhBharatNagpurEntities(AppId);
            dbMain = new DevSwachhBharatMainEntities();
        }
    }
}
