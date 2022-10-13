using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwachhBharatAbhiyan.CMS.Controllers
{
    public class LeagueController : Controller
    {

        IChildRepository childRepository;
        IMainRepository mainRepository;
       

        public LeagueController()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                mainRepository = new MainRepository();
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
            }
            else
                Redirect("/Account/Login");
        }
        // GET: League
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuIndex()
        {
            return View();
        }


        public ActionResult Feedbacklogin()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                //Session["NewAppID"] = SessionHandler.Current.AppId;
                // Session["YoccClientID"] = SessionHandler.Current.YoccClientID;
                //ViewBag.IframeUrl = "https://way2voice.in/";
                //int ClientID = 0;
                var ClientID = SessionHandler.Current.YoccClientID;
                //if (SessionHandler.Current.AppId == 1003)
                //{

                //    ClientID = 735;
                //}
                //else if (SessionHandler.Current.AppId == 1)
                //{

                //    ClientID = 570;
                //}
                return Redirect("https://way2voice.in/Home/Feedbacklogin?ClientID=" + ClientID);
            }
            else
                return Redirect("/Account/Login");
        }

    }
}