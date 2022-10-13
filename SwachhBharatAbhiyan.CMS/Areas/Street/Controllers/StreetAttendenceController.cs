using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;

namespace SwachhBharatAbhiyan.CMS.Areas.Street.Controllers
{
    public class StreetAttendenceController : Controller
    {
        IMainRepository mainRepository;
        IChildRepository childRepository;
        public StreetAttendenceController()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                mainRepository = new MainRepository();
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
            }
            else
                Redirect("/Account/Login");
        }

        // GET: Street/StreetAttendence
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

        public ActionResult HouseRouteData(int daId,int areaid)
        {
            if (SessionHandler.Current.AppId != 0)
            {

                List<SBALUserLocationMapView> obj = new List<SBALUserLocationMapView>();
                obj = childRepository.GetStreetAttenRoute(daId, areaid);
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

    }
}