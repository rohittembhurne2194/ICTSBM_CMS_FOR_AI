using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachBharat.CMS.Dal.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.Repository.GridRepository
{
    public class StreetDashBoardRepository
    {
        public IEnumerable<DashBoardVM> getEmployeeStreetTargetData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId)
        {
            List<DashBoardVM> obj = new List<DashBoardVM>();
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {


                var data = db.SP_StreetEmployeeTarget(fdate, tdate, userId).ToList();


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

        public IEnumerable<EmployeeStreetCollectionType> getEmployeeStreetCollectionType(int appId)
        {
            List<EmployeeStreetCollectionType> obj = new List<EmployeeStreetCollectionType>();
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = db.SP_EmployeeStreetCollectionType().ToList();

                foreach (var x in data)
                {
                    obj.Add(new EmployeeStreetCollectionType()
                    {
                        inTime = x.inTime,
                        Count = x.Count,
                        ToDate = x.TodayDate.ToString(),
                        StreetCollectionCount = x.StreetCollectionCount,
                        userId = x.userId,
                        userName = x.userName,
                        gcTarget=x.gcTarget
                    });
                }
                return obj.OrderBy(c => c.userName);
            }
        }
    }
}
