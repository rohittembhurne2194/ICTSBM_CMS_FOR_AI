using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.Grid
{
    public class SBADumpYardDetailsGridRow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameMar { get; set; }
        public string Zone { get; set; }
        public string Ward { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string QrCode { get; set; }
        public Nullable<int> wardId { get; set; }
        public Nullable<int> zoneId { get; set; }
        public Nullable<int> areaId { get; set; }
        public string ReferanceId { get; set; }
    }
}
