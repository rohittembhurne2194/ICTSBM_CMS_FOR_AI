using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwachBharat.CMS.Bll.ViewModels.Grid;

namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
    public class ULBAdminStatusGridRepository : IDataTableRepository
    {
        IEnumerable<AdminULBStatusDetails> dataset;

        DashBoardRepository objRep = new DashBoardRepository();

        public ULBAdminStatusGridRepository(long wildcard, string SearchString, int? DivisionId, int? DistricrId, int? AppId,int? status, int UserId)
        {
            dataset = objRep.GetAdminULBStatusDetails(wildcard, SearchString, DivisionId, DistricrId, AppId, status, UserId);
        }

        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataset.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }
    }
}
