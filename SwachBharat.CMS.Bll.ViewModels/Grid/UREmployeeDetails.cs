using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.Grid
{
  public  class UREmployeeDetails
    {
        public int EmpId { get; set; }
        public Nullable<int> appId { get; set; }
        public string EmpName { get; set; }
        public string EmpNameMar { get; set; }

        public string EmpPassword { get; set; }
        public string EmpMobileNumber { get; set; }
        public string EmpAddress { get; set; }
        public string type { get; set; }
        public Nullable<int> typeId { get; set; }
        public string userEmployeeNo { get; set; }
        public string imoNo { get; set; }
        public string isActiveULB { get; set; }


        public Nullable<bool> isActive { get; set; }
        public string target { get; set; }
        public string lastModifyDateEntry { get; set; }

      
        public string EmpLoginId { get; set; }

        public string LoginId { get; set; }
        //[Remote("CheckUserDetails", "HouseScanify", HttpMethod = "POST", ErrorMessage = "Name already exists!", AdditionalFields = "userId")]
        public string Password { get; set; }
    }
}
