using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GramPanchayat.CMS.Bll.ViewModels.ChildModel.Grid
{
    public class GPSchemesGridRow
    {
        public int SchemeId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; } 
        public bool IsActive { get; set; }
        public string Image { get; set; }
        public string language { get; set; }
        public Nullable<int> languageId { get; set; }

    }
}
