using SwachBharat.CMS.Bll.ViewModels.ChildModel.Grid;
using SwachBharat.CMS.Bll.ViewModels.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
    public class MonthlyAttendeceGridRepository : IDataTableRepository
    {
        IEnumerable<SBAAttendenceGrid> dataset;

        DashBoardRepository objRep = new DashBoardRepository();
        public MonthlyAttendeceGridRepository(long wildcard, string SearchString, string smonth , string emonth , string syear, string eyear , int userId, int appId, string Emptype)
        {
            dataset = objRep.GetMonthlyAttendeceData(wildcard, SearchString, smonth, emonth, syear, eyear, userId, appId, Emptype);
        }

        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataset.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }
    }
}
