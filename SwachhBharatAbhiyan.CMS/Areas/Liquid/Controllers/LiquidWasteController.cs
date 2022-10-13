using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Newtonsoft.Json;
using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using SwachBharat.CMS.Dal.DataContexts;
using GramPanchayat.API.Bll.ViewModels.ChildModel.Model;
using System.Collections.Generic;
using System.Globalization;


namespace SwachhBharatAbhiyan.CMS.Areas.Liquid.Controllers
{

    public class LiquidWasteController : Controller
    {


        IChildRepository childRepository;
        IMainRepository mainRepository;
        // GET: Liquid/LiquidHome
        public LiquidWasteController()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                mainRepository = new MainRepository();
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
            }
            else
                Redirect("/Account/Login");
        }
        // GET: Liquid/LiquidWaste

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
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }


        public ActionResult ReportIndex()
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
        public ActionResult AddLiquidWaste(int teamId = -1)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                LiquidWasteVM dump = childRepository.GetLiquidWasteId(teamId);
                return View(dump);
            }
            else
                return Redirect("/Account/Login");
        }


        [HttpPost]
        public ActionResult AddLiquidWaste(LiquidWasteVM LiquidWaste, HttpPostedFileBase filesUpload)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                int teamId = LiquidWaste.LWId;
                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
                var LWId = childRepository.GetLiquidWasteId(teamId);

                if (LWId.LWQRCode == "/Images/QRcode.png" || LWId.LWQRCode == "/Images/default_not_upload.png")
                {
                    LWId.LWQRCode = null;
                }

                if (LWId.LWQRCode == null)
                {
                    var guid = Guid.NewGuid().ToString().Split('-');
                    string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

                    //Converting  Url to image 
                    // var url = string.Format("http://api.qrserver.com/v1/create-qr-code/?data="+ point.ReferanceId);

                    var url = string.Format("https://chart.googleapis.com/chart?cht=qr&chl=" + LiquidWaste.ReferanceId + "&chs=160x160&chld=L|0");

                    WebResponse response = default(WebResponse);
                    Stream remoteStream = default(Stream);
                    StreamReader readStream = default(StreamReader);
                    WebRequest request = WebRequest.Create(url);
                    response = request.GetResponse();
                    remoteStream = response.GetResponseStream();
                    readStream = new StreamReader(remoteStream);
                    //Creating Path to save image in folder
                    System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
                    string imgpath = Path.Combine(Server.MapPath(AppDetails.basePath + AppDetails.LiquidQRCode), image_Guid);
                    var exists = System.IO.Directory.Exists(Server.MapPath(AppDetails.basePath + AppDetails.LiquidQRCode));
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + AppDetails.LiquidQRCode));
                    }
                    img.Save(imgpath);
                    response.Close();
                    remoteStream.Close();
                    readStream.Close();
                    LiquidWaste.LWQRCode = image_Guid;
                }
                else
                {
                    string bb = LWId.LWQRCode;
                    var ii = bb.Split('/');
                    if (ii.Length == 6)
                    {
                        LiquidWaste.LWQRCode = ii[6];
                    }
                    if (ii.Length > 6)
                    {
                        LiquidWaste.LWQRCode = ii[6];
                    }
                }
                LiquidWasteVM pointDetails = childRepository.SaveLiquidWastes(LiquidWaste);
                return Redirect("Index");
            }
            else
                return Redirect("/Account/Login");
        }


        public ActionResult Export(int id)
        {
            if (SessionHandler.Current.AppId != 0)
            {

                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
                string Filename = "", owner = "";

                var details = childRepository.GetLiquidWasteId(id);
                string cdatetime = DateTime.Now.ToString("_ddmmyyyyhhmmss");

                if (details.LWName != null)
                {
                    Filename = Regex.Replace(details.LWName, @"\s+", "") + cdatetime + ".pdf";
                    owner = details.LWName;
                }
                else
                {
                    Filename = Regex.Replace(details.LWId.ToString(), @"\s+", "") + cdatetime + ".pdf";
                    owner = "_ _ _ _ _ _ _ _ _ _ _ _";

                }
                string GridHtml = "";
                //string src = AppDetails.baseImageUrlCMS + "/Content/images/img/app_icon_cms.png";
                //string GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;margin-top: 8px;font-size:22px;background: #abd037;'> O </div> <div style='background: #abd037;;font-weight: bold;font-size: 18px;'> " + AppDetails.AppName + "</div><div style='font-size: 15px;background: #abd037;'> House Id: " + details.ReferanceId + " </div><div style='height:10px;background: #abd037;'></div> <div style='height:10px;background: #fff;'></div><div style='background: #fff;'> <img style='width:250px;height:250px;' src='" + details.houseQRCode + "'/> </div></div>";



                string src = AppDetails.baseImageUrlCMS + "/Content/images/icons/Nagpur_logo.png";
                //For Satana Only

                string top_img_new = AppDetails.baseImageUrlCMS + AppDetails.basePath + "Content/icons/Top_image.png";
                string slogan_new = AppDetails.baseImageUrlCMS + AppDetails.basePath + "Content/icons/slogan.png";
                string round = AppDetails.baseImageUrlCMS + AppDetails.basePath + "Content/icons/round.png";

                //string top_img_new = "http://localhost:34557/" + AppDetails.basePath + "Content/icons/Top_image.png";
                //string slogan_new = "http://localhost:34557/" + AppDetails.basePath + "Content/icons/slogan.png";
                //string round = "http://localhost:34557/" + AppDetails.basePath + "Content/icons/round.png";

                GridHtml = "<div style='width:100%;height: 100%;background:#ffffff;border : 2px solid #4fa30a;'><div style='float:left;width:7%;padding-top:110px;padding-left:8px;'><img src='" + round + "' style = 'width:20px;height:20px;margin-left:5px;'/></div><div style='float:left;width:58%;padding-left:16px;padding-top:7px;'><img src='" + details.LWQRCode + "' style = 'width:20px;height:20px;'/></div><div style='float:left;width:83%;padding-left:5px;padding-top:10px;padding-bottom:6px;'><div style='padding-left:5px;'><img style='width:150px;height:95px;' src='" + top_img_new + "'/></div><div style='text-align: center;font-weight: 900;padding-bottom:3px;'>&nbsp;&nbsp;&nbsp;<span style='color:#000000;text-align: center;font-size: 16px'>LWC Id </span><br/><span style='color:#000000;text-align: center;font-size: 21px'>" + details.ReferanceId + "</span></div><div style='padding-left:5px;'><img src='" + slogan_new + "' style='width: 150px; height:49px;'/><br/></div></div><div style='float:left;width:3%;padding-top:110px;padding-left:22px;text-align:center;'><img src='" + round + "' style = 'width:20px;height:20px;'/></div></div>";



                using (MemoryStream stream = new System.IO.MemoryStream())
                {
                    StringReader sr = new StringReader(GridHtml);

                    var pgSize = new iTextSharp.text.Rectangle(324, 180);
                    Document pdfDoc = new Document(pgSize, 1f, 1f, 1f, 1f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    var content = writer.DirectContent;
                    var pageBorderRect = new Rectangle(pdfDoc.PageSize);

                    pageBorderRect.Left += pdfDoc.LeftMargin;
                    pageBorderRect.Right -= pdfDoc.RightMargin;
                    pageBorderRect.Top -= pdfDoc.TopMargin;
                    pageBorderRect.Bottom += pdfDoc.BottomMargin;

                    content.SetColorStroke(BaseColor.BLACK);
                    content.SetLineWidth(5);
                    content.Rectangle(pageBorderRect.Left, pageBorderRect.Bottom, pageBorderRect.Width, pageBorderRect.Height);
                    content.Stroke();
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();


                    return File(stream.ToArray(), "application/pdf", Filename);
                }
            }
            else
                return Redirect("/Account/Login");
        }


        public ActionResult GenratePDF(string name, string ReferanceId)
        {
            if (SessionHandler.Current.AppId != 0)
            {

                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);

                string Filename = "";


                string cdatetime = DateTime.Now.ToString("_ddmmyyyyhhmmss");

                if (name != null && name != "")
                {
                    Filename = name + "(" + cdatetime + ").pdf";
                }
                else
                {
                    Filename = ReferanceId + "(" + cdatetime + ").pdf";
                    name = "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ ";
                }



                var guid = Guid.NewGuid().ToString().Split('-');
                string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";
                var url = string.Format("http://api.qrserver.com/v1/create-qr-code/?data=" + ReferanceId);
                WebResponse response = default(WebResponse);
                Stream remoteStream = default(Stream);
                StreamReader readStream = default(StreamReader);
                WebRequest request = WebRequest.Create(url);
                response = request.GetResponse();
                remoteStream = response.GetResponseStream();
                readStream = new StreamReader(remoteStream);


                //Creating Path to save image in folder
                System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
                string imgpath = Path.Combine(Server.MapPath(AppDetails.basePath + AppDetails.LiquidQRCode), image_Guid);
                var exists = System.IO.Directory.Exists(Server.MapPath(AppDetails.basePath + AppDetails.LiquidQRCode));
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + AppDetails.LiquidQRCode));
                }
                img.Save(imgpath);
                response.Close();
                remoteStream.Close();
                readStream.Close();


                string GridHtml = string.Empty;
                // string GridHtml = "<style>table{border-collapse: collapse;}table, td, th { padding:8px;border: 1px solid black;}<style><div><center><p style='margin-top:0px'><img style='width:200px;height200px' src='http://sbaappynitty.co.in:4022/Content/images/img/app_icon_cms.png'/></p><p style='font-size:25px;font-weight:bold;margin:0px'>Ghanta Gadi</p><p style='font-size:25px;font-weight:bold;margin:0px'>" + AppDetails.AppName + "</p><br/><hr/><br/></center></div><div><center><p style='font-size:32px;margin-bottom:15px'>< b id = 'hide_Name' > " + owner + "</b ></p ></center><table align='center' class='table'> <tr> <td style='font-weight: bold;'>House Number  </td> <td style=''>" + number + "</td> </tr> <tr> <td style='font-weight: bold;'>Ward Number  </td> <td style=''>" + ward.WardNo+ "</td> </tr> <tr> <td style='font-weight: bold;'>Area  </td> <td style=''> " + area.Name + "</td> </tr> <tr> <td style='font-weight: bold;'> House Id  </td> <td style=''>" + ReferanceId + "</td> </tr> </table><center><br/><p style='font-weight: bold;'> Scan Scaniffy Code <br /><br/>< img class='img-responsive' id='imggg' alt='hoto Not Found'  src='" +string.Format("https://api.qrserver.com/v1/create-qr-code/?data=" + ReferanceId ) + "'/></p><br /></center><div class='modal-footer'></div></div>";

                //string slogan = AppDetails.baseImageUrlCMS + "/Content/images/icons/slogan.png";
                //string topimg = AppDetails.baseImageUrlCMS + "/content/images/icons/top_outdoor.png";
                //string leftbottomimg = AppDetails.baseImageUrlCMS + "/Content/images/icons/left_outdoor.png";
                //string dry_wet_new = AppDetails.baseImageUrlCMS + "/Content/images/icons/Khapa/dry&wet.png";

                //string src = AppDetails.baseImageUrlCMS + "/Content/images/icons/Nagpur_logo.png";

                string top_img_new = AppDetails.baseImageUrlCMS + AppDetails.basePath + "Content/icons/Top_image.png";
                string slogan_new = AppDetails.baseImageUrlCMS + AppDetails.basePath + "Content/icons/slogan.png";
                string round = AppDetails.baseImageUrlCMS + AppDetails.basePath + "Content/icons/round.png";

                //string top_img_new = "http://localhost:34557/" + AppDetails.basePath + "Content/icons/Top_image.png";
                //string slogan_new = "http://localhost:34557/" + AppDetails.basePath + "Content/icons/slogan.png";
                //string round = "http://localhost:34557/" + AppDetails.basePath + "Content/icons/round.png";


                GridHtml = "<div style='width:100%;height: 100%;background:#ffffff;border : 2px solid #4fa30a;'><div style='float:left;width:7%;padding-top:110px;padding-left:8px;'><img src='" + round + "' style = 'width:20px;height:20px;margin-left:5px;'/></div><div style='float:left;width:58%;padding-left:16px;padding-top:7px;'><img src='" + imgpath + "' style = 'width:20px;height:20px;'/></div><div style='float:left;width:83%;padding-left:5px;padding-top:10px;padding-bottom:6px;'><div style='padding-left:5px;'><img style='width:150px;height:95px;' src='" + top_img_new + "'/></div><div style='text-align: center;font-weight: 900;padding-bottom:3px;'>&nbsp;&nbsp;&nbsp;<span style='color:#000000;text-align: center;font-size: 16px'>LWC Id</span><br/><span style='color:#000000;text-align: center;font-size: 21px'>" + ReferanceId + "</span></div><div style='padding-left:5px;'><img src='" + slogan_new + "' style='width: 150px; height:49px;'/><br/></div></div><div style='float:left;width:3%;padding-top:110px;padding-left:22px;text-align:center;'><img src='" + round + "' style = 'width:20px;height:20px;'/></div></div>";


                //string GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;margin-top: 8px;font-size:22px;background: #abd037;'> O </div> <div style='background: #abd037;;font-weight: bold;font-size: 18px;'> " + AppDetails.AppName + "</div><div style='font-size: 15px;background: #abd037;'> House Id: " + ReferanceId + " </div><div style='height:10px;background: #abd037;'></div> <div style='height:10px;background: #fff;'></div><div style='background: #fff;'> <img style='width:250px;height:250px;' src='" + string.Format("https://api.qrserver.com/v1/create-qr-code/?data=" + ReferanceId) + "'/>  </div></div>";
                using (MemoryStream stream = new System.IO.MemoryStream())
                {
                    StringReader sr = new StringReader(GridHtml);
                    var pgSize = new iTextSharp.text.Rectangle(324, 180);
                    Document pdfDoc = new Document(pgSize, 1f, 1f, 1f, 1f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    var content = writer.DirectContent;
                    var pageBorderRect = new Rectangle(pdfDoc.PageSize);

                    pageBorderRect.Left += pdfDoc.LeftMargin;
                    pageBorderRect.Right -= pdfDoc.RightMargin;
                    pageBorderRect.Top -= pdfDoc.TopMargin;
                    pageBorderRect.Bottom += pdfDoc.BottomMargin;

                    content.SetColorStroke(BaseColor.BLACK);
                    content.SetLineWidth(5);
                    content.Rectangle(pageBorderRect.Left, pageBorderRect.Bottom, pageBorderRect.Width, pageBorderRect.Height);
                    content.Stroke();
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    return File(stream.ToArray(), "application/pdf", Filename);
                }

            }
            else
                return Redirect("/Account/Login");
        }



    }
}