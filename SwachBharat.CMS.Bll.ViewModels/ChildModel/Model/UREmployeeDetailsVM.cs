using SwachBharat.CMS.Bll.ViewModels.MainModel;
using SwachBharat.CMS.Dal.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
  public  class UREmployeeDetailsVM :BaseVM
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

        public List<CheckAppD> CheckAppDs { get; set; }


      


        public Nullable<bool> isActive { get; set; }
        public string target { get; set; }
        public Nullable<System.DateTime> lastModifyDate { get; set; }

        //public int userId { get; set; }
        //[Remote("CheckUserDetails", "HouseScanify", HttpMethod = "POST", ErrorMessage = "Name already exists!", AdditionalFields = "userId")]

        [Remote("CheckUserDetails", "HouseScanify", HttpMethod = "POST", ErrorMessage = "Name already exists!", AdditionalFields = "EmpId")]
        public string EmpLoginId { get; set; }

        public string LoginId { get; set; }
        //[Remote("CheckUserDetails", "HouseScanify", HttpMethod = "POST", ErrorMessage = "Name already exists!", AdditionalFields = "userId")]
        public string Password { get; set; }
    }

   
} 
