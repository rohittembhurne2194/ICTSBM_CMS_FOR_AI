using SwachBharat.CMS.Bll.ViewModels.ChildModel.Grid;
using SwachBharat.CMS.Bll.ViewModels.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
  public  class AdminCountGridRepository : IDataTableRepository
    {


        DashBoardRepository objRep = new DashBoardRepository();
        IEnumerable<SBAAdminCountGrid> dataset;
        public AdminCountGridRepository(long wildcard, string SearchString, int AppId)
        {
            dataset = objRep.GetAdminCountData(wildcard, SearchString, AppId);
        }



        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataset.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }
    }
}
