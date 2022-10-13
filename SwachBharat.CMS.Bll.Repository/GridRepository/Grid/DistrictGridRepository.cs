using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;  
using SwachBharat.CMS.Bll.Repository.GridRepository;
using SwachBharat.CMS.Bll.Repository.GridRepository.Grid;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Grid;

namespace SwachBharat.CMS.Bll.Repository.RepositoryGrid.Grid
{
   public class DistrictGridRepository : IDataTableRepository
    {
        IEnumerable<SBADistrictGridRow> dataset; 
        DashBoardRepository objRep = new DashBoardRepository();


        public DistrictGridRepository(long wildcard, string SearchString, int AppId)
        {
            dataset = objRep.GetDistrictData(wildcard, SearchString, AppId);
        }

        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataset.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }

       
    }
}
