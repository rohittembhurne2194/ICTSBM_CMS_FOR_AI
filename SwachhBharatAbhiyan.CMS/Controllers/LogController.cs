using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwachhBharatAbhiyan.CMS.Controllers
{
    public class LogController : Controller
    {
        // GET: Log
        IChildRepository childRepository;
        public LogController()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
            }
            else
                Redirect("/Account/Login");
        }

        
        public ActionResult MenuIndex()
        {
            return View();
        }

        public ActionResult Tracking()
        {
            try
            {
                List<LogVM> house = new List<LogVM>();
                if (SessionHandler.Current.AppId != 0)
                {
                    house = childRepository.GetLogString();
                }
                else
                {
                    Redirect("/Account/Login");
                }
                return Json(house, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //throw ex;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

    }
}