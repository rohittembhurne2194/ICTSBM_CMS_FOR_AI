using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
    public class BaseVM
    { 
        public List<SelectListItem> WardNoList { get; set; }
        public List<SelectListItem> languageList { get; set; }
        public List<SelectListItem> StateList { get; set; }
        public List<SelectListItem> DistrictList { get; set; }
        public List<SelectListItem> TalukaList { get; set; }

        public List<SelectListItem> AreaList { get; set; }
        public List<SelectListItem> WardList { get; set; }
        public List<SelectListItem> ZoneList { get; set; }
        public List<SelectListItem> VehicleList { get; set; }
        public List<SelectListItem> UserList { get; set; }
        public List<SelectListItem> WasteCategoryList { get; set; }
        public List<SelectListItem> WasteSubCategoryList { get; set; }
        public List<SelectListItem> WM_UserList { get; set; }

        public List<SelectListItem> DivisionList { get; set; }

        public List<SelectListItem> OccupancyStatus { get; set; }
        public List<SelectListItem> Property_Type { get; set; }

        public List<SelectListItem> BeatList { get; set; }

        public List<SelectListItem> HouseList { get; set; }

        public List<SelectListItem> BunchList { get; set; }

    }



}

