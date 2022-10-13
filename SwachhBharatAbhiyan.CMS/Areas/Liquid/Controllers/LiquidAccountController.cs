using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SwachBharat.CMS.Bll.Services.Support;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.ViewModels.MainModel;

namespace SwachhBharatAbhiyan.CMS.Areas.Liquid.Controllers
{

    [Authorize]
    [AllowAnonymous]
    public class LiquidAccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IMainRepository mainrepository;

        public LiquidAccountController()
        {
            // AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            mainrepository = new MainRepository();
        }

        public LiquidAccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // GET: Liquid/LiquidAccount
        public ActionResult Index()
        {
            return View();
        }

        // POST: /Account/LogOff
        [HttpPost]
        //   [ValidateAntiForgeryToken]
        public ActionResult LogOff(string returnUrl)
        {
            Session["status"] = null; //it's my session variable
            SessionHandler.Current.AppId = 0;
            returnUrl = null;
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut(); //you write this when you use FormsAuthentication
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AddSession(null, null, null, null);
            RouteData.Values.Remove("ReturnUrl");      
            return RedirectToAction("Login", "Account", new { area = "" });
          
        }



        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        #endregion

        #region CustomChanges
        [NonAction]
        private void AddSession(string UserId, string UserRole, string UserEmail, string UserName)
        {
            try
            {
                int AppId = mainrepository.GetUserAppId(UserId);
                if (AppId != 0)
                {
                    AppDetailsVM ApplicationDetails = mainrepository.GetApplicationDetails(AppId);
                    string DB_Connect = mainrepository.GetDatabaseFromAppID(AppId);
                    SessionHandler.Current.UserId = UserId;
                    SessionHandler.Current.UserRole = UserRole;
                    SessionHandler.Current.UserEmail = UserEmail;
                    SessionHandler.Current.UserName = UserName;
                    SessionHandler.Current.AppId = ApplicationDetails.AppId;
                    SessionHandler.Current.AppName = ApplicationDetails.AppName;
                    SessionHandler.Current.IsLoggedIn = true;
                    SessionHandler.Current.Type = ApplicationDetails.Type;
                    SessionHandler.Current.Latitude = ApplicationDetails.Latitude;
                    SessionHandler.Current.Logitude = ApplicationDetails.Logitude;
                    SessionHandler.Current.DB_Name = DB_Connect;
                    SessionHandler.Current.YoccClientID = ApplicationDetails.YoccClientID;
                    SessionHandler.Current.GramPanchyatAppID = ApplicationDetails.GramPanchyatAppID;
                    SessionHandler.Current.YoccFeddbackLink = ApplicationDetails.YoccFeddbackLink;
                    SessionHandler.Current.YoccDndLink = ApplicationDetails.YoccDndLink;
                }
                else
                {
                    SessionHandler.Current.UserId = null;
                    SessionHandler.Current.UserRole = null;
                    SessionHandler.Current.UserEmail = null;
                    SessionHandler.Current.UserName = null;
                    SessionHandler.Current.AppId = 0;
                    SessionHandler.Current.AppName = null;
                    SessionHandler.Current.IsLoggedIn = false;
                    SessionHandler.Current.Type = null;
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


        #endregion
    }

}