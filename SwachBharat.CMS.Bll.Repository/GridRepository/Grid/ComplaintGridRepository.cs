using SwachBharat.CMS.Bll.ViewModels.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
  public  class ComplaintGridRepository : IDataTableRepository
    {
        IEnumerable<ComplaintGrid> dataset;

        DashBoardRepository objRep = new DashBoardRepository();

        public ComplaintGridRepository(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate,int appId)
        {
            dataset = objRep.GetComplaintData(wildcard, SearchString, fdate, tdate, appId);
        }

        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataset.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }
    }
}
