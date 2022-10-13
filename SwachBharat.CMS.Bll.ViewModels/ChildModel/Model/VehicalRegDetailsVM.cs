using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
   public class VehicalRegDetailsVM : BaseVM
    {
        public int vqrId { get; set; }
        public string Vehican_No { get; set; }
        public string vehicalQRCode { get; set; }
        public string ReferanceId { get; set; }

        public string SerielNo { get; set; }

        public Nullable<int> VehicalId { get; set; }
        public string Vehical_Type { get; set; }
    }
}
