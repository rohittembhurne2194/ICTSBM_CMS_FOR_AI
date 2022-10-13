using iTextSharp.text.pdf.parser;
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
    public class ComplaintController : Controller
    {
        IChildRepository childRepository;
        IMainRepository mainRepository;
        // GET: Complaint

        public ComplaintController()
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

        public ActionResult MenuIndex()
        {
            return View();
        }

        public ActionResult EditDetails(int teamId = -1)
        {

            if (SessionHandler.Current.AppId != 0)
            {
                ComplaintVM clVM = childRepository.GetComplaint(teamId);

                return PartialView(clVM);

            }
            else
                return RedirectToAction("Login", "Account");

        }


        [HttpPost]
        public ActionResult EditDetails(ComplaintVM contactUsTeamDetailVM, HttpPostedFileBase filesUpload)
        {

          
            var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
            if (filesUpload != null)
            {
                var guiddd = Guid.NewGuid().ToString().Split('-');
                string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guiddd[1] + ".jpg";


                string imagePath = System.IO.Path.Combine(Server.MapPath(AppDetails.basePath + AppDetails.UserProfile), image_Guid);
                var exists = System.IO.Directory.Exists(Server.MapPath(AppDetails.basePath + AppDetails.UserProfile));
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + AppDetails.UserProfile));
                }
                filesUpload.SaveAs(imagePath);
                contactUsTeamDetailVM.endImage= image_Guid;

            }
            else
            {
                if (contactUsTeamDetailVM.endImage.StartsWith("http"))
                    contactUsTeamDetailVM.endImage= System.IO.Path.GetFileName(contactUsTeamDetailVM.endImage);

                if (string.IsNullOrEmpty(contactUsTeamDetailVM.endImage))
                    contactUsTeamDetailVM.endImage= null;
            }
           childRepository.SaveComplaintStatus(contactUsTeamDetailVM);
            // Email(contactUsTeamDetailVM.status, contactUsTeamDetailVM.ccId);
            contactUsTeamDetailVM = childRepository.GetComplaint(contactUsTeamDetailVM.complaintId); 
            return Redirect("Index");
        }

        

    }
}