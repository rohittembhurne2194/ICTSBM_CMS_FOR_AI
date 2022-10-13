using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GramPanchayat.CMS.Bll.ViewModels.ChildModel.Grid
{
   public  class AdsClassificationGridRow
    {
        public int cId { get; set; }
     
        public string userName { get; set; }
        public string image { get; set; }
        public Nullable<int> days { get; set; }
        public string fDate { get; set; }
        public string tDate { get; set; }
        public string address { get; set; }
        public string mobile { get; set; }
        public string emailId { get; set; }
        public string RenewDays { get; set; }
        public string Type { get; set; }
        public string date { get; set; }

        public string city { get; set; }
        public Nullable<int> reNewId { get; set; }
      
        public Nullable<System.DateTime> modifiedDate { get; set; }
        public string classifyName { get; set; }
        public Nullable<DateTime> fromDate { get; set; }
        public Nullable<DateTime> toDate { get; set; }
        public Nullable<DateTime> createdDate { get; set; }
    }
}
