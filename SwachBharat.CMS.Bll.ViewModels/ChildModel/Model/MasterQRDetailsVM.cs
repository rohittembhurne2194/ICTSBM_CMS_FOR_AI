using SwachBharat.CMS.Dal.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
   public class MasterQRDetailsVM : BaseVM
    {
        public int masterId { get; set; }

        public string ReferanceId { get; set; }

        public string QRList { get; set; }

        public Nullable<bool> isActive { get; set; }

        public List<HouseList> CheckHlist { get; set; }
        public List<HouseList> SelectedHouseList { get; set; }

        public Nullable<int> HouseBunchId { get; set; }


        public string BunchName { get; set; }
    }
}
