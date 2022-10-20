using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SwachhBharatAbhiyan.CMS.Controllers
{
    public class ForeCastReportsController : Controller
    {
        // GET: ForeCastReports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EMp_WiseCollection_AI()
        {

            //var test = Process.Start("D:/Rohit/AI_Documents/heatmap_Armori.py");
            //return View(test);
            var IP = SessionHandler.Current.DB_Source;
            var DB = SessionHandler.Current.DB_Name;
            var ULB_Name = SessionHandler.Current.AppName;

            string trim_ULB_Name = ULB_Name.Replace(" ", "");

            var psi = new ProcessStartInfo();
            string HostName = Request.Url.Host;
            string port = Convert.ToString(Request.Url.Port);

            if (HostName == "localhost")
            {
                psi.FileName = @"C:\Users\user\AppData\Local\Programs\Python\Python37\python.exe"; // or any python environment


            }
            else
            {
                HostName = HostName + ":" + port;
                psi.FileName = @"C:\Users\Administrator\AppData\Local\Programs\Python\Python37\python.exe"; // or any python environment

            }

            //psi.Arguments = $"\"D:/Rohit/ICTSBM_CMS_AI_TEST_NEW/SwachhBharatAbhiyan.CMS/AI_ReportsFiles/EmpWise_Collection.py";
            // string pythonfile = System.Web.Hosting.HostingEnvironment.MapPath("~/AI_ReportsFiles/EmpWise_Collection.py");
            string pythonfile = System.Web.Hosting.HostingEnvironment.MapPath("~/AI_ReportsFiles/EmpWise_Collection.py");

            psi.Arguments = string.Format("{0} {1} {2} {3} {4}", pythonfile, "-ip " + IP, "-db " + DB, "-ulbname " + trim_ULB_Name, "-hostname " + HostName);


            psi.UseShellExecute = false;
            psi.CreateNoWindow = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.StandardOutputEncoding = Encoding.UTF8;

            string errors = "", result = "";

            using (var process = Process.Start(psi))
            {
                result = process.StandardOutput.ReadToEnd();
                errors = process.StandardError.ReadToEnd();

            }
            StringWriter writer = new StringWriter();
            HttpUtility.HtmlDecode(result, writer);
            var decodedString = writer.ToString();

            ViewBag.AIReportFolder = trim_ULB_Name;
            return View();
        }

        public ActionResult DumpYardForecastReport()
        {

            //var test = Process.Start("D:/Rohit/AI_Documents/heatmap_Armori.py");
            //return View(test);
            var IP = SessionHandler.Current.DB_Source;
            var DB = SessionHandler.Current.DB_Name;
            var ULB_Name = SessionHandler.Current.AppName;

            string trim_ULB_Name = ULB_Name.Replace(" ", "");

            var psi = new ProcessStartInfo();
            string HostName = Request.Url.Host;
            string port = Convert.ToString(Request.Url.Port);

            if (HostName == "localhost")
            {
                psi.FileName = @"C:\Users\user\AppData\Local\Programs\Python\Python37\python.exe"; // or any python environment


            }
            else
            {
                HostName = HostName + ":" + port;
                psi.FileName = @"C:\Users\Administrator\AppData\Local\Programs\Python\Python37\python.exe"; // or any python environment

            }

            //psi.Arguments = $"\"D:/Rohit/ICTSBM_CMS_AI_TEST_NEW/SwachhBharatAbhiyan.CMS/AI_ReportsFiles/EmpWise_Collection.py";
            // string pythonfile = System.Web.Hosting.HostingEnvironment.MapPath("~/AI_ReportsFiles/EmpWise_Collection.py");
            string pythonfile = System.Web.Hosting.HostingEnvironment.MapPath("~/AI_ReportsFiles/DumpYardForcast/main.py");

            psi.Arguments = string.Format("{0} {1} {2} {3} {4}", pythonfile, "-ip " + IP, "-db " + DB, "-ulbname " + trim_ULB_Name, "-hostname " + HostName);


            psi.UseShellExecute = false;
            psi.CreateNoWindow = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.StandardOutputEncoding = Encoding.UTF8;

            string errors = "", result = "";

            using (var process = Process.Start(psi))
            {
                result = process.StandardOutput.ReadToEnd();
                errors = process.StandardError.ReadToEnd();

            }
            StringWriter writer = new StringWriter();
            HttpUtility.HtmlDecode(result, writer);
            var decodedString = writer.ToString();

            ViewBag.AIReportFolder = trim_ULB_Name;
            return View();
        }
    }
}