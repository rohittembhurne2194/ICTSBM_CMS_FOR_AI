using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.Grid
{
    public class GPLeagueGridRow
    {
        public int ID { get; set; }
        public string HouseId { get; set; }
        public string Name { get; set; }
        public string Zone { get; set; }
        public string Ward { get; set; }
        public string Area { get; set; }
        public string JSon { get; set; }
        public int AnswerDetailId { get; set; }
        public string ReferenceId { get; set; }
        public string AnsDate { get; set; }
    }
}
