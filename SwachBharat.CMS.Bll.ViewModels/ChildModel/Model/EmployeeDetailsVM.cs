using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
   public class EmployeeDetailsVM
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string userLoginId { get; set; }
        public string userConfirmPassword { get; set; }
        public string userPassword { get; set; }
        public string userMobileNumber { get; set; }
        public string userAddress { get; set; }
        public string userProfileImage { get; set; }
        public string Type { get; set; }
        public string userNameMar { get; set; }
        public string userEmployeeNo { get; set; }
        public string imoNo{ get; set; }
        public Nullable<bool> isActive { get; set; }
        public string bloodGroup { get; set; }
        public string gcTarget { get; set; }

        public string EmployeeType { get; set; }
        public string LoginId { get; set; }
        public List<Shift> Shifts { get; set; }
    }

    public class Shift
    {
        public int shiftId { get; set; }
        public string shiftName { get; set; }
        public bool IsChecked { get; set; }
    }
}
