using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.Repository.GridRepository;
using SwachBharat.CMS.Bll.Repository.GridRepository.Grid;
using SwachBharat.CMS.Bll.Repository.RepositoryGrid.Grid;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SwachBharat.CMS.Dal.DataContexts;
using SwachhBharatAbhiyan.CMS.Controllers;

namespace SwachhBharatAbhiyan.CMS.Controllers
{
    public class InfotainmentController : Controller
    {
        IChildRepository childRepository;
        IMainRepository mainRepository;
        IDataTableRepository gridRepository;

       //public static int abc = SessionHandler.Current.AppId;
        //abc = SessionHandler.Current.AppId;
        public InfotainmentController()
        {
            mainRepository = new MainRepository();

            if (SessionHandler.Current.AppId != 0)
            {
                //mainRepository = new MainRepository();
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
            }
            else
                Redirect("/Account/Login");
        }

        //GET: Infotainment
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
            return View();
        }
         
        public ActionResult IndexExternal()
        {
            return View();
        }

        public ActionResult MenuIndexExternal()
        {
            return View();
        }

        public ActionResult MenuInfotainmentDetails()
        {
            return View();
        }

        public ActionResult InfotainmentDetails()
        {
            return View();
        }

        public ActionResult AddInfotainmentDetails(int ID = -1)
        {
            //if (SessionHandler.Current.AppId != 0)
            //{
            //    InfotainmentDetailsVW Details = mainRepository.GetInfotainmentDetailsById(ID);
            //    return View(Details);
            //}
            //else
            //    return Redirect("/Account/Login");

            InfotainmentDetailsVW Details = mainRepository.GetInfotainmentDetailsById(ID);
            return View(Details);
        }

        [HttpPost]
        public ActionResult AddInfotainmentDetails(InfotainmentDetailsVW Info, HttpPostedFileBase filesUpload)
        {

            //string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

            string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);


            //if (SessionHandler.Current.AppId != 0)
            //{

            //var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
            var AppDetails = mainRepository.GetApplicationDetails( 1 );

            string path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;

            if (filesUpload != null)
                {
                    var guid = Guid.NewGuid().ToString().Split('-');
                    string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

                //Converting  Url to image 

                //string imagePath = Path.Combine(Server.MapPath(AppDetails.basePath + "/GameImages"), image_Guid);
                //var exists = System.IO.Directory.Exists(Server.MapPath(AppDetails.basePath + "/GameImages"));

                string imagePath = Path.Combine(Server.MapPath("/Images/GameImages"), image_Guid);
                var exists = System.IO.Directory.Exists(Server.MapPath("/Images/GameImages"));

                if (!exists)
                    {
                    // System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + "/GameImages"));
                    System.IO.Directory.CreateDirectory(Server.MapPath("/Images/GameImages"));
                }
                    filesUpload.SaveAs(imagePath);
                //Info.Image = AppDetails.baseImageUrl + AppDetails.basePath + "/GameImages/" + image_Guid;
                Info.Image = baseUrl + "/Images/GameImages/" + image_Guid;
            }

                mainRepository.SaveGameDetails(Info);
                return View("InfotainmentDetails"); //Redirect("MenuInfotainmentDetails");
            //}
            //else
                //return Redirect("/Account/Login");
        }


        public ActionResult GameList()
        {
            try
            {

                mainRepository = new MainRepository();
                List<GameMaster> house = new List<GameMaster>();
                //objDetail = objRep.GetActiveEmployee(AppId);
                house = mainRepository.GetGameList();
                //AppId = house.app
                //AddSession(UserId, UserRole, UserEmail, UserName);
                return Json(house, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //throw ex;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

    }
}