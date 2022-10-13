using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace SwachhBharatAbhiyan.CMS.Controllers
{
    public class SauchalayController : Controller
    {
        IChildRepository childRepository;
        IMainRepository mainRepository;


        public SauchalayController()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                mainRepository = new MainRepository();
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
            }
            else
                Redirect("/Account/Login");
        }

        public ActionResult SauchalayIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult SauchalayMenuIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult SauchalayRegistrationIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult SauchalayRegistrationMenuIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult AddSauchalayDetails(int teamId = -2)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                SauchalayDetailsVM details = childRepository.GetSauchalayById(teamId);
                
                return View(details);

            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult AddSauchalayDetails(SauchalayDetailsVM data, HttpPostedFileBase filesUpload, HttpPostedFileBase filesUpload1)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);

                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);

                string path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;

                if (filesUpload != null)
                {
                    var guid = Guid.NewGuid().ToString().Split('-');
                    string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

                    string imagePath = Path.Combine(Server.MapPath("/Images/CTPTImages/Images"), image_Guid);
                    var exists = System.IO.Directory.Exists(Server.MapPath("Images/CTPTImages/Images"));

                    if (!exists)
                    {
                        // System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + "/GameImages"));
                        System.IO.Directory.CreateDirectory(Server.MapPath("/Images/CTPTImages/Images"));
                    }
                    filesUpload.SaveAs(imagePath);
                    //Info.Image = AppDetails.baseImageUrl + AppDetails.basePath + "/GameImages/" + image_Guid;
                    data.Image = baseUrl + "/Images/CTPTImages/Images/" + image_Guid;
                }

                if (filesUpload1 != null)
                {
                    var guid = Guid.NewGuid().ToString().Split('-');
                    string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

                    string imagePath = Path.Combine(Server.MapPath("/Images/CTPTImages/Qr"), image_Guid);
                    var exists = System.IO.Directory.Exists(Server.MapPath("/Images/CTPTImages/Qr"));

                    if (!exists)
                    {
                        // System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + "/GameImages"));
                        System.IO.Directory.CreateDirectory(Server.MapPath("//Images/CTPTImages/Qr"));
                    }
                    filesUpload1.SaveAs(imagePath);
                    //Info.Image = AppDetails.baseImageUrl + AppDetails.basePath + "/GameImages/" + image_Guid;
                    data.QrImage = baseUrl + "/Images/CTPTImages/Qr/" + image_Guid;
                }

                int teamId = data.Id;
                var houseId = childRepository.GetSauchalayById(teamId);

                SauchalayDetailsVM sauchalayDetails = childRepository.SaveSauchalay(data);
                return Redirect("SauchalayRegistrationIndex");
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MenuCtptMap()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult CtptMap()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                ViewBag.lat = SessionHandler.Current.Latitude;
                ViewBag.lang = SessionHandler.Current.Logitude;

                List<SauchalayDetailsVM> obj = new List<SauchalayDetailsVM>();
                obj = childRepository.GetCTPTLocation();
                return View(obj);
            }
            else
                return Redirect("/Account/Login");
        }
    }
}