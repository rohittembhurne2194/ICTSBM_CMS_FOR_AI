using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Grid
{
   public class SBAGrabageCollectionGridRow
    {

        public int  Id { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string HouseNumber { get; set; }
        public string Employee { get; set; }
        public string VehicleNumber { get; set; }
        public string Note { get; set; } 
        public string attandDate { get; set; }
        public string gpBeforImage { get; set; }
        public string gpAfterImage { get; set; }
        public Nullable<DateTime> gcDate { get; set; }
        public Nullable<int> userId { get; set; }
        public Nullable<int> houseId { get; set; }
        public Nullable<int> gpIdfk { get; set; }
        public Nullable<int> gpIdpk { get; set; }
        public Nullable<int> gcType { get; set; }
        public string ReferanceId { get; set; }
        public string type1 { get; set; }

        public Nullable<int> dyIdfk { get; set; }
        public Nullable<int> dyIdpk { get; set; }

        public Nullable<int> dyId { get; set; }
        public Nullable<decimal> totalGcWeight { get; set; }
        public Nullable<decimal> totalDryWeight { get; set; }
        public Nullable<decimal> totalWetWeight { get; set; }
        public string batteryStatus { get; set; }

        public string Lat { get; set; }
        public string Long { get; set; }
        public Nullable<int> zoneId { get; set; }
        public string RFIDTagId { get; set; }
        public string RFIDReaderId { get; set; }
        public Nullable<int> SourceId { get; set; }

        public string wastetype { get; set; }



    }
}
