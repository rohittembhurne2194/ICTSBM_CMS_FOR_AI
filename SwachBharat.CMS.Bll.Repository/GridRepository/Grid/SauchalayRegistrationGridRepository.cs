using SwachBharat.CMS.Bll.ViewModels.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
    public class SauchalayRegistrationGridRepository : IDataTableRepository
    {
        IEnumerable<SauchalayRegistrationGridRow> dataSet;

        DashBoardRepository objRep = new DashBoardRepository();

        public SauchalayRegistrationGridRepository(long wildcard, string SearchString, int appId)
        {
            dataSet = objRep.GetSauchalayRegistrationDetailsData(wildcard, SearchString, appId);
        }

        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataSet.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }
    }
}
