using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
    public class VehicleRegVM : BaseVM
    {
        public int vehicleId { get; set; }
        public Nullable<int> vehicleType { get; set; }
        public string vehicleNumber { get; set; }
        public Nullable<int> AreaId { get; set; }
        public Nullable<bool> isActive { get; set; }


    }
}
