
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Grid;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachBharat.CMS.Bll.ViewModels.Grid;
using SwachBharat.CMS.Dal.DataContexts;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data.SqlClient;
using System.Globalization;

namespace SwachBharat.CMS.Bll.Repository.GridRepository
{
 public  class LiquidDashBoardRepository
    {

        public IEnumerable<DashBoardVM> getEmployeeLiquidTargetData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId)
        {
            List<DashBoardVM> obj = new List<DashBoardVM>();
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {


                var data = db.SP_LiquidEmployeeTarget(fdate, tdate, userId).ToList();


                foreach (var x in data)
                {

                    obj.Add(new DashBoardVM()
                    {
                        UserName = x.userName,
                        Target = x.gcTarget,
                        FromDate = Convert.ToDateTime(x.fromDate).ToString("dd/MM/yyyy"),
                        ToDate = Convert.ToDateTime(x.ToDate).ToString("dd/MM/yyyy"),
                        _Count = Convert.ToInt32(x.Count),

                    });
                }
                return obj;
            }
        }

        public IEnumerable<EmployeeLiquidCollectionType> getEmployeeLiquidCollectionType(int appId)
        {
            List<EmployeeLiquidCollectionType> obj = new List<EmployeeLiquidCollectionType>();
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = db.SP_EmployeeLiquidCollectionType().ToList();

                foreach (var x in data)
                {
                    obj.Add(new EmployeeLiquidCollectionType()
                    {
                        inTime = x.inTime,
                        Count = x.Count,
                        ToDate = x.TodayDate.ToString(),
                        LiquidCollectionCount = x.LiquidCollectionCount,
                        userId = x.userId,
                        gcTarget = x.gcTarget,
                        userName = x.userName
                    });
                }
                return obj.OrderBy(c => c.userName);
            }
        }
    }
}
