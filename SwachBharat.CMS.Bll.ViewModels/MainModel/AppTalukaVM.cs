using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SwachBharat.CMS.Bll.ViewModels.MainModel
{
   public class AppTalukaVM :BaseVM
    {
        public int talukaId { get; set; }
        [Remote("CheckTalukaDetails", "MainMaster", HttpMethod = "POST", ErrorMessage = "Name already exists!")]
        public string talukaName { get; set; }
        public string talukaNameMar { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public Nullable<int> stateId { get; set; }
        public Nullable<int> districtId { get; set; }

        public bool IsCheked { get; set; }
    }
}
