using SwachBharat.CMS.Bll.Repository.GridRepository;
using SwachBharat.CMS.Bll.Repository.GridRepository.Grid;
using SwachBharat.CMS.Bll.Repository.RepositoryGrid.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwachhBharatAbhiyan.CMS.Controllers
{

    public class GridController : Controller
    {
        // GET: Grid
        IGridRepository gridRepository;

        public string GetJqGridJson(string sord, int page, int rows, bool _search, string rn, string sidx = "1", long wildcard = 0, int receiptId = 0, string searchString = "", string fdate = "", string tdate = "", string RadioData = "")
        {

            Dictionary<string, string> filtersFromSession = CreateFiltersDictionary(Session);

            gridRepository = GetRepository(rn, wildcard, searchString, fdate, tdate, RadioData);
            string x = gridRepository.GetJqGridJson(sord, page, rows, _search, Request.QueryString, filtersFromSession, sidx);
            return gridRepository.GetJqGridJson(sord, page, rows, _search, Request.QueryString, filtersFromSession, sidx);
        }

        private Dictionary<string, string> CreateFiltersDictionary(System.Web.HttpSessionStateBase Session)
        {

            Dictionary<string, string> FiltersDictionary = new Dictionary<string, string>();

            foreach (string key in Session.Keys)
            {
                FiltersDictionary.Add(key, Session[key] == null ? null : Session[key].ToString());
            }

            return FiltersDictionary;
        }

        private IGridRepository GetRepository(string RepositoryName, long wildcard = 0, string searchString = "", string fdate = "", string tdate = "", string radiodata = "")
        {
            //var appId = Convert.ToInt32(Session["AppId"].ToString());
             var appId = 1;
           
            return null;
        }

    }
}