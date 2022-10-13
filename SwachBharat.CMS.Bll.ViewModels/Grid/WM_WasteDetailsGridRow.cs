using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.Grid
{
    public class WM_WasteDetailsGridRow
    {
        public int GarbageDetailsID { get; set; }
        public Nullable<int> SubCategoryID { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<int> UserId { get; set; }
        public String CreatedDate { get; set; }
        public string UserName { get; set; }
        public string DisplayTime { get; set; }
        
    }
}
