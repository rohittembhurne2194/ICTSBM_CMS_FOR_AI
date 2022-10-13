using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
  public  class GetEmpWiseHouseScan
    {
      
        public int userId { get; set; }
        public string userName { get; set; }
        public Nullable<int> TotalHouseScanTime { get; set; }
        public Nullable<int> Totalhousecollection { get; set; }
        public Nullable<int> TotalMixed { get; set; }
        public Nullable<int> TotalSeg { get; set; }
        public Nullable<int> TotalNotColl { get; set; }
        public Nullable<int> TotalNotSpecified { get; set; }

        public Nullable<int> TotalDump { get; set; }

        public Nullable<int> TotalDumpScanTime { get; set; }
        public string TotalHouseScanTimeHours { get; set; }
        public string TotalDumpScanTimeHours { get; set; }
    }
}
