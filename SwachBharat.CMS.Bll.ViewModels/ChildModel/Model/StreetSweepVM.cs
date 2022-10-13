using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
  public  class StreetSweepVM : BaseVM
    {
        public string SerielNo;

        public int SSId { get; set; }
        public string SSName { get; set; }
        public string SSNameMar { get; set; }
        public string SSLat { get; set; }
        public string SSLong { get; set; }
        public string SSQRCode { get; set; }
        public Nullable<int> ZoneId { get; set; }
        public Nullable<int> WardNo { get; set; }
        public Nullable<int> areaId { get; set; }
        public string ReferanceId { get; set; }
        public string SSAddress { get; set; }
        public Nullable<System.DateTime> lastModifiedDate { get; set; }
        public Nullable<int> BeatId { get; set; }
        public string SSBeatone { get; set; }
        public string SSBeattwo { get; set; }
        public string SSBeatthree { get; set; }
        public string SSBeatfour { get; set; }
        public string SSBeatfive { get; set; }
    }
}
