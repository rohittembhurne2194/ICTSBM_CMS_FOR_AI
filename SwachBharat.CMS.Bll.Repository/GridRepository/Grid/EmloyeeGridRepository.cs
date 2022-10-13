
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
    public class EmployeeGridRepository : IDataTableRepository
    {
        IEnumerable<SBAEmployeeDetailsGridRow> dataSet;

        DashBoardRepository objRep = new DashBoardRepository();

        public EmployeeGridRepository(long wildcard, string SearchString, int AppId, string isActive, string Emptype)
        {
            dataSet =  objRep.GetEmployeeDetailsData(wildcard, SearchString, AppId, isActive,Emptype);
        }

       

        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataSet.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }
      
    }
}
