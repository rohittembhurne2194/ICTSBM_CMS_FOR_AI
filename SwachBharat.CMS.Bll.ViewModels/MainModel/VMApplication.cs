using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GramPanchayat.CMS.Bll.ViewModels
{
    public class VMApplication
    {
        public long id { get; set; }
        public string name { get; set; }
    }

    public class SubscriptionVM
    {
        public long id { get; set; }
        public string name { get; set; }
    }
}