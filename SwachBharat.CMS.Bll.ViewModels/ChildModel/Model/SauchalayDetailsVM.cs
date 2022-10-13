using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
    public class SauchalayDetailsVM
    {
        public int Id { get; set; }
        public string SauchalayID { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string QrImage { get; set; }
        public string Mobile { get; set; }
    }
}
