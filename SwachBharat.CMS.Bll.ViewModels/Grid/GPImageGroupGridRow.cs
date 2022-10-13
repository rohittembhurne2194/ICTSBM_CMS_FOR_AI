using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GramPanchayat.CMS.Bll.ViewModels.ChildModel.Grid
{
  public  class GPImageGroupGridRow
    {
        public int groupId { get; set; }
        public string groupName { get; set; }
        public string language { get; set; }
        public Nullable<int> languageId { get; set; }
    }
}
