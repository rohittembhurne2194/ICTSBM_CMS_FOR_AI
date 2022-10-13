using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Grid
{
   public class SBALocationGridRow
    {
        public int locId { get; set; }
        public string userName { get; set; }
        public string date { get; set; }
        public string time{ get; set; }
        public string latlong { get; set; }
        public int userId { get; set; }
        public Nullable<DateTime> CompareDate { get; set; }
    }
}
