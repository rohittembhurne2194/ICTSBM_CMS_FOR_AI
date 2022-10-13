﻿using SwachBharat.CMS.Bll.ViewModels.ChildModel.Grid;
using SwachBharat.CMS.Bll.ViewModels.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
   public class HSStreetDetailsGridRepository: IDataTableRepository
    {
        IEnumerable<SBAHSStreetDetailsGrid> dataset;

        DashBoardRepository objRep = new DashBoardRepository();

        public HSStreetDetailsGridRepository(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId, int? QrStatus)
        {
            dataset = objRep.GetHSStreetDetailsData(wildcard, SearchString, fdate, tdate, userId, appId, QrStatus);
        }

        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataset.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }
    }
}
