using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachBharat.CMS.Dal.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.MainModel
{
 public   class AEmployeeDetailVM : BaseVM
    {
        public int qrEmpId { get; set; }
        public Nullable<int> appId { get; set; }
        public string qrEmpName { get; set; }
        public string qrEmpNameMar { get; set; }

        public string qrEmpPassword { get; set; }
        public string qrEmpMobileNumber { get; set; }
        public string qrEmpAddress { get; set; }
        public string type { get; set; }
        public Nullable<int> typeId { get; set; }
        public string userEmployeeNo { get; set; }
        public string imoNo { get; set; }
        public string bloodGroup { get; set; }

        public Nullable<bool> isActive { get; set; }
        public string target { get; set; }
        public Nullable<System.DateTime> lastModifyDate { get; set; }
        public Nullable<bool> IsCheked { get; set; }
        public List<tehsil> CheckDist { get; set; }
        public string qrEmpLoginId { get; set; }

        public string LoginId { get; set; }
        //[Remote("CheckUserDetails", "HouseScanify", HttpMethod = "POST", ErrorMessage = "Name already exists!", AdditionalFields = "userId")]
        public string Password { get; set; }

        public int StateId { get; set; }

        public int DivisionId { get; set; }

        public string DivisionName { get; set; }

        public int DistictId { get; set; }

        public List<MenuItemDivison> ULBList { get; set; }

    }
}
