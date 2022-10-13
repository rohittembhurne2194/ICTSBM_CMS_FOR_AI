using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwachBharat.CMS.Bll.ViewModels.Grid;


namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
    public class StreetSweepBeatGrid : IDataTableRepository
    {
        IEnumerable<SBAStreetSweepBeatDetailsGridRow> dataset;

        DashBoardRepository objRep = new DashBoardRepository();
        public StreetSweepBeatGrid(long wildcard, string SearchString, int AppId)
        {
            dataset = objRep.GetStreetSweepBeatData(wildcard, SearchString, AppId);
        }
        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataset.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }
    }
}
