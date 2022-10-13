using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.Grid
{
   public class SBAAdminCountGrid
    {

     
        public string Name { get; set; }
        public int employee { get; set; }
        public string attenemplyee { get; set; }
        public int notrecivedcol { get; set; }
        public int bybufer { get; set; }
        public int mixed { get; set; }
        public int total{ get; set; }
        public int notspecified { get; set; }
        public int TotalLiquidCount { get; set; }
        public int TotalStreetCount { get; set; }
    }
}
