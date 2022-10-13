using Newtonsoft.Json;
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

namespace SwachhBharatAbhiyan.CMS.Areas.Liquid.Controllers
{

    public class LiquidHomeController : Controller
    {


        IChildRepository childRepository;
        IMainRepository mainRepository;
        // GET: Liquid/LiquidHome
        public LiquidHomeController()
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
                var details = childRepository.GetLiquidDashBoardDetails();
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

        public ActionResult DashBoard()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                var details = childRepository.GetLiquidDashBoardDetails();

                if (details != null)
                {
                    string jsonstr = JsonConvert.SerializeObject(details);
                    return Json(jsonstr, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
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

                LiquidDashBoardRepository objRep = new LiquidDashBoardRepository();

                obj = objRep.getEmployeeLiquidTargetData(0, "", fdate, tdate, Convert.ToInt32(userId), SessionHandler.Current.AppId);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult EmployeeLiquidCollectionType()
        {
            if (SessionHandler.Current.AppId != 0)
            {

                IEnumerable<EmployeeLiquidCollectionType> obj;

                LiquidDashBoardRepository objRep = new LiquidDashBoardRepository();

                obj = objRep.getEmployeeLiquidCollectionType(SessionHandler.Current.AppId);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }
    }
}