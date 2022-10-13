using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwachhBharatAbhiyan.CMS.Controllers
{
    public class DumpyardEmployeeController : Controller
    {
        // GET: DumpyardEmployee
        // GET: Employee 
        IChildRepository childRepository;
        IMainRepository mainRepository;
        public DumpyardEmployeeController()
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


        public ActionResult AddEmployeeDetails(int teamId = -1)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                EmployeeDetailsVM house = childRepository.GetEmployeeById(teamId);
                return View(house);
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult AddEmployeeDetails(EmployeeDetailsVM emp, HttpPostedFileBase filesUpload)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
                if (filesUpload != null)
                {

                    var guid = Guid.NewGuid().ToString().Split('-');
                    string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

                    //Converting  Url to image 

                    string imagePath = Path.Combine(Server.MapPath(AppDetails.basePath + AppDetails.UserProfile), image_Guid);
                    var exists = System.IO.Directory.Exists(Server.MapPath(AppDetails.basePath + AppDetails.UserProfile));
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + AppDetails.UserProfile));
                    }
                    filesUpload.SaveAs(imagePath);
                    emp.userProfileImage = image_Guid;
                }

                childRepository.SaveEmployee(emp, "D");
                return Redirect("Index");
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpGet]
        public ActionResult DeleteEmployee(int teamId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository.DeleteEmployee(teamId);
                return Redirect("Index");
            }
            else
                return Redirect("/Account/Login");
        }


        public ActionResult EmployeeSummaryIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuEmployeeSummaryIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }


        [HttpPost]
        public string IsLIdEOnHSAndUM(string LoginId)
        {

            int AppID = SessionHandler.Current.AppId;
            childRepository = new ChildRepository(AppID);
            var isrecord = childRepository.GetLoginidData(LoginId);

            return isrecord;


        }
        [HttpPost]
        public string CheckName(string userName)
        {

            int AppID = SessionHandler.Current.AppId;
            childRepository = new ChildRepository(AppID);
            var isrecord = childRepository.GetUserName(userName);

            return isrecord;


        }
    }
}
