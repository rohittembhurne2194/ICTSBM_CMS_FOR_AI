using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Grid
{
    public class SBAHouseScanifyDetailsGridRow
    {
        public int qrEmpId { get; set; }
        public string qrEmpName { get; set; }
        public string qrEmpNameMar { get; set; }
        public string qrEmpMobileNumber { get; set; }
        public string qrEmpAddress { get; set; }
        public string qrEmpLoginId { get; set; }
        public string qrEmpPassword { get; set; }
        public string bloodGroup { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<System.DateTime> lastModifyDate { get; set; }

        public Nullable<int> HouseCount { get; set; }

        public Nullable<int> LiquidCount { get; set; }
        public Nullable<int> StreetCount { get; set; }
        public Nullable<int> DumpCount { get; set; }
        public Nullable<int> PointCount { get; set; }

        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndDate { get; set; }
        public string EndTime { get; set; }



    }
}
