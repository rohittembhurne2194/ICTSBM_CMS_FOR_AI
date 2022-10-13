using SwachBharat.CMS.Bll.Repository.GridRepository;
using System.Collections.Generic;
using System.Collections.Specialized;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Grid;
using SwachBharat.CMS.Bll.Repository.GridRepository.Grid;
using System;
using SwachBharat.CMS.Bll.ViewModels.Grid;

namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
   public class VehicalRegDetailsGridRepository : IDataTableRepository
    {
        IEnumerable<SBAVehicalRegDetailsGridRow> dataSet;
        DashBoardRepository objRep = new DashBoardRepository();

        public VehicalRegDetailsGridRepository(long wildcard, string SearchString, int appId)
        {
            dataSet = objRep.GetVehicalRegDetailsData(wildcard, SearchString, appId);
        }

        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataSet.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }
    }
}
