using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
    public class LiquidWasteVM : BaseVM
    {
        public int LWId { get; set; }
        public string LWName { get; set; }
        public string LWNameMar { get; set; }
        public string LWLat { get; set; }
        public string LWLong { get; set; }
        public string LWQRCode { get; set; }
        public Nullable<int> ZoneId { get; set; }
        public Nullable<int> WardNo { get; set; }
        public Nullable<int> areaId { get; set; }
        public string ReferanceId { get; set; }
        public string LWAddress { get; set; }
        public Nullable<System.DateTime> lastModifiedDate { get; set; }
    }
}
