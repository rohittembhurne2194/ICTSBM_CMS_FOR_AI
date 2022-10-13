using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SwachhBharatAbhiyan.CMS
{
    public partial class DisplayReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    var AppID = Request.QueryString["AppID"];
                    var ReportName = Request.QueryString["ReportName"];
                    var DB_Name = Request.QueryString["DB_Name"];
                    var DB_Source = Request.QueryString["DB_Source"];
                    var param1 = Request.QueryString["Param1"];
                    var SelectDate = Request.QueryString["SelectDate"];
                    var FromDate = Request.QueryString["FromDate"];
                    var ToDate = Request.QueryString["ToDate"];
                    var Date = Request.QueryString["Date"];
                    var INSERT_ID = Request.QueryString["INSERT_ID"];

                    //DateTime fdate = Convert.ToDateTime(Request.QueryString["FromDate"]);
                    //DateTime tdate = Convert.ToDateTime(Request.QueryString["ToDate"]);
                    //string FromDate = fdate.ToString("dd/MM/yyyy");
                    //string ToDate = tdate.ToString("dd/MM/yyyy");

                    var OperatorID = Request.QueryString["OperatorID"];
                    var UserId = Request.QueryString["UserId"];
                    var DyId = Request.QueryString["DyId"];
                    var garbageType = Request.QueryString["Type"];

                    if (UserId == "-1")
                    {
                        UserId = null;
                    }

                    if (garbageType == "4" || garbageType == "undefined")
                    {
                        garbageType = null;
                    }


                    //string urlReportServer = "http://TESTYOCC-1:80/reportServer";
                    //  string urlReportServer = "http://YOCC-2:82/reportServer";
                    //string urlReportServer = "http://192.168.100.123/ReportServer";
                    //string urlReportServer = "http://192.168.100.123/ReportServer";
                    string urlReportServer = "";
                   
                    
                    urlReportServer = "http://202.65.157.253:85/ReportServer";
                        //string urlReportServer = "http://COMP-7/ReportServer";
                        //rptViewer.ServerReport.ReportServerCredentials = new ReportServerCredentials("Administrator", "Pass@123", "192.168.100.7");
                    rptViewer.ServerReport.ReportServerCredentials = new ReportServerCredentials("Administrator", "Telec0m#B!9V", "202.65.157.253");
                  
                    rptViewer.ProcessingMode = ProcessingMode.Remote;

                    rptViewer.ServerReport.ReportServerUrl = new Uri(urlReportServer);
                    rptViewer.ServerReport.ReportPath = "/ICTSBM_BI_REPORT_NEW/" + ReportName;
                    rptViewer.ShowToolBar = true;
                    rptViewer.BackColor = System.Drawing.Color.White;
                    //rptViewer.Parent.ResolveClientUrl.

                    if (ReportName == "Ghar Sankalan Tapashil")
                    {
                        ReportParameter[] param = new ReportParameter[7];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("userid", UserId);
                        param[4] = new ReportParameter("gartype", garbageType);
                        param[5] = new ReportParameter("DBName", DB_Name);
                        param[6] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);

                        // param[1] = new ReportParameter("clientid", _userInfo.ClientID.ToString());
                    }
                    else if (ReportName == "Employee_Attendance")
                    {
                        ReportParameter[] param = new ReportParameter[6];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("userid", UserId);
                        param[4] = new ReportParameter("DBName", DB_Name);
                        param[5] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);

                        // param[1] = new ReportParameter("clientid", _userInfo.ClientID.ToString());
                    }
                    else if (ReportName == "single Employee collection")
                    {
                        ReportParameter[] param = new ReportParameter[6];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("userid", UserId);
                        //param[4] = new ReportParameter("gartype", garbageType);
                        param[4] = new ReportParameter("DBName", DB_Name);
                        param[5] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "Dumpyard_Employeeswise")
                    {
                        ReportParameter[] param = new ReportParameter[6];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("dyid", DyId);
                        param[4] = new ReportParameter("DBName", DB_Name);
                        param[5] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);

                    }
                    else if (ReportName == "Garbase Collection Percentage")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "Employee Collection Summary")
                    {
                        ReportParameter[] param = new ReportParameter[6];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("fromd", Date);
                        param[5] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "Employee Garbage Collection Count")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "Areawise Garbage Collection")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "Citywise Garbage Report")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);

                    }
                    else if (ReportName == "Areawise Garbage Type Collection")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);

                    }
                    else if (ReportName == "Dashboard_1")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);

                    }

                    else if (ReportName == "DumpYard_Summary")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);

                    }

                          
                    else if (ReportName == "Housewise Garbage Collection")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);

                    }
                    else if (ReportName == "Daywise Collection")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);

                    }
                    else if (ReportName == "CTPT_Feedback")
                    {
                        ReportParameter[] param = new ReportParameter[3];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        rptViewer.ServerReport.SetParameters(param);

                    }
                    
                    else if (ReportName == "T20_1.1")
                    {
                        ReportParameter[] param = new ReportParameter[4];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "T20_1.2")
                    {
                        ReportParameter[] param = new ReportParameter[4];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "T20_1.3")
                    {
                        ReportParameter[] param = new ReportParameter[4];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "T20_1.4")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("INSERT_ID", INSERT_ID);
                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "T20_1.5")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("INSERT_ID", INSERT_ID);
                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "T20_1.6")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("INSERT_ID", INSERT_ID);
                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "Employee_Performance_Report")
                     {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("DBName", DB_Name);
                        param[2] = new ReportParameter("from", FromDate);
                        param[3] = new ReportParameter("to", ToDate);
                        param[4] = new ReportParameter("DBSource", DB_Source);

                        rptViewer.ServerReport.SetParameters(param);
                    }


                    // Liquid Report Start
                    else if (ReportName == "Liquid Ghar Sankalan Tapashil")
                    {
                        ReportParameter[] param = new ReportParameter[6];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("userid", UserId);
                        param[4] = new ReportParameter("DBName", DB_Name);
                        param[5] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);

                        // param[1] = new ReportParameter("clientid", _userInfo.ClientID.ToString());
                    }
                    else if (ReportName == "Liquid single Employee Collection")
                    {
                        ReportParameter[] param = new ReportParameter[6];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("userid", UserId);
                        //param[4] = new ReportParameter("gartype", garbageType);
                        param[4] = new ReportParameter("DBName", DB_Name);
                        param[5] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "Liquid Citywise Garbage Report")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);

                    }
                    else if (ReportName == "Liquid Areawise Garbage Collection")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "Liquid Areawise Garbage Type Collection")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);

                    }
                    else if (ReportName == "Liquid Garbase Collection Percentage")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "Liquid Employee Garbage Collection Count")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "LiquidDashboard_1")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);

                    }
                    else if (ReportName == "Liquid Daywise Collection")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);

                    }
                    else if (ReportName == "Liquid Employee_Performance_Report")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("DBName", DB_Name);
                        param[2] = new ReportParameter("from", FromDate);
                        param[3] = new ReportParameter("to", ToDate);
                        param[4] = new ReportParameter("DBSource", DB_Source);

                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "Liquid Employee Collection Summary")
                    {
                        ReportParameter[] param = new ReportParameter[6];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("fromd", Date);
                        param[5] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);
                    }

                    // Street Report Start
                    else if (ReportName == "Street single Employee Collection")
                    {
                        ReportParameter[] param = new ReportParameter[6];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("userid", UserId);
                        //param[4] = new ReportParameter("gartype", garbageType);
                        param[4] = new ReportParameter("DBName", DB_Name);
                        param[5] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "Street Citywise Garbage Report")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);

                    }
                    else if (ReportName == "Street Areawise Garbage Collection")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "Street Areawise Garbage Type Collection")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);

                    }
                    else if (ReportName == "Street Garbase Collection Percentage")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "Street Employee Garbage Collection Count")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "StreetDashboard_1")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);

                    }
                    else if (ReportName == "Street Daywise Collection")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);

                    }
                    else if (ReportName == "Street Employee_Performance_Report")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("DBName", DB_Name);
                        param[2] = new ReportParameter("from", FromDate);
                        param[3] = new ReportParameter("to", ToDate);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);
                    }

                    else if (ReportName == "Street Garbase Collection Percentage")
                    {
                        ReportParameter[] param = new ReportParameter[5];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);
                    }

                    else if (ReportName == "Street Employee Collection Summary")
                    {
                        ReportParameter[] param = new ReportParameter[6];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("DBName", DB_Name);
                        param[4] = new ReportParameter("fromd", Date);
                        param[5] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);
                    }
                    else if (ReportName == "Street Ghar Sankalan Tapashil")
                    {
                        ReportParameter[] param = new ReportParameter[6];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("userid", UserId);
                        param[4] = new ReportParameter("DBName", DB_Name);
                        param[5] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);

                        // param[1] = new ReportParameter("clientid", _userInfo.ClientID.ToString());
                    }
                   else if (ReportName == "Street Sweeping Report")
                    {
                        ReportParameter[] param = new ReportParameter[6];
                        param[0] = new ReportParameter("appid", AppID);
                        param[1] = new ReportParameter("from", FromDate);
                        param[2] = new ReportParameter("to", ToDate);
                        param[3] = new ReportParameter("userid", UserId);
                        param[4] = new ReportParameter("DBName", DB_Name);
                        param[5] = new ReportParameter("DBSource", DB_Source);
                        rptViewer.ServerReport.SetParameters(param);

                        // param[1] = new ReportParameter("clientid", _userInfo.ClientID.ToString());
                    }
                    rptViewer.ServerReport.Refresh();



                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        protected void repViewer_Drillthrough(object sender, Microsoft.Reporting.WebForms.DrillthroughEventArgs e)
        {
            ReportParameterInfoCollection rpic = e.Report.GetParameters();
            List<ReportParameter> paramList = new List<ReportParameter>();
            foreach (ReportParameterInfo inf in rpic)
                paramList.Add(new ReportParameter(inf.Name, inf.Values[0]));
            string rPath = e.ReportPath;
            //string urlReportServer = "http://TESTYOCC-1:80/reportServer";
            //rptViewer.ServerReport.ReportServerUrl = new Uri("http://TESTYOCC-1:80/reportServer");
            // rptViewer.ServerReport.ReportServerUrl = new Uri("http://YOCC-2:82/reportServer");
            //rptViewer.ServerReport.ReportServerUrl = new Uri("http://192.168.100.123/ReportServer");
            rptViewer.ServerReport.ReportServerUrl = new Uri("http://202.65.157.253:85/ReportServer");
            rptViewer.ServerReport.ReportServerCredentials = new ReportServerCredentials("Administrator", "Telec0m#B!9V", "202.65.157.253");
            rptViewer.ServerReport.ReportPath = rPath;
            rptViewer.ServerReport.SetParameters(paramList);
            rptViewer.ServerReport.Refresh();
            e.Cancel = true;
            string script = @"$('#jqueryRpt').dialog({
                        title: 'Drill Through Report',
                        resizable: true,
                        modal: false,
                        show: 'blind',
                        hide: 'fade',
                        width: 1000,
                        height: 700,
                        buttons: {
                            'Close': function() {
                                $(this).dialog('close');
                            }
                        }
                    });";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "open", script, true);
        }

    }
}