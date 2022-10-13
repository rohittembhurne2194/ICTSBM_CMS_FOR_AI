using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.ViewModels.MainModel;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SwachBharat.CMS.Dal.DataContexts;
using SwachhBharatAbhiyan.CMS.Models;
using SwachBharat.CMS.Bll.ViewModels.Grid;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace SwachhBharatAbhiyan.CMS.Controllers
{
   
    public class MainMasterController : Controller
    {
        // GET: AdsClassification
        IMainRepository mainRepository;
        IChildRepository childRepository;
        public MainMasterController()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                mainRepository = new MainRepository();
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
            }
            else
                Redirect("/Account/Login");
        }

        #region State
        [HttpGet]
      
        public ActionResult StateIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MenuStateIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        [HttpGet]
        public ActionResult AddStateDetails(int teamId = -1)
        {
             if (SessionHandler.Current.AppId != 0)
             { 
                AppStateVM state = mainRepository.GetStateById(teamId); 
                return View(state);
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost ]
        public ActionResult AddStateDetails(AppStateVM state)
        {
            if (state.stateId<=0)
            {
                state.stateId = 0;
            }
            if (SessionHandler.Current.AppId != 0)
            { 
                mainRepository.SaveState(state); 
                return Redirect("StateIndex");
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpGet]
     
        public ActionResult DeleteState(int teamId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                mainRepository.DeleteState(teamId); 
                return Redirect("StateIndex");
            }
            else
                return Redirect("/Account/Login");
        }
        #endregion

        #region District


        [HttpGet]
        public ActionResult DistrictIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

       
        public ActionResult MenuDistrictIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult AddDistrictDetails(int teamId = -1)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                AppDistrictVM district = mainRepository.GetDistrictById(teamId);
                return View(district);
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult AddDistrictDetails(AppDistrictVM district)
        {
            if (district.districtId <= 0)
            {
                district.districtId = 0;
            }
            if (SessionHandler.Current.AppId != 0)
            { 
                mainRepository.SaveDistrict(district); 
                return Redirect("DistrictIndex");
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpGet]
        public ActionResult DeleteDistrict(int teamId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                mainRepository.DeleteDistrict(teamId); 
                return Redirect("DistrictIndex");
            }
            else
                return Redirect("/Account/Login");
        }
        #endregion
         
        #region Taluka
        [HttpGet] 
        public ActionResult TalukaIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MenuTalukaIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

      
        public ActionResult AddTalukaDetails(int teamId = -1)
        {
           if (SessionHandler.Current.AppId != 0)
            {
                AppTalukaVM taluka = mainRepository.GetTalukaById(teamId,"");
                return View(taluka);
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult CheckTalukaDetails(string talukaName)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                AppTalukaVM taluka = mainRepository.GetTalukaById(0,talukaName);
           
                if(taluka.talukaName!=null)
                    { return Json(false, JsonRequestBehavior.AllowGet); }else
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult AddTalukaDetails(AppTalukaVM taluka)
        {
            if (taluka.districtId <= 0)
            {
                taluka.districtId = 0;
            }
            if (SessionHandler.Current.AppId != 0)
            { 
                mainRepository.SaveTaluka(taluka); 
                return Redirect("TalukaIndex");
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpGet] 
        public ActionResult DeleteTaluka(int teamId)
        {
             if (SessionHandler.Current.AppId != 0)
                {
                    mainRepository.DeleteTaluka(teamId); 
                return Redirect("TalukaIndex");
            }
            else
                return Redirect("/Account/Login");
        }
        #endregion

        #region Vehicle Type
        [HttpGet]
        public ActionResult VehicleIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuVehicleIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult AddVehicleDetails(int teamId = -1)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                VehicleTypeVM vehicle = childRepository.GetVehicleType(teamId);
                return View(vehicle);
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult AddVehicleDetails(VehicleTypeVM vehicle)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                if (vehicle.Id <= 0)
                {
                    vehicle.Id = 0;
                }
                childRepository.SaveVehicleType(vehicle);
                return Redirect("VehicleIndex");
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpGet]
        public ActionResult DeleteVehicle(int teamId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository.DeletVehicleType(teamId); 
                return Redirect("VehicleIndex");
            }
            else
                return Redirect("/Account/Login");
        }


        [HttpPost]
        public ActionResult CheckVehcileDetails(WardNumberVM obj)
        {
            if (SessionHandler.Current.AppId != 0)
            {

                WardNumberVM area = childRepository.GetWardNumber(0, obj.WardNo);


                if (obj.Id > 0)
                {
                    if (area.WardNo == obj.WardNo & area.Id != obj.Id)
                    { return Json(false, JsonRequestBehavior.AllowGet); }
                    else
                        return Json(true, JsonRequestBehavior.AllowGet);
                }

                else
                      if (area.WardNo != null)
                { return Json(false, JsonRequestBehavior.AllowGet); }
                else
                    return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(true, JsonRequestBehavior.AllowGet);



        }

        public ActionResult VehicalRegistrationIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                Session["AppName"] = SessionHandler.Current.AppName;
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuVehicalRegistrationIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult AddVehicalRegDetails(int teamId = -2)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                VehicalRegDetailsVM house = childRepository.GetVehicalRegById(teamId);


                return View(house);


            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult AddVehicalRegDetails(VehicalRegDetailsVM house)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                int teamId = house.vqrId;
                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
                var VehicalReg = childRepository.GetVehicalRegById(teamId);
                if (VehicalReg.vehicalQRCode == "/Images/QRcode.png" || VehicalReg.vehicalQRCode == "/Images/default_not_upload.png")
                {
                    VehicalReg.vehicalQRCode = null;
                }
                if (VehicalReg.vehicalQRCode == null)
                {
                    var guid = Guid.NewGuid().ToString().Split('-');
                    string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

                    //Converting  Url to image 
                    // var url = string.Format("http://api.qrserver.com/v1/create-qr-code/?data="+ house.ReferanceId);
                    var url = string.Format("https://chart.googleapis.com/chart?cht=qr&chl=" + VehicalReg.ReferanceId + "&chs=160x160&chld=L|0");
                    WebResponse response = default(WebResponse);
                    Stream remoteStream = default(Stream);
                    StreamReader readStream = default(StreamReader);
                    WebRequest request = WebRequest.Create(url);
                    response = request.GetResponse();
                    remoteStream = response.GetResponseStream();
                    readStream = new StreamReader(remoteStream);
                    //Creating Path to save image in folder
                    System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
                    string imgpath = Path.Combine(Server.MapPath(AppDetails.basePath + AppDetails.VehicalQRCode), image_Guid);
                    var exists = System.IO.Directory.Exists(Server.MapPath(AppDetails.basePath + AppDetails.VehicalQRCode));
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + AppDetails.VehicalQRCode));
                    }
                    img.Save(imgpath);
                    response.Close();
                    remoteStream.Close();
                    readStream.Close();
                    house.vehicalQRCode = image_Guid;
                }
                else
                {

                    string bb = VehicalReg.vehicalQRCode;
                    var ii = bb.Split('/');
                    if (ii.Length == 6)
                    {
                        VehicalReg.vehicalQRCode = ii[6];
                    }
                    if (ii.Length > 6)
                    {
                        VehicalReg.vehicalQRCode = ii[6];
                        house.vehicalQRCode = VehicalReg.vehicalQRCode;
                    }
                }
                VehicalRegDetailsVM vehicalDetails = childRepository.SaveVehicalReg(house);
                return Redirect("VehicalRegistrationIndex");
            }
            else
                return Redirect("/Account/Login");
        }

        #endregion

        #region Vehicle Registration
        [HttpGet]
        public ActionResult VehicleRegIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuVehicleRegIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult AddVehicleRegDetails(int teamId = -1)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                VehicleRegVM vehicle = childRepository.GetVehicleReg(teamId);
                return View(vehicle);
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult AddVehicleRegDetails(VehicleRegVM vehicle)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                if (vehicle.vehicleId <= 0)
                {
                    vehicle.vehicleId = 0;
                }
                childRepository.SaveVehicleReg(vehicle);
                return Redirect("VehicleRegIndex");
            }
            else
                return Redirect("/Account/Login");
        }



        #endregion

        #region Area
        [HttpGet]
        public ActionResult AreaIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuAreaIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult AddAreaDetails(int teamId = -1)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                AreaVM vehicle = childRepository.GetArea(teamId,"");
                return View(vehicle);
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpGet]
        public ActionResult CheckAreaDetails(AreaVM obj)
        {
            if (SessionHandler.Current.AppId != 0)
            { string area1 = "";

                if (obj.Name != null)
                {
                    area1 = obj.Name;
                }
                if (obj.NameMar != null)
                {
                    area1 = obj.NameMar;

                }
                AreaVM area = childRepository.GetArea(0, area1);

               
               

            if (obj.Id > 0)
            {
                    if (area.Id == 0)
                    {
                        area.Id = obj.Id;
                    }
                if (area1 == obj.Name & area.Id != obj.Id)
                { return Json(false, JsonRequestBehavior.AllowGet); }
                   else if (area1 ==area.NameMar & area.Id != obj.Id)
                    { return Json(false, JsonRequestBehavior.AllowGet); }
                    else
                    return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
                 if (area.Name != null)
            { return Json(false, JsonRequestBehavior.AllowGet); }
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }
            else
                return Json(true, JsonRequestBehavior.AllowGet);


    }


        [HttpPost]
        public string CheckAreaName(string Name)
        {
            int Appid = SessionHandler.Current.AppId;
            using (DevChildSwachhBharatNagpurEntities db = new DevChildSwachhBharatNagpurEntities(Appid))
            {
                //check if any of the UserName matches the UserName specified in the Parameter using the ANY extension method.  

                var isrecord = db.TeritoryMasters.Where(x => x.Area == Name).FirstOrDefault();
                if (isrecord != null)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }

            }
        }

        [HttpPost]
        public ActionResult AddAreaDetails(AreaVM area)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository.SaveArea(area);
                return Redirect("AreaIndex");
            }
            else
                return Redirect("/Account/Login");
           
        }

        [HttpGet]
        public ActionResult DeleteArea(int teamId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository.DeletArea(teamId);
            return Redirect("AreaIndex");
            }
            else
                return Redirect("/Account/Login");
        }
        #endregion
        
        #region Ward Number
        [HttpGet]
        public ActionResult WardIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuWardIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult AddWardDetails(int teamId = -1)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                WardNumberVM vehicle = childRepository.GetWardNumber(teamId,"");
                return View(vehicle);
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult AddWardDetails(WardNumberVM area)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository.SaveWardNumber(area);
                return Redirect("WardIndex");
            }
            else
                return Redirect("/Account/Login");

        }

        [HttpGet]
        public ActionResult DeleteWard(int teamId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository.DeleteWardNumber(teamId);
                return Redirect("WardIndex");
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult CheckWardDetails(WardNumberVM obj)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                      
             WardNumberVM area = childRepository.GetWardNumber(0, obj.WardNo);


            if (obj.Id > 0)
            {
                if (area.WardNo == obj.WardNo & area.Id != obj.Id)
                { return Json(false, JsonRequestBehavior.AllowGet); }
                else
                    return Json(true, JsonRequestBehavior.AllowGet);
            }

            else
                  if (area.WardNo != null)
            { return Json(false, JsonRequestBehavior.AllowGet); }
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }
            else
                return Json(true, JsonRequestBehavior.AllowGet);



    }


        [HttpPost]
        public string CheckWardName(string Name)
        {
            int Appid = SessionHandler.Current.AppId;
            using (DevChildSwachhBharatNagpurEntities db = new DevChildSwachhBharatNagpurEntities(Appid))
            {
                //check if any of the UserName matches the UserName specified in the Parameter using the ANY extension method.  

                var isrecord = db.WardNumbers.Where(x => x.WardNo == Name).FirstOrDefault();
                if (isrecord != null)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }

            }
        }


        #endregion

        #region Zone
        [HttpGet]
        public ActionResult ZoneIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuZoneIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult AddZoneDetails(int teamId = -1)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                ZoneMasterVM obj = new ZoneMasterVM();
               ZoneVM  zone =childRepository.GetZone(teamId);
                obj.zoneId=zone.id;
                obj.name=zone.name;

                return View(obj);
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult AddZoneDetails(ZoneMasterVM a)
        {
            ZoneVM area = new ZoneVM();
            area.id = a.zoneId;
            area.name = a.name;
            if (SessionHandler.Current.AppId != 0)
              {
                childRepository.SaveZone(area);
                return Redirect("ZoneIndex");
            }
            else
                return Redirect("/Account/Login");

        }
        [HttpGet]
        public JsonResult IsNameExist(string name,int zoneId)
        {
            var validateName = childRepository.GetValidZone(name,zoneId);
            if (validateName != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult CheckZoneDetails(ZoneMasterVM obj)
        {
            if (SessionHandler.Current.AppId != 0)
            {

               ZoneVM area = childRepository.GetValidZone(obj.name, 0);
                if(obj.zoneId>0)
                {
                    if (area.name == obj.name & area.id != obj.zoneId)
                    { return Json(false, JsonRequestBehavior.AllowGet); }
                    else
                        return Json(true, JsonRequestBehavior.AllowGet);
                }

                else
                   if (area.name !=null)
                { return Json(false, JsonRequestBehavior.AllowGet); }
                   else
                    return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string CheckZoneName(string Name)
        {
            int Appid = SessionHandler.Current.AppId;
            using (DevChildSwachhBharatNagpurEntities db = new DevChildSwachhBharatNagpurEntities(Appid))
            {
                //check if any of the UserName matches the UserName specified in the Parameter using the ANY extension method.  

                var isrecord = db.ZoneMasters.Where(x => x.name == Name).FirstOrDefault();
                if (isrecord != null)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }

            }
        }
        #endregion

        #region AttendenceSettings
        [HttpGet]
        public ActionResult AttendenceSettings()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult AttendenceSettingsDetail()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult EditAttendenceSettingsDetail(int teamId = -1)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                SBAAttendenceSettingsGridRow house = childRepository.GetAttendenceEmployeeById(teamId);
                return View(house);
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult EditAttendenceSettingsDetail(SBAAttendenceSettingsGridRow emp)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
               

                childRepository.SaveAttendenceSettingsDetail(emp);
                return Redirect("Index");
            }
            else
                return Redirect("/Account/Login");
        }


        #endregion

        public ActionResult Export(int id)
        {
            if (SessionHandler.Current.AppId != 0)
            {

                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
                string Filename = "", owner = "";

                var details = childRepository.GetVehicalRegById(id);
                string cdatetime = DateTime.Now.ToString("_ddmmyyyyhhmmss");

                if (details.Vehican_No != null)
                {
                    Filename = Regex.Replace(details.Vehican_No, @"\s+", "") + cdatetime + ".pdf";
                    owner = details.Vehican_No;
                }
                else
                {
                    Filename = Regex.Replace(details.vqrId.ToString(), @"\s+", "") + cdatetime + ".pdf";
                    owner = "_ _ _ _ _ _ _ _ _ _ _ _";

                }
                string GridHtml = "";
                //string src = AppDetails.baseImageUrlCMS + "/Content/images/img/app_icon_cms.png";
                //string GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;margin-top: 8px;font-size:22px;background: #abd037;'> O </div> <div style='background: #abd037;;font-weight: bold;font-size: 18px;'> " + AppDetails.AppName + "</div><div style='font-size: 15px;background: #abd037;'> House Id: " + details.ReferanceId + " </div><div style='height:10px;background: #abd037;'></div> <div style='height:10px;background: #fff;'></div><div style='background: #fff;'> <img style='width:250px;height:250px;' src='" + details.houseQRCode + "'/> </div></div>";


                if (SessionHandler.Current.AppId == 3068) // For Nagpur ULB
                {
                    string src = AppDetails.baseImageUrlCMS + "/Content/images/icons/Nagpur_logo.png";
                    //For Satana Only
                    GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;padding-top: 5px;background: #abd037;'> <img style='width:250px;height:86px;' src='" + src + "'/> </div> <div style='font-size: 14px;background: #abd037;'><b> Vehical QR Id: " + details.ReferanceId + "</b> </div> <div style='height:10px;background: #fff;'></div><div style='background: #fff;'> <img style='width:245px;height:245px;' src='" + details.vehicalQRCode + "'/></div></div>";
                }
                else
                {
                    //string slogan = AppDetails.baseImageUrlCMS + "/Content/images/icons/slogan.png";
                    //string topimg = AppDetails.baseImageUrlCMS + "/content/images/icons/top_outdoor.png";
                    //string leftbottomimg = AppDetails.baseImageUrlCMS + "/Content/images/icons/left_outdoor.png";
                    //string dry_wet_new = AppDetails.baseImageUrlCMS + "/Content/images/icons/Khapa/dry&wet.png";

                    string top_img_new = AppDetails.baseImageUrlCMS + AppDetails.basePath + "Content/icons/Top_image.png";
                    string slogan_new = AppDetails.baseImageUrlCMS + AppDetails.basePath + "Content/icons/slogan.png";
                    string round = AppDetails.baseImageUrlCMS + AppDetails.basePath + "Content/icons/round.png";

                    //string top_img_new = "http://localhost:34557/" + AppDetails.basePath + "Content/icons/Top_image.png";
                    //string slogan_new = "http://localhost:34557/" + AppDetails.basePath + "Content/icons/slogan.png";
                    //string round = "http://localhost:34557/" + AppDetails.basePath + "Content/icons/round.png";

                    GridHtml = "<div style='width:100%;height: 100%;background:#ffffff;border : 2px solid #4fa30a;'><div style='float:left;width:7%;padding-top:110px;padding-left:8px;'><img src='" + round + "' style = 'width:20px;height:20px;margin-left:5px;'/></div><div style='float:left;width:58%;padding-left:16px;padding-top:7px;'><img src='" + details.vehicalQRCode + "' style = 'width:20px;height:20px;'/></div><div style='float:left;width:83%;padding-left:5px;padding-top:10px;padding-bottom:6px;'><div style='padding-left:5px;'><img style='width:150px;height:95px;' src='" + top_img_new + "'/></div><div style='text-align: center;font-weight: 900;padding-bottom:3px;'>&nbsp;&nbsp;&nbsp;<span style='color:#000000;text-align: center;font-size: 16px'>Vehical QR Id</span><br/><span style='color:#000000;text-align: center;font-size: 21px'>" + details.ReferanceId + "</span></div><div style='padding-left:5px;'><img src='" + slogan_new + "' style='width: 150px; height:49px;'/><br/><div style='float:right;'><b style='font-size:9px;'>" + details.SerielNo + "</b></div></div></div><div style='float:left;width:3%;padding-top:110px;padding-left:22px;text-align:center;'><img src='" + round + "' style = 'width:20px;height:20px;'/></div></div>";
                }


                using (MemoryStream stream = new System.IO.MemoryStream())
                {
                    StringReader sr = new StringReader(GridHtml);

                    if (SessionHandler.Current.AppId == 3068)
                    {
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
                    }
                    else
                    {
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
                    }

                    return File(stream.ToArray(), "application/pdf", Filename);
                }
            }
            else
                return Redirect("/Account/Login");
        }


        #region HouseQRBunch
        [HttpGet]
        public ActionResult HouseQRBunchIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuHouseQRBunchIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult AddHouseQRBunchDetails(int teamId = -1)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                HouseQRBunchVM HouseBunch = childRepository.GetHouseBunch(teamId);
                return View(HouseBunch);
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult AddHouseQRBunchDetails(HouseQRBunchVM housebunch)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository.SaveHouseBunch(housebunch);
                return Redirect("HouseQRBunchIndex");
            }
            else
                return Redirect("/Account/Login");

        }

        [HttpPost]
        public string CheckHouseBunchName(string Name)
        {
            int Appid = SessionHandler.Current.AppId;
            using (DevChildSwachhBharatNagpurEntities db = new DevChildSwachhBharatNagpurEntities(Appid))
            {
                //check if any of the UserName matches the UserName specified in the Parameter using the ANY extension method.  

                var isrecord = db.HouseBunches.Where(x => x.bunchname == Name).FirstOrDefault();
                if (isrecord != null)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }

            }
        }
        #endregion HouseQRBunch




    }
}
