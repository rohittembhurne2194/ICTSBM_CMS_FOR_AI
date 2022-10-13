using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.Grid
{
    public class SBAEmpolyeeSummaryGrid
    {
        public int daID { get; set; }
        public string UserName { get; set; }
        public string daDate { get; set; }
        public string StartTime { get; set; }
        public string DaEndDate { get; set; }
        public string EndTime { get; set; }
        public string IdelTime { get; set; }
        public string Totalhousecollection { get; set; }
        public string Totaldistance { get; set; }
        public string Totaldumpyard { get; set; }
        public int userId { get; set; }

        public string daDateTIme { get; set; }
        public string InBatteryStatus { get; set; }

        public string OutBatteryStatus { get; set; }
        public string TotalHouseScanTimeHours { get; set; }
        public string TotalDumpScanTimeHours { get; set; }
    }
}
