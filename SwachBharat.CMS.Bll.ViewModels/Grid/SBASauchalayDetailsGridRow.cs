using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.Grid
{
    public class SBASauchalayDetailsGridRow
    {
        public int SauchalayFeedback_ID { get; set; }
        public string ULB { get; set; }
        public string SauchalayID { get; set; }
        public Nullable<int> AppId { get; set; }
        public string Fullname { get; set; }
        public string MobileNo { get; set; }
        public string que1 { get; set; }
        public string que2 { get; set; }
        public string que3 { get; set; }
        public string Rating { get; set; }
        public string Feedback { get; set; }
        public string Date { get; set; }
        public string Address { get; set; }
    }
}
