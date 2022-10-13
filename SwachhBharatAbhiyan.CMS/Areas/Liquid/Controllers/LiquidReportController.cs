using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using SwachBharat.CMS.Bll.ViewModels.MainModel;
using SwachBharat.CMS.Bll.ViewModels.SS2020Reports;

namespace SwachhBharatAbhiyan.CMS.Areas.Liquid.Controllers
{
    public class LiquidReportController : Controller
    {
        // GET: Liquid/LiquidReport
        IMainRepository mainRepository;
        IChildRepository childRepository;
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ReportEnable(string date, string userid)
        {

            mainRepository = new MainRepository();
            AppDetailsVM obj = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SingleEmployeeCollection()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["User_ID"] = SessionHandler.Current.UserId;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult Dumpyardidwise()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult GarbageCollectionPercentage()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["User_ID"] = SessionHandler.Current.UserId;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult EmployeeCollectionCount()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult EmployeeCollectionSummary()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["User_ID"] = SessionHandler.Current.UserId;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult EmployeeCollectionAverage()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["User_ID"] = SessionHandler.Current.UserId;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult AreawiseGarbageCollection()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult CitywiseGarbageReport()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;


                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult AreawiseGarbageTypeCollection()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult Dashboard1()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult DumpYardSummary()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult Housewise()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult Daywise()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
       
    }
}