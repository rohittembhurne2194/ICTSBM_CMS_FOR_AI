using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachBharat.CMS.Bll.ViewModels.MainModel;
using SwachhBharatAbhiyan.CMS.Models;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using SwachBharat.CMS.Dal.DataContexts;
using System.Globalization;
using SwachBharat.CMS.Bll.ViewModels.Grid;
using System.IO.Compression;
using System.Threading.Tasks;

namespace SwachhBharatAbhiyan.CMS.Controllers
{
    public class HouseScanifyController : Controller
    {
        // GET: HouseScanify
        IChildRepository childRepository;
        IMainRepository mainRepository;

        public HouseScanifyController()
        {

        }
        public ActionResult MenuIndex()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult login()
        {
            return View();
        }
        public ActionResult Index()
        {
            int appid = SessionHandler.Current.AppId;
            if (SessionHandler.Current.AppId != 0)
            {
                mainRepository = new MainRepository();
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
                var details = childRepository.GetHSDashBoardDetails();
                ViewBag.AppId = appid;
                return View(details);
            }
            else
                return Redirect("/HouseScanify/Login");
        }

        public ActionResult AttendenceIndex()
        {
            int appid = SessionHandler.Current.AppId;
            if (SessionHandler.Current.AppId != 0)
            {
                ViewBag.AppId = appid;
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult HouseDetails()
        {
            int appid = SessionHandler.Current.AppId;
            if (SessionHandler.Current.AppId != 0)
            {
                ViewBag.AppId = appid;
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult login(LoginViewModel model, string returnUrl)
        {
            if (model.Password == "Bigv#123" & model.Email == "Bigv")
            {

                return RedirectToAction("Login", "HouseScanifyemp");
            }
            else
            {
                return View();

            }
        }

        public ActionResult GetAppNames()
        {
            try
            {

                mainRepository = new MainRepository();
                List<AppDetail> appName = new List<AppDetail>();
                List<HSDashBoardVM> details = new List<HSDashBoardVM>();
                //objDetail = objRep.GetActiveEmployee(AppId);

                appName = mainRepository.GetAppName();
                foreach (var x in appName)
                {
                    var appId = x.AppId;
                    childRepository = new ChildRepository(appId);
                    var detail = childRepository.GetHSDashBoardDetails();
                    details.Add(new HSDashBoardVM()
                    {
                        AppId = x.AppId,
                        AppName = x.AppName,
                        TotalHouseUpdated_CurrentDay = detail.TotalHouseUpdated_CurrentDay,
                        TotalPointUpdated_CurrentDay = detail.TotalPointUpdated_CurrentDay,
                        TotalDumpUpdated_CurrentDay = detail.TotalDumpUpdated_CurrentDay,


                    });

                }
                //AppId = house.app
                //AddSession(UserId, UserRole, UserEmail, UserName);
                return Json(details, JsonRequestBehavior.AllowGet);

                //return Json(new
                //{
                //    AppNames = appName,
                //    Details = details
                //}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //throw ex;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult UserList(int AppId)
        {

            int AppID = AppId;
            AddSession(AppID);

            if (SessionHandler.Current.AppId != 0)
            {
                ViewBag.AppId = AppId;

                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult UserListByAppId(int AppId)
        {
            //AddSession(UserId, UserRole, UserEmail, UserName);

            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(AppId);
                //SBALUserLocationMapView obj = new SBALUserLocationMapView();
                List<QrEmployeeMaster> obj = new List<QrEmployeeMaster>();
                obj = childRepository.GetUserList(AppId, -1);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult AddHSEmployeeDetails(int teamId = -1)
        {

            if (SessionHandler.Current.AppId != 0)
            {
                mainRepository = new MainRepository();
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
                HouseScanifyEmployeeDetailsVM house = childRepository.GetHSEmployeeById(teamId);
                return View(house);
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult AddHSEmployeeDetails(HouseScanifyEmployeeDetailsVM emp)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                mainRepository = new MainRepository();
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);


                childRepository.SaveHSEmployee(emp);
                return Redirect("Index");
            }
            else
                return Redirect("/Account/Login");
        }


        [HttpPost]
        public ActionResult CheckUserDetails(HouseScanifyEmployeeDetailsVM obj)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
                string user1 = "";

                if (obj.qrEmpLoginId != null)
                {
                    user1 = obj.qrEmpLoginId;
                }

                HouseScanifyEmployeeDetailsVM user = childRepository.GetUser(0, user1);

                if (obj.qrEmpId > 0)
                {
                    if (user.qrEmpId == 0)
                    {
                        user.qrEmpId = obj.qrEmpId;
                    }
                    if (user1 == obj.qrEmpLoginId & user.qrEmpId != obj.qrEmpId)
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                    else if (user1 == user.qrEmpLoginId & user.qrEmpId != obj.qrEmpId)
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                    else
                        return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                 if (user.qrEmpLoginId != null)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(true, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(true, JsonRequestBehavior.AllowGet);




        }

        public ActionResult HSUserRoute(int qrEmpDaId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                ViewBag.qrEmpDaId = qrEmpDaId;
                return View();
            }
            else
                return Redirect("/Account/Login");

        }

        public ActionResult HSUserRouteData(int qrEmpDaId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
                List<SBALHSUserLocationMapView> obj = new List<SBALHSUserLocationMapView>();
                obj = childRepository.GetHSUserAttenRoute(qrEmpDaId);
                // return Json(obj);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }
        private void AddSession(int AppID)
        {
            try
            {
                mainRepository = new MainRepository();
                if (AppID != 0)
                {
                    AppDetailsVM ApplicationDetails = mainRepository.GetApplicationDetails(AppID);
                    string DB_Connect = mainRepository.GetDatabaseFromAppID(AppID);
                    //SessionHandler.Current.UserId = UserId;
                    //SessionHandler.Current.UserRole = UserRole;
                    //SessionHandler.Current.UserEmail = UserEmail;
                    //SessionHandler.Current.UserName = UserName;
                    SessionHandler.Current.AppId = ApplicationDetails.AppId;
                    SessionHandler.Current.AppName = ApplicationDetails.AppName;
                    SessionHandler.Current.IsLoggedIn = true;
                    SessionHandler.Current.Type = ApplicationDetails.Type;
                    SessionHandler.Current.Latitude = ApplicationDetails.Latitude;
                    SessionHandler.Current.Logitude = ApplicationDetails.Logitude;
                    SessionHandler.Current.DB_Name = DB_Connect;
                    SessionHandler.Current.AppId = AppID;
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


            }
            catch (Exception exception)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
            }
        }

        //Added by milind 09-03-2022
        public ActionResult Export(int type, int UserId, string fdate = null, string tdate = null, string QrStatus = null)
        {
            DateTime fdt;
            DateTime tdt;
            //Task<List<SBAHSHouseDetailsGrid>> data;
            List<SBAHSHouseDetailsGrid> data = new List<SBAHSHouseDetailsGrid>();
            string strType = string.Empty;
            string strFileDownloadName = string.Empty;
            if (type == 0)
            {
                strType = "HousesQRCodeImage";
            }
            else if (type == 1)
            {
                strType = "DumpyardQRCodeImage";
            }
            else if (type == 2)
            {
                strType = "LiquidQRCodeImage";
            }
            else if (type == 3)
            {
                strType = "StreetQRCodeImage";
            }
            //strFileDownloadName = String.Format("Zip_{0}_{1}.zip", strType, DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            if (string.IsNullOrEmpty(fdate) || string.IsNullOrEmpty(tdate))
            {
                string dt = DateTime.Now.ToString("MM/dd/yyyy");
                fdt = Convert.ToDateTime(dt + " " + "00:00:00");
                tdt = Convert.ToDateTime(dt + " " + "23:59:59");

            }
            else if (DateTime.TryParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out fdt) && DateTime.TryParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out tdt))
            {
                fdt = Convert.ToDateTime(fdt.ToString("MM/dd/yyyy") + " " + "00:00:00");
                tdt = Convert.ToDateTime(tdt.ToString("MM/dd/yyyy") + " " + "23:59:59");

            }
            else
            {
                string dt = DateTime.Now.ToString("MM/dd/yyyy");
                fdt = Convert.ToDateTime(dt + " " + "00:00:00");
                tdt = Convert.ToDateTime(dt + " " + "23:59:59");

            }

            strFileDownloadName = String.Format("Zip_{0}_From_{1}_To_{2}.zip", strType, fdt.ToString("yyyy-MMM-dd"), tdt.ToString("yyyy-MMM-dd"));


            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
                //data1 = childRepository.GetHSQRCodeImageByDate(type, UserId, fdt, tdt);
                //string strFileType = string.Empty;
                //if (data != null && data.Count > 0)
                //{
                //    using (var compressedFileStream = new MemoryStream())
                //    {
                //        //Create an archive and store the stream in memory.
                //        using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                //        {
                //            foreach (var item in data)
                //            {
                //                string strImageTypePart = item.QRCodeImage.Split(',').First();
                //                if (strImageTypePart.ToUpper().Contains("JPEG"))
                //                {
                //                    strFileType = "jpeg";
                //                }
                //                else if (strImageTypePart.ToUpper().Contains("BMP"))
                //                {
                //                    strFileType = "bmp";
                //                }
                //                else if (strImageTypePart.ToUpper().Contains("PNG"))
                //                {
                //                    strFileType = "png";
                //                }
                //                else if (strImageTypePart.ToUpper().Contains("JPG"))
                //                {
                //                    strFileType = "jpg";
                //                }
                //                else if (strImageTypePart.ToUpper().Contains("GIF"))
                //                {
                //                    strFileType = "gif";
                //                }
                //                else
                //                {
                //                    strFileType = "jpeg";
                //                }
                //                //Create a zip entry for each attachment
                //                var zipEntry = zipArchive.CreateEntry(string.Format("{0}.{1}", item.ReferanceId, strFileType));
                //                byte[] file = Convert.FromBase64String(item.QRCodeImage.Substring(item.QRCodeImage.LastIndexOf(',') + 1));
                //                //Get the stream of the attachment
                //                using (var originalFileStream = new MemoryStream(file))
                //                using (var zipEntryStream = zipEntry.Open())
                //                {
                //                    //Copy the attachment stream to the zip entry stream
                //                    originalFileStream.CopyTo(zipEntryStream);
                //                }
                //            }

                //        }

                //        return new FileContentResult(compressedFileStream.ToArray(), "application/zip") { FileDownloadName = strFileDownloadName };
                //    }
                //}

                data = childRepository.GetHSQRCodeImageByDate(type, UserId, fdt, tdt, QrStatus);
                string strFileType = string.Empty;
                if (data != null)
                {
                    using (var compressedFileStream = new MemoryStream())
                    {
                        //Create an archive and store the stream in memory.
                        using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                        {
                            foreach (var item in data)
                            {
                                strFileType = "jpeg";

                                //Create a zip entry for each attachment
                                var zipEntry = zipArchive.CreateEntry(string.Format("{0}.{1}", item.ReferanceId, strFileType));
                                byte[] file = item.BinaryQrCodeImage;
                                //Get the stream of the attachment
                                using (var originalFileStream = new MemoryStream(file))
                                using (var zipEntryStream = zipEntry.Open())
                                {
                                    //Copy the attachment stream to the zip entry stream
                                    originalFileStream.CopyTo(zipEntryStream);
                                }
                            }

                        }

                        return new FileContentResult(compressedFileStream.ToArray(), "application/zip") { FileDownloadName = strFileDownloadName };
                    }
                }
                else
                    return new FileContentResult(new byte[] { }, "application/zip") { FileDownloadName = strFileDownloadName };

            }
            else
                return new FileContentResult(new byte[] { }, "application/zip") { FileDownloadName = strFileDownloadName };
            //return data;

        }
    }
}