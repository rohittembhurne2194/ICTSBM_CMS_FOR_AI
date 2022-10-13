using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.MainModel
{
    public class MenuItem
    {
        public int M_ID { get; set; }
        public int? M_P_ID { get; set; }
        public string LinkText { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string returnUrl { get; set; }
        public string Type { get; set; }

        public string Url { get; set; }

        public Nullable<bool> isActive { get; set; }

        public string FAQ { get; set; }
        public Nullable<bool> Today_Waste_Status { get; set; }
        public Nullable<bool> Today_Liquid_Status { get; set; }
        public Nullable<bool> Today_Street_Status { get; set; }

    }

    public class MenuItemULB
    {
        public int? divisionId { get; set; }
        public int? districtId { get; set; }
        public int? ULBId { get; set; }
        public string LinkText { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string returnUrl { get; set; }
        public string Type { get; set; }
        public bool IsChecked { get; set; }


    }

    public class MenuItemDistrict
    {
        public int? divisionId { get; set; }
        public int? districtId { get; set; }
        public int? ULBId { get; set; }
        public string LinkText { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string returnUrl { get; set; }
        public string Type { get; set; }
        public bool IsChecked { get; set; }

        public List<MenuItemULB> ULBList { get; set; } = new List<MenuItemULB>();


    }

    public class MenuItemDivison
    {
        public int? divisionId { get; set; }
        public int? districtId { get; set; }
        public int? ULBId { get; set; }
        public string LinkText { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string returnUrl { get; set; }
        public string Type { get; set; }
        public bool IsChecked { get; set; }

        public List<MenuItemDistrict> DistrictList { get; set; } = new List<MenuItemDistrict>();


    }

}
