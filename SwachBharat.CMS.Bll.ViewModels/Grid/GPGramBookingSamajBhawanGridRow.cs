using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GramPanchayat.CMS.Bll.ViewModels.ChildModel.Grid
{
   public class GPGramBookingSamajBhawanGridRow
    {
        public int bookingId { get; set; }
        public string ref_id { get; set; }
        public string date { get; set; }
        public string name { get; set; }
        public string number { get; set; }
        public string email { get; set; }
        public string wardNo { get; set; }
        public string address { get; set; }
        public string doorNo { get; set; }
        public string language { get; set; }
        public Nullable<int> languageId { get; set; }

        public DateTime? Createddate { get; set; }


    }
}
