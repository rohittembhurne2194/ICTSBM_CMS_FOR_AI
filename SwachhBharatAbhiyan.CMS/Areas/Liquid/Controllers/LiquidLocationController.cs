using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SwachBharat.CMS.Dal.DataContexts;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachhBharatAbhiyan.CMS.Models;

namespace SwachhBharatAbhiyan.CMS.Areas.Liquid.Controllers
{
    public class LiquidLocationController : Controller
    {
        IMainRepository mainRepository;
        IChildRepository childRepository;
        public LiquidLocationController()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                mainRepository = new MainRepository();
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
            }
            else
                Redirect("/Account/Login");
        }


        // GET: Location
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
        public ActionResult MenuMapIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult ViewLocation(int teamId = -1)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                SBALUserLocationMapView loc = childRepository.GetLocation(teamId, "L");
                TempData.Keep();
                return View(loc);
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult ShowGrid()// default or index Method
        {
            if (SessionHandler.Current.AppId != 0)
            {
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult AllUserLocation()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                TempData.Keep();
                ViewBag.lat = SessionHandler.Current.Latitude;
                ViewBag.lang = SessionHandler.Current.Logitude;
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult LocatioList(string date, string userid)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                if (date == null || date == "")
                {
                    date = DateTime.Now.ToShortDateString();
                }

                List<SBALUserLocationMapView> obj = new List<SBALUserLocationMapView>();
                obj = childRepository.GetAllUserLocation(date,"L");
                // return Json(obj);
                if (userid != null && userid != "null" && userid != "-1")
                {
                    obj = obj.Where(c => c.userId == Convert.ToInt32(userid)).ToList();
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult UserWiseLocatioList(int userId, string date)
        {
            if (SessionHandler.Current.AppId != 0)
            {

                if (date == null || date == "")
                {
                    date = DateTime.Now.ToShortDateString();
                }
                else
                {
                    date = date.ToString();

                }
                List<SBALUserLocationMapView> obj = new List<SBALUserLocationMapView>();
                obj = childRepository.GetUserWiseLocation(userId, date,"L");
                // return Json(obj);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult UserList(string rn)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                SBALUserLocationMapView obj = new SBALUserLocationMapView();

                obj = childRepository.GetLocation(-1, rn);
                return Json(obj.UserList, JsonRequestBehavior.AllowGet);

            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult UserCurrentLocation(int userId, string date)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                if (date == null || date == "")
                {
                    date = DateTime.Now.ToShortDateString();
                }
                List<SBALUserLocationMapView> obj = new List<SBALUserLocationMapView>();
                obj = childRepository.GetAllUserLocation(date,"L").Where(c => c.userId == userId).ToList();
                // return Json(obj);
                return Json(obj, JsonRequestBehavior.AllowGet);

            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult GetAddress(string location)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                string obj;


                obj = childRepository.Address(location);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }


        public ActionResult LocatioAdmin(string date, string userid)
        {

            if (date == null || date == "")
            {
                date = DateTime.Now.ToShortDateString();
            }

            List<SBALUserLocationMapView> obj = new List<SBALUserLocationMapView>();
            childRepository = new ChildRepository(1);
            obj = childRepository.GetAdminLocation();
            // return Json(obj);
            if (userid != null && userid != "null" && userid != "-1")
            {
                obj = obj.Where(c => c.userId == Convert.ToInt32(userid)).ToList();
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Login(string courseID)
        {


            return Json(courseID, JsonRequestBehavior.AllowGet);
        }

        #region HouseOnMap

        public ActionResult MenuHouseMapIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult AllHouseLocation()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                ViewBag.lat = SessionHandler.Current.Latitude;
                ViewBag.lang = SessionHandler.Current.Logitude;
                ViewBag.AppName = SessionHandler.Current.AppName;

                var details = childRepository.GetLiquidWasteDetails();
                return View(details);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult AllLiquidWasteLocation()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                ViewBag.lat = SessionHandler.Current.Latitude;
                ViewBag.lang = SessionHandler.Current.Logitude;
                ViewBag.AppName = SessionHandler.Current.AppName;

                var details = childRepository.GetLiquidWasteDetails();
                return View(details);
            }
            else
                return Redirect("/Account/Login");
        }

        //Code Optimization (code)
        //public ActionResult LocatioList(string date, string userid)
        //{
        //    if (SessionHandler.Current.AppId != 0)
        //    {
        //        if (date == null || date == "")
        //        {
        //            date = DateTime.Now.ToShortDateString();
        //        }

        //        List<SBALUserLocationMapView> obj = new List<SBALUserLocationMapView>();
        //      obj = childRepository.GetAllUserLocation(date);
        //        // return Json(obj);
        //        if (userid != null && userid != "null" && userid != "-1")
        //        {
        //            obj = obj.Where(c => c.userId == Convert.ToInt32(userid)).ToList();
        //        }
        //        return Json(obj, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //        return Redirect("/Account/Login");
        //}
        //public ActionResult HouseLocationList(string date, string userid, string areaId, string wardNo, string SearchString,string start)
        //{
        //    if (SessionHandler.Current.AppId != 0)
        //    {
        //        int user;
        //        int area;
        //        int ward;
        //        if (userid == "-1" || userid == "0" || userid == "null")
        //        {
        //            user = 0;
        //        }
        //        else {
        //            user = Convert.ToInt32(userid);
        //        }

        //        if (areaId == "-1" || areaId == "0" || areaId == "null")
        //        {
        //            area = 0;
        //        }
        //        else {
        //            area = Convert.ToInt32(areaId);
        //        }
        //        if (wardNo == "-1" || wardNo == "0" || wardNo == "null")
        //        {
        //            ward = 0;
        //        }
        //        else {
        //            ward = Convert.ToInt32(wardNo);
        //        }


        //        if (date == null || date == "")
        //        {
        //            date = DateTime.Now.ToShortDateString();
        //        }

        //        SBALHouseLocationMapView1 obj = new SBALHouseLocationMapView1();
        //        obj = childRepository.GetAllHouseLocation(date, user, area, ward, SearchString, start);
        //        // return Json(obj);
        //        //if (houseid != null && houseid != "null" && houseid != "-1")
        //        //{
        //        //    obj = obj.Where(c => c.houseId == Convert.ToInt32(houseid)).ToList();
        //        //}
        //       // return Json(obj, JsonRequestBehavior.AllowGet);

        //        var jsonResult = Json(obj, JsonRequestBehavior.AllowGet);
        //        jsonResult.MaxJsonLength = int.MaxValue;
        //        return jsonResult;
        //    }
        //    else
        //        return Redirect("/Account/Login");
        //}

        public ActionResult HouseLocationList(string date, string userid, string areaId, string wardNo, string SearchString, string garbageType, string filterType)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                int user;
                int area;
                int ward;
                int? GarbageType;
                int FilterType;
                if (userid == "-1" || userid == "0" || userid == "null")
                {
                    user = 0;
                }
                else
                {
                    user = Convert.ToInt32(userid);
                }

                if (areaId == "-1" || areaId == "0" || areaId == "null")
                {
                    area = 0;
                }
                else
                {
                    area = Convert.ToInt32(areaId);
                }
                if (wardNo == "-1" || wardNo == "0" || wardNo == "null")
                {
                    ward = 0;
                }
                else
                {
                    ward = Convert.ToInt32(wardNo);
                }
                if (garbageType == "-1" || garbageType == null)
                {
                    GarbageType = null;
                }
                else
                {
                    GarbageType = Convert.ToInt32(garbageType);
                }
                if (filterType == "-1" || filterType == "0" || filterType == "null")
                {
                    FilterType = 0;
                }
                else
                {
                    FilterType = Convert.ToInt32(filterType);
                }
                if (date == null || date == "")
                {
                    date = DateTime.Now.ToShortDateString();
                }

                List<SBALHouseLocationMapView> obj = new List<SBALHouseLocationMapView>();
                obj = childRepository.GetAllHouseLocation(date, user, area, ward, SearchString, GarbageType, FilterType,"L");
                // return Json(obj);
                //if (houseid != null && houseid != "null" && houseid != "-1")
                //{
                //    obj = obj.Where(c => c.houseId == Convert.ToInt32(houseid)).ToList();
                //}
                // return Json(obj, JsonRequestBehavior.AllowGet);

                var jsonResult = Json(obj, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else
                return Redirect("/Account/Login");
        }
        #endregion
    }
}