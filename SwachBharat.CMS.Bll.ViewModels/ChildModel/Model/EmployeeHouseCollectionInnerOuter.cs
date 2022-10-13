using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
    public class EmployeeHouseCollectionInnerOuter
    {
        public Nullable<int> InnerCount { get; set; }

        public Nullable<int> OuterCount { get; set; }
        public Nullable<int> TotalCount { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public string inTime { get; set; }
        public string ToDate { get; set; }

    }
}
