using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
   public class ZoneVM
    {
        public int id { get; set; }
        [Remote("CheckZoneDetails", "MainMaster", HttpMethod = "POST", ErrorMessage = "Name already exists!")]
        public string name { get; set; }

        public int SSid { get; set; }
        [Remote("CheckZoneDetails", "StreetMainMaster", HttpMethod = "POST", ErrorMessage = "Name already exists!")]
        public string SSname { get; set; }
    }
}
