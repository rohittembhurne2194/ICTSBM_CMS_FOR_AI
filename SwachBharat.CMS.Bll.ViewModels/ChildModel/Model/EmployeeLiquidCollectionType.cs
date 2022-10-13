using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
   public class EmployeeLiquidCollectionType
    {
        public Nullable<int> LiquidCollectionCount { get; set; }
      

        public Nullable<int> DryWaste { get; set; }

        public Nullable<int> WetWaste { get; set; }
        public string inTime { get; set; }
        public Nullable<int> Count { get; set; }

        public int userId { get; set; }
        public string userName { get; set; }
        public string gcTarget { get; set; }
        public string ToDate { get; set; }
    }
}
