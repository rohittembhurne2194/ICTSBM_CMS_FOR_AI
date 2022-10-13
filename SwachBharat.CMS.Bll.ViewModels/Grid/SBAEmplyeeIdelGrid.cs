using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.Grid
{
   public class SBAEmplyeeIdelGrid
    {    
        public string UserName { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string IdelTime { get; set; }
        public string StartAddress { get; set; }
        public string EndAddress { get; set; }
        public string startLat { get; set; }
        public string startLong { get; set; }
        public string EndLat { get; set; }
        public string EndLong { get; set; }
        public Nullable<int> userId { get; set; }
        public string daDateTIme { get; set; }

        public int Result { get; set; }

    }
}
