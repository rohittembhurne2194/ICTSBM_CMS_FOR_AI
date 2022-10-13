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

namespace SwachhBharatAbhiyan.CMS.Controllers
{
    public class AttendenceBeatMapController : Controller
    {
        // GET: AttendenceBeatMap
        IMainRepository mainRepository;
        IChildRepository childRepository;
        public AttendenceBeatMapController()
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
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MonthlyAttedanceIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MonthlyAttedance()
        {
            if (SessionHandler.Current.AppId != 0)
            {

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

        //public ActionResult HouseRouteData(int daId, int areaid)
        //{
        //    if (SessionHandler.Current.AppId != 0)
        //    {

        //        List<SBALUserLocationMapView> obj = new List<SBALUserLocationMapView>();
        //        EmpBeatMapCountVM beatMapCount = new EmpBeatMapCountVM();
        //        obj = childRepository.GetHouseAttenRoute(daId, areaid);
        //        beatMapCount = childRepository.GetbeatMapCount(daId, areaid);
        //        // return Json(obj);

        //        return Json(new { markers = obj, mapCount = beatMapCount }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //        return Redirect("/Account/Login");

        //}

        public ActionResult HouseRouteData(int daId, int areaid, int polyId)
        {
            if (SessionHandler.Current.AppId != 0)
            {

                HouseAttenRouteVM obj = new HouseAttenRouteVM();
               
                obj = childRepository.GetBeatHouseAttenRoute(daId, areaid, polyId);
                
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

        public ActionResult HouseTimeWiseRouteData(string date = "", DateTime? fTime = null, DateTime? tTime = null, int? userId = null)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                List<SBALUserLocationMapView> obj = new List<SBALUserLocationMapView>();
                obj = childRepository.GetHouseTimeWiseRoute(date, fTime, tTime, userId);
                // return Json(obj);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }



       
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


        public ActionResult ListBeatMapArea(int daId, int areaid)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                List<SelectListItem> lstAreass = childRepository.ListBeatMapArea(daId, areaid);
                return Json(lstAreass, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }

    }
}