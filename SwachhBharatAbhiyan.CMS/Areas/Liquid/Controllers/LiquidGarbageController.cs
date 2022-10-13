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

namespace SwachhBharatAbhiyan.CMS.Areas.Liquid.Controllers
{
    public class LiquidGarbageController : Controller
    {
     
        IMainRepository mainRepository;
        IChildRepository childRepository;

        public LiquidGarbageController()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                mainRepository = new MainRepository();
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
            }
            else
                Redirect("/Account/Login");
        }
        // GET: Liquid/LiquidGarbage
        public ActionResult LiquidGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuLiquidGarbageIndex()
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
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
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
                TempData.Keep();
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
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult Idealtime()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        //Add by neha 12 june 2019
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

        //Add by neha 12 june 2019
        //public ActionResult IdleTimeRouteData(int userId, string Date)
        //{
        //    if (SessionHandler.Current.AppId != 0)
        //    {

        //        List<SBAEmplyeeIdelGrid> obj = new List<SBAEmplyeeIdelGrid>();
        //        obj = childRepository.GetIdleTimeRoute(userId, Date);

        //        // return Json(obj);
        //        return Json(obj, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //        return Redirect("/Account/Login");

        //}
        //public ActionResult UserCount(DateTime? fdate = null, DateTime? tdate = null, int userId = 0)
        //{
        //    if (SessionHandler.Current.AppId != 0)
        //    {
        //        if (fdate == null)
        //        {
        //            string dt = DateTime.Now.ToString("MM/dd/yyyy");
        //            fdate = Convert.ToDateTime(dt + " " + "00:00:00");
        //            tdate = Convert.ToDateTime(dt + " " + "23:59:59");
        //        }

        //        IEnumerable<SBAGarbageCountDetails> obj;

        //        DashBoardRepository objRep = new DashBoardRepository();

        //        obj = objRep.GetGarbageCountData(0, "", fdate, tdate, Convert.ToInt32(userId), SessionHandler.Current.AppId);
        //        return Json(obj, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //        return Redirect("/Account/Login");
        //}

        public ActionResult IdelTimeNotification()
        {
            if (SessionHandler.Current.AppId != 0)

            {
                List<SBAEmplyeeIdelGrid> obj = new List<SBAEmplyeeIdelGrid>();
                obj = childRepository.GetLiquidIdelTimeNotification();

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
  