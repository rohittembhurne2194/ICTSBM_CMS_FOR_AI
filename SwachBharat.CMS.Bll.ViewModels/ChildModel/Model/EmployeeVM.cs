using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
    public class EmployeeVM
    {
        public int ADUM_USER_CODE { get; set; }
        public int SERVER_ID { get; set; }
        public int APP_ID { get; set; }
        public string ADUM_USER_ID { get; set; }
        public string ADUM_USER_NAME { get; set; }
        public string ADUM_LOGIN_ID { get; set; }
        public string ADUM_PASSWORD { get; set; }
        public string ADUM_EMPLOYEE_ID { get; set; }
        public string ADUM_DESIGNATION { get; set; }

        public string ADUM_MOBILE { get; set; }
        public string ADUM_EMAIL { get; set; }
        public string LOCAL_USER_NAME { get; set; }
        public string PROFILE_IMAGE { get; set; }
        public string ADUM_FRDT { get; set; }
        public string ADUM_TODT { get; set; }
        public bool ADUM_STATUS { get; set; }
        public bool UPDATE_FLAG { get; set; }
        public string LAST_UPDATE { get; set; }
        public int AD_USER_TYPE_ID { get; set; }
        public string MOBILE_ID { get; set; }
        public bool IS_ACTIVE { get; set; }

        public string IS_ACTIVETEXT { get; set; }

        public string IMO_NO { get; set; }

        public string DEVICE_ID { get; set; }
        public string status { get; set; }
    }
}
