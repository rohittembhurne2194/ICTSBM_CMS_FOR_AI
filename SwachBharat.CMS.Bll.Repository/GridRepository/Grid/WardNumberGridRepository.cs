
using GramPanchayat.CMS.Bll.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized; 
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Grid; 

namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
    public class WardNumberGridRepository : IDataTableRepository
    {
        IEnumerable<SBAWardNumberGridRow> dataSet; 
        DashBoardRepository objRep = new DashBoardRepository();

        public WardNumberGridRepository(long wildcard, string SearchString, int AppId)
        {
            dataSet = objRep.GetWardNoData(wildcard, SearchString, AppId);
        }

        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataSet.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }


    }
}
