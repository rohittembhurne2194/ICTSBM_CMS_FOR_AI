using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.Grid
{
    public class SBAHSAttendanceGrid
    {
        public int qrEmpDaId { get; set; }
        public Nullable<int> qrEmpId { get; set; }
        public string startLat { get; set; }
        public string startLong { get; set; }
        public string endLat { get; set; }
        public string endLong { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string startNote { get; set; }
        public string endNote { get; set; }
        public string batteryStatus { get; set; }
        public string userName { get; set; }
        public Nullable<DateTime> CompareDate { get; set; }

        public int HouseCount { get; set; }
        public int LiquidCount { get; set; }
        public int StreetCount { get; set; }
        public string daDateTIme { get; set; }

        public int DumpYardCount { get; set; }
    }
}
