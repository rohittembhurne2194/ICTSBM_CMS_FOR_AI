using SwachBharat.CMS.Bll.ViewModels.ChildModel.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
   public class MasterQRDetailsGridRepository : IDataTableRepository
    {
        IEnumerable<SBAHouseDetailsGridRow> dataSet;
        DashBoardRepository objRep = new DashBoardRepository();

        public MasterQRDetailsGridRepository(long wildcard, string SearchString, int appId)
        {
            dataSet = objRep.GetMasterQRDetailsData(wildcard, SearchString, appId);
        }

        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataSet.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }

    }
}
