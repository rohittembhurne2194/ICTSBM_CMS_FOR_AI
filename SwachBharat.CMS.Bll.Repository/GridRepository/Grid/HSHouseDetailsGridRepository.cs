using SwachBharat.CMS.Bll.ViewModels.ChildModel.Grid;
using SwachBharat.CMS.Bll.ViewModels.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
    public class HSHouseDetailsGridRepository: IDataTableRepository
    {
        IEnumerable<SBAHSHouseDetailsGrid> dataset;

        DashBoardRepository objRep = new DashBoardRepository();

        public HSHouseDetailsGridRepository(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int? QrStatus, int appId, string sortColumn = "", string sortColumnDir = "", string draw = "", string length = "", string start = "")
        {
            dataset = objRep.GetHSHouseDetailsData(wildcard, SearchString, fdate, tdate, userId, QrStatus, appId, sortColumn, sortColumnDir, draw, length, start);
        }

        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            int recordsTotal = 0;
            if (dataset != null && dataset.Count() > 0)
            {
                recordsTotal = dataset.First().totalRowCount;
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            var result = serializer.Serialize(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = dataset });
            return result;
        }
    }
}
