using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GramPanchayat.CMS.Bll.ViewModels.ChildModel.Grid
{
  public  class GPGramBandhKamCompliantGridRow
    {
        public int BandhKamId { get; set; }
        public string ref_id { get; set; }
        public string date { get; set; }
        public string image { get; set; }
        public string wardNo { get; set; }
        public string place { get; set; }
        public string details { get; set; }
        public string tip { get; set; }        
        public string name { get; set; }
        public string number { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string language { get; set; }
        public string Lat_Log { get; set; }
        public string status { get; set; }
        public string Comment { get; set; }
        public string status_Image { get; set; }
        public string Type { get; set; }
        public DateTime? Createddate { get; set; }
        public Nullable<int> languageId { get; set; }
    }
}
