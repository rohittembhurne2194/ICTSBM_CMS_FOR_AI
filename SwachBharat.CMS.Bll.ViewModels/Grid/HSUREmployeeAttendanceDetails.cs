using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.Grid
{
   public class HSUREmployeeAttendanceDetails
    {
        public string LOGIN_ID { get; set; }
        public int EmpId { get; set; }
        public string StartTime { get; set; }
        public string EmpName { get; set; }

        public string EmployeeType { get; set; }
        public string startDate { get; set; }
        public string EndDate { get; set; }
        public string startDateTime { get; set; }
        public string EndDateTime { get; set; }


        public Nullable<System.DateTime> daDate { get; set; }
        public Nullable<System.DateTime> daEndDate { get; set; }

        public string ipaddress { get; set; }

        public string logindevice { get; set; }
        public string HostName { get; set; }
        public Nullable<bool> isActive { get; set; }
        public int daID { get; set; }
    
        public string startLat { get; set; }
        public string startLong { get; set; }
        public string endLat { get; set; }
        public string endLong { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string type { get; set; }
        public object UserList { get; set; }
    }
}
