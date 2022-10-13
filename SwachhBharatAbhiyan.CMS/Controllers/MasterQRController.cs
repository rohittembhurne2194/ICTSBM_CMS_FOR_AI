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
    public class MasterQRController : Controller
    {
        // GET: MasterQR
        // GET: HouseMaster  
        IChildRepository childRepository;
        IMainRepository mainRepository;

        public MasterQRController()
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


        public ActionResult AddMasterQRDetails(int teamId = -2 , string houseId = "")
        {
            if (SessionHandler.Current.AppId != 0)
            {
                MasterQRDetailsVM house = childRepository.GetMasterQRById(teamId , houseId);


                return View(house);


            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult AddMasterQRDetails(MasterQRDetailsVM emp)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository.SaveMasterQrDetails(emp);
                return Redirect("Index");


            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult QRBunchIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["AppName"] = SessionHandler.Current.AppName;
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MenuQRBunchIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult AddMasterQRBunchDetails(int teamId = -2)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                MasterQRDetailsVM house = childRepository.GetMasterQRBunchById(teamId);


                return View(house);


            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult AddMasterQRBunchDetails(MasterQRDetailsVM emp)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository.SaveMasterQrBunchDetails(emp);
                return Redirect("QRBunchIndex");


            }
            else
                return Redirect("/Account/Login");
        }
    }
}