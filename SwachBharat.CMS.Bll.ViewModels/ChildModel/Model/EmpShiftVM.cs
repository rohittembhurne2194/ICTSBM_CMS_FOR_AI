using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
    public class EmpShiftVM
    {
        public int shiftId { get; set; }
        public string shiftName { get; set; }
        public string shiftStart { get; set; }
        public string shiftEnd { get; set; }
    }
}
