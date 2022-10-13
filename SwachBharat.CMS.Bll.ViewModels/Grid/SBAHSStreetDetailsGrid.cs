using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.Grid
{
  public  class SBAHSStreetDetailsGrid
    {
        public int streetId { get; set; }
        public string ReferanceId { get; set; }
        public string Name { get; set; }
        public string HouseLat { get; set; }
        public string HouseLong { get; set; }
        public string QRCodeImage { get; set; }
        public string modifiedDate { get; set; }
        public Nullable<bool> QRStatus { get; set; }
        public string QRStatusDate { get; set; }
    }
}
