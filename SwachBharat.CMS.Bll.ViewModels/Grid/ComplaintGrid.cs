using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.Grid
{
   public class ComplaintGrid
    {       
        public int complaintId { get; set; }
        public string date { get; set; }
        public string houseNumber { get; set; }
        public string place { get; set; }
        public string wardNo { get; set; }
        public string details { get; set; }          
        public string status { get; set; }
        public string startImage { get; set; }
        public string endImage { get; set; }
        public string comment { get; set; }
        public string tips { get; set; }
        public string refId { get; set; }    
        public string typeMar { get; set; }
        public string address { get; set; }
        public Nullable<DateTime> CompareDate { get; set; }

    }
}
