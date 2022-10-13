using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
  public  class LocationVM:BaseVM
    {
        public int locId { get; set; }
        public int userId { get; set; }     
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address { get; set; }

    }
}
