using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
   public class GarbageCollectionVM
    {
        public int Id { get; set; }
        public Nullable<int> userId { get; set; }
        public Nullable<int> houseId { get; set; }
        public Nullable<int> gPointId { get; set; }
        public Nullable<System.DateTime> gcDate { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string gcImage { get; set; }
        public string gcQRcode { get; set; }
        public string gpBeforImage { get; set; }
        public string gpAfterImage { get; set; }
        public Nullable<int> gcType { get; set; }
        public string vehicleNumber { get; set; }
        public string note { get; set; }
    }
}
