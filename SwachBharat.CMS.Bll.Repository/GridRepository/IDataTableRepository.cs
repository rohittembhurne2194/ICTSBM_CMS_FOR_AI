using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.Repository.GridRepository
{
  public  interface IDataTableRepository
    {
        string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start);

    }
}
