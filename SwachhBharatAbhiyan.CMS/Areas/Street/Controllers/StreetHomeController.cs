using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.Repository.GridRepository;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SwachhBharatAbhiyan.CMS.Areas.Street.Controllers
{
    public class StreetHomeController : Controller
    {


        IChildRepository childRepository;
        IMainRepository mainRepository;
        // GET: Liquid/LiquidHome
        public StreetHomeController()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                mainRepository = new MainRepository();
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
            }
            else
                Redirect("/Account/Login");
        }
        public ActionResult Index()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                ViewBag.lat = SessionHandler.Current.Latitude;
                ViewBag.lang = SessionHandler.Current.Logitude;
                ViewBag.YoccFeddbackLink = SessionHandler.Current.YoccFeddbackLink;
                TempData.Keep();
                var details = childRepository.GetStreetDashBoardDetails();
                return View(details);
            }
            else
                return Redirect("/Account/Login");
        }


        public ActionResult MenuIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                //TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult EmployeeTargetCount(DateTime? fdate = null, DateTime? tdate = null, int userId = 0)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                if (fdate == null)
                {
                    string dt = DateTime.Now.ToString("MM/dd/yyyy");
                    fdate = Convert.ToDateTime(dt + " " + "00:00:00");
                    tdate = Convert.ToDateTime(dt + " " + "23:59:59");
                }

                IEnumerable<DashBoardVM> obj;

                StreetDashBoardRepository objRep = new StreetDashBoardRepository();

                obj = objRep.getEmployeeStreetTargetData(0, "", fdate, tdate, Convert.ToInt32(userId), SessionHandler.Current.AppId);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult EmployeeStreetCollectionType()
        {
            if (SessionHandler.Current.AppId != 0)
            {

                IEnumerable<EmployeeStreetCollectionType> obj;

                StreetDashBoardRepository objRep = new StreetDashBoardRepository();

                obj = objRep.getEmployeeStreetCollectionType(SessionHandler.Current.AppId);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }

    }
}