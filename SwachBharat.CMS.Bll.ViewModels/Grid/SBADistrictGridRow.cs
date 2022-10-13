using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Grid
{
  public class SBADistrictGridRow
    {
        public int Id { get; set; } 
        public string State { get; set; }
        public string Name { get; set; }
        public string NameMar { get; set; }
        public int StateId { get; set; } 
        public int languageId { get; set; }
    }
}
