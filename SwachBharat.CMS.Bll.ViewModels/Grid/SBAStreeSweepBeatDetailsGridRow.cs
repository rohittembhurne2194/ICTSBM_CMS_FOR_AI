using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.Grid
{
   public class SBAStreeSweepBeatDetailsGridRow
    {
        public Nullable<long> RowCounts { get; set; }
        public Nullable<int> userId { get; set; }
        public string EmpName { get; set; }
        public string Date { get; set; }
        public Nullable<int> BeatId { get; set; }
        public string ReferanceId1 { get; set; }
        public string ReferanceId2 { get; set; }
        public string ReferanceId3 { get; set; }
        public string Status { get; set; }
    }
}
