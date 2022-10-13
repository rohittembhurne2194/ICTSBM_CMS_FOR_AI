using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.SS2020Reports
{
   public  class OnePointSevenVM
    {
        //public int ID { get; set; }
        //public string  Area { get; set; }
        //public string AreaMar { get; set; }
        //public int WardID { get; set; }
        //public int WaterBodies { get; set; }
        //public int StormWater { get; set; }

        //public int Location { get; set; }

        //public int Outlet { get; set; }

        public int Trno { get; set; }
        public int id { get; set; }
        public Nullable<int> No_water_bodies { get; set; }
        public Nullable<int> No_drain_nallas { get; set; }
        public Nullable<int> No_locations { get; set; }
        public Nullable<int> No_outlets { get; set; }
        public Nullable<int> INSERT_ID { get; set; }
    }
}
