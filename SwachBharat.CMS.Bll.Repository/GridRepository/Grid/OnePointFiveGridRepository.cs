using System;
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

    public class OnePointFiveGridRepository : IDataTableRepository
    {
        IEnumerable<OnePointfourGridRow> dataset;
        DashBoardRepository objRep = new DashBoardRepository();

        public OnePointFiveGridRepository(long wildcard, string SearchString, int AppId)
        {
            dataset = objRep.GetOnepointfiveData(wildcard, SearchString, AppId);
        }
        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataset.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }
    }
}
