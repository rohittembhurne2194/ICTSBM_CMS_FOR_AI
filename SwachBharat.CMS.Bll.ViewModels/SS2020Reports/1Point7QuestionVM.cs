using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.SS2020Reports
{
   public class OnePoint7QuestionVM
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public Nullable<int> wardId { get; set; }
        public string No_Water_Bodies { get; set; }
        public string No_drain_nallas { get; set; }
        public string No_locations { get; set; }
        public string No_outlets { get; set; }

        public int INSERT_ID { get; set; }
    }
}
