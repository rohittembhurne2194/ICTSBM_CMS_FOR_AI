using SwachBharat.CMS.Bll.Repository.ChildRepository;
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
using System.Xml;

namespace SwachhBharatAbhiyan.CMS.Controllers
{
    public class EmpShiftController : Controller
    {
        IChildRepository childRepository;
        IMainRepository mainRepository;
        public EmpShiftController()
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


        public ActionResult AddEmpShiftDetails(int teamId = -1)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                EmpShiftVM house = childRepository.GetEmpShiftById(teamId);
                return View(house);
            }
            else
                return Redirect("/Account/Login");
        }
        [HttpPost]
        public ActionResult AddEmpShiftDetails(EmpShiftVM empShift)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository.SaveEmpShift(empShift);
                return Redirect("Index");
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult CheckShiftName()
        {
            List<string> lstShifts = new List<string>();
            int AppID = SessionHandler.Current.AppId;
            childRepository = new ChildRepository(AppID);
            lstShifts = childRepository.CheckShiftName();

            return Json(lstShifts, JsonRequestBehavior.AllowGet);


        }
    }
}