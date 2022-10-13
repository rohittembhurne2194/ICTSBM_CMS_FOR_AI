using SwachBharat.CMS.Bll.ViewModels.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
   public class LiquidWasteGridRepository : IDataTableRepository
    {

        IEnumerable<SBALiquidWasteGridRow> dataset;

        DashBoardRepository objRep = new DashBoardRepository();

        public LiquidWasteGridRepository(long wildcard, string SearchString, int AppId)
        {
            dataset = objRep.GetLiquidWasteData(wildcard, SearchString, AppId);
        }
        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataset.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }
    }
}