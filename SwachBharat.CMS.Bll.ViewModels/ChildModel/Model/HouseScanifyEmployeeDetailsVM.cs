using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
    public class HouseScanifyEmployeeDetailsVM
    {
        public int qrEmpId { get; set; }
        public Nullable<int> appId { get; set; }

      //  [Remote("CheckUserName", "HouseScanifyEmp", HttpMethod = "POST", ErrorMessage = "Name Is already exists!", AdditionalFields = "userId")]
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

        //[Remote("CheckUserDetails", "HouseScanify", HttpMethod = "POST", ErrorMessage = "Login Id already exists!", AdditionalFields = "userId")]
        public string qrEmpLoginId { get; set; }
        
        public string LoginId { get; set; }
       // [Remote("CheckUserDetails", "HouseScanify", HttpMethod = "POST", ErrorMessage = "Name already exists!", AdditionalFields = "userId")]
          public string Password { get; set; }
    }
}
