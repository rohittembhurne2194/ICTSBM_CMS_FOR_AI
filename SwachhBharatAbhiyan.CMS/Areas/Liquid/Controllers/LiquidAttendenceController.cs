using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.Repository.GridRepository;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachBharat.CMS.Bll.ViewModels.Grid;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace SwachhBharatAbhiyan.CMS.Areas.Liquid.Controllers
{
    public class LiquidAttendenceController : Controller
    {
        IMainRepository mainRepository;
        IChildRepository childRepository;
        public LiquidAttendenceController()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                mainRepository = new MainRepository();
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
            }
            else
                Redirect("/Account/Login");
        }
        // GET: Attendence
        public ActionResult Index()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }



        public ActionResult Location(int daId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                ViewBag.daId = daId;
                return View();
            }
            else
                return Redirect("/Account/Login");

        }
        public ActionResult UserAttendenceLocation(int daId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                List<SBALUserLocationMapView> obj = new List<SBALUserLocationMapView>();
                obj = childRepository.GetUserAttenLocation(daId);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult UserRoute(int daId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                ViewBag.daId = daId;
                SBALUserLocationMapView obj = new SBALUserLocationMapView();
                obj = childRepository.GetHouseByIdforMap(-1, daId);
                return View(obj);
            }
            else
                return Redirect("/Account/Login");

        }

        public ActionResult UserRouteData(int daId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                List<SBALUserLocationMapView> obj = new List<SBALUserLocationMapView>();
                obj = childRepository.GetUserAttenRoute(daId);
                // return Json(obj);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }

        public ActionResult HouseRoute(int daId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                ViewBag.daId = daId;
                ViewBag.lat = SessionHandler.Current.Latitude;
                ViewBag.lang = SessionHandler.Current.Logitude;
                SBALUserLocationMapView obj = new SBALUserLocationMapView();
                obj = childRepository.GetHouseByIdforMap(-1, daId);
                return View(obj);
            }
            else
                return Redirect("/Account/Login");

        }
        [HttpPost]
        public ActionResult HouseRouteData(int daId, int areaid)
        {
            if (SessionHandler.Current.AppId != 0)
            {

                List<SBALUserLocationMapView> obj = new List<SBALUserLocationMapView>();
                obj = childRepository.GetLiquidAttenRoute(daId, areaid);
                // return Json(obj);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }

        public ActionResult UserTimeWiseRouteData(string date = "", DateTime? fTime = null, DateTime? tTime = null, int? userId = null)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                List<SBALUserLocationMapView> obj = new List<SBALUserLocationMapView>();
                obj = childRepository.GetUserTimeWiseRoute(date, fTime, tTime, userId);
                // return Json(obj);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }



        //public ActionResult AttendanceCount(DateTime? fdate = null, DateTime? tdate = null, int userId = 0)
        //{
        //    if (SessionHandler.Current.AppId != 0)
        //    {
        //        if (fdate == null)
        //        {
        //            string dt = DateTime.Now.ToString("MM/dd/yyyy");
        //            fdate = Convert.ToDateTime(dt + " " + "00:00:00");
        //            tdate = Convert.ToDateTime(dt + " " + "23:59:59");
        //        }

        //        IEnumerable<SBAAttendenceGrid> obj;

        //       // IEnumerable<SBAAttendenceGrid> obj1;

        //        DashBoardRepository objRep = new DashBoardRepository();

        //        obj = objRep.GetAttendeceData(0, "", fdate, tdate, Convert.ToInt32(userId), SessionHandler.Current.AppId).ToList();



        //        //var abc = obj.Select(o => o.userId).AsEnumerable().Distinct();

        //        var abc = obj.GroupBy(o => o.userId ).Select(o => o.First());
        //        //var abc = obj.

        //      //  obj1 = abc;

        //        return Json(abc, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //        return Redirect("/Account/Login");
        //}

        public ActionResult LoadWardNoList(int ZoneId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                HouseDetailsVM obj = new HouseDetailsVM();
                try
                {
                    obj.WardList = childRepository.LoadListWardNo(ZoneId);
                }
                catch (Exception ex) { throw ex; }

                return Json(obj.WardList, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }

        [HttpPost]
        public ActionResult LoadAreaList(int WardNo)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                HouseDetailsVM obj = new HouseDetailsVM();
                try
                {
                    obj.AreaList = childRepository.LoadListArea(WardNo);
                }
                catch (Exception ex) { throw ex; }

                return Json(obj.AreaList, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }


    }
}