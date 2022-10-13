using Microsoft.AspNet.Identity;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.ViewModels.MainModel;
using SwachhBharatAbhiyan.CMS.Models;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Owin.Security;

namespace SwachhBharatAbhiyan.CMS.Controllers
{
    public class AdminController : Controller
    {
         IMainRepository mainrepository;
        public AdminController()
        {
            // AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            mainrepository = new MainRepository();
        }
        // GET: Admin
        public ActionResult MenuIndex()
        {
            if (SessionHandler.Current.UserEmail != null)
            {
                List<MenuItem> menuList = GetMenus();
                return View(menuList);
            }
           
            else
            {
                return Redirect("/Admin/Login");
            }
        }

        //public ActionResult MenuIndex()
        //{
        //    List<MenuItemULB> menuList = GetULBMenus();
        //    return View("MenuIndex2", menuList);
        //}
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Menus()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult login(LoginViewModel model, string returnUrl)
        {
            if (model.Password == "Admin#123" & model.Email == "Admin")
            {
                string UserEmail = model.Email;
                Session["UserName"] = model.Email;
                AddSession(UserEmail);
                return RedirectToAction("MenuIndex");
            }
            else {
                return View();

            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult login1(LoginViewModel model, string returnUrl)
        {
            if (model.Password == "Admin#123" & model.Email == "Admin")
            {
                
                return RedirectToAction("MenuIndex");
            }
            else
            {
                return View();

            }
        }


        public List<MenuItem> GetMenus()
        {
            List<MenuItem> menuList = new List<MenuItem>();
            menuList = mainrepository.GetMenus();
            return menuList;
        }


        [NonAction]
        private void AddSession(string UserEmail)
        {
            try
            {
                

                if (UserEmail != null)
                {
                 
                    SessionHandler.Current.UserEmail = UserEmail;
                }
                else
                {
               
                    SessionHandler.Current.UserEmail = null;
                }
                // if (SessionHandler.Current.Type.Trim() == "np")
                // {
                //     SessionHandler.Current.sessionType = "नगर पंचायत | Our Nagar Panchayat";
                // }
                // else
                //if (SessionHandler.Current.Type.Trim() == "npp")
                // {
                //     SessionHandler.Current.sessionType = "नगरपरिषद | Municipal Council";
                // }
                // else
                //     if (SessionHandler.Current.Type.Trim() == "gp")
                // {
                //     SessionHandler.Current.sessionType = "ग्रामपंचायत | Gram Panchayat";
                // }
                // else
                // {
                //     SessionHandler.Current.sessionType = "ग्रामपंचायत | Gram Panchayat";
                // }

            }
            catch (Exception exception)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["__MySession__"] = null; //it's my session variable
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut(); 
            AddSession(null);
            return RedirectToAction("Login", "Admin");
        }

    }
}