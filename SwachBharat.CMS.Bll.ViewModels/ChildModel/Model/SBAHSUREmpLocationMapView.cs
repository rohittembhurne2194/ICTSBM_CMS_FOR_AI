using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
  public  class SBAHSUREmpLocationMapView : BaseVM
    {

        public int userId { get; set; }
        public string userName { get; set; }
        public string datetime { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string lat { get; set; }
        public string log { get; set; }
        public string address { get; set; }
        public string vehcileNumber { get; set; }
        public string userMobile { get; set; }

        public string HouseId { get; set; }
        public string DyId { get; set; }
        public string HouseOwnerName { get; set; }
        public string OwnerMobileNo { get; set; }
        public string HouseAddress { get; set; }
        public int type { get; set; }




        public string WasteType { get; set; }
        public string gpBeforImage { get; set; }
        public string gpAfterImage { get; set; }
        public string DryWaste { get; set; }
        public string WetWaste { get; set; }
        public string TotWaste { get; set; }
        public string DumpAddress { get; set; }
        public string DumpYardName { get; set; }

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

        public string SerielNo { get; set; }

        public bool IsIn { get; set; }


    }
}
