using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Newtonsoft.Json;
using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using SwachBharat.CMS.Dal.DataContexts;
using GramPanchayat.API.Bll.ViewModels.ChildModel.Model;
using System.Collections.Generic;
using System.Globalization;

namespace SwachhBharatAbhiyan.CMS.Controllers
{
    public class EmpBeatMapController : Controller
    {
        IChildRepository childRepository;
        IMainRepository mainRepository;

        public EmpBeatMapController()
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
                Session["AppName"] = SessionHandler.Current.AppName;
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
        public ActionResult AddEmpBeatMap(int ebmId = -1)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                ViewBag.lat = SessionHandler.Current.Latitude;
                ViewBag.lang = SessionHandler.Current.Logitude;
                EmpBeatMapVM empBeatMap = childRepository.GetEmpBeatMap(ebmId);
                return View(empBeatMap);
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult SaveEmpBeatMap(EmpBeatMapVM EBMObj)
        {

            if (SessionHandler.Current.AppId != 0)
            {
                // return PartialView("AreaMaster");
                childRepository.SaveEmpBeatMap(EBMObj);
                return Json(new { data = "Beat Map Saved Successfully"}, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }

        [HttpGet]
        public ActionResult GetEmpBeatMap(int userId)
        {

            if (SessionHandler.Current.AppId != 0)
            {
                // return PartialView("AreaMaster");
                EmpBeatMapVM empBeatMap =   childRepository.GetEmpBeatMap(userId);
                return Json(empBeatMap, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }

        public ActionResult ListUserBeatMap(string Emptype)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                if (Emptype == "W")
                {
                    Emptype = null;
                }
                List<SelectListItem> lstUsers = childRepository.ListUserBeatMap(Emptype);
                return Json(lstUsers, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }


        //[HttpPost]
        //public JsonResult IsPointInPolygon(EmpBeatMapVM EBMObj)
        //{
        //    bool isInside = false;
        //    if (SessionHandler.Current.AppId != 0)
        //    {
        //        // return PartialView("AreaMaster");
        //        isInside = childRepository.IsPointInPolygon(EBMObj.ebmId, EBMObj.ebmLatLong[0]);
        //        return Json(isInside, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //        return Json(false, JsonRequestBehavior.AllowGet);

        //}
    }
}