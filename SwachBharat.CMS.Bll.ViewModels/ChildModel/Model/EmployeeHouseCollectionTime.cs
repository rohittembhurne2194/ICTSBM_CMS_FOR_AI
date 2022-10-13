using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
  public  class EmployeeHouseCollectionTime
    {

        public Nullable<int> MixedCount { get; set; }
        public Nullable<int> Bifur { get; set; }
        public Nullable<int> NotCollected { get; set; }
        public Nullable<int> NotSpecidfied { get; set; }

        public Nullable<int> DryWaste { get; set; }

        public Nullable<int> WetWaste { get; set; }
        public string inTime { get; set; }
        public Nullable<int> Count { get; set; }

        public int userId { get; set; }
        public string userName { get; set; }
        public string gcTarget { get; set; }
        public string ToDate { get; set; }
        public Nullable<int> MinuteDiff { get; set; }
        public string TimeDuration { get; set; }
    }
}
