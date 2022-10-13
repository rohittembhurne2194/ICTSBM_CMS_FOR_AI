using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.ViewModels.MainModel;
using SwachBharat.CMS.Bll.ViewModels.SS2020Reports;

namespace SwachhBharatAbhiyan.CMS.Areas.Street.Controllers
{
    public class StreetReportController : Controller
    {
        // GET: Street/StreetReport
        IMainRepository mainRepository;
        IChildRepository childRepository;
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ReportEnable(string date, string userid)
        {

            mainRepository = new MainRepository();
            AppDetailsVM obj = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SingleEmployeeCollection()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["User_ID"] = SessionHandler.Current.UserId;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult Dumpyardidwise()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult GarbageCollectionPercentage()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["User_ID"] = SessionHandler.Current.UserId;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult EmployeeCollectionCount()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult EmployeeCollectionSummary()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["User_ID"] = SessionHandler.Current.UserId;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult AreawiseGarbageCollection()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult CitywiseGarbageReport()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;


                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult AreawiseGarbageTypeCollection()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult Dashboard1()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult DumpYardSummary()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult Housewise()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult Daywise()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult CommercialWise()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult StreetSweepingReport()
        {

            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");

                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        // Pointwise Reports
        #region 1.1
        public ActionResult OnePointOne2020()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["User_ID"] = SessionHandler.Current.UserId;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");

                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        #endregion
        #region 1.2
        public ActionResult OnePointTwo2020()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["User_ID"] = SessionHandler.Current.UserId;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");

                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        #endregion
        #region 1.3
        public ActionResult OnePointThree2020()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["User_ID"] = SessionHandler.Current.UserId;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");

                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        #endregion
        #region EmpCollectionAverage
        public ActionResult EmployeeCollectionAverage()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["User_ID"] = SessionHandler.Current.UserId;
                Session["DB_Source"] = SessionHandler.Current.DB_Source;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");

                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        #endregion
        #region 1.4
        public ActionResult Show1point4Report()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(SessionHandler.Current.AppId);

                OnePoint4VM OnePointFour = childRepository.GetMaxINSERTID();
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["User_ID"] = SessionHandler.Current.UserId;
                Session["INSERT_ID"] = OnePointFour.INSERT_ID;

                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult OnePointfour2020(int INSERT_ID)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["User_ID"] = SessionHandler.Current.UserId;
                Session["INSERT_ID"] = INSERT_ID;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");

                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult Onepoint4Index(string ReportName)
        {
            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            if (SessionHandler.Current.AppId != 0)
            {
                TempData["Message"] = null;
                TempData["Message1"] = 1;
                TempData["Error"] = null;
                List<OnePoint4VM> que = new List<OnePoint4VM>();
                que = childRepository.GetQuetions(ReportName);
                return View(que);
            }
            else
                return Redirect("/Report/OnePointfour2020");
        }

        [HttpPost]
        public ActionResult Onepoint4Index(List<OnePoint4VM> point)
        {
            string ReportName = "1.4";
            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            if (SessionHandler.Current.AppId != 0)
            {
                foreach (var item in point)
                {
                    if (item.TOTAL_COUNT == null)
                    {
                        TempData["Message2"] = "Please Enter Values";
                        TempData["Message1"] = "Disabled Button";
                        List<OnePoint4VM> que1 = new List<OnePoint4VM>();
                        que1 = childRepository.GetQuetions(ReportName);
                        ModelState.Clear();
                        return View(que1);
                    }
                }
                TempData["Message1"] = null;
                TempData["Message"] = "Active Button";
                TempData["Message2"] = "Data Saved Successfully";
                childRepository.Save1Point4(point);
                List<OnePoint4VM> que = new List<OnePoint4VM>();
                que = childRepository.GetQuetions(ReportName);
                ModelState.Clear();
                return View(que);
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult onepointfourIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuonepointfourIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult Modifypoint4Report()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");

        }
        public ActionResult MenuOnePointFourEdit(int ANS_ID)
        {
            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            if (SessionHandler.Current.AppId != 0)
            {
                OnePoint4VM OnePointFour = childRepository.GetOnePointFourTotalCount(ANS_ID);
                return View(OnePointFour);
            }
            else
                return Redirect("/Account/Login");

        }

        [HttpPost]
        public ActionResult MenuOnePointFourEdit(OnePoint4VM onepointfour)
        {
            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            if (SessionHandler.Current.AppId != 0)
            {
                int INSERT_ID = onepointfour.INSERT_ID;
                childRepository.SaveTotalCount(onepointfour);
                return Redirect("MenuOnePointFourDetails?INSERT_ID=" + INSERT_ID);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MenuOnePointFourDetails(int INSERT_ID)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return Redirect("/Report/OnePointFourEditIndex?INSERT_ID=" + INSERT_ID);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult OnePointFourEditIndex(int INSERT_ID)
        {
            TempData["INSERT_ID"] = INSERT_ID;
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        #endregion
        #region 1.5
        public ActionResult Onepoint5Index(string ReportName)
        {
            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            if (SessionHandler.Current.AppId != 0)
            {
                TempData["Message"] = null;
                TempData["Message1"] = 1;
                TempData["Error"] = null;
                List<OnePoint4VM> que = new List<OnePoint4VM>();
                que = childRepository.GetQuetions(ReportName);
                return View(que);
            }
            else
                return Redirect("/Report/OnePointfour2020");
        }

        [HttpPost]
        public ActionResult Onepoint5Index(List<OnePoint4VM> point)
        {
            string ReportName = "1.5";
            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            if (SessionHandler.Current.AppId != 0)
            {
                foreach (var item in point)
                {
                    if (item.TOTAL_COUNT == null)
                    {
                        TempData["Message2"] = "Please Enter Values";
                        TempData["Message1"] = "Disabled Button";
                        List<OnePoint4VM> que1 = new List<OnePoint4VM>();
                        que1 = childRepository.GetQuetions(ReportName);
                        ModelState.Clear();
                        return View(que1);
                    }
                }
                TempData["Message1"] = null;
                TempData["Message"] = "Active Button";
                TempData["Message2"] = "Data Saved Successfully";
                childRepository.Save1Point4(point);
                List<OnePoint4VM> que = new List<OnePoint4VM>();
                que = childRepository.GetQuetions(ReportName);
                ModelState.Clear();
                return View(que);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MenuonepointfiveIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult Modifypoint5Report()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");

        }

        public ActionResult OnePointfive20201(int INSERT_ID)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["User_ID"] = SessionHandler.Current.UserId;
                Session["INSERT_ID"] = INSERT_ID;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");

                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuOnePointFiveDetails(int INSERT_ID)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return Redirect("/Report/OnePointFiveEditIndex?INSERT_ID=" + INSERT_ID);
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult OnePointFiveEditIndex(int INSERT_ID)
        {
            TempData["INSERT_ID"] = INSERT_ID;
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuOnePointFiveEdit(int ANS_ID)
        {
            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            if (SessionHandler.Current.AppId != 0)
            {
                OnePoint4VM OnePointFour = childRepository.GetOnePointFourTotalCount(ANS_ID);
                return View(OnePointFour);
            }
            else
                return Redirect("/Account/Login");

        }

        [HttpPost]
        public ActionResult MenuOnePointFiveEdit(OnePoint4VM onepointfour)
        {
            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            if (SessionHandler.Current.AppId != 0)
            {
                int INSERT_ID = onepointfour.INSERT_ID;
                childRepository.SaveTotalCount(onepointfour);
                return Redirect("MenuOnePointFiveDetails?INSERT_ID=" + INSERT_ID);
            }
            else
                return Redirect("/Account/Login");
        }
        #endregion

        #region 1.6
        public ActionResult Onepoint6Index(string ReportName)
        {
            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            if (SessionHandler.Current.AppId != 0)
            {
                TempData["Message"] = null;
                TempData["Message1"] = 1;
                TempData["Error"] = null;
                List<OnePoint4VM> que = new List<OnePoint4VM>();
                que = childRepository.GetQuetions(ReportName);
                return View(que);
            }
            else
                return Redirect("/Report/OnePointfour2020");
        }

        [HttpPost]
        public ActionResult Onepoint6Index(List<OnePoint4VM> point)
        {
            string ReportName = "1.6";
            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            if (SessionHandler.Current.AppId != 0)
            {
                foreach (var item in point)
                {
                    if (item.TOTAL_COUNT == null)
                    {
                        TempData["Message2"] = "Please Enter Values";
                        TempData["Message1"] = "Disabled Button";
                        List<OnePoint4VM> que1 = new List<OnePoint4VM>();
                        que1 = childRepository.GetQuetions(ReportName);
                        ModelState.Clear();
                        return View(que1);
                    }
                }
                TempData["Message1"] = null;
                TempData["Message"] = "Active Button";
                TempData["Message2"] = "Data Saved Successfully";
                childRepository.Save1Point4(point);
                List<OnePoint4VM> que = new List<OnePoint4VM>();
                que = childRepository.GetQuetions(ReportName);
                ModelState.Clear();
                return View(que);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MenuonepointsixIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult Modifypoint6Report()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");

        }

        public ActionResult OnePointSix20201(int INSERT_ID)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["User_ID"] = SessionHandler.Current.UserId;
                Session["INSERT_ID"] = INSERT_ID;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");

                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuOnePointSixDetails(int INSERT_ID)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return Redirect("/Report/OnePointSixEditIndex?INSERT_ID=" + INSERT_ID);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult OnePointSixEditIndex(int INSERT_ID)
        {
            TempData["INSERT_ID"] = INSERT_ID;
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MenuOnePointSixEdit(int ANS_ID)
        {
            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            if (SessionHandler.Current.AppId != 0)
            {
                OnePoint4VM OnePointFour = childRepository.GetOnePointFourTotalCount(ANS_ID);
                return View(OnePointFour);
            }
            else
                return Redirect("/Account/Login");

        }

        [HttpPost]
        public ActionResult MenuOnePointSixEdit(OnePoint4VM onepointfour)
        {
            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            if (SessionHandler.Current.AppId != 0)
            {
                int INSERT_ID = onepointfour.INSERT_ID;
                childRepository.SaveTotalCount(onepointfour);
                return Redirect("MenuOnePointSixDetails?INSERT_ID=" + INSERT_ID);
            }
            else
                return Redirect("/Account/Login");
        }
        #endregion

        #region 1.7

        public ActionResult Onepoint7Index1()
        {

            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            if (SessionHandler.Current.AppId != 0)
            {
                TempData["Message"] = null;
                TempData["Message1"] = 1;
                TempData["Error"] = null;
                List<OnePoint7QuestionVM> que = new List<OnePoint7QuestionVM>();
                que = childRepository.GetOnePointSevenQuestions();
                return View(que);
            }
            else
                return Redirect("/Report/OnePointfour2020");
        }

        public ActionResult Onepoint7Index()
        {
            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            if (SessionHandler.Current.AppId != 0)
            {
                TempData["Message"] = null;
                TempData["Message1"] = 1;
                TempData["Error"] = null;
                List<OnePoint7QuestionVM> que = new List<OnePoint7QuestionVM>();
                que = childRepository.GetOnePointSevenQuestions();
                return View(que);
            }
            else
                return Redirect("/Report/OnePointfour2020");
        }

        [HttpPost]
        public ActionResult Onepoint7Index(List<OnePointSevenVM> point)
        {
            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            List<OnePoint7QuestionVM> que = new List<OnePoint7QuestionVM>();
            if (SessionHandler.Current.AppId != 0)
            {
                TempData["Message1"] = null;
                TempData["Message"] = "Active Button";
                TempData["Message2"] = "Data Saved Successfully";
                childRepository.Save1Point7(point);
                que = childRepository.GetOnePointSevenQuestions();



                ModelState.Clear();
                return View(que);
            }
            else
                return Redirect("/Account/Login");
            return View();
        }

        public ActionResult MenuonepointsevenIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult Modifypoint7Report()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");

        }

        public ActionResult Onepoint7Indexnew()
        {

            return Redirect("Report/Onepoint7Index");

        }

        public ActionResult OnePointSeven20201(int INSERT_ID)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["User_ID"] = SessionHandler.Current.UserId;
                Session["INSERT_ID"] = INSERT_ID;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");

                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuOnePointSevenDetails(int INSERT_ID)
        {
            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            if (SessionHandler.Current.AppId != 0)
            {
                TempData["Message"] = null;
                TempData["Message1"] = 1;
                TempData["Error"] = null;
                List<OnePoint7QuestionVM> que = new List<OnePoint7QuestionVM>();
                que = childRepository.GetOnePointSevenAnswers(INSERT_ID);
                return View(que);
            }
            else
                return Redirect("/Report/OnePointfour2020");
        }

        [HttpPost]
        public ActionResult MenuOnePointSevenDetails(List<OnePoint7QuestionVM> point)
        {

            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository.EditOnePointSeven(point);
                return Redirect("Modifypoint7Report?INSERT_ID=1");

            }
            else
                return Redirect("/Account/Login");
        }
        #endregion

        #region 1.8

        public ActionResult Show1point8Report()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(SessionHandler.Current.AppId);

                OnePoint4VM OnePointFour = childRepository.GetMaxINSERTID();
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["User_ID"] = SessionHandler.Current.UserId;
                Session["INSERT_ID"] = OnePointFour.INSERT_ID;

                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult OnePointeight20201(int INSERT_ID)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                Session["User_ID"] = SessionHandler.Current.UserId;
                Session["INSERT_ID"] = INSERT_ID;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");

                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult Onepoint8Index(string ReportName)
        {
            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            if (SessionHandler.Current.AppId != 0)
            {
                TempData["Message"] = null;
                TempData["Message1"] = 1;
                TempData["Error"] = null;
                List<OnePoint4VM> que = new List<OnePoint4VM>();
                que = childRepository.GetQuetions(ReportName);
                return View(que);
            }
            else
                return Redirect("/Report/OnePointfour2020");
        }

        [HttpPost]
        public ActionResult Onepoint8Index(List<OnePoint4VM> point)
        {
            string ReportName = "1.8";
            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            if (SessionHandler.Current.AppId != 0)
            {
                foreach (var item in point)
                {
                    if (item.TOTAL_COUNT == null)
                    {
                        TempData["Message2"] = "Please Enter Values";
                        TempData["Message1"] = "Disabled Button";
                        List<OnePoint4VM> que1 = new List<OnePoint4VM>();
                        que1 = childRepository.GetQuetions(ReportName);
                        ModelState.Clear();
                        return View(que1);
                    }
                }
                TempData["Message1"] = null;
                TempData["Message"] = "Active Button";
                TempData["Message2"] = "Data Saved Successfully";
                childRepository.Save1Point4(point);
                List<OnePoint4VM> que = new List<OnePoint4VM>();
                que = childRepository.GetQuetions(ReportName);
                ModelState.Clear();
                return View(que);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult onepointeightIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuonepointeightIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult Modifypoint8Report()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");

        }
        public ActionResult MenuOnePointEightEdit(int ANS_ID)
        {
            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            if (SessionHandler.Current.AppId != 0)
            {
                OnePoint4VM OnePointFour = childRepository.GetOnePointFourTotalCount(ANS_ID);
                return View(OnePointFour);
            }
            else
                return Redirect("/Account/Login");

        }

        [HttpPost]
        public ActionResult MenuOnePointEightEdit(OnePoint4VM onepointfour)
        {
            childRepository = new ChildRepository(SessionHandler.Current.AppId);
            if (SessionHandler.Current.AppId != 0)
            {
                int INSERT_ID = onepointfour.INSERT_ID;
                childRepository.SaveTotalCount(onepointfour);
                return Redirect("MenuOnePointFourDetails?INSERT_ID=" + INSERT_ID);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MenuOnePointEightDetails(int INSERT_ID)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return Redirect("/Report/OnePointEightEditIndex?INSERT_ID=" + INSERT_ID);
            }
            else
                return Redirect("/Account/Login");
        }


        public ActionResult OnePointEightEditIndex(int INSERT_ID)
        {
            TempData["INSERT_ID"] = INSERT_ID;
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        #endregion
    }
}