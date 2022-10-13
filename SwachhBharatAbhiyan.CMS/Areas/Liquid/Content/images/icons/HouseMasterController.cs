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

namespace SwachhBharatAbhiyan.CMS.Controllers
{
    public class HouseMasterController : Controller
    {
        // GET: HouseMaster  
        IChildRepository childRepository;
        IMainRepository mainRepository;

        public HouseMasterController()
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
        public ActionResult ReportIndex()
        {

            if (SessionHandler.Current.AppId != 0)
            {
                Session["NewAppID"] = SessionHandler.Current.AppId;
                Session["DB_Name"] = SessionHandler.Current.DB_Name;
                string Reportname = "ss";

                ViewBag.IframeUrl = "/DisplayReports.aspx?FromDate=" + DateTime.Now.ToString("MM/dd/yyyy");

                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MenuReportIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult AddHouseDetails(int teamId = -2)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                HouseDetailsVM house = childRepository.GetHouseById(teamId);


                return View(house);


            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult AddHouseDetails(HouseDetailsVM house)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                int teamId = house.houseId;
                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
                var houseId = childRepository.GetHouseById(teamId);
                if (houseId.houseQRCode == "/Images/QRcode.png" || houseId.houseQRCode == "/Images/default_not_upload.png")
                {
                    houseId.houseQRCode = null;
                }
                if (houseId.houseQRCode == null)
                {
                    var guid = Guid.NewGuid().ToString().Split('-');
                    string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

                    //Converting  Url to image 
                    // var url = string.Format("http://api.qrserver.com/v1/create-qr-code/?data="+ house.ReferanceId);
                    var url = string.Format("https://chart.googleapis.com/chart?cht=qr&chl=" + house.ReferanceId + "&chs=160x160&chld=L|0");
                    WebResponse response = default(WebResponse);
                    Stream remoteStream = default(Stream);
                    StreamReader readStream = default(StreamReader);
                    WebRequest request = WebRequest.Create(url);
                    response = request.GetResponse();
                    remoteStream = response.GetResponseStream();
                    readStream = new StreamReader(remoteStream);
                    //Creating Path to save image in folder
                    System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
                    string imgpath = Path.Combine(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode), image_Guid);
                    var exists = System.IO.Directory.Exists(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode));
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode));
                    }
                    img.Save(imgpath);
                    response.Close();
                    remoteStream.Close();
                    readStream.Close();
                    house.houseQRCode = image_Guid;
                }
                else
                {
                    string bb = houseId.houseQRCode;
                    var ii = bb.Split('/');
                    house.houseQRCode = ii[7];
                }
                HouseDetailsVM houseDetails = childRepository.SaveHouse(house);
                return Redirect("Index");
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpGet]
        public ActionResult DeleteHouse(int teamId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository.DeletHouse(teamId);
                return Redirect("Index");
            }
            else
                return Redirect("/Account/Login");
        }

        #region AjaxCallFunctions 

        public ActionResult Address(string location)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                if (location != string.Empty && location != null)
                {
                    HouseDetailsVM house = new HouseDetailsVM();
                    XmlDocument doc = new XmlDocument();
                    //doc.Load("http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + lat + "," + log + "& sensor=false");
                    doc.Load("https://maps.googleapis.com/maps/api/geocode/xml?address=" + location + "&sensor=false&key=%20AIzaSyBy6BUqH6o1r7JBS8s1Tk7cmllapL6xuMA");

                    XmlNode element = doc.SelectSingleNode("//GeocodeResponse/status");
                    if (element.InnerText == "ZERO_RESULTS")
                    {
                        Console.WriteLine("No data available for the specified location");
                    }
                    else
                    {
                        XmlNode xnList1 = doc.SelectSingleNode("//GeocodeResponse/result/geometry/location");

                        house.houseLat = xnList1["lat"].InnerText;
                        house.houseLong = xnList1["lng"].InnerText;
                    }
                    string jsonstr = JsonConvert.SerializeObject(house);
                    return Json(jsonstr, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json(null, JsonRequestBehavior.AllowGet);
                }
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

                var details = childRepository.GetHouseById(id);
                string cdatetime = DateTime.Now.ToString("_ddmmyyyyhhmmss");

                if (details.houseOwner != null)
                {
                    Filename = Regex.Replace(details.houseOwner, @"\s+", "") + cdatetime + ".pdf";
                    owner = details.houseOwner;
                }
                else
                {
                    Filename = Regex.Replace(details.houseId.ToString(), @"\s+", "") + cdatetime + ".pdf";
                    owner = "_ _ _ _ _ _ _ _ _ _ _ _";

                }
                string src = AppDetails.baseImageUrlCMS + "/Content/images/img/app_icon_cms.png";
                string GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;margin-top: 8px;font-size:22px;background: #abd037;'> O </div> <div style='background: #abd037;;font-weight: bold;font-size: 18px;'> " + AppDetails.AppName + "</div><div style='font-size: 15px;background: #abd037;'> House Id: " + details.ReferanceId + " </div><div style='height:10px;background: #abd037;'></div> <div style='height:10px;background: #fff;'></div><div style='background: #fff;'> <img style='width:250px;height:250px;' src='" + details.houseQRCode + "'/> </div></div>";
                using (MemoryStream stream = new System.IO.MemoryStream())
                {
                    StringReader sr = new StringReader(GridHtml);
                    var pgSize = new iTextSharp.text.Rectangle(216, 288);
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


        public ActionResult AreaList()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                HouseDetailsVM obj = new HouseDetailsVM();

                obj = childRepository.GetHouseById(-1);
                return Json(obj.AreaList, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }


        public ActionResult WardList()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                HouseDetailsVM obj = new HouseDetailsVM();

                obj = childRepository.GetHouseById(-1);
                return Json(obj.WardList, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult ZoneList()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                HouseDetailsVM obj = new HouseDetailsVM();

                obj = childRepository.GetHouseById(-1);
                return Json(obj.ZoneList, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult LoadWardNoList(int ZoneId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                HouseDetailsVM obj = new HouseDetailsVM();
                try
                {
                    obj.WardList = childRepository.LoadListWardNo(ZoneId);
                }
                catch (Exception ex) { throw ex; }

                return Json(obj.WardList, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }


        public ActionResult LoadAreaList(int WardNo)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                HouseDetailsVM obj = new HouseDetailsVM();
                try
                {
                    obj.AreaList = childRepository.LoadListArea(WardNo);
                }
                catch (Exception ex) { throw ex; }

                return Json(obj.AreaList, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }


        #endregion
        //public ActionResult GenratePDF(string name, string number, string ReferanceId, string Area, string Ward)
        //{
        //    if (SessionHandler.Current.AppId != 0 && name != null && number != null && ReferanceId != null)
        //    {

        //        var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
        //        var ward = childRepository.GetWardNumber(Convert.ToInt32(Ward), "");
        //        var area = childRepository.GetArea(Convert
        //            .ToInt32(Area), "");
        //        string Filename = "", owner = "";


        //        string cdatetime = DateTime.Now.ToString("_ddmmyyyyhhmmss");

        //        if (name != null)
        //        {
        //            Filename = Regex.Replace(name, @"\s+", "") + cdatetime + ".pdf";
        //            owner = name;
        //        }
        //        else {
        //            Filename = Regex.Replace(ReferanceId, @"\s+", "") + cdatetime + ".pdf";
        //            owner = "_ _ _ _ _ _ _ _ _ _ _ _";
        //        }
        //        string src = AppDetails.baseImageUrlCMS + "/Content/images/img/app_icon_cms.png";

        //        string GridHtml = "<style>table{border-collapse: collapse;}table, td, th { padding:8px;border: 1px solid black;}<style><div><center><p style='margin-top:0px'><img style='width:200px;height200px' src='" + src + "'/></p><p style='font-size:25px;font-weight:bold;margin:0px'>Ghanta Gadi</p><p style='font-size:25px;font-weight:bold;margin:0px'>" + AppDetails.AppName + "</p><br/><hr/><br/></center></div><div><center><p style='font-size:32px;margin-bottom:15px'>< b id = 'hide_Name' > " + owner + "</b ></p ></center><table align='center' class='table'> <tr> <td style='font-weight: bold;'>House Number  </td> <td style=''>" + number + "</td> </tr> <tr> <td style='font-weight: bold;'>Ward Number  </td> <td style=''>" + ward.WardNo + "</td> </tr> <tr> <td style='font-weight: bold;'>Area  </td> <td style=''> " + area.Name + "</td> </tr> <tr> <td style='font-weight: bold;'> House Id  </td> <td style=''>" + ReferanceId + "</td> </tr> </table><center><br/><p style='font-weight: bold;'> Scan Scaniffy Code <br /><br/>< img class='img-responsive' id='imggg' alt='hoto Not Found'  src='" + string.Format("http://api.qrserver.com/v1/create-qr-code/?data=" + ReferanceId) + "'/></p><br /></center><div class='modal-footer'></div></div>";
        //        using (MemoryStream stream = new System.IO.MemoryStream())
        //        {
        //            StringReader sr = new StringReader(GridHtml);
        //            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
        //            pdfDoc.Open();
        //            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        //            pdfDoc.Close();
        //            return File(stream.ToArray(), "application/pdf", Filename);
        //        }

        //    }
        //    else
        //        return Redirect("/Account/Login");
        //}


        public ActionResult GenratePDF(string name, string number, string ReferanceId, string Area, string Ward)
        {
            if (SessionHandler.Current.AppId != 0 && name != null && number != null && ReferanceId != null)
            {

                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
                var ward = childRepository.GetWardNumber(Convert.ToInt32(Ward), "");
                var area = childRepository.GetArea(Convert
                    .ToInt32(Area), "");
                string Filename = "", owner = "";


                string cdatetime = DateTime.Now.ToString("_ddmmyyyyhhmmss");

                if (name != null)
                {
                    Filename = Regex.Replace(name, @"\s+", "") + cdatetime + ".pdf";
                    owner = name;
                }
                else
                {
                    Filename = Regex.Replace(ReferanceId, @"\s+", "") + cdatetime + ".pdf";
                    owner = "_ _ _ _ _ _ _ _ _ _ _ _";
                }


                // string GridHtml = "<style>table{border-collapse: collapse;}table, td, th { padding:8px;border: 1px solid black;}<style><div><center><p style='margin-top:0px'><img style='width:200px;height200px' src='http://sbaappynitty.co.in:4022/Content/images/img/app_icon_cms.png'/></p><p style='font-size:25px;font-weight:bold;margin:0px'>Ghanta Gadi</p><p style='font-size:25px;font-weight:bold;margin:0px'>" + AppDetails.AppName + "</p><br/><hr/><br/></center></div><div><center><p style='font-size:32px;margin-bottom:15px'>< b id = 'hide_Name' > " + owner + "</b ></p ></center><table align='center' class='table'> <tr> <td style='font-weight: bold;'>House Number  </td> <td style=''>" + number + "</td> </tr> <tr> <td style='font-weight: bold;'>Ward Number  </td> <td style=''>" + ward.WardNo+ "</td> </tr> <tr> <td style='font-weight: bold;'>Area  </td> <td style=''> " + area.Name + "</td> </tr> <tr> <td style='font-weight: bold;'> House Id  </td> <td style=''>" + ReferanceId + "</td> </tr> </table><center><br/><p style='font-weight: bold;'> Scan Scaniffy Code <br /><br/>< img class='img-responsive' id='imggg' alt='hoto Not Found'  src='" +string.Format("https://api.qrserver.com/v1/create-qr-code/?data=" + ReferanceId ) + "'/></p><br /></center><div class='modal-footer'></div></div>";



                string GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;margin-top: 8px;font-size:22px;background: #abd037;'> O </div> <div style='background: #abd037;;font-weight: bold;font-size: 18px;'> " + AppDetails.AppName + "</div><div style='font-size: 15px;background: #abd037;'> House Id: " + ReferanceId + " </div><div style='height:10px;background: #abd037;'></div> <div style='height:10px;background: #fff;'></div><div style='background: #fff;'> <img style='width:250px;height:250px;' src='" + string.Format("https://api.qrserver.com/v1/create-qr-code/?data=" + ReferanceId) + "'/>  </div></div>";
                using (MemoryStream stream = new System.IO.MemoryStream())
                {
                    StringReader sr = new StringReader(GridHtml);
                    var pgSize = new iTextSharp.text.Rectangle(216, 288);
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

        [HttpGet]
        public ActionResult AreaMaster()
        {
            return View();
        }
        [HttpPost]

        public ActionResult AreaMaster(AreaVM area)
        {

            if (SessionHandler.Current.AppId != 0)
            {
                childRepository.SaveArea(area);
                // return PartialView("AreaMaster");
                return Json(area, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }


        [HttpPost]

        public ActionResult WardMaster(WardNumberVM area)
        {

            if (SessionHandler.Current.AppId != 0)
            {
                childRepository.SaveWardNumber(area);
                // return PartialView("AreaMaster");
                return Json(area, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }


        [HttpPost]

        public ActionResult ZoneMaster(ZoneVM zone)
        {

            if (SessionHandler.Current.AppId != 0)
            {
                childRepository.SaveZone(zone);
                // return PartialView("AreaMaster");
                return Json(zone, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }
        [HttpPost]
        public ActionResult Save2(string name, string number, string ReferanceId, string mobile, string Area, string Ward)
        {
            if (SessionHandler.Current.AppId != 0)
            {

                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
                var ward = childRepository.GetWardNumber(Convert.ToInt32(Ward), "");
                var area = childRepository.GetArea(Convert
                    .ToInt32(Area), "");
                string Filename = "";


                string cdatetime = DateTime.Now.ToString("_ddmmyyyyhhmmss");

                Filename = Regex.Replace(name, @"\s+", "") + cdatetime + ".pdf";

                string msg = "नमस्कार आपणास खालील लिंक द्वारे आपल्या घराचा  विशिष्ट स्कॅनिफाय कोड पाठवण्यात आलेला आहे. कचरा संकलन हेतू हा कोड आपल्या घराला देण्यात आलेला आहे कृपया हा कोड आपण डाउनलोड करून ठेवावा धन्यवाद. " + AppDetails.yoccContact + " आपल्या सेवेशी " + AppDetails.AppName_mar + ". " + AppDetails.baseImageUrlCMS + AppDetails.basePath + AppDetails.HouseQRCode + "/" + Filename;


                string GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;margin-top: 8px;font-size:22px;background: #abd037;'> O </div> <div style='background: #abd037;;font-weight: bold;font-size: 18px;'> " + AppDetails.AppName + "</div><div style='font-size: 15px;background: #abd037;'> House Id: " + ReferanceId + " </div><div style='height:10px;background: #abd037;'></div> <div style='height:10px;background: #fff;'></div><div style='background: #fff;'> <img style='width:250px;height:250px;' src='" + string.Format("https://api.qrserver.com/v1/create-qr-code/?data=" + ReferanceId) + "'/>  </div></div>";


                using (MemoryStream stream = new System.IO.MemoryStream())
                {
                    StringReader sr = new StringReader(GridHtml);
                    var pgSize = new iTextSharp.text.Rectangle(216, 288);
                    Document pdfDoc = new Document(pgSize, 1f, 1f, 1f, 1f);
                    var path = Path.Combine(Server.MapPath("~/" + AppDetails.basePath + AppDetails.HouseQRCode), Filename);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(path, FileMode.Create));
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
                    sendSMS(msg, mobile);
                    return Redirect("Index");
                }
            }
            else
                return Redirect("Index");
        }


        public void Save(int id)
        {
            if (SessionHandler.Current.AppId != 0)
            {

                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
                string Filename = "";

                var details = childRepository.GetHouseById(id);
                string cdatetime = DateTime.Now.ToString("_ddmmyyyyhhmmss");

                Filename = details.houseOwner + cdatetime + ".pdf";
                string msg = AppDetails.baseImageUrlCMS + AppDetails.basePath + AppDetails.HouseQRCode + "/" + Filename;

                string GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;margin-top: 8px;font-size:22px;background: #abd037;'> O </div> <div style='background: #abd037;;font-weight: bold;font-size: 18px;'> " + AppDetails.AppName + "</div><div style='font-size: 15px;background: #abd037;'> House Id: " + details.ReferanceId + " </div><div style='height:10px;background: #abd037;'></div> <div style='height:10px;background: #fff;'></div><div style='background: #fff;'> <img style='width:250px;height:250px;' src='" + details.houseQRCode + "'/> </div></div>";
                using (MemoryStream stream = new System.IO.MemoryStream())
                {
                    StringReader sr = new StringReader(GridHtml);
                    var pgSize = new iTextSharp.text.Rectangle(216, 288);
                    Document pdfDoc = new Document(pgSize, 1f, 1f, 1f, 1f);
                    var path = Path.Combine(Server.MapPath("~/" + AppDetails.basePath + AppDetails.HouseQRCode), Filename);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(path, FileMode.Create));
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
                    sendSMS(msg, details.houseMobile);
                }
            }

        }


        public void sendSMS(string sms, string MobilNumber)
        {
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://www.smsjust.com/sms/user/urlsms.php?username=ycagent&pass=yocc@5095&senderid=YOCCAG&dest_mobileno=" + MobilNumber + "&msgtype=UNI&message=" + sms + "&response=Y");
                //Get response from Ozeki NG SMS Gateway Server and read the answer
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
            }
            catch { }

        }

        public void test(HouseDetailsVM house)
        {

            var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
            var guid = Guid.NewGuid().ToString().Split('-');
            string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

            //Converting  Url to image 
            var url = string.Format("http://api.qrserver.com/v1/create-qr-code/?data=" + house.ReferanceId);
            WebResponse response = default(WebResponse);
            Stream remoteStream = default(Stream);
            StreamReader readStream = default(StreamReader);
            WebRequest request = WebRequest.Create(url);
            response = request.GetResponse();
            remoteStream = response.GetResponseStream();
            readStream = new StreamReader(remoteStream);
            //Creating Path to save image in folder
            System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
            string imgpath = Path.Combine(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode), image_Guid);
            var exists = System.IO.Directory.Exists(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode));
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode));
            }
            img.Save(imgpath);
            response.Close();
            remoteStream.Close();
            readStream.Close();
            house.houseQRCode = image_Guid;

            HouseDetailsVM houseDetails = childRepository.SaveHouse(house);

            // generate pdf
            string Filename = "";


            string cdatetime = DateTime.Now.ToString("_ddmmyyyyhhmmss");

            Filename = house.ReferanceId + "(" + cdatetime + ").pdf";

            string GridHtml = "<style>table{border-collapse: collapse;}table, td, th { padding:8px;border: 1px solid black;}<style><div><center><p style='margin-top:0px'><img style='width:200px;height200px' src='http://sbaappynitty.co.in:4022/Content/images/img/app_icon_cms.png'/></p><p style='font-size:25px;font-weight:bold;margin:0px'>Ghanta Gadi</p><p style='font-size:25px;font-weight:bold;margin:0px'>" + AppDetails.AppName + "</p><br/><hr/><br/></center></div><div><center><p style='font-size:32px;margin-bottom:15px'>< b id = 'hide_Name' > _ _ _ _ _ _ _ _ _ _ _</b ></p ></center><table align='center' class='table'> <tr> <td style='font-weight: bold;'>House Number  </td> <td style=''></td> </tr> <tr> <td style='font-weight: bold;'>Ward Number  </td> <td style=''></td> </tr> <tr> <td style='font-weight: bold;'>Area  </td> <td style=''> </td> </tr> <tr> <td style='font-weight: bold;'> House Id  </td> <td style=''>" + house.ReferanceId + "</td> </tr> </table><center><br/><p style='font-weight: bold;'> Scan Scaniffy Code <br /><br/>< img class='img-responsive' id='imggg' alt='hoto Not Found'  src='" + string.Format("http://api.qrserver.com/v1/create-qr-code/?data=" + house.ReferanceId) + "'/></p><br /></center><div class='modal-footer'></div></div>";

            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                //string path = Path.GetDirectoryName(Application.ExecutablePath);

                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                var path = Path.Combine(Server.MapPath("~/" + AppDetails.basePath + AppDetails.HouseQRCode + "/bundel"), Filename);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(path, FileMode.Create));
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();

            }

        }

        [HttpGet]
        public ActionResult SaveBundel(int teamId = -1)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                //HouseDetailsVM house = childRepository.GetHouseById(teamId);
                //test(house);
                ZoneVM v = new ZoneVM();
                return View(v);


            }
            else
                return Redirect("/Account/Login");
        }



        [HttpPost]
      //  for middle size
        public ActionResult SaveBundel(ZoneVM zone)
        {

            if (SessionHandler.Current.AppId != 0)
            {
                HouseDetailsVM house = childRepository.GetHouseById(-1);
                List<string> list = new List<string>();
                int n = house.houseId;
                for (int i = 1; i <= zone.id; i++)
                {
                    int number = 1000;
                    string refer = "HPSBA" + (number + n + i);
                    house.ReferanceId = refer;


                    double refer1 = Convert.ToDouble((n + i));
                    double xyz = refer1 / 100;

                    string s = xyz.ToString("0.00", CultureInfo.InvariantCulture);
                    string[] parts = s.Split('.');
                    int i1 = int.Parse(parts[0]);
                    int i2 = int.Parse(parts[1]);

                    if (i2 == 0)
                    {
                        //i1 = i1 + 1;
                        s = "S" + i1.ToString();
                    }
                    else
                    {
                        s = "S" + (i1 + 1);
                    }


                    house.SerielNo = s;

                    house.houseId = 0;
                    var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
                    var guid = Guid.NewGuid().ToString().Split('-');
                    string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

                    //Converting  Url to image 
                    var url = string.Format("http://api.qrserver.com/v1/create-qr-code/?data=" + house.ReferanceId);
                    // var url = string.Format("https://chart.googleapis.com/chart?cht=qr&chl=" + house.ReferanceId + "&chs=250x250&chld=L|0");
                    WebResponse response = default(WebResponse);
                    Stream remoteStream = default(Stream);
                    StreamReader readStream = default(StreamReader);
                    WebRequest request = WebRequest.Create(url);
                    response = request.GetResponse();
                    remoteStream = response.GetResponseStream();
                    readStream = new StreamReader(remoteStream);

                    readStream = new StreamReader(remoteStream);
                    //Creating Path to save image in folder
                    System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
                    string imgpath = Path.Combine(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode), image_Guid);
                    var exists = System.IO.Directory.Exists(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode));
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode));
                    }
                    img.Save(imgpath);
                    response.Close();
                    remoteStream.Close();
                    readStream.Close();
                    house.houseQRCode = image_Guid;
                    house.ReferanceId = house.ReferanceId;
                    HouseDetailsVM houseDetails = childRepository.SaveHouse(house);

                    // generate pdf
                    string Filename = "";

                    string cdatetime = DateTime.Now.ToString("_ddmmyyyyhhmmss");

                    Filename = house.ReferanceId + "(" + cdatetime + ").pdf";
                    string src = AppDetails.baseImageUrlCMS + "/Content/images/icons/Naldurg_logo.png";

                    //For Satana Only
                   string GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;padding-top: 5px;background: #abd037;'> <img style='width:250px;height:63px;' src='" + src + "'/> </div> <div style='font-size: 14px;background: #abd037;'> House Id: " + house.ReferanceId + " </div> <div style='height:10px;background: #fff;'></div><div style='background: #fff;'> <img style='width:250px;height:250px;' src='" + imgpath + "'/> <p style='text-align:right;font-size: 10px;margin-right:20px;margin-top:0px'><b>" + house.SerielNo + "</b></p></div></div>";

                   // string GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;padding-top: 5px;background: #abd037;'> <img style='width:182px;height:63px;' src='" + src + "'/> </div> <div style='font-size: 14px;background: #abd037;'> House Id: " + house.ReferanceId + " </div> <div style='height:10px;background: #fff;'></div><div style='background: #fff;'> <img style='width:250px;height:250px;' src='" + imgpath + "'/> <p style='text-align:right;font-size: 10px;margin-right:20px;margin-top:0px'><b>" + house.SerielNo + "</b></p></div></div>";

                    // Main Design
                    //string GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;margin-top: 8px;font-size:22px;background: #abd037;'> O </div> <div style='background: #abd037;;font-weight: bold;font-size: 18px;'> " + AppDetails.AppName + "</div><div style='font-size: 15px;background: #abd037;'> House Id: " + house.ReferanceId + " </div><div style='height:10px;background: #abd037;'></div> <div style='height:10px;background: #fff;'></div><div style='background: #fff;'> <img style='width:250px;height:250px;' src='" + imgpath + "'/><p style='text-align:right;font-size: 10px;margin-right:20px;margin-top:0px'><b>" + house.SerielNo + "</b></p></div></div>";




                    //using (MemoryStream stream = new System.IO.MemoryStream())
                    //{
                    //    StringReader sr = new StringReader(GridHtml);
                    //    var pgSize = new iTextSharp.text.Rectangle(216, 288);
                    //    Document pdfDoc = new Document(pgSize, 1f, 1f, 1f, 1f);
                    //    var path = Path.Combine(Server.MapPath("~/" + AppDetails.basePath + AppDetails.HouseQRCode + "/bundel"), Filename);

                    //    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(path, FileMode.Create));
                    //    pdfDoc.Open();
                    //    var content = writer.DirectContent;
                    //    var pageBorderRect = new Rectangle(pdfDoc.PageSize);

                    //    pageBorderRect.Left += pdfDoc.LeftMargin;
                    //    pageBorderRect.Right -= pdfDoc.RightMargin;
                    //    pageBorderRect.Top -= pdfDoc.TopMargin;
                    //    pageBorderRect.Bottom += pdfDoc.BottomMargin;


                    //    content.SetColorStroke(BaseColor.BLACK);
                    //    content.SetLineWidth(5);
                    //    content.Rectangle(pageBorderRect.Left, pageBorderRect.Bottom, pageBorderRect.Width, pageBorderRect.Height);
                    //    content.Stroke();
                    //    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    //    pdfDoc.Close();
                    //    return File(stream.ToArray(), "application/pdf", Filename);
                    //}


                    using (MemoryStream stream = new System.IO.MemoryStream())
                    {
                        //string path = Path.GetDirectoryName(Application.ExecutablePath);
                        StringReader sr = new StringReader(GridHtml);
                        var pgSize = new iTextSharp.text.Rectangle(216, 288);
                        Document pdfDoc = new Document(pgSize, 1f, 1f, 1f, 1f);

                        var path = Path.Combine(Server.MapPath("~/" + AppDetails.basePath + AppDetails.HouseQRCode + "/bundel"), Filename);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(path, FileMode.Create));
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

                    }

                }
                // test(house);
                ZoneVM v = new ZoneVM();
                return View();
            }
            else
                return Redirect("/Account/Login");

        }

      //  end of middle size



      //   for visiting card size currently working//


        [HttpPost]
        //for visiting card size
        public ActionResult SaveBundelVisiting(ZoneVM zone)
        {

            if (SessionHandler.Current.AppId != 0)
            {
                HouseDetailsVM house = childRepository.GetHouseById(-1);
                List<string> list = new List<string>();
                double n = house.houseId;

                for (int i = 1; i <= zone.id; i++)
                {

                    int number = 1000;
                    string refer = "HPSBA" + (number + n + i);
                    house.ReferanceId = refer;

                    //HouseDetailsVM house1 = childRepository.GetHouseById(-1);
                    //List<string> list1 = new List<string>();
                    //double n1 = house1.houseId;
                    // int abc = zone.id / 100;


                    double refer1 = Convert.ToDouble((n + i));
                    double xyz = refer1 / 100;

                    string s = xyz.ToString("0.00", CultureInfo.InvariantCulture);
                    string[] parts = s.Split('.');
                    int i1 = int.Parse(parts[0]);
                    int i2 = int.Parse(parts[1]);

                    if (i2 == 0)
                    {
                        //i1 = i1 + 1;
                        s = "S" + i1.ToString();
                    }
                    else
                    {
                        s = "S" + (i1 + 1);
                    }


                    house.SerielNo = s;
                    house.houseId = 0;
                    var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
                    var guid = Guid.NewGuid().ToString().Split('-');
                    string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

                    //Converting  Url to image 
                    var url = string.Format("http://api.qrserver.com/v1/create-qr-code/?data=" + house.ReferanceId);
                    // var url = string.Format("https://chart.googleapis.com/chart?cht=qr&chl=" + house.ReferanceId + "&chs=250x250&chld=L|0");
                    WebResponse response = default(WebResponse);
                    Stream remoteStream = default(Stream);
                    StreamReader readStream = default(StreamReader);
                    WebRequest request = WebRequest.Create(url);
                    response = request.GetResponse();
                    remoteStream = response.GetResponseStream();
                    readStream = new StreamReader(remoteStream);

                    readStream = new StreamReader(remoteStream);
                    //Creating Path to save image in folder
                    System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
                    string imgpath = Path.Combine(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode), image_Guid);
                    var exists = System.IO.Directory.Exists(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode));
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode));
                    }
                    img.Save(imgpath);
                    response.Close();
                    remoteStream.Close();
                    readStream.Close();
                    house.houseQRCode = image_Guid;
                    house.ReferanceId = house.ReferanceId;
                    HouseDetailsVM houseDetails = childRepository.SaveHouse(house);

                    // generate pdf
                    string Filename = "";

                    string cdatetime = DateTime.Now.ToString("_ddmmyyyyhhmmss");

                    Filename = house.ReferanceId + "(" + cdatetime + ").pdf";
                    string src = AppDetails.baseImageUrlCMS + "/Content/images/icons/sarni_outdoor.png";

                    //For Main Design
                    string GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;margin-top: 8px;font-size:22px;background: #abd037;'> O </div> <div style='background: #abd037;;font-weight: bold;font-size: 18px;'> " + AppDetails.AppName + "</div><div style='font-size: 15px;background: #abd037;'> House Id: " + house.ReferanceId + " </div><div style='height:10px;background: #abd037;'></div> <div style='height:10px;background: #fff;'></div><div style='background: #fff;'> <img style='width:188px;height:188px;' src='" + imgpath + "'/><p style='text-align:right;font-size: 10px;margin-right:8px;margin-top:0px'><b>" + house.SerielNo + "</b></p> </div></div>";

                    // For Satana only
                    //string GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;padding-top: 8px;font-size:22px;background: #abd037;'> <img style='width:188px;height:90px;' src='" + src + "'/> </div> <div style='font-size: 15px;background: #abd037;'> House Id: " + house.ReferanceId + " </div> <div style='height:10px;background: #fff;'></div><div style='background: #fff;'> <img style='width:188px;height:188px;' src='" + imgpath + "'/> <p style='text-align:right;font-size: 10px;margin-right:8px;margin-top:0px'><b>" + house.SerielNo + "</b></p> </div></div>";


                    //using (MemoryStream stream = new System.IO.MemoryStream())
                    //{
                    //    StringReader sr = new StringReader(GridHtml);
                    //    var pgSize = new iTextSharp.text.Rectangle(148, 258);
                    //    Document pdfDoc = new Document(pgSize, 1f, 1f, 1f, 1f);
                    //    var path = Path.Combine(Server.MapPath("~/" + AppDetails.basePath + AppDetails.HouseQRCode + "/bundel"), Filename);

                    //    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(path, FileMode.Create));
                    //    pdfDoc.Open();
                    //    var content = writer.DirectContent;
                    //    var pageBorderRect = new Rectangle(pdfDoc.PageSize);

                    //    pageBorderRect.Left += pdfDoc.LeftMargin;
                    //    pageBorderRect.Right -= pdfDoc.RightMargin;
                    //    pageBorderRect.Top -= pdfDoc.TopMargin;
                    //    pageBorderRect.Bottom += pdfDoc.BottomMargin;


                    //    content.SetColorStroke(BaseColor.BLACK);
                    //    content.SetLineWidth(5);
                    //    content.Rectangle(pageBorderRect.Left, pageBorderRect.Bottom, pageBorderRect.Width, pageBorderRect.Height);
                    //    content.Stroke();
                    //    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    //    pdfDoc.Close();
                    //    return File(stream.ToArray(), "application/pdf", Filename);
                    //}


                    using (MemoryStream stream = new System.IO.MemoryStream())
                    {
                        //string path = Path.GetDirectoryName(Application.ExecutablePath);
                        StringReader sr = new StringReader(GridHtml);
                        var pgSize = new iTextSharp.text.Rectangle(148, 258);
                        Document pdfDoc = new Document(pgSize, 1f, 1f, 1f, 1f);

                        var path = Path.Combine(Server.MapPath("~/" + AppDetails.basePath + AppDetails.HouseQRCode + "/bundel"), Filename);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(path, FileMode.Create));
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

                    }

                }
                // test(house);
                ZoneVM v = new ZoneVM();
                return View();
            }
            else
                return Redirect("/Account/Login");

        }

        //end of visiting card size


        [HttpPost]
        //for Out Door Dry and Wet  size 
        public ActionResult SaveDryWet(ZoneVM zone)
        {

            if (SessionHandler.Current.AppId != 0)
            {
                HouseDetailsVM house = childRepository.GetHouseById(-1);
                List<string> list = new List<string>();
                double n = house.houseId;

                for (int i = 1; i <= zone.id; i++)
                {

                    int number = 1000;
                    string refer = "HPSBA" + (number + n + i);
                    house.ReferanceId = refer;

                    //HouseDetailsVM house1 = childRepository.GetHouseById(-1);
                    //List<string> list1 = new List<string>();
                    //double n1 = house1.houseId;
                    // int abc = zone.id / 100;


                    double refer1 = Convert.ToDouble((n + i));
                    double xyz = refer1 / 100;

                    string s = xyz.ToString("0.00", CultureInfo.InvariantCulture);
                    string[] parts = s.Split('.');
                    int i1 = int.Parse(parts[0]);
                    int i2 = int.Parse(parts[1]);

                    if (i2 == 0)
                    {
                        //i1 = i1 + 1;
                        s = "S" + i1.ToString();
                    }
                    else
                    {
                        s = "S" + (i1 + 1);
                    }


                    house.SerielNo = s;
                    house.houseId = 0;
                    var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
                    var guid = Guid.NewGuid().ToString().Split('-');
                    string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

                    //Converting  Url to image 
                    var url = string.Format("http://api.qrserver.com/v1/create-qr-code/?data=" + house.ReferanceId);
                    // var url = string.Format("https://chart.googleapis.com/chart?cht=qr&chl=" + house.ReferanceId + "&chs=250x250&chld=L|0");
                    WebResponse response = default(WebResponse);
                    Stream remoteStream = default(Stream);
                    StreamReader readStream = default(StreamReader);
                    WebRequest request = WebRequest.Create(url);
                    response = request.GetResponse();
                    remoteStream = response.GetResponseStream();
                    readStream = new StreamReader(remoteStream);

                    readStream = new StreamReader(remoteStream);
                    //Creating Path to save image in folder
                    System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
                    string imgpath = Path.Combine(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode), image_Guid);
                    var exists = System.IO.Directory.Exists(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode));
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode));
                    }
                    img.Save(imgpath);
                    response.Close();
                    remoteStream.Close();
                    readStream.Close();
                    house.houseQRCode = image_Guid;
                    house.ReferanceId = house.ReferanceId;
                    HouseDetailsVM houseDetails = childRepository.SaveHouse(house);

                    // generate pdf
                    string Filename = "";

                    string cdatetime = DateTime.Now.ToString("_ddmmyyyyhhmmss");

                    Filename = house.ReferanceId + "(" + cdatetime + ").pdf";
                    string slogan = AppDetails.baseImageUrlCMS + "/Content/images/icons/slogan.png";
                    string topimg = AppDetails.baseImageUrlCMS + "/Content/images/icons/top_image (1).png";

                    string leftbottomimg = AppDetails.baseImageUrlCMS + "/Content/images/icons/new_left_strip.png";

                    string background = AppDetails.baseImageUrlCMS + "/Content/images/icons/background.png";

                    string src = AppDetails.baseImageUrlCMS + "/Content/images/icons/01_small_v.png";

                    string GridHtml = "<div style='width:100%;height: 100%;background:#0080ff;border : 2px solid black;'><div style='text-align:center;padding-top: 10px;background: #0080ff;'> <img style='width:260px;height:101px;' src='" + topimg + "'/> </div> <div style='font-size: 16px;width:250px;margin-left:190px;'><b style='color:#ffffff;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  House Id: " + house.ReferanceId + "  </b><p></p></div> <span style='margin-right:25px;text-align:right;'><img  style='width:55;height:201px;margin-right:18px;padding-top: 0px;' src='" + leftbottomimg + "'/></span> &nbsp;&nbsp;&nbsp;<img style='width:200px;height:200px;text-align:right;padding-right:20px;padding-top: 0px;' src='" + imgpath + "'  /> <p style='text-align:right;font-size: 10px;margin-right:10px;margin-top:10px'><img style='width:152px;height:16px;margin-right:18px;'src='" + slogan + "'/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b style='color:#ffffff;vertical-align: top;font-size: 12px'>" + house.SerielNo + "</b></p></div>";

                    using (MemoryStream stream = new System.IO.MemoryStream())
                    {
                        //string path = Path.GetDirectoryName(Application.ExecutablePath);
                        StringReader sr = new StringReader(GridHtml);
                        var pgSize = new iTextSharp.text.Rectangle(216, 288);
                        Document pdfDoc = new Document(pgSize, 1f, 1f, 1f, 1f);

                        var path = Path.Combine(Server.MapPath("~/" + AppDetails.basePath + AppDetails.HouseQRCode + "/bundel"), Filename);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(path, FileMode.Create));
                        //    iTextSharp.text.Image img1 = iTextSharp.text.Image.GetInstance(writer.AddDirectImageSimple.To);
                        pdfDoc.Open();

                        var content = writer.DirectContent;
                        var pageBorderRect = new Rectangle(pdfDoc.PageSize);

                        pageBorderRect.Left += pdfDoc.LeftMargin;
                        pageBorderRect.Right -= pdfDoc.RightMargin;
                        pageBorderRect.Top -= pdfDoc.TopMargin;
                        pageBorderRect.Bottom += pdfDoc.BottomMargin;


                        content.SetColorStroke(BaseColor.BLACK);
                        content.SetLineWidth(10);
                        content.Rectangle(pageBorderRect.Left, pageBorderRect.Bottom, pageBorderRect.Width, pageBorderRect.Height);
                        content.Stroke();


                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                        pdfDoc.Close();

                    }

                }
                // test(house);
                ZoneVM v = new ZoneVM();
                return View();
            }
            else
                return Redirect("/Account/Login");

        }

        //end of Dry and Wet  size

        [HttpPost]
        //for start DryWet visiting card size
        public ActionResult SaveBundelVisitingDryWet(ZoneVM zone)
        {

            if (SessionHandler.Current.AppId != 0)
            {
                HouseDetailsVM house = childRepository.GetHouseById(-1);
                List<string> list = new List<string>();
                double n = house.houseId;

                for (int i = 1; i <= zone.id; i++)
                {

                    int number = 1000;
                    string refer = "HPSBA" + (number + n + i);
                    house.ReferanceId = refer;

                    //HouseDetailsVM house1 = childRepository.GetHouseById(-1);
                    //List<string> list1 = new List<string>();
                    //double n1 = house1.houseId;
                    // int abc = zone.id / 100;


                    double refer1 = Convert.ToDouble((n + i));
                    double xyz = refer1 / 100;

                    string s = xyz.ToString("0.00", CultureInfo.InvariantCulture);
                    string[] parts = s.Split('.');
                    int i1 = int.Parse(parts[0]);
                    int i2 = int.Parse(parts[1]);

                    if (i2 == 0)
                    {
                        //i1 = i1 + 1;
                        s = "S" + i1.ToString();
                    }
                    else
                    {
                        s = "S" + (i1 + 1);
                    }


                    house.SerielNo = s;
                    house.houseId = 0;
                    var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
                    var guid = Guid.NewGuid().ToString().Split('-');
                    string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

                    //Converting  Url to image 
                    var url = string.Format("http://api.qrserver.com/v1/create-qr-code/?data=" + house.ReferanceId);
                    // var url = string.Format("https://chart.googleapis.com/chart?cht=qr&chl=" + house.ReferanceId + "&chs=250x250&chld=L|0");
                    WebResponse response = default(WebResponse);
                    Stream remoteStream = default(Stream);
                    StreamReader readStream = default(StreamReader);
                    WebRequest request = WebRequest.Create(url);
                    response = request.GetResponse();
                    remoteStream = response.GetResponseStream();
                    readStream = new StreamReader(remoteStream);

                    readStream = new StreamReader(remoteStream);
                    //Creating Path to save image in folder
                    System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
                    string imgpath = Path.Combine(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode), image_Guid);
                    var exists = System.IO.Directory.Exists(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode));
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode));
                    }
                    img.Save(imgpath);
                    response.Close();
                    remoteStream.Close();
                    readStream.Close();
                    house.houseQRCode = image_Guid;
                    house.ReferanceId = house.ReferanceId;
                    HouseDetailsVM houseDetails = childRepository.SaveHouse(house);

                    // generate pdf
                    string Filename = "";

                    string cdatetime = DateTime.Now.ToString("_ddmmyyyyhhmmss");

                    Filename = house.ReferanceId + "(" + cdatetime + ").pdf";
                    string byhandy_top_image = AppDetails.baseImageUrlCMS + "/Content/images/icons/byhandy_top_image.png";
                    string kannad_byhandy = AppDetails.baseImageUrlCMS + "/Content/images/icons/kannad_byhandy.png";
                    string slogan = AppDetails.baseImageUrlCMS + "/Content/images/icons/slogan.png";

                    // For DryWet without Yocc Image visiting card size
                  //  string GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #0080ff;border : 2px solid black;'><div style='text-align:center;padding-top: 8px;font-size:22px;background: #0080ff;'> <img style='width:180px;height:101px;' src='" + byhandy_top_image + "'/> </div> <div style='font-size: 12px;background: #0080ff;color:#ffffff'><b> House Id:" + house.ReferanceId + "</b> </div> <div style='height:5px;background: #0080ff;'></div><div style='background:#0080ff;'> <img style='width:180px;height:180px;' src='" + imgpath + "'/> <p style='text-align:right;font-size: 10px;margin-right:8px;margin-top:10px'><img style='width:120px;height:12px;' src='" + slogan + "'/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b style='color:#ffffff;vertical-align: middle;'>S12</b></p> </div></div>";

                    // For DryWet with Yocc Image visiting card size
                  string GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #0080ff;border : 1px solid black;'><div style='text-align:center;padding-top: 8px;font-size:22px;background: #0080ff;'> <img style='width:170px;height:95px;' src='" + byhandy_top_image + "'/> </div> <div style='font-size: 12px;background: #0080ff;color:#ffffff'><b> House Id:" + house.ReferanceId + "</b> </div> <div style='height:5px;background: #0080ff;'></div><div style='background:#0080ff;'> <img style='width:170px;height:170px;' src='" + imgpath + "'/> <p style='text-align:left;font-size: 10px;margin-left:12px;margin-top:5px'><img style='width:138px;height:35px;' src='" + kannad_byhandy + "'/>&nbsp;&nbsp;&nbsp;&nbsp;<b style='color:#ffffff;vertical-align:top;margin-left:25px'>S100</b></p> </div></div>";


                    using (MemoryStream stream = new System.IO.MemoryStream())
                    {
                        //string path = Path.GetDirectoryName(Application.ExecutablePath);
                        StringReader sr = new StringReader(GridHtml);
                        var pgSize = new iTextSharp.text.Rectangle(148, 258);
                        Document pdfDoc = new Document(pgSize, 1f, 1f, 1f, 1f);

                        var path = Path.Combine(Server.MapPath("~/" + AppDetails.basePath + AppDetails.HouseQRCode + "/bundel"), Filename);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(path, FileMode.Create));
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

                    }

                }
                // test(house);
                ZoneVM v = new ZoneVM();
                return View();
            }
            else
                return Redirect("/Account/Login");

        }
    //}

    //end of DryWet visiting card size

    //    [HttpPost]

    ////For bigger size
    //public ActionResult SaveBundel(ZoneVM zone)
    //{

    //    if (SessionHandler.Current.AppId != 0)
    //    {
    //        HouseDetailsVM house = childRepository.GetHouseById(-1);
    //        List<string> list = new List<string>();
    //        int n = house.houseId;
    //        for (int i = 1; i <= zone.id; i++)
    //        {
    //            int number = 1000;
    //            string refer = "HPSBA" + (number + n + i);
    //            house.ReferanceId = refer;

    //            house.houseId = 0;
    //            var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
    //            var guid = Guid.NewGuid().ToString().Split('-');
    //            string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

    //            //Converting  Url to image 
    //            var url = string.Format("http://api.qrserver.com/v1/create-qr-code/?data=" + house.ReferanceId);
    //            // var url = string.Format("https://chart.googleapis.com/chart?cht=qr&chl=" + house.ReferanceId + "&chs=250x250&chld=L|0");
    //            WebResponse response = default(WebResponse);
    //            Stream remoteStream = default(Stream);
    //            StreamReader readStream = default(StreamReader);
    //            WebRequest request = WebRequest.Create(url);
    //            response = request.GetResponse();
    //            remoteStream = response.GetResponseStream();
    //            readStream = new StreamReader(remoteStream);

    //            readStream = new StreamReader(remoteStream);
    //            //Creating Path to save image in folder
    //            System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
    //            string imgpath = Path.Combine(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode), image_Guid);
    //            var exists = System.IO.Directory.Exists(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode));
    //            if (!exists)
    //            {
    //                System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode));
    //            }
    //            img.Save(imgpath);
    //            response.Close();
    //            remoteStream.Close();
    //            readStream.Close();
    //            house.houseQRCode = image_Guid;
    //            house.ReferanceId = house.ReferanceId;
    //            HouseDetailsVM houseDetails = childRepository.SaveHouse(house);

    //            // generate pdf
    //            string Filename = "";

    //            string cdatetime = DateTime.Now.ToString("_ddmmyyyyhhmmss");

    //            Filename = house.ReferanceId + "(" + cdatetime + ").pdf";
    //            string src = AppDetails.baseImageUrlCMS + "/Content/images/img/app_icon_cms.png";
    //            string GridHtml = "<style>table{border-collapse: collapse;}table, td, th { padding:8px;border: 1px solid black;}<style><div><center><p style='margin-top:0px'><img style='width:200px;height200px' src='" + src + "'/></p><p style='font-size:25px;font-weight:bold;margin:0px'>Ghanta Gadi</p><p style='font-size:25px;font-weight:bold;margin:0px'>" + AppDetails.AppName + "</p><br/><hr/><br/></center></div><div><center><p style='font-size:32px;margin-bottom:15px'>< b id = 'hide_Name' > _ _ _ _ _ _ _ _ _ _ _</b ></p ></center><table align='center' class='table'> <tr> <td style='font-weight: bold;'>House Number  </td> <td style=''></td> </tr> <tr> <td style='font-weight: bold;'>Ward Number  </td> <td style=''></td> </tr> <tr> <td style='font-weight: bold;'>Area  </td> <td style=''> </td> </tr> <tr> <td style='font-weight: bold;'> House Id  </td> <td style=''>" + house.ReferanceId + "</td> </tr> </table><center><br/><p style='font-weight: bold;'> Scan Scaniffy Code <br /><br/>< img class='img-responsive' id='imggg' alt='hoto Not Found'  src='" + url + "'/></p><br /></center><div class='modal-footer'></div></div>";

    //            using (MemoryStream stream = new System.IO.MemoryStream())
    //            {
    //                //string path = Path.GetDirectoryName(Application.ExecutablePath);

    //                StringReader sr = new StringReader(GridHtml);
    //                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
    //                var path = Path.Combine(Server.MapPath("~/" + AppDetails.basePath + AppDetails.HouseQRCode + "/bundel"), Filename);
    //                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(path, FileMode.Create));
    //                pdfDoc.Open();
    //                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
    //                pdfDoc.Close();

    //            }
    //        }
    //        // test(house);
    //        ZoneVM v = new ZoneVM();
    //        return View();
    //    }
    //    else
    //        return Redirect("/Account/Login");

    //}
  //  end of bigger size

     }

}

 



