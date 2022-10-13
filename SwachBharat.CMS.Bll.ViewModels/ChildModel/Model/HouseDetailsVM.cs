using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
   public class HouseDetailsVM :BaseVM
    {
        public int houseId { get; set; }
        public Nullable<int> AreaId { get; set; }
        public Nullable<int> WardNo { get; set; }
        public Nullable<int> ZoneId { get; set; }
        public string houseNumber { get; set; }
        public string houseOwnerMar { get; set; }
        public string houseOwner { get; set; }
        public string houseMobile { get; set; }
        public string houseAddress { get; set; }
        public string houseLat { get; set; }
        public string houseLong { get; set; }
        public string houseQRCode { get; set; }
        public string ReferanceId { get; set; }
        public string JavascriptToRun { get; set; }
        public string areaName { get; set; }
        public string wardName { get; set; }

        public Nullable<int> userId { get; set; }

        public string SerielNo { get; set; }

        public string WasteType { get; set; }
        public string OccupancyStatus { get; set; }
        public string Property_Type { get; set; }

    }
}
