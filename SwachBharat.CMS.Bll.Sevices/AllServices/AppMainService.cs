
    
using SwachBharat.CMS.Dal.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.Services
{
    public abstract class AppMainService
    {
        protected DevSwachhBharatMainEntities dbMain;
        public AppMainService()
        {
            dbMain = new DevSwachhBharatMainEntities();
        }
    }
}
