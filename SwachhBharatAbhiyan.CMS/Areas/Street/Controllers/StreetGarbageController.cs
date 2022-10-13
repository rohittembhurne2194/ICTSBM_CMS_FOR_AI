using SwachBharat.CMS.Bll.Repository.GridRepository;
using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.ViewModels.Grid;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwachhBharatAbhiyan.CMS.Areas.Street.Controllers
{
    public class StreetGarbageController : Controller
    {
        IMainRepository mainRepository;
        IChildRepository childRepository;


        // GET: Liquid/LiquidHome
        public StreetGarbageController()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                mainRepository = new MainRepository();
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
            }
            else
                Redirect("/Account/Login");
        }
        // GET: Street/StreetGarbage
        public ActionResult StreetGarbageIndex()
        {

            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuStreetGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }


        public ActionResult MenuStreetBitMapIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult StreetBitMapIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult PointGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuPointGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuStreetDetailGarbageIndex(int teamId, string fdate, string tdate, int param1)
        {

            ViewBag.userid = teamId;
            ViewBag.fdate = fdate;
            ViewBag.tdate = tdate;
            ViewBag.Rown = param1;
            return PartialView(@"/Areas/Street/Views/Shared/_StreetBeatMap.cshtml");


        }
        public ActionResult DumpYardIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                var details = childRepository.GetDashBoardDetails();
                return View(details);
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuDumpYardIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MenuEntryDetails()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult EntryDetails()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuIdealtime()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult Idealtime()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult IdleTime_Route()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                //ViewBag.userId = userId;
                //ViewBag.Date = Date;
                //return Json(userId, JsonRequestBehavior.AllowGet);
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }


        public ActionResult IdelTimeNotification()
        {
            if (SessionHandler.Current.AppId != 0)

            {
                List<SBAEmplyeeIdelGrid> obj = new List<SBAEmplyeeIdelGrid>();
                obj = childRepository.GetStreetIdelTimeNotification();

                //List<SBAEmplyeeIdelGrid> obj = new List<SBAEmplyeeIdelGrid>()
                //{
                //     new SBAEmplyeeIdelGrid() { UserName = " sad", StartTime = "50", EndTime = "1", IdelTime = "00:22" },
                //       new SBAEmplyeeIdelGrid() { UserName = " gfhfh", StartTime = "50", EndTime = "1", IdelTime = "00:22" },
                //   //........................ and so on
                //};
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }
    }
}