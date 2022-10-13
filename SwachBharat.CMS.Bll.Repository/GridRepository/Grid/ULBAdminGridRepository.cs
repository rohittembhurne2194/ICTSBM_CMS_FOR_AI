using SwachBharat.CMS.Bll.ViewModels.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
    public class ULBAdminGridRepository : IDataTableRepository
    {
        IEnumerable<AdminULBDetails> dataset;

        DashBoardRepository objRep = new DashBoardRepository();

        public ULBAdminGridRepository(long wildcard, string SearchString, int? DivisionId, int? DistricrId, int? AppId, int UserId)
        {
            dataset = objRep.GetAdminULBDetails(wildcard, SearchString, DivisionId, DistricrId, AppId, UserId);
        }

        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataset.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }
    }
}
