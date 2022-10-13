
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using SwachBharat.CMS.Bll.Repository.GridRepository;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Grid;

namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
     public class HouseScanifyGridRepository : IDataTableRepository
    {
        IEnumerable<SBAHouseScanifyDetailsGridRow> dataSet;

        DashBoardRepository objRep = new DashBoardRepository();

        public HouseScanifyGridRepository(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId)
        {
            dataSet = objRep.GetHouseScanifyDetailsData(wildcard, SearchString, fdate, tdate, userId, appId);
        }

        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataSet.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }
    }
}
