using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.MainModel
{
    public class AppDistrictVM :BaseVM
    {
        public int districtId { get; set; }
        public int stateId { get; set; }
        public string districtName { get; set; }
        public string districtNameMmar { get; set; }
    }
}
