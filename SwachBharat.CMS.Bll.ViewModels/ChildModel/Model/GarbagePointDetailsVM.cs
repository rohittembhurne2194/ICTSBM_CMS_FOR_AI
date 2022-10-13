using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
   public class GarbagePointDetailsVM : BaseVM
    {

        public int gpId { get; set; }
        public string gpName { get; set; }
        public string gpLat { get; set; }
        public string gpLong { get; set; }
        public string qrCode { get; set; }
        public string gpNameMar { get; set; }
        public Nullable<int> ZoneId { get; set; }
        public Nullable<int> WardNo { get; set; }
        public Nullable<int> areaId { get; set; }
        public string ReferanceId { get; set; }
        public string gpAddress { get; set; }
        
    }
}
