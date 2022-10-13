using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Newtonsoft.Json;
using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace SwachhBharatAbhiyan.CMS.Controllers
{
    public class GarbagePointController : Controller
    {
        // GET: GarbagePoint
         IChildRepository childRepository;
        IMainRepository mainRepository;
        public GarbagePointController()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                mainRepository = new MainRepository();
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
            }
            else
                Redirect("/Account/Login");
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
        public ActionResult Index()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult AddPointDetails(int teamId = -1)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                GarbagePointDetailsVM house = childRepository.GetGarbagePointById(teamId);
                return View(house);
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult AddPointDetails(GarbagePointDetailsVM point, HttpPostedFileBase filesUpload)
        {
           if (SessionHandler.Current.AppId != 0)
            {
                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
                var guid = Guid.NewGuid().ToString().Split('-');
                string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

                //Converting  Url to image 
               // var url = string.Format("http://api.qrserver.com/v1/create-qr-code/?data="+ point.ReferanceId);

                var url = string.Format("https://chart.googleapis.com/chart?cht=qr&chl=" + point.ReferanceId + "&chs=160x160&chld=L|0");

                WebResponse response = default(WebResponse);
                Stream remoteStream = default(Stream);
                StreamReader readStream = default(StreamReader);
                WebRequest request = WebRequest.Create(url);
                response = request.GetResponse();
                remoteStream = response.GetResponseStream();
                readStream = new StreamReader(remoteStream);
                //Creating Path to save image in folder
                System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
                string imgpath = Path.Combine(Server.MapPath(AppDetails.basePath + AppDetails.PointQRCode), image_Guid);
                var exists = System.IO.Directory.Exists(Server.MapPath(AppDetails.basePath + AppDetails.PointQRCode));
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + AppDetails.PointQRCode));
                }
                img.Save(imgpath);
                response.Close();
                remoteStream.Close();
                readStream.Close();
                point.qrCode = image_Guid;

                GarbagePointDetailsVM pointDetails =  childRepository.SaveGarbagePoint(point);
               
                
                return Redirect("Index");
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpGet] 
        public ActionResult DeleteGarbagePoint(int teamId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository.DeletGarbagePoint(teamId);
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
                    doc.Load("https://maps.googleapis.com/maps/api/geocode/xml?address=" + location + "&sensor=false&key=%20AIzaSyAnpDwF1bzgUsM38_pWgmr70Dgy3jNsdBI");

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
       
       
        public ActionResult Export(int id )
        {
            if (SessionHandler.Current.AppId != 0)
            {
                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
                string Filename = "",name="";

                var details = childRepository.GetGarbagePointById(id);
                string cdatetime = DateTime.Now.ToString("_ddmmyyyyhhmmss");

                if (details.gpName != null && details.gpName != "")
                {
                    Filename = details.gpName + "(" + cdatetime + ").pdf";
                    name = details.gpName;
                }
                else
                {
                    Filename = details.ReferanceId + "(" + cdatetime + ").pdf";
                    name = "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ ";
                }

                string src = AppDetails.baseImageUrlCMS + "/Content/images/img/app_icon_cms.png";
                string GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;margin-top: 8px;font-size:22px;background: #abd037;'> O </div> <div style='background: #abd037;;font-weight: bold;font-size: 18px;'> " + AppDetails.AppName + "</div><div style='font-size: 15px;background: #abd037;'> Point Id: " + details.ReferanceId + " </div><div style='height:10px;background: #abd037;'></div> <div style='height:10px;background: #fff;'></div><div style='background: #fff;'> <img style='width:250px;height:250px;' src='" + details.qrCode + "'/> </div></div>";


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

        #endregion

        public ActionResult GenratePDF(string name,string ReferanceId)
        {
            if (SessionHandler.Current.AppId != 0 && name != null && ReferanceId != null)
            {

                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
                string Filename = ""; 
                string cdatetime = DateTime.Now.ToString("_ddmmyyyyhhmmss");
                if (name != null && name != "")
                {
                    Filename = name+"(" + cdatetime + ").pdf";
                }
                else {
                    Filename = ReferanceId+ "(" + cdatetime + ").pdf";
                    name = "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ ";
                }

         

                string GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;margin-top: 8px;font-size:22px;background: #abd037;'> O </div> <div style='background: #abd037;;font-weight: bold;font-size: 18px;'> " + AppDetails.AppName + "</div><div style='font-size: 15px;background: #abd037;'> Point Id: " + ReferanceId + " </div><div style='height:10px;background: #abd037;'></div> <div style='height:10px;background: #fff;'></div><div style='background: #fff;'> <img style='width:250px;height:250px;' src='" + string.Format("https://api.qrserver.com/v1/create-qr-code/?data=" + ReferanceId) + "'/>  </div></div>";
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




        public void test(GarbagePointDetailsVM point)
        {

            var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
            var guid = Guid.NewGuid().ToString().Split('-');
            string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

            //Converting  Url to image 
            var url = string.Format("http://api.qrserver.com/v1/create-qr-code/?data=" + point.ReferanceId);
            WebResponse response = default(WebResponse);
            Stream remoteStream = default(Stream);
            StreamReader readStream = default(StreamReader);
            WebRequest request = WebRequest.Create(url);
            response = request.GetResponse();
            remoteStream = response.GetResponseStream();
            readStream = new StreamReader(remoteStream);
            //Creating Path to save image in folder
            System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
            string imgpath = Path.Combine(Server.MapPath(AppDetails.basePath + AppDetails.PointQRCode), image_Guid);
            var exists = System.IO.Directory.Exists(Server.MapPath(AppDetails.basePath + AppDetails.PointQRCode));
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + AppDetails.PointQRCode));
            }
            img.Save(imgpath);
            response.Close();
            remoteStream.Close();
            readStream.Close();
            point.qrCode = image_Guid;

            GarbagePointDetailsVM pointDetails = childRepository.SaveGarbagePoint(point);

            // generate pdf
            string Filename = "";


            string cdatetime = DateTime.Now.ToString("_ddmmyyyyhhmmss");

            Filename = point.ReferanceId + "(" + cdatetime + ").pdf";

            string GridHtml = "<div><center><p style='margin-top:0px'><img style='width:200px;height200px' src='http://sbaappynitty.co.in:4022/Content/images/img/app_icon_cms.png'/></p><p style='font-size:25px;font-weight:bold;margin:0px'>Ghanta Gadi</p><p style='font-size:25px;font-weight:bold;margin:0px'>" + AppDetails.AppName + "</p><br/><hr/><br/></center></div><div class='modal-body'>< center >< br/><h1 style='font-size:32px;'>< b id = 'hide_Name' > _ _ _ _ _ _ _ _ _ _</b ></h1 ><h3> Garbage Point Id :  " + point.ReferanceId + "</h3 ><h3> Scan Scaniffy Code <br /><br/>< img class='img-responsive' id='imggg' alt='hoto Not Found'  src='" + string.Format("http://api.qrserver.com/v1/create-qr-code/?data=" + point.ReferanceId) + "'/></h3><br /></center><div class='modal-footer'></div></div>";

            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                //string path = Path.GetDirectoryName(Application.ExecutablePath);

                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                var path = Path.Combine(Server.MapPath("~/" + AppDetails.basePath + AppDetails.PointQRCode + "/bundel"), Filename);
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

        public ActionResult SaveBundel(ZoneVM zone)
        {

            if (SessionHandler.Current.AppId != 0)
            {
                GarbagePointDetailsVM house = childRepository.GetGarbagePointById(-1);
                List<string> list = new List<string>();
                int n = house.gpId;
                for (int i = 1; i <= zone.id; i++)
                {
                    int number = 1000;
                    string refer = "GPSBA" + (number + n + i);
                    house.ReferanceId = refer;

                    house.gpId = 0;
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
                    string imgpath = Path.Combine(Server.MapPath(AppDetails.basePath + AppDetails.PointQRCode), image_Guid);
                    var exists = System.IO.Directory.Exists(Server.MapPath(AppDetails.basePath + AppDetails.PointQRCode));
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + AppDetails.PointQRCode));
                    }
                    img.Save(imgpath);
                    response.Close();
                    remoteStream.Close();
                    readStream.Close();
                    house.qrCode = image_Guid;
                    house.ReferanceId = house.ReferanceId;

                    GarbagePointDetailsVM pointDetails = childRepository.SaveGarbagePoint(house);

                    // generate pdf
                    string Filename = "";

                    string cdatetime = DateTime.Now.ToString("_ddmmyyyyhhmmss");

                    Filename = house.ReferanceId + "(" + cdatetime + ").pdf";
                    string src = AppDetails.baseImageUrlCMS + "/Content/images/icons/middle_size_3.png";

                    // For Satana Only
                   // string GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;padding-top: 5px;background: #abd037;'> <img style='width:180px;height:78px;' src='" + src + "'/> </div> <div style='font-size: 14px;background: #abd037;'> Point Id: " + house.ReferanceId + " </div> <div style='height:10px;background: #fff;'></div><div style='background: #fff;'> <img style='width:250px;height:250px;' src='" + imgpath + "'/> </div></div>";


                    // Main Design
                    string GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;margin-top: 8px;font-size:22px;background: #abd037;'> O </div> <div style='background: #abd037;;font-weight: bold;font-size: 18px;'> " + AppDetails.AppName + "</div><div style='font-size: 15px;background: #abd037;'> House Id: " + house.ReferanceId + " </div><div style='height:10px;background: #abd037;'></div> <div style='height:10px;background: #fff;'></div><div style='background: #fff;'> <img style='width:250px;height:250px;' src='" + imgpath + "'/></div></div>";




                    using (MemoryStream stream = new System.IO.MemoryStream())
                    {
                        //string path = Path.GetDirectoryName(Application.ExecutablePath);
                        StringReader sr = new StringReader(GridHtml);
                        var pgSize = new iTextSharp.text.Rectangle(216, 288);
                        Document pdfDoc = new Document(pgSize, 1f, 1f, 1f, 1f);

                        var path = Path.Combine(Server.MapPath("~/" + AppDetails.basePath + AppDetails.PointQRCode + "/bundel"), Filename);
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


        public ActionResult LoadWardNoList(int ZoneId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                GarbagePointDetailsVM obj = new GarbagePointDetailsVM();
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
                GarbagePointDetailsVM obj = new GarbagePointDetailsVM();
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



    }
}
