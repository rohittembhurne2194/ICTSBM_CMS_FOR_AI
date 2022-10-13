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
    public class WasteManagementController : Controller
    {
        // GET: WasteManagement
        IChildRepository childRepository;
        IMainRepository mainRepository;

        public WasteManagementController()
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
            return View();
        }
       
        public ActionResult WasteIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        
        public ActionResult MenuWasteIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuAddWasteDetails()
        {
            return View();
        }
        public ActionResult AddWasteDetails(int teamId = -2)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                WasteDetailsVM waste = childRepository.GetWasteById(teamId);

                //List<WasteDetailsVM> obj = new List<WasteDetailsVM>()
                //{
                //   new WasteDetailsVM() {SubCategoryID=1,Weight=50,UserID = 1 },
                //   new WasteDetailsVM() {SubCategoryID=2,Weight=100,UserID = 1 },
                //   new WasteDetailsVM() {SubCategoryID=3,Weight=150,UserID = 1 },
                //   //........................ and so on
                //};
                //var c = AddWasteDetails(obj);

                return View(waste);


            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult AddWasteDetails(String waste)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                //List<WasteDetailsVM> obj = new List<WasteDetailsVM>()
                //{
                //   new WasteDetailsVM() {ID=1,SubCategoryID=1,Weight=50,UserID = 1 },
                //   new WasteDetailsVM() {ID=1,SubCategoryID=2,Weight=100,UserID = 1 },
                //   new WasteDetailsVM() {ID=1,SubCategoryID=3,Weight=150,UserID = 1 },
                //   //........................ and so on
                //};

                //List<WasteDetailsVM> obj2 = JsonConvert.DeserializeObject<List<WasteDetailsVM>>(waste);
                var obj1 = childRepository.SaveWaste(waste);
               
                if (obj1 == "Success")
                {
                    return Content(obj1);
                }
                else {

                    return Content(obj1);
                }
            }
                 
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MenuAddSalesWasteDetails()
        {
            return View();
        }
        public ActionResult AddSalesWasteDetails(int teamId = -2)
        {
            if (SessionHandler.Current.AppId != 0)
            {
               WasteDetailsVM waste = childRepository.GetWasteById(teamId);

                //List<WasteDetailsVM> obj = new List<WasteDetailsVM>()
                //{
                //   new WasteDetailsVM() {SubCategoryID=1,Weight=50,UserID = 1 },
                //   new WasteDetailsVM() {SubCategoryID=2,Weight=100,UserID = 1 },
                //   new WasteDetailsVM() {SubCategoryID=3,Weight=150,UserID = 1 },
                //   //........................ and so on
                //};
                //var c = AddWasteDetails(obj);

                return View(waste);


            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult AddSalesWasteDetails(String waste1)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                //List<WasteDetailsVM> obj = new List<WasteDetailsVM>()
                //{
                //   new WasteDetailsVM() {ID=1,SubCategoryID=1,Weight=50,UserID = 1 },
                //   new WasteDetailsVM() {ID=1,SubCategoryID=2,Weight=100,UserID = 1 },
                //   new WasteDetailsVM() {ID=1,SubCategoryID=3,Weight=150,UserID = 1 },
                //   //........................ and so on
                //};

                //List<WasteDetailsVM> obj2 = JsonConvert.DeserializeObject<List<WasteDetailsVM>>(waste);
                var obj1 = childRepository.SaveWaste(waste1);

                if (obj1 == "Success")
                {
                    return Content(obj1);
                }
                else {

                    return Content(obj1);
                }
            }

            else
                return Redirect("/Account/Login");
        }

        public ActionResult WasteCategoryList()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                WasteDetailsVM obj = new WasteDetailsVM();

                obj = childRepository.GetWasteById(-1);
                return Json(obj.WasteCategoryList, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult WasteSubCategoryList(int categoryId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                WasteDetailsVM obj = new WasteDetailsVM();
                try
                {
                    obj.WasteSubCategoryList = childRepository.LoadListSubCategory(categoryId);
                }

                catch (Exception ex) { throw ex; }
                return Json(obj.WasteSubCategoryList, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult UserList()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                WasteDetailsVM obj = new WasteDetailsVM();

                obj.WM_UserList = childRepository.GetWMUser();
                return Json(obj.WM_UserList, JsonRequestBehavior.AllowGet);

            }
            else
                return Redirect("/Account/Login");
        }

    }
}