
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
    public class UserMasterVM : BaseVM
    {
        public int Id { get; set; }
        [Remote("CheckUserDetails", "HouseScanify", HttpMethod = "POST", ErrorMessage = "Name already exists!", AdditionalFields = "Id")]
        public string LoginId { get; set; }
        [Remote("CheckUserDetails", "HouseScanify", HttpMethod = "POST", ErrorMessage = "Name already exists!", AdditionalFields = "Id")]
        public string Password { get; set; }
       // public Nullable<int> wardId { get; set; }
    }
}
