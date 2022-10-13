using SwachBharat.CMS.Bll.ViewModels.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
    public class InfotainmentGridRepository : IDataTableRepository
    {
        IEnumerable<InfotainmentDetailsGridRow> dataSet;

        DashBoardRepository objRep = new DashBoardRepository();

        public InfotainmentGridRepository(long wildcard, string SearchString, int AppId)
        {
            dataSet = objRep.GetInfotainmentDetailsData(wildcard, SearchString, AppId);
        }

        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataSet.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }
    }
}
