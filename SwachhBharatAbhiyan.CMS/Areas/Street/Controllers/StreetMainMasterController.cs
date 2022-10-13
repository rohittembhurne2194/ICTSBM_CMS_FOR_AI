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
namespace SwachhBharatAbhiyan.CMS.Areas.Street.Controllers
{
    public class StreetMainMasterController : Controller
    {
        // GET: AdsClassification
        IMainRepository mainRepository;
        IChildRepository childRepository;
        public StreetMainMasterController()
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

        [HttpPost]
        public ActionResult AddStateDetails(AppStateVM state)
        {
            if (state.stateId <= 0)
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
                AppTalukaVM taluka = mainRepository.GetTalukaById(teamId, "");
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
                AppTalukaVM taluka = mainRepository.GetTalukaById(0, talukaName);

                if (taluka.talukaName != null)
                { return Json(false, JsonRequestBehavior.AllowGet); }
                else
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
                AreaVM vehicle = childRepository.GetArea(teamId, "");
                return View(vehicle);
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult CheckAreaDetails(AreaVM obj)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                string area1 = "";

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
                    else if (area1 == area.NameMar & area.Id != obj.Id)
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
                childRepository.StreetSaveArea(area);
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
                WardNumberVM vehicle = childRepository.GetWardNumber(teamId, "");
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
                childRepository.StreetSaveWardNumber(area);
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
                ZoneVM zone = childRepository.GetZone(teamId);
                obj.SSzoneId = zone.id;
                obj.SSname = zone.name;

                return View(obj);
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult AddZoneDetails(ZoneMasterVM a)
        {
            ZoneVM area = new ZoneVM();
            area.id = a.SSzoneId;
            area.name = a.SSname;
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository.SaveZone(area);
                return Redirect("ZoneIndex");
            }
            else
                return Redirect("/Account/Login");

        }
        [HttpGet]
        public JsonResult IsNameExist(string name, int zoneId)
        {
            var validateName = childRepository.GetValidZone(name, zoneId);
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
                if (obj.zoneId > 0)
                {
                    if (area.name == obj.name & area.id != obj.zoneId)
                    { return Json(false, JsonRequestBehavior.AllowGet); }
                    else
                        return Json(true, JsonRequestBehavior.AllowGet);
                }

                else
                   if (area.name != null)
                { return Json(false, JsonRequestBehavior.AllowGet); }
                else
                    return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(true, JsonRequestBehavior.AllowGet);
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

    }
}