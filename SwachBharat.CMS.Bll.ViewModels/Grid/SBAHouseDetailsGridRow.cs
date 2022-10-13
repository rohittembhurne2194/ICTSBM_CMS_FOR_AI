using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Grid
{
   public class SBAHouseDetailsGridRow
    {
        public int houseId { get; set; }
        public int masterId { get; set; }
        public string Name { get; set; }
        public string zone { get; set; }
        public string WardNo { get; set; }
        public string Area { get; set; }
        public string houseNo { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string QRCode { get; set; }
        public string houseOwnerMar { get; set; }
        public Nullable<int> AreaId { get; set; }
        public Nullable<int> WardNoId { get; set; }
        public Nullable<int> zoneId { get; set; }
        public string ReferanceId { get; set; }
        public string OccupancyStatus { get; set; }
        public string Property_Type { get; set; }
        public string QRList { get; set; }
        public Nullable<int> ISActive { get; set; }
        public Nullable<bool> isActive { get; set; }
        public string TotalCount { get; set; }
        public string BunchName { get; set; }

    }
}
