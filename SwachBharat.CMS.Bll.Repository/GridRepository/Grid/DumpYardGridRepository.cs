using GramPanchayat.CMS.Bll.ViewModels.ChildModel.Grid;
using SwachBharat.CMS.Bll.Repository.GridRepository;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Grid;
using SwachBharat.CMS.Bll.ViewModels.Grid;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
    public class DumpYardGridRepository : IDataTableRepository
    {
        IEnumerable<SBADumpYardDetailsGridRow> dataset;

        DashBoardRepository objRep = new DashBoardRepository();

        public DumpYardGridRepository(long wildcard, string SearchString, int AppId)
        {
            dataset = objRep.GetDumpYardData(wildcard, SearchString, AppId);
        }

        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataset.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }
    }
}
