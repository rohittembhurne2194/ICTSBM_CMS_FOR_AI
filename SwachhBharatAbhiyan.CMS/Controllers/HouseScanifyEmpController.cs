using Microsoft.AspNet.Identity;
using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachBharat.CMS.Bll.ViewModels.Grid;
using SwachBharat.CMS.Bll.ViewModels.MainModel;
using SwachBharat.CMS.Dal.DataContexts;
using SwachhBharatAbhiyan.CMS.Models;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Data;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace SwachhBharatAbhiyan.CMS.Controllers
{
    public class HouseScanifyEmpController : Controller
    {
        // GET: HouseScanifyEmp
        IChildRepository childRepository;
       // IMainRepository mainRepository;

        private IMainRepository mainRepository;

        public ActionResult MenuIndex()
        {
            ViewBag.UType = Session["utype"];
            return View();
        }

        public ActionResult HSMenuIndex()
        {
            ViewBag.UType = Session["utype"];
            ViewBag.HSuserid = Session["Id"];
            return View();
        }
        public HouseScanifyEmpController()
        {
            mainRepository = new MainRepository();
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
                ViewBag.UType = Session["utype"];
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

        public ActionResult HSAppArea()
        {
            if (Session["utype"] != null && Session["utype"].ToString() == "SUA")
            {
                ViewBag.UType = Session["utype"];
                ViewBag.HSuserid = Session["Id"];
                return View();

            }
            else
            {
                return Redirect("/Account/Login");
            }

        }
        public ActionResult HSHousesListDownload()
        {
            if (Session["utype"] != null && Session["utype"].ToString() == "SUA")
            {
                ViewBag.UType = Session["utype"];
                ViewBag.HSuserid = Session["Id"];
                return View();

            }
            else
            {
                return Redirect("/Account/Login");
            }

        }
        public ActionResult HSAppAreaIndex()
        {
            if (Session["utype"] != null && Session["utype"].ToString() == "SUA")
            {
                ViewBag.UType = Session["utype"];
                ViewBag.HSuserid = Session["Id"];
                return View();
            }
            else
            {

                return Redirect("/Account/Login");
            }
        }

        public ActionResult AddAppAreaMap(int AppId = -1)
        {
            if (Session["utype"] != null && Session["utype"].ToString() == "SUA")
            {
                ViewBag.UType = Session["utype"];
                ViewBag.HSuserid = Session["Id"];
                mainRepository = new MainRepository();
                AppAreaMapVM appAreaMap = mainRepository.GetAppAreaMap(AppId);
                return View(appAreaMap);
            }
            else
            {
                return Redirect("/Account/Login");

            }
        }

        public ActionResult ListAppMap()
        {
            if (Session["utype"] != null && Session["utype"].ToString() == "SUA")
            {
                mainRepository = new MainRepository();
                List<SelectListItem> lstApps = mainRepository.ListAppMap();
                return Json(lstApps, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Redirect("/Account/Login");
            }
        }

        public ActionResult ListAllApp()
        {
            if (Session["utype"] != null && Session["utype"].ToString() == "SUA")
            {
                mainRepository = new MainRepository();
                List<SelectListItem> lstApps = mainRepository.ListAllApp();
                return Json(lstApps, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Redirect("/Account/Login");
            }
        }



        public ActionResult GetAppLatLong(int AppId = -1)
        {
            if (Session["utype"] != null && Session["utype"].ToString() == "SUA")
            {
                mainRepository = new MainRepository();
                AppAreaMapVM App = mainRepository.GetAppAreaMap(AppId);
                return Json(new { lat = App.AppLat, lng = App.AppLong }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Redirect("/Account/Login");
            }
        }

        public ActionResult SaveAppAreaMap(AppAreaMapVM AppAreaObj)
        {
            if (Session["utype"] != null && Session["utype"].ToString() == "SUA")
            {
                mainRepository = new MainRepository();
                mainRepository.SaveAppAreaMap(AppAreaObj);
                return Json(new { data = "Beat Map Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Redirect("/Account/Login");
            }

        }

        public ActionResult URIndex()
        {
            int appid = 1;
            ViewBag.AppId = appid;
            return View();


        }

        public ActionResult HSURIndex()
        {
            if (Session["utype"] != null && Session["utype"].ToString() == "SUA")
            {
                int appid = 1;
                ViewBag.AppId = appid;
                ViewBag.UType = Session["utype"];
                ViewBag.HSuserid = Session["Id"];
                return View();
            }
            else
            {
                return Redirect("/HouseScanifyEmp/Login");
            }


        }
        public ActionResult HSURAttendance()
        {
            if (Session["utype"] != null && Session["utype"].ToString() == "SUA")
            {
                int appid = 1;
                ViewBag.AppId = appid;
                ViewBag.UType = Session["utype"];
                ViewBag.HSuserid = Session["Id"];
                return View();
            }
            else 
            {
                return Redirect("/HouseScanifyEmp/Login");
            }

        }
        

        public ActionResult MenuURIndex()
        {
            return View();
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
            //------------Get Ip Start---------------------

            // Getting host name
            string host = Dns.GetHostName();

            //// Getting ip address using host name
            IPHostEntry ip = Dns.GetHostEntry(host);
            string hname = ip.HostName.ToString();
            string ipAdd = (ip.AddressList[1].ToString());
            //------------Get Ip End---------------------

            EmployeeVM Result = new EmployeeVM();
            Result.ADUM_LOGIN_ID = model.Email;
            Result.ADUM_PASSWORD = model.Password;
            Result.ADUM_DESIGNATION = model.AType;
            Result = mainRepository.LoginUR(Result);

            //HSUR_Daily_AttendanceVM Daily_Attendance = new HSUR_Daily_AttendanceVM();
            //Daily_Attendance = mainRepository.SaveDailyAttendance(Result);

            if (Result.status == "Success")
            {
                HSUR_Daily_AttendanceVM Daily_Attendance = new HSUR_Daily_AttendanceVM();
                Daily_Attendance.LOGIN_ID = model.Email;
                Daily_Attendance.EmpId = Result.ADUM_USER_CODE;
                Daily_Attendance.EmployeeType = Result.ADUM_DESIGNATION;
                Daily_Attendance.ipaddress = ipAdd;
                Daily_Attendance.HostName = hname;
                if (Daily_Attendance.EmployeeType != "SUA")
                {                  
                    mainRepository.SaveAttendance(Daily_Attendance);
                }

                Session["utype"] = Result.ADUM_DESIGNATION;
                Session["Id"] = Result.ADUM_LOGIN_ID;
                Session["Pwd"] = Result.ADUM_PASSWORD;
                Session["status"] = "Success";
                TempData["status"] = "Success";
                TempData["ADUM_USER_NAME"] = Result.ADUM_LOGIN_ID;
                ViewBag.HSuserid = Result.ADUM_LOGIN_ID;
                return RedirectToAction("HSMenuIndex");
            }
            else
            {
                return View();

            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            var action = filterContext.RequestContext.RouteData.Values["action"] as string;
            var controller = filterContext.RequestContext.RouteData.Values["controller"] as string;

            if ((filterContext.Exception is HttpAntiForgeryException) &&
                action == "LogOff" &&
                controller == "HouseScanifyEmp" &&
                filterContext.RequestContext.HttpContext.User != null &&
                filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.ExceptionHandled = true;

                // redirect/show error/whatever?
                filterContext.Result = new RedirectResult("/HouseScanifyEmp/login");
            }
            else
            {
                filterContext.ExceptionHandled = true;
                // redirect/show error/whatever?
                filterContext.Result = new RedirectResult("/HouseScanifyEmp/login");
            }
        }

        public ActionResult GetURAppNames()
        {
            try
            {

                mainRepository = new MainRepository();
                List<AppDetail> appName = new List<AppDetail>();
                List<HSDashBoardVM> details = new List<HSDashBoardVM>();
                //objDetail = objRep.GetActiveEmployee(AppId);
                var utype = (string)Session["utype"];
                var LoginId = (string)Session["Id"];
                var Password = (string)Session["Pwd"];
                appName = mainRepository.GetURAppName(utype, LoginId, Password);
                foreach (var x in appName)
                {
                    var appId = x.AppId;
                    childRepository = new ChildRepository(appId);
                    var detail = childRepository.GetURDashBoardDetails();
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


        [HttpPost]
        public string IsLoginIdExists(string LoginId)
        {

            mainRepository = new MainRepository();
            var isrecord = mainRepository.GetLoginid(LoginId);

            return isrecord;


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
            var isrecord = childRepository.GetHSUserName(userName);

            return isrecord;


        }

        [HttpPost]
        public ActionResult CheckUserName(HouseScanifyEmployeeDetailsVM obj)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
                string user1 = "";

                if (obj.qrEmpName != null)
                {
                    user1 = obj.qrEmpName;
                }

                HouseScanifyEmployeeDetailsVM user = childRepository.GetUserName(0, user1);

                if (obj.qrEmpId > 0)
                {
                    if (user.qrEmpId == 0)
                    {
                        user.qrEmpId = obj.qrEmpId;
                    }
                    if (user1 == obj.qrEmpName & user.qrEmpId != obj.qrEmpId)
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                    else if (user1 == user.qrEmpName & user.qrEmpId != obj.qrEmpId)
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                    else
                        return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                 if (user.qrEmpName != null)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(true, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(true, JsonRequestBehavior.AllowGet);




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

        public ActionResult GameAppList()
        {
            try
            {

                mainRepository = new MainRepository();
                List<AppDetail> house = new List<AppDetail>();
                //objDetail = objRep.GetActiveEmployee(AppId);

                var utype = (string)Session["utype"];
                var LoginId = (string)Session["Id"];
                var Password = (string)Session["Pwd"];

                house = mainRepository.GetAppList(utype, LoginId, Password);


                return Json(house, JsonRequestBehavior.AllowGet);
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
                ViewBag.UType = Session["utype"];
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult HSUserList(int AppId)
        {

            int AppID = AppId;
            AddSession(AppID);

            if (SessionHandler.Current.AppId != 0)
            {
                ViewBag.AppId = AppId;
                ViewBag.UType = Session["utype"];
                ViewBag.HSuserid = Session["Id"];
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult HSEmployeeList(string rn)
        {
            
                //UREmployeeDetails obj = new UREmployeeDetails();
                List<EmployeeMaster> obj = new List<EmployeeMaster>();
                obj = mainRepository.GetEmployeeList(-1, rn);
                return Json(obj, JsonRequestBehavior.AllowGet);

          
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


        public void SaveQRStatusHouse(int appId, int houseId, string QRStatus)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
                childRepository.SaveHSQRStatusHouse(houseId, QRStatus);
            }
        }

        public void SaveQRStatusDump(int appId, int dumpId, string QRStatus)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
                childRepository.SaveQRStatusDump(dumpId, QRStatus);
            }
        }
        public void SaveQRStatusLiquid(int appId, int liquidId, string QRStatus)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
                childRepository.SaveQRStatusLiquid(liquidId, QRStatus);
            }
        }

        public void SaveQRStatusStreet(int appId, int streetId, string QRStatus)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
                childRepository.SaveQRStatusStreet(streetId, QRStatus);
            }
        }

        public ActionResult GetHSHouseDetailsID(string fdate, string tdate, int userId, string searchString, int? qrStatus, string sortColumn, string sortOrder)
        {
            List<int> obj = new List<int>();
            DateTime? fromDate;
            DateTime? toDate;
            if (!string.IsNullOrEmpty(fdate))
            {
                fromDate = Convert.ToDateTime(fdate + " " + "00:00:00");
            }
            else
            {
                fromDate = null;
            }

            if (!string.IsNullOrEmpty(tdate))
            {
                toDate = Convert.ToDateTime(tdate + " " + "23:59:59");
            }
            else
            {
                toDate = null;
            }

            int iQRStatus = qrStatus ?? -1;
            iQRStatus = iQRStatus == 2 ? 0 : iQRStatus;
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
                //SBALUserLocationMapView obj = new SBALUserLocationMapView();

                obj = childRepository.GetHSHouseDetailsID(fromDate, toDate, userId, searchString, iQRStatus, sortColumn, sortOrder);

            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetHSDumpDetailsID(string fdate, string tdate, int userId, string searchString, int? qrStatus, string sortColumn, string sortOrder)
        {
            List<int> obj = new List<int>();
            DateTime? fromDate;
            DateTime? toDate;
            if (!string.IsNullOrEmpty(fdate))
            {
                fromDate = Convert.ToDateTime(fdate + " " + "00:00:00");
            }
            else
            {
                fromDate = null;
            }

            if (!string.IsNullOrEmpty(tdate))
            {
                toDate = Convert.ToDateTime(tdate + " " + "23:59:59");
            }
            else
            {
                toDate = null;
            }

            int iQRStatus = qrStatus ?? -1;
            // iQRStatus = iQRStatus == 2 ? 0 : iQRStatus;
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
                //SBALUserLocationMapView obj = new SBALUserLocationMapView();

                obj = childRepository.GetHSDumpDetailsID(fromDate, toDate, userId, searchString, iQRStatus, sortColumn, sortOrder);

            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetHSLiquidDetailsID(string fdate, string tdate, int userId, string searchString, int? qrStatus, string sortColumn, string sortOrder)
        {
            List<int> obj = new List<int>();
            DateTime? fromDate;
            DateTime? toDate;
            if (!string.IsNullOrEmpty(fdate))
            {
                fromDate = Convert.ToDateTime(fdate + " " + "00:00:00");
            }
            else
            {
                fromDate = null;
            }

            if (!string.IsNullOrEmpty(tdate))
            {
                toDate = Convert.ToDateTime(tdate + " " + "23:59:59");
            }
            else
            {
                toDate = null;
            }

            int iQRStatus = qrStatus ?? -1;
            // iQRStatus = iQRStatus == 2 ? 0 : iQRStatus;
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
                //SBALUserLocationMapView obj = new SBALUserLocationMapView();

                obj = childRepository.GetHSLiquidDetailsID(fromDate, toDate, userId, searchString, iQRStatus, sortColumn, sortOrder);

            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetHSStreetDetailsID(string fdate, string tdate, int userId, string searchString, int? qrStatus, string sortColumn, string sortOrder)
        {
            List<int> obj = new List<int>();
            DateTime? fromDate;
            DateTime? toDate;
            if (!string.IsNullOrEmpty(fdate))
            {
                fromDate = Convert.ToDateTime(fdate + " " + "00:00:00");
            }
            else
            {
                fromDate = null;
            }

            if (!string.IsNullOrEmpty(tdate))
            {
                toDate = Convert.ToDateTime(tdate + " " + "23:59:59");
            }
            else
            {
                toDate = null;
            }

            int iQRStatus = qrStatus ?? -1;
            // iQRStatus = iQRStatus == 2 ? 0 : iQRStatus;
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
                //SBALUserLocationMapView obj = new SBALUserLocationMapView();

                obj = childRepository.GetHSStreetDetailsID(fromDate, toDate, userId, searchString, iQRStatus, sortColumn, sortOrder);

            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetHouseDetailsById(int houseId)
        {
            SBAHSHouseDetailsGrid obj = new SBAHSHouseDetailsGrid();
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
                //SBALUserLocationMapView obj = new SBALUserLocationMapView();

                obj = childRepository.GetHouseDetailsById(houseId);

            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDumpDetailsById(int dumpId)
        {
            SBAHSDumpyardDetailsGrid obj = new SBAHSDumpyardDetailsGrid();
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
                //SBALUserLocationMapView obj = new SBALUserLocationMapView();

                obj = childRepository.GetDumpDetailsById(dumpId);

            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLiquidDetailsById(int liquidId)
        {
            SBAHSLiquidDetailsGrid obj = new SBAHSLiquidDetailsGrid();
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
                //SBALUserLocationMapView obj = new SBALUserLocationMapView();

                obj = childRepository.GetLiquidDetailsById(liquidId);

            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetStreetDetailsById(int streetId)
        {
            SBAHSStreetDetailsGrid obj = new SBAHSStreetDetailsGrid();
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
                //SBALUserLocationMapView obj = new SBALUserLocationMapView();

                obj = childRepository.GetStreetDetailsById(streetId);

            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AddUREmployeeDetails(int teamId = -1)
        {

            mainRepository = new MainRepository();
            childRepository = new ChildRepository(1);
            UREmployeeDetailsVM house = childRepository.GetUREmployeeById(teamId);
            ViewBag.EmpId = teamId;
            return View(house);
        }

        [HttpPost]
        public ActionResult AddUREmployeeDetails(UREmployeeDetailsVM emp)
        {

            mainRepository = new MainRepository();
            childRepository = new ChildRepository(1);
            childRepository.SaveUREmployee(emp);
            return Redirect("URIndex");

        }


        public ActionResult AddHSUREmployeeDetails(int teamId = -1)
        {

            mainRepository = new MainRepository();
            childRepository = new ChildRepository(1);
            UREmployeeDetailsVM house = childRepository.GetUREmployeeById(teamId);
            ViewBag.EmpId = teamId;
            ViewBag.UType = Session["utype"];
            ViewBag.HSuserid = Session["Id"];
            return View(house);
        }

        [HttpPost]
        public ActionResult AddHSUREmployeeDetails(UREmployeeDetailsVM emp)
        {

            mainRepository = new MainRepository();
            childRepository = new ChildRepository(1);
            childRepository.SaveUREmployee(emp);
            ViewBag.UType = Session["utype"];
            ViewBag.HSuserid = Session["Id"];
            return Redirect("HSURIndex");

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

        public ActionResult HSUREMPRoute(int daId)
        {
            if (Session["utype"] != null && Session["utype"].ToString() == "SUA")
            {
                ViewBag.daId = daId;
                ViewBag.UType = Session["utype"];
                ViewBag.HSuserid = Session["Id"];
                mainRepository = new MainRepository();
                SBAHSUREmpLocationMapView obj = new SBAHSUREmpLocationMapView();
                obj = mainRepository.GetEmpByIdforMap(-1, daId);
                return View(obj);
            }
            else
            {
                return Redirect("/HouseScanifyEmp/Login");
            }

        }



        
        public ActionResult HSURRouteData(int daId)
        {
            if (Session["utype"] != null && Session["utype"].ToString() == "SUA")
            {
                List<SBAHSUREmpLocationMapView> obj = new List<SBAHSUREmpLocationMapView>();
                obj = mainRepository.GetHSUserAttenRoute(daId);
                // return Json(obj);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Redirect("/HouseScanifyEmp/Login");
            }

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

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            ViewBag.UType = Session["utype"];
            ViewBag.HSuserid = Session["Id"];
            if (ViewBag.UType != "SUA")
            {
                HSUR_Daily_AttendanceVM Daily_Attendance = new HSUR_Daily_AttendanceVM();
                Daily_Attendance.LOGIN_ID = ViewBag.HSuserid;
                Daily_Attendance.EmployeeType = ViewBag.UType;
                Daily_Attendance.logoff = "LogOut";

                mainRepository.SaveAttendance(Daily_Attendance);
            }
            Session["__MySession__"] = null; //it's my session variable
            Session.Clear();
            Session.Abandon();
            Server.ClearError();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "HouseScanifyEmp");
        }
        public ActionResult ExportHouseListPDF(int appId,string appName, int option, int type)
        {
            if (Session["utype"] != null && Session["utype"].ToString() == "SUA")
            {
                string strOption = string.Empty;
                string strType = string.Empty;
                if (type == 1)
                {
                    strType = "Street";
                }
                else if (type == 2)
                {
                    strType = "Liquid";

                }
                else if (type == 3)
                {
                    strType = "Dumpyard";

                }
                else
                {
                    strType = "Houses";

                }

                if (option == 1)
                {
                    strOption = strType + " with Lat/Long no data entry";
                }
                else if (option == 2)
                {
                    strOption = strType + " with data entry";

                }
                else
                {
                    strOption = "All " + strType;

                }


                childRepository = new ChildRepository(appId);
                var dt = childRepository.getHousesList(option,type);
                byte[] filecontent = exportpdf(dt, appName, strOption, type);
                //byte[] filecontent = genPDF(dt,appName);

                string filename = appName + "_" + strType + "_List_PDF_" + DateTime.Now.ToString("yyyy-MMM-dd") + ".pdf";
                return File(filecontent, "application/pdf", filename);
            }
            else
            {
                return Redirect("/Account/Login");
            }

        }


        public ActionResult ExportHouseListExcel(int appId, string appName, int option, int type)
        {
            if (Session["utype"] != null && Session["utype"].ToString() == "SUA")
            {
                string strOption = string.Empty;
                string strType = string.Empty;
                if (type == 1)
                {
                    strType = "Street";
                }
                else if (type == 2)
                {
                    strType = "Liquid";

                }
                else if (type == 3)
                {
                    strType = "Dumpyard";

                }
                else
                {
                    strType = "Houses";

                }

                if (option == 1)
                {
                    strOption = strType + " with Lat/Long no data entry";
                }
                else if (option == 2)
                {
                    strOption = strType + " with data entry";

                }
                else
                {
                    strOption = "All " + strType;

                }


                
                childRepository = new ChildRepository(appId);
                var dt = childRepository.getHousesList(option,type);
                byte[] filecontent = ExcelExport(dt, appName, strOption);
                //byte[] filecontent = genPDF(dt,appName);

                string filename = appName + "_" + strType + "_List_Excel_" + DateTime.Now.ToString("yyyy-MMM-dd") + ".xlsx";
                return File(filecontent, "application/octet-stream", filename);
            }
            else
            {
                return Redirect("/Account/Login");
            }

        }

        //public byte[] ExcelExport(DataTable dt,string appName)
        //{
        //    byte[] result;


        //        using (var excelPackage = new ExcelPackage())
        //        {
        //        var worksheet = excelPackage.Workbook.Worksheets.Add("Export");
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            #region HeaderBuilder
        //            for (int c = 0; c < dt.Columns.Count; c++)
        //            {
        //                worksheet.Cells[1, c + 1].Value = dt.Columns[c].ColumnName;
        //                #region Style
        //                worksheet.Cells[1, c + 1].Style.Font.Bold = true;
        //                worksheet.Cells[1, c + 1].Style.Font.Size = 12;
        //                worksheet.Cells[1, c + 1].Style.Font.Name = "Tahoma";
        //                worksheet.Cells[1, c + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
        //                worksheet.Cells[1, c + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#13A38D"));
        //                worksheet.Cells[1, c + 1].Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#FFF"));
        //                worksheet.Cells[1, c + 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                worksheet.Cells[1, c + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //                worksheet.Cells[1, c + 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //                worksheet.Cells[1, c + 1].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //                worksheet.Cells[1, c + 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //                worksheet.Cells[1, c + 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //                worksheet.Cells[1, c + 1].Style.Border.Left.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#DDD"));
        //                worksheet.Cells[1, c + 1].Style.Border.Top.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#DDD"));
        //                worksheet.Cells[1, c + 1].Style.Border.Right.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#DDD"));
        //                worksheet.Cells[1, c + 1].Style.Border.Bottom.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#DDD"));
        //                #endregion
        //            }
        //            #endregion
        //            #region ContentBuilder
        //            for (int r = 0; r < dt.Rows.Count; r++)
        //            {
        //                for (int c = 0; c < dt.Columns.Count; c++)
        //                {
        //                    worksheet.Cells[r + 2, c + 1].Value = dt.Rows[r][c];
        //                    #region Style
        //                    worksheet.Cells[r + 2, c + 1].Style.Font.Bold = false;
        //                    worksheet.Cells[r + 2, c + 1].Style.Font.Size = 12;
        //                    worksheet.Cells[r + 2, c + 1].Style.Font.Name = "Tahoma";
        //                    worksheet.Cells[r + 2, c + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
        //                    worksheet.Cells[r + 2, c + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#FFF"));
        //                    worksheet.Cells[r + 2, c + 1].Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#444"));
        //                    worksheet.Cells[r + 2, c + 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells[r + 2, c + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

        //                    worksheet.Cells[r + 2, c + 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //                    worksheet.Cells[r + 2, c + 1].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //                    worksheet.Cells[r + 2, c + 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //                    worksheet.Cells[r + 2, c + 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //                    worksheet.Cells[r + 2, c + 1].Style.Border.Left.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#DDD"));
        //                    worksheet.Cells[r + 2, c + 1].Style.Border.Top.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#DDD"));
        //                    worksheet.Cells[r + 2, c + 1].Style.Border.Right.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#DDD"));
        //                    worksheet.Cells[r + 2, c + 1].Style.Border.Bottom.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#DDD"));
        //                    #endregion
        //                }
        //            }
        //            #endregion
        //            #region ColumnResize
        //            for (int c = 0; c < dt.Columns.Count; c++)
        //            {
        //                worksheet.Column(c + 1).AutoFit();
        //            }
        //            #endregion

        //        }
        //        result = excelPackage.GetAsByteArray();

        //        }


        //    return result;
        //}

        public byte[] ExcelExport(DataTable dt, string appName,string option)
        {
            byte[] result;


            using (var excelPackage = new ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Export");
                if (dt != null )
                {
                    worksheet.Cells["A3"].LoadFromDataTable(dt, true, TableStyles.None);
                    worksheet.Cells[1, 1, 1, 10].Merge = true;
                    worksheet.Cells[1, 1].Value = option + " for ULB Name: " + appName + "   Date : " + DateTime.Now.ToString("yyyy-MMM-dd");
                    worksheet.Cells[1, 1].Style.Font.Bold = true;
                    worksheet.Cells[1, 1].Style.Font.Size = 16;
                    worksheet.Cells["A3:AN3"].Style.Font.Bold = true;
                    worksheet.Cells["A3:AN3"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells["A3:AN3"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#d4d6d5"));
                    #region ColumnResize
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        worksheet.Column(c + 1).AutoFit();
                    }
                    #endregion

                }
                result = excelPackage.GetAsByteArray();

            }


            return result;
        }




        private byte[] exportpdf(DataTable dtEmployee, string appName, string option, int type)
        {

            // creating document object  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            //iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(PageSize.A4);
            iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(1500, 1500);
            //rec.BackgroundColor = new BaseColor(System.Drawing.Color.Olive);
            Document doc = new Document(rec);
            //doc.SetPageSize(iTextSharp.text.PageSize.A4);
            doc.SetPageSize(rec);

            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            doc.Open();

            //Creating paragraph for header  
            BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfntHead, 20, 1, iTextSharp.text.BaseColor.BLUE);
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_LEFT;
            prgHeading.Add(new Chunk(option + " for ULB Name: " + appName + "   Date : " + DateTime.Now.ToString("yyyy-MMM-dd"), fntHead));
            doc.Add(prgHeading);

            //Adding paragraph for report generated by  
            Paragraph prgGeneratedBY = new Paragraph();
            BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntAuthor = new iTextSharp.text.Font(btnAuthor, 8, 2, iTextSharp.text.BaseColor.BLUE);
            prgGeneratedBY.Alignment = Element.ALIGN_RIGHT;
            //prgGeneratedBY.Add(new Chunk("Report Generated by : ASPArticles", fntAuthor));  
            //prgGeneratedBY.Add(new Chunk("\nGenerated Date : " + DateTime.Now.ToShortDateString(), fntAuthor));  
            doc.Add(prgGeneratedBY);

            //Adding a line  
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            doc.Add(p);

            //Adding line break  
            doc.Add(new Chunk("\n", fntHead));

            //Adding  PdfPTable  
            PdfPTable table = new PdfPTable(dtEmployee.Columns.Count);
            table.WidthPercentage = 100;
            //int[] firstTablecellwidth = { 8, 18, 6, 6, 8, 6, 6, 18, 8, 8, 8 };
            int[] firstTablecellwidth = { 8, 18, 6, 6, 8, 6, 6, 18, 8, 8, 8 };
            if (type == 1)
            {
                firstTablecellwidth = new int[] { 10, 20, 20, 7, 7, 10, 16, 10};
            }
            else if(type == 2)
            {
                firstTablecellwidth = new int[] { 10, 20, 20, 7, 7, 10, 16, 10 }; 
            }
            else if(type == 3)
            {
                firstTablecellwidth = new int[] { 10, 20, 20, 7, 7, 10, 16, 10 };
            }
            else
            {
                firstTablecellwidth = new int[] { 8, 18, 6, 6, 8, 6, 6, 18, 8, 8, 8 };
            }

            table.SetWidths(firstTablecellwidth);
            
            for (int i = 0; i < dtEmployee.Columns.Count; i++)
            {
                string cellText = Server.HtmlDecode(dtEmployee.Columns[i].ColumnName);
                PdfPCell cell = new PdfPCell();
                cell.Phrase = new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10, 1, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));
                cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#C8C8C8"));
                //cell.Phrase = new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 10, 1, new BaseColor(grdStudent.HeaderStyle.ForeColor)));  
                //cell.BackgroundColor = new BaseColor(grdStudent.HeaderStyle.BackColor);  
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.PaddingBottom = 5;
                cell.NoWrap = false;
                table.AddCell(cell);
            }

            //writing table Data  
            for (int i = 0; i < dtEmployee.Rows.Count; i++)
            {
                for (int j = 0; j < dtEmployee.Columns.Count; j++)
                {
                    table.AddCell(dtEmployee.Rows[i][j].ToString());
                }
            }

            doc.Add(table);
            doc.Close();

            byte[] result = ms.ToArray();
            return result;

        }





        private byte[] genPDF(DataTable dt, string appName)
        {
            byte[] result;
            //Dummy data for Invoice (Bill).

            StringBuilder sb = new StringBuilder();

            //Generate Invoice (Bill) Header.
            sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
            sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>Houses List</b></td></tr>");
            sb.Append("<tr><td colspan = '2'></td></tr>");
            sb.Append("<tr><td><b>ULB Name: </b>");
            sb.Append(appName);
            sb.Append("</td><td align = 'right'><b>Date: </b>");
            sb.Append(DateTime.Now.ToString("yyyy-MMM-dd"));
            sb.Append(" </td></tr>");
            sb.Append("</table>");
            sb.Append("<br />");

            //Generate Invoice (Bill) Items Grid.
            sb.Append("<table style='width:100%;height:100%;border:1px solid;border-collapse:collapse;'>");
            sb.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                sb.Append("<th style = 'background-color: #D20B0C;color:#ffffff'>");
                sb.Append(column.ColumnName);
                sb.Append("</th>");
            }
            sb.Append("</tr>");
            foreach (DataRow row in dt.Rows)
            {
                sb.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    sb.Append("<td>");
                    sb.Append(row[column]);
                    sb.Append("</td>");
                }
                sb.Append("</tr>");
            }
            
            sb.Append("</table>");
            using (var ms = new MemoryStream())
            {

                //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
                using (var doc = new Document(new Rectangle(200, 200)))
                {

                    //Create a writer that's bound to our PDF abstraction and our stream
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {
                        doc.Open();
                        using (var srHtml = new StringReader(sb.ToString()))
                        {

                            //Parse the HTML
                            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, srHtml);
                        }

                        doc.Close();
                    }
                }
                result = ms.ToArray();
            }

            return result;
        }
    }
}