using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.Repository.GridRepository
{
    public interface IGridRepository
    {
        string GetJqGridJson(string sord, int page, int rows, bool _search, NameValueCollection QueryString, Dictionary<string, string> FiltersFromSession, string sidx);
    }
}
