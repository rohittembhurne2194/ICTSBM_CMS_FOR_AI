using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GramPanchayat.CMS.Bll.ViewModels.ChildModel.Grid
{
   public class GPGramPanchayatEventGridRow
    {
        public int id { get; set; }
        public string subject { get; set; }
        public string description { get; set; } 
        public string date { get; set; }
        public string days { get; set; }
        public string time { get; set; }
        public string loocation { get; set; }
        public string imageUrl { get; set; }
        public string language { get; set; }
        public Nullable<int> languageId { get; set; }
        public Nullable<DateTime> createdDate { get; set; }
        public string enddate { get; set; }
    }
}
