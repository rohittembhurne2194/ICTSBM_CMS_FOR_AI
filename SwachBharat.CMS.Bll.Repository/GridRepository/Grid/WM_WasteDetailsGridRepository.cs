using SwachBharat.CMS.Bll.ViewModels.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
    public class WM_WasteDetailsGridRepository : IDataTableRepository   
    {
        IEnumerable<WM_WasteDetailsGridRow> dataSet;

        DashBoardRepository objRep = new DashBoardRepository();

        public WM_WasteDetailsGridRepository(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId,int? param1, int? param2)
        {
            dataSet = objRep.GetWasteDetailsData(wildcard, SearchString, fdate, tdate, userId, appId,param1, param2);
        }

        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataSet.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }
    }
}
