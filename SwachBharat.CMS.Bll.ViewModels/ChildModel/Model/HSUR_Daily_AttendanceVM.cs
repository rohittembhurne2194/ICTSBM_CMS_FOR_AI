using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
   public class HSUR_Daily_AttendanceVM : BaseVM
    {
        public string LOGIN_ID { get; set; }
        public int EmpId { get; set; }
        public string StartTime { get; set; }
        public string EmpName { get; set; }

        public string EmployeeType { get; set; }
        public string daDate { get; set; }

        public string ipaddress { get; set; }
        public string HostName { get; set; }

        public string logindevice { get; set; }
        public Nullable<bool> isActive { get; set; }
        public int daID { get; set; }
        public string daEndDate { get; set; }
        public string startLat { get; set; }
        public string startLong { get; set; }
        public string endLat { get; set; }
        public string endLong { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string type { get; set; }

        public string logoff { get; set; }
    }
}
