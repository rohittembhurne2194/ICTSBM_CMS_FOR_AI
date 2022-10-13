using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
    public class VehicleTypeVM :BaseVM
    {
        public int Id { get; set; }
        public string description { get; set; }
        public string descriptionMar { get; set; }
        public Nullable<bool> isActive { get; set; }
    }
}
