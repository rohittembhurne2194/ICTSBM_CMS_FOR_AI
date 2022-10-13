using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.Grid
{
  public class SBAGarbageCountDetails
    {

        public string UserName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int _Count { get; set; }

        public string StartTime { get; set; }

        public string Target { get; set; }
    }
}
