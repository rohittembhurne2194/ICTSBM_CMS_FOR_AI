using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
  public  class ComplaintVM
    {
        public string userId { get; set; }
        public int complaintId { get; set; }
        public string complaintType { get; set; }
        public string complaintTypeMar { get; set; }
        public string place { get; set; }
        public string wardNo { get; set; }
        public string details { get; set; }
        public string createdDate { get; set; }
        public DateTime createdDate2 { get; set; }
        public string status { get; set; }
        public string startImage { get; set; }
        public string endImage { get; set; }
        public string comment { get; set; }
        public string tips { get; set; }
        public string refId { get; set; }
        public string type { get; set; }
        public string typeMar { get; set; }
        public string address { get; set; }
        public string houseNumber { get; set; }
    }
}
