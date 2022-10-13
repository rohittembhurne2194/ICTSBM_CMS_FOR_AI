using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
    public class DumpYardDetailsVM : BaseVM
    {
        public int dyId { get; set; }
        public string dyName { get; set; }
        public string dyNameMar { get; set; }
        public string dyLat { get; set; }
        public string dyLong { get; set; }
        public string dyQRCode { get; set; }
        public Nullable<int> ZoneId { get; set; }
        public Nullable<int> WardNo { get; set; }
        public Nullable<int> areaId { get; set; }
        public string ReferanceId { get; set; }
        public string dyAddress { get; set; }
        public Nullable<System.DateTime> lastModifiedDate { get; set; }

        public string EmployeType { get; set; }
    }
}
