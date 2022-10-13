using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel;
using System.Net.Mail;
using System.Threading;
using SwachBharat.CMS.Dal.DataContexts;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachBharat.CMS.Bll.ViewModels.SS2020Reports;
using System.Xml;
using SwachBharat.CMS.Bll.ViewModels.Grid;
using Newtonsoft.Json;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;
using System.Globalization;
using System.Web;
using Microsoft.SqlServer.Server;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using System.Data;
using System.Net.Http;
using System.Xml.Linq;

namespace SwachBharat.CMS.Bll.Services
{
    public class ScreenService : AppService, IScreenService
    {
        private int AppID;
        public ScreenService(int AppId) : base(AppId)
        {
            AppID = AppId;
        }

        #region CheckForNull
        public int checkIntNull(string str)
        {
            int result = 0;
            if (str == null || str == "")
            {
                result = 0;
                return result;
            }
            else
            {
                result = Convert.ToInt32(str);
                return result;
            }
        }
        public string checkNull(string str)
        {
            string result = "";
            if (str == null || str == "")
            {
                result = "";
                return result;
            }
            else
            {
                result = str;
                return result;
            }
        }

        #endregion
        #region Dashboard
        public DashBoardVM GetDashBoardDetails()
        {
            DashBoardVM model = new DashBoardVM();
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {

                    DevSwachhBharatMainEntities dbm = new DevSwachhBharatMainEntities();
                    var appdetails = dbm.AppDetails.Where(c => c.AppId == AppID).FirstOrDefault();
                    List<ComplaintVM> obj = new List<ComplaintVM>();
                    //if (AppID==1)
                    //{
                    //    string json = new WebClient().DownloadString(appdetails.Grampanchayat_Pro + "/api/Get/Complaint?appId=1");
                    //     obj = JsonConvert.DeserializeObject<List<ComplaintVM>>(json).Where(c => Convert.ToDateTime(c.createdDate2).ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")).ToList();
                    //}
                    if (appdetails.GramPanchyatAppID != null)
                    {
                        string json = new WebClient().DownloadString(appdetails.Grampanchayat_Pro + "/api/Get/Complaint?appId=" + appdetails.GramPanchyatAppID);
                        obj = JsonConvert.DeserializeObject<List<ComplaintVM>>(json).Where(c => Convert.ToDateTime(c.createdDate2).ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")).ToList();
                    }
                    var data = db.SP_Dashboard_Details().First();

                    var date = DateTime.Today;
                    var Newdate = DateTime.Now.ToString("yyyy-MM-dd");
                    var houseCount = db.SP_TotalHouseCollection_Count(date, AppID).FirstOrDefault();
                    if (data != null)
                    {

                        model.TodayAttandence = data.TodayAttandence;
                        model.TotalAttandence = data.TotalAttandence;
                        model.HouseCollection = data.TotalHouse;
                        model.PointCollection = data.TotalPoint;
                        model.TotalComplaint = obj.Count();
                        model.TotalHouseCount = houseCount.TotalHouseCount;
                        model.MixedCount = houseCount.MixedCount;
                        model.BifurgatedCount = houseCount.BifurgatedCount;
                        model.NotCollected = houseCount.NotCollected;
                        model.DumpYardCount = data.TotalDump;
                        model.NotSpecified = houseCount.NotSpecified;
                        //model.TotalGcWeightCount = houseCount.TotalGcWeightCount;
                        //model.GcWeightCount = Convert.ToDouble(string.Format("{0:0.00}", houseCount.GcWeightCount));
                        //model.DryWeightCount =Convert.ToDouble(string.Format("{0:0.00}", houseCount.DryWeightCount));
                        //model.WetWeightCount =Convert.ToDouble(string.Format("{0:0.00}", houseCount.WetWeightCount));

                        model.GcWeightCount = Convert.ToDouble(houseCount.GcWeightCount);
                        model.DryWeightCount = Convert.ToDouble(houseCount.DryWeightCount);
                        model.WetWeightCount = Convert.ToDouble(houseCount.WetWeightCount);
                        model.TotalGcWeightCount = Convert.ToDouble(houseCount.TotalGcWeightCount);
                        model.TotalDryWeightCount = Convert.ToDouble(houseCount.TotalDryWeightCount);
                        model.TotalWetWeightCount = Convert.ToDouble(houseCount.TotalWetWeightCount);

                        model.SV_GcWeightCount = Convert.ToDouble(houseCount.SV_GcWeightCount);
                        model.SV_DryWeightCount = Convert.ToDouble(houseCount.SV_DryWeightCount);
                        model.SV_WetWeightCount = Convert.ToDouble(houseCount.SV_WetWeightCount);
                        model.SV_TotalGcWeightCount = Convert.ToDouble(houseCount.SV_TotalGcWeightCount);
                        model.SV_TotalDryWeightCount = Convert.ToDouble(houseCount.SV_TotalDryWeightCount);
                        model.SV_TotalWetWeightCount = Convert.ToDouble(houseCount.SV_TotalWetWeightCount);

                        model.TotalHousePropertyCount = Convert.ToInt32(houseCount.TotalHousePropertyCount);
                        model.TotalDumpPropertyCount = Convert.ToInt32(houseCount.TotalDumpPropertyCount);

                        //For Liquid Waste

                        model.LWGcWeightCount = Convert.ToDouble(houseCount.LWGcWeightCount);
                        model.LWDryWeightCount = Convert.ToDouble(houseCount.LWDryWeightCount);
                        model.LWWetWeightCount = Convert.ToDouble(houseCount.LWWetWeightCount);
                        model.LWTotalGcWeightCount = Convert.ToDouble(houseCount.LWTotalGcWeightCount);
                        model.LWTotalDryWeightCount = Convert.ToDouble(houseCount.LWTotalDryWeightCount);
                        model.LWTotalWetWeightCount = Convert.ToDouble(houseCount.LWTotalWetWeightCount);

                        //For Street Sweeping
                        model.SSGcWeightCount = Convert.ToDouble(houseCount.SSGcWeightCount);
                        model.SSDryWeightCount = Convert.ToDouble(houseCount.SSDryWeightCount);
                        model.SSWetWeightCount = Convert.ToDouble(houseCount.SSWetWeightCount);
                        model.SSTotalGcWeightCount = Convert.ToDouble(houseCount.SSTotalGcWeightCount);
                        model.SSTotalDryWeightCount = Convert.ToDouble(houseCount.SSTotalDryWeightCount);
                        model.SSTotalWetWeightCount = Convert.ToDouble(houseCount.SSTotalWetWeightCount);

                        // For Mangalwedha ULB
                        var D1 = db.GarbageCollectionDetails.Where(a => a.dyId == 1 && EntityFunctions.TruncateTime(a.gcDate) == EntityFunctions.TruncateTime(date)).Select(a => a.totalDryWeight).Sum();
                        var D2 = db.GarbageCollectionDetails.Where(a => a.dyId == 2 && EntityFunctions.TruncateTime(a.gcDate) == EntityFunctions.TruncateTime(date)).Select(a => a.totalDryWeight).Sum();
                        var D3 = db.GarbageCollectionDetails.Where(a => a.dyId == 3 && EntityFunctions.TruncateTime(a.gcDate) == EntityFunctions.TruncateTime(date)).Select(a => a.totalDryWeight).Sum();
                        var D4 = db.GarbageCollectionDetails.Where(a => a.dyId == 4 && EntityFunctions.TruncateTime(a.gcDate) == EntityFunctions.TruncateTime(date)).Select(a => a.totalDryWeight).Sum();
                        var D5 = db.GarbageCollectionDetails.Where(a => a.dyId == 5 && EntityFunctions.TruncateTime(a.gcDate) == EntityFunctions.TruncateTime(date)).Select(a => a.totalDryWeight).Sum();
                        var D6 = db.GarbageCollectionDetails.Where(a => a.dyId == 6 && EntityFunctions.TruncateTime(a.gcDate) == EntityFunctions.TruncateTime(date)).Select(a => a.totalDryWeight).Sum();
                        var TotalD = db.GarbageCollectionDetails.Where(a => EntityFunctions.TruncateTime(a.gcDate) == EntityFunctions.TruncateTime(date)).Select(a => a.totalGcWeight).Sum();

                        model.DumpYardDryCount = Convert.ToDouble(D1);
                        model.DumpYardWetCount = Convert.ToDouble(D2);
                        model.DumpYardConstructionCount = Convert.ToDouble(D3);
                        model.DumpYardFSTPCount = Convert.ToDouble(D4);
                        model.DumpYardDomesticCount = Convert.ToDouble(D5);
                        model.DumpYardSanitaryCount = Convert.ToDouble(D6);
                        model.DumpYardTotalCount = Convert.ToDouble(TotalD);
                        //End

                        return model;
                    }

                    // String.Format("{0:0.00}", 123.4567); 

                    else
                    {
                        return model;
                    }
                }
            }
            catch (Exception e)
            {
                return model;
            }
        }
        #endregion

        #region Area
        public AreaVM GetAreaDetails(int teamId, string name)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.TeritoryMasters.Where(x => x.Id == teamId || x.Area.ToUpper()
                    == name.ToUpper() || x.AreaMar == name).FirstOrDefault();
                    if (Details != null)
                    {
                        AreaVM area = FillAreaViewModel(Details);
                        area.WardList = ListWardNo();
                        return area;
                    }
                    else
                    {
                        AreaVM area = new AreaVM();
                        area.WardList = ListWardNo();

                        return area;
                    }
                }
            }
            catch (Exception)
            {
                return new AreaVM();
            }
        }
        public void SaveAreaDetails(AreaVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.Id > 0)
                    {
                        var model = db.TeritoryMasters.Where(x => x.Id == data.Id).FirstOrDefault();
                        if (model != null)
                        {
                            model.Id = data.Id;
                            model.Area = data.Name;
                            model.AreaMar = data.NameMar;
                            model.wardId = data.wardId;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var area = FillAreaDataModel(data);
                        if (area.Area != null && area.wardId != null)
                        {
                            db.TeritoryMasters.Add(area);
                            db.SaveChanges();
                        }

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LiquidSaveAreaDetails(AreaVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.LWId > 0)
                    {
                        var model = db.TeritoryMasters.Where(x => x.Id == data.LWId).FirstOrDefault();
                        if (model != null)
                        {
                            model.Id = data.LWId;
                            model.Area = data.LWName;
                            model.AreaMar = data.LWNameMar;
                            model.wardId = data.LWwardId;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var area = LiquidFillAreaDataModel(data);
                        db.TeritoryMasters.Add(area);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void StreetSaveAreaDetails(AreaVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.SSId > 0)
                    {
                        var model = db.TeritoryMasters.Where(x => x.Id == data.SSId).FirstOrDefault();
                        if (model != null)
                        {
                            model.Id = data.SSId;
                            model.Area = data.SSName;
                            model.AreaMar = data.SSNameMar;
                            model.wardId = data.SSwardId;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var area = StreetFillAreaDataModel(data);
                        db.TeritoryMasters.Add(area);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeletAreaDetails(int teamId)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.TeritoryMasters.Where(x => x.Id == teamId).FirstOrDefault();
                    if (Details != null)
                    {
                        db.TeritoryMasters.Remove(Details);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Vehicle


        public VehicleTypeVM GetVehicleTypeDetails(int teamId)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.VehicleTypes.Where(x => x.vtId == teamId).FirstOrDefault();
                    if (Details != null)
                    {
                        VehicleTypeVM type = FillVehicleViewModel(Details);
                        return type;
                    }
                    else
                    {
                        return new VehicleTypeVM();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SaveVehicleTypeDetails(VehicleTypeVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.Id > 0)
                    {
                        var model = db.VehicleTypes.Where(x => x.vtId == data.Id).FirstOrDefault();
                        if (model != null)
                        {
                            model.vtId = data.Id;
                            model.description = data.description;
                            model.descriptionMar = data.descriptionMar;
                            model.isActive = data.isActive;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var type = FillVehicleDataModel(data);
                        db.VehicleTypes.Add(type);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeletVehicleTypeDetails(int teamId)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.VehicleTypes.Where(x => x.vtId == teamId).FirstOrDefault();
                    if (Details != null)
                    {
                        db.VehicleTypes.Remove(Details);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Vehicl Registration
        public VehicleRegVM GetVehicleDetails(int teamId)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.VehicleRegistrations.Where(x => x.vehicleId == teamId).FirstOrDefault();
                    if (Details != null)
                    {
                        VehicleRegVM vechile = FillVehicleTegViewModel(Details);
                        vechile.AreaList = ListArea();
                        vechile.VehicleList = ListVehicle();
                        return vechile;
                    }
                    else
                    {
                        VehicleRegVM vechile = new VehicleRegVM();
                        vechile.AreaList = ListArea();
                        vechile.VehicleList = ListVehicle();
                        return vechile;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveVehicleRegDetails(VehicleRegVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.vehicleId > 0)
                    {
                        var model = db.VehicleRegistrations.Where(x => x.vehicleId == data.vehicleId).FirstOrDefault();
                        if (model != null)
                        {
                            model.vehicleId = data.vehicleId;
                            model.vehicleType = data.vehicleType;
                            model.vehicleNo = data.vehicleNumber;
                            model.areaId = data.AreaId;
                            model.isActive = data.isActive;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var type = FillVehicleRegDataModel(data);
                        db.VehicleRegistrations.Add(type);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private VehicleRegistration FillVehicleRegDataModel(VehicleRegVM data)
        {
            VehicleRegistration model = new VehicleRegistration();
            model.vehicleId = data.vehicleId;
            model.vehicleType = data.vehicleType;
            model.vehicleNo = data.vehicleNumber;
            model.areaId = data.AreaId;
            model.isActive = data.isActive;
            return model;
        }
        private VehicleRegVM FillVehicleTegViewModel(VehicleRegistration data)
        {
            VehicleRegVM model = new VehicleRegVM();
            model.vehicleId = data.vehicleId;
            model.vehicleNumber = data.vehicleNo;
            model.vehicleType = data.vehicleType;
            model.AreaId = data.areaId;
            model.isActive = data.isActive;
            return model;
        }
        #endregion
        #region Ward Number
        public WardNumberVM GetWardNumberDetails(int teamId, string name)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.WardNumbers.Where(x => x.Id == teamId || x.WardNo == name).FirstOrDefault();
                    if (Details != null)
                    {
                        WardNumberVM type = FillWardViewModel(Details);
                        type.ZoneList = ListZone();
                        return type;
                    }
                    else
                    {
                        WardNumberVM type = new WardNumberVM();
                        type.ZoneList = ListZone();
                        return type;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SaveWardNumberDetails(WardNumberVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.Id > 0)
                    {
                        var model = db.WardNumbers.Where(x => x.Id == data.Id).FirstOrDefault();
                        if (model != null)
                        {
                            model.Id = data.Id;
                            model.WardNo = data.WardNo;
                            model.zoneId = data.zoneId;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var type = FillWardDataModel(data);
                        db.WardNumbers.Add(type);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveMasterQRDetails(MasterQRDetailsVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.masterId > 0)
                    {
                        var model = db.MasterQRs.Where(x => x.MasterId == data.masterId).FirstOrDefault();
                        if (model != null)
                        {

                            model.MasterId = data.masterId;
                            model.ReferanceId = data.ReferanceId;
                            model.ISActive = data.isActive;
                            string state1 = "";
                            foreach (var s in data.CheckHlist)
                            {
                                if (s.IsCheked == true)
                                {

                                    state1 += s.ReferanceId + ",";

                                }
                            }
                            model.QRList = state1;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var type = FillMasterQRDataModel(data);

                        //arr[CheckAppD] myArray = data.CheckAppDs.ToArray();

                        string state1 = "";
                        foreach (var s in data.CheckHlist)
                        {
                            if (s.IsCheked == true)
                            {

                                state1 += s.ReferanceId + ",";

                            }
                        }

                        type.QRList = state1;

                        db.MasterQRs.Add(type);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }


        }
        public void LiquidSaveWardNumberDetails(WardNumberVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.LWId > 0)
                    {
                        var model = db.WardNumbers.Where(x => x.Id == data.LWId).FirstOrDefault();
                        if (model != null)
                        {
                            model.Id = data.LWId;
                            model.WardNo = data.LWWardNo;
                            model.zoneId = data.LWzoneId;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var type = LiquidFillWardDataModel(data);
                        db.WardNumbers.Add(type);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void StreetSaveWardNumberDetails(WardNumberVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.SSId > 0)
                    {
                        var model = db.WardNumbers.Where(x => x.Id == data.SSId).FirstOrDefault();
                        if (model != null)
                        {
                            model.Id = data.SSId;
                            model.WardNo = data.SSWardNo;
                            model.zoneId = data.SSzoneId;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var type = StreetFillWardDataModel(data);
                        db.WardNumbers.Add(type);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void DeletWardNumberDetails(int teamId)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.WardNumbers.Where(x => x.Id == teamId).FirstOrDefault();
                    if (Details != null)
                    {
                        db.WardNumbers.Remove(Details);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region House Details
        public HouseDetailsVM GetHouseDetails(int teamId)
        {
            try
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();

                string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.HouseQRCode + "/";
                HouseDetailsVM house = new HouseDetailsVM();

                var Details = db.HouseMasters.Where(x => x.houseId == teamId).FirstOrDefault();
                if (Details != null)
                {
                    house = FillHouseDetailsViewModel(Details);
                    if (house.houseQRCode != null && house.houseQRCode != "")
                    {
                        HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(ThumbnaiUrlCMS + house.houseQRCode.Trim());
                        HttpWebResponse httpRes = null;
                        try
                        {
                            httpRes = (HttpWebResponse)httpReq.GetResponse(); // Error 404 right here,
                            if (httpRes.StatusCode == HttpStatusCode.NotFound)
                            {
                                house.houseQRCode = "/Images/default_not_upload.png";
                            }
                            else
                            {
                                house.houseQRCode = ThumbnaiUrlCMS + house.houseQRCode.Trim();

                                int n = teamId;
                                double refer1 = Convert.ToDouble((n + 1));
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
                            }
                        }
                        catch (Exception e) { house.houseQRCode = "/Images/default_not_upload.png"; }

                    }
                    else
                    {
                        house.houseQRCode = "/Images/default_not_upload.png";
                    }

                    house.WardList = LoadListWardNo(Convert.ToInt32(house.ZoneId)); //ListWardNo();
                    house.AreaList = LoadListArea(Convert.ToInt32(house.WardNo)); //ListArea();
                    house.ZoneList = ListZone();
                    return house;
                }
                else if (teamId == -2)
                {
                    var id = db.HouseMasters.OrderByDescending(x => x.houseId).Select(x => x.houseId).FirstOrDefault();
                    int number = 1000;
                    string refer = "HPSBA" + (number + id + 1);
                    house.ReferanceId = refer;
                    house.houseQRCode = "/Images/QRcode.png";
                    //house.WardList = ListWardNo();
                    //house.AreaList = ListArea();

                    var WWWW = new List<SelectListItem>();
                    SelectListItem itemAdd = new SelectListItem() { Text = "Select Ward / Prabhag", Value = "0" };
                    WWWW.Insert(0, itemAdd);

                    var ARRR = new List<SelectListItem>();
                    SelectListItem itemAddARR = new SelectListItem() { Text = "Select Area", Value = "0" };
                    ARRR.Insert(0, itemAddARR);


                    house.WardList = WWWW;
                    house.AreaList = ARRR;
                    house.ZoneList = ListZone();
                    house.houseId = 0;
                    return house;
                }
                else
                {
                    var id = db.HouseMasters.OrderByDescending(x => x.houseId).Select(x => x.houseId).FirstOrDefault();
                    int number = 1000;
                    string refer = "HPSBA" + (number + id + 1);
                    house.ReferanceId = refer;
                    house.houseQRCode = "/Images/QRcode.png";
                    house.WardList = ListWardNo();
                    house.AreaList = ListArea();
                    house.ZoneList = ListZone();
                    house.houseId = id;
                    return house;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public MasterQRDetailsVM GetMasterQRDetails(int teamId, string houseId)
        {
            try
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();


                string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.UserProfile + "/";

                MasterQRDetailsVM master = new MasterQRDetailsVM();


                var Details = db.MasterQRs.Where(x => x.MasterId == teamId).FirstOrDefault();
                if (houseId != "")
                {
                    Details = db.MasterQRs.Where(x => x.MasterId == teamId & x.ReferanceId == houseId).FirstOrDefault();

                }
                if (Details != null)
                {
                    master = FillMasterQRDetailsViewModel(Details);

                    if (houseId != "")
                    {
                        var areaid = db.HouseLists.Where(x => x.ReferanceId == houseId).FirstOrDefault();
                        master.CheckHlist = db.HouseLists.Where(x => x.IsActive == true & x.AreaId == areaid.AreaId).OrderBy(x => x.ReferanceId).ToList<HouseList>();
                    }
                    else
                    {
                        var areaid = db.HouseLists.Where(x => x.ReferanceId == master.ReferanceId).FirstOrDefault();
                        master.CheckHlist = db.HouseLists.Where(x => x.IsActive == true & x.AreaId == areaid.AreaId).OrderBy(x => x.ReferanceId).ToList<HouseList>();
                    }


                    // master.CheckAppDs = (List<HouseMaster>)db.HouseMasters.Where(x => x.ReferanceId != null).Select(x => new { x.ReferanceId, x.houseId });

                    if (master.QRList != null)
                    {
                        string s = master.QRList;
                        string[] values = s.Split(',');

                        for (int i = 0; i < values.Length; i++)
                        {
                            char[] MyChar = { 'H', 'P', 'S', 'B', 'A' };
                            values[i] = values[i].Trim(MyChar);


                            int u = 0;
                            if (values[i] != "")
                            {
                                u = Convert.ToInt32(values[i]);
                                int number = 1000;
                                u = u - number;
                            }
                            string state1 = "";
                            foreach (var v in master.CheckHlist)
                            {
                                if (v.HouseId == u)
                                {

                                    v.IsCheked = true;

                                }
                            }
                        }

                    }
                    master.HouseList = ListHouse(teamId, houseId);
                    return master;
                }
                else
                {

                    var areaid = db.HouseLists.Where(x => x.ReferanceId == houseId).FirstOrDefault();
                    if (areaid == null)
                    {
                        var AreaId = 0;
                        master.CheckHlist = db.HouseLists.Where(x => x.IsActive == true & x.AreaId > 0).OrderBy(x => x.HouseId).ToList<HouseList>();
                        // master.CheckAppDs = (List<HouseMaster>)db.HouseMasters.Where(x => x.ReferanceId != null).Select(x => new { x.ReferanceId, x.houseId });
                        master.HouseList = ListHouse(teamId, houseId);
                        return master;
                    }
                    else
                    {
                        var AreaId = areaid.AreaId;
                        master.CheckHlist = db.HouseLists.Where(x => x.IsActive == true & x.AreaId == AreaId).OrderBy(x => x.HouseId).ToList<HouseList>();
                        // master.CheckAppDs = (List<HouseMaster>)db.HouseMasters.Where(x => x.ReferanceId != null).Select(x => new { x.ReferanceId, x.houseId });
                        master.HouseList = ListHouse(teamId, houseId);
                        return master;
                    }

                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public VehicalRegDetailsVM GetVehicalRegDetails(int teamId)
        {
            try
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();

                string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.VehicalQRCode + "/";
                VehicalRegDetailsVM vehicalReg = new VehicalRegDetailsVM();

                var Details = db.Vehical_QR_Master.Where(x => x.vqrId == teamId).FirstOrDefault();
                if (Details != null)
                {
                    vehicalReg = FillVehicalRegDetailsViewModel(Details);
                    if (vehicalReg.vehicalQRCode != null && vehicalReg.vehicalQRCode != "")
                    {
                        HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(ThumbnaiUrlCMS + vehicalReg.vehicalQRCode.Trim());
                        HttpWebResponse httpRes = null;
                        try
                        {
                            httpRes = (HttpWebResponse)httpReq.GetResponse(); // Error 404 right here,
                            if (httpRes.StatusCode == HttpStatusCode.NotFound)
                            {
                                vehicalReg.vehicalQRCode = "/Images/default_not_upload.png";
                            }
                            else
                            {
                                vehicalReg.vehicalQRCode = ThumbnaiUrlCMS + vehicalReg.vehicalQRCode.Trim();

                                int n = teamId;
                                double refer1 = Convert.ToDouble((n + 1));
                                double xyz = refer1 / 100;

                                string s = xyz.ToString("0.00", CultureInfo.InvariantCulture);
                                string[] parts = s.Split('.');
                                int i1 = int.Parse(parts[0]);
                                int i2 = int.Parse(parts[1]);

                                if (i2 == 0)
                                {
                                    //i1 = i1 + 1;
                                    s = "V" + i1.ToString();
                                }
                                else
                                {
                                    s = "V" + (i1 + 1);
                                }

                                vehicalReg.SerielNo = s;
                            }
                        }
                        catch (Exception e) { vehicalReg.vehicalQRCode = "/Images/default_not_upload.png"; }

                    }
                    else
                    {
                        vehicalReg.vehicalQRCode = "/Images/default_not_upload.png";
                    }

                    vehicalReg.VehicleList = VehicleList();
                    return vehicalReg;
                }
                else if (teamId == -2)
                {
                    var id = db.Vehical_QR_Master.OrderByDescending(x => x.vqrId).Select(x => x.vqrId).FirstOrDefault();
                    int number = 1000;
                    string refer = "VQR" + (number + id + 1);
                    vehicalReg.ReferanceId = refer;
                    vehicalReg.vehicalQRCode = "/Images/QRcode.png";
                    //house.WardList = ListWardNo();
                    //house.AreaList = ListArea();
                    vehicalReg.VehicleList = VehicleList();

                    vehicalReg.vqrId = 0;
                    return vehicalReg;
                }
                else
                {
                    var id = db.Vehical_QR_Master.OrderByDescending(x => x.vqrId).Select(x => x.vqrId).FirstOrDefault();
                    int number = 1000;
                    string refer = "VQR" + (number + id + 1);
                    vehicalReg.ReferanceId = refer;
                    vehicalReg.vehicalQRCode = "/Images/QRcode.png";

                    vehicalReg.VehicleList = VehicleList();
                    vehicalReg.vqrId = id;
                    return vehicalReg;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public SBALUserLocationMapView GetHouseByIdforMap(int teamId, int daId)
        {
            try
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();

                string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.HouseQRCode + "/";
                SBALUserLocationMapView house = new SBALUserLocationMapView();

                var Details = db.HouseMasters.Where(x => x.houseId == teamId).FirstOrDefault();
                if (Details != null)
                {
                    house = FillHouseDetailsViewModelforMap(Details);
                    Daily_Attendance Daily_Attendanceuser = new Daily_Attendance();
                    Daily_Attendanceuser = db.Daily_Attendance.Where(x => x.daID == daId).FirstOrDefault();
                    UserMaster user = new UserMaster();
                    user = db.UserMasters.Where(x => x.userId == Daily_Attendanceuser.userId).FirstOrDefault();
                    house.userName = user.userName;
                    if (house.houseQRCode != null && house.houseQRCode != "")
                    {
                        HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(ThumbnaiUrlCMS + house.houseQRCode.Trim());
                        HttpWebResponse httpRes = null;
                        try
                        {
                            httpRes = (HttpWebResponse)httpReq.GetResponse(); // Error 404 right here,
                            if (httpRes.StatusCode == HttpStatusCode.NotFound)
                            {
                                house.houseQRCode = "/Images/default_not_upload.png";
                            }
                            else
                            {
                                house.houseQRCode = ThumbnaiUrlCMS + house.houseQRCode.Trim();
                            }
                        }
                        catch (Exception e) { house.houseQRCode = "/Images/default_not_upload.png"; }

                    }
                    else
                    {
                        house.houseQRCode = "/Images/default_not_upload.png";
                    }

                    house.WardList = LoadListWardNo(Convert.ToInt32(house.ZoneId)); //ListWardNo();
                    house.AreaList = LoadListArea(Convert.ToInt32(house.WardNo)); //ListArea();
                    house.ZoneList = ListZone();
                    return house;
                }


                else
                {
                    Daily_Attendance Daily_Attendanceuser = new Daily_Attendance();
                    Daily_Attendanceuser = db.Daily_Attendance.Where(x => x.daID == daId).FirstOrDefault();
                    UserMaster user = new UserMaster();
                    user = db.UserMasters.Where(x => x.userId == Daily_Attendanceuser.userId).FirstOrDefault();
                    house.userName = user.userName;

                    var id = db.HouseMasters.OrderByDescending(x => x.houseId).Select(x => x.houseId).FirstOrDefault();
                    int number = 1000;
                    string refer = "HPSBA" + (number + id + 1);
                    house.ReferanceId = refer;
                    house.houseQRCode = "/Images/QRcode.png";
                    house.WardList = ListWardNo();
                    house.AreaList = ListArea();
                    house.ZoneList = ListZone();
                    house.houseId = id;
                    return house;
                }


            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public SBALUserLocationMapView GetLiquidByIdforMap(int teamId, int daId, string EmpType)
        {
            try
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();

                string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.HouseQRCode + "/";
                SBALUserLocationMapView house = new SBALUserLocationMapView();

                var Details = db.HouseMasters.Where(x => x.houseId == teamId).FirstOrDefault();
                if (Details != null)
                {

                    house = FillHouseDetailsViewModelforMap(Details);
                    Daily_Attendance Daily_Attendanceuser = new Daily_Attendance();
                    Daily_Attendanceuser = db.Daily_Attendance.Where(x => x.daID == daId & x.EmployeeType == EmpType).FirstOrDefault();
                    UserMaster user = new UserMaster();
                    user = db.UserMasters.Where(x => x.userId == Daily_Attendanceuser.userId & x.EmployeeType == EmpType).FirstOrDefault();
                    house.userName = user.userName;
                    if (house.houseQRCode != null && house.houseQRCode != "")
                    {
                        HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(ThumbnaiUrlCMS + house.houseQRCode.Trim());
                        HttpWebResponse httpRes = null;
                        try
                        {
                            httpRes = (HttpWebResponse)httpReq.GetResponse(); // Error 404 right here,
                            if (httpRes.StatusCode == HttpStatusCode.NotFound)
                            {
                                house.houseQRCode = "/Images/default_not_upload.png";
                            }
                            else
                            {
                                house.houseQRCode = ThumbnaiUrlCMS + house.houseQRCode.Trim();
                            }
                        }
                        catch (Exception e) { house.houseQRCode = "/Images/default_not_upload.png"; }

                    }
                    else
                    {
                        house.houseQRCode = "/Images/default_not_upload.png";
                    }

                    house.WardList = LoadListWardNo(Convert.ToInt32(house.ZoneId)); //ListWardNo();
                    house.AreaList = LoadListArea(Convert.ToInt32(house.WardNo)); //ListArea();
                    house.ZoneList = ListZone();
                    return house;
                }


                else
                {
                    Daily_Attendance Daily_Attendanceuser = new Daily_Attendance();
                    Daily_Attendanceuser = db.Daily_Attendance.Where(x => x.daID == daId).FirstOrDefault();
                    UserMaster user = new UserMaster();
                    user = db.UserMasters.Where(x => x.userId == Daily_Attendanceuser.userId).FirstOrDefault();
                    house.userName = user.userName;

                    var id = db.HouseMasters.OrderByDescending(x => x.houseId).Select(x => x.houseId).FirstOrDefault();
                    int number = 1000;
                    string refer = "HPSBA" + (number + id + 1);
                    house.ReferanceId = refer;
                    house.houseQRCode = "/Images/QRcode.png";
                    house.WardList = ListWardNo();
                    house.AreaList = ListArea();
                    house.ZoneList = ListZone();
                    house.houseId = id;
                    return house;
                }


            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public SBALUserLocationMapView GetDumpByIdforMap(int teamId, int daId, string EmpType)
        {
            try
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();

                string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.HouseQRCode + "/";
                SBALUserLocationMapView house = new SBALUserLocationMapView();

                var Details = db.HouseMasters.Where(x => x.houseId == teamId).FirstOrDefault();
                if (Details != null)
                {

                    house = FillHouseDetailsViewModelforMap(Details);
                    Daily_Attendance Daily_Attendanceuser = new Daily_Attendance();
                    Daily_Attendanceuser = db.Daily_Attendance.Where(x => x.daID == daId & x.EmployeeType == EmpType).FirstOrDefault();
                    UserMaster user = new UserMaster();
                    user = db.UserMasters.Where(x => x.userId == Daily_Attendanceuser.userId & x.EmployeeType == EmpType).FirstOrDefault();
                    house.userName = user.userName;
                    if (house.houseQRCode != null && house.houseQRCode != "")
                    {
                        HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(ThumbnaiUrlCMS + house.houseQRCode.Trim());
                        HttpWebResponse httpRes = null;
                        try
                        {
                            httpRes = (HttpWebResponse)httpReq.GetResponse(); // Error 404 right here,
                            if (httpRes.StatusCode == HttpStatusCode.NotFound)
                            {
                                house.houseQRCode = "/Images/default_not_upload.png";
                            }
                            else
                            {
                                house.houseQRCode = ThumbnaiUrlCMS + house.houseQRCode.Trim();
                            }
                        }
                        catch (Exception e) { house.houseQRCode = "/Images/default_not_upload.png"; }

                    }
                    else
                    {
                        house.houseQRCode = "/Images/default_not_upload.png";
                    }

                    house.WardList = LoadListWardNo(Convert.ToInt32(house.ZoneId)); //ListWardNo();
                    house.AreaList = LoadListArea(Convert.ToInt32(house.WardNo)); //ListArea();
                    house.ZoneList = ListZone();
                    return house;
                }


                else
                {
                    Daily_Attendance Daily_Attendanceuser = new Daily_Attendance();
                    Daily_Attendanceuser = db.Daily_Attendance.Where(x => x.daID == daId).FirstOrDefault();
                    UserMaster user = new UserMaster();
                    user = db.UserMasters.Where(x => x.userId == Daily_Attendanceuser.userId).FirstOrDefault();
                    house.userName = user.userName;

                    var id = db.HouseMasters.OrderByDescending(x => x.houseId).Select(x => x.houseId).FirstOrDefault();
                    int number = 1000;
                    string refer = "HPSBA" + (number + id + 1);
                    house.ReferanceId = refer;
                    house.houseQRCode = "/Images/QRcode.png";
                    house.WardList = ListWardNo();
                    house.AreaList = ListArea();
                    house.ZoneList = ListZone();
                    house.houseId = id;
                    return house;
                }


            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public HouseDetailsVM SaveHouseDetails(HouseDetailsVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.houseId > 0)
                    {
                        var model = db.HouseMasters.Where(x => x.houseId == data.houseId).FirstOrDefault();
                        if (model != null)
                        {
                            model.WardNo = data.WardNo;
                            model.AreaId = data.AreaId;
                            model.houseOwner = data.houseOwner;
                            model.houseOwnerMar = data.houseOwnerMar;
                            model.houseAddress = data.houseAddress;
                            model.houseOwnerMobile = data.houseMobile;
                            model.houseNumber = data.houseNumber;
                            model.houseQRCode = data.houseQRCode;
                            model.houseLat = data.houseLat;
                            model.houseLong = data.houseLong;
                            model.ZoneId = data.ZoneId;
                            model.lastModifiedEntry = DateTime.Now;
                            model.OccupancyStatus = data.OccupancyStatus;
                            model.Property_Type = data.Property_Type;
                            //if(data.WasteType== "DW")
                            //{ 
                            //model.WasteType = data.WasteType;
                            //}
                            //if (data.WasteType == "WW")
                            //{
                            //    model.WasteType = data.WasteType;
                            //}
                            //model.userId = data.userId;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        //var id = db.HouseMasters.OrderByDescending(x => x.houseId).Select(x => x.houseId).FirstOrDefault();
                        //int number = 1000;
                        //string refer = "SBA" + (number + id + 1);
                        // data.ReferanceId = refer;
                        var type = FillHouseDetailsDataModel(data);
                        db.HouseMasters.Add(type);
                        db.SaveChanges();
                    }
                }
                var houseid = db.HouseMasters.OrderByDescending(x => x.houseId).Select(x => x.houseId).FirstOrDefault();
                HouseDetailsVM vv = GetHouseDetails(houseid);
                return vv;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public VehicalRegDetailsVM SaveVehicalRegDetails(VehicalRegDetailsVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.vqrId > 0)
                    {
                        var model = db.Vehical_QR_Master.Where(x => x.vqrId == data.vqrId).FirstOrDefault();
                        if (model != null)
                        {

                            model.VehicalNumber = data.Vehican_No;
                            model.VehicalQRCode = data.vehicalQRCode;
                            model.lastModifiedEntry = DateTime.Now;
                            model.VehicalType = data.Vehical_Type;

                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        //var id = db.HouseMasters.OrderByDescending(x => x.houseId).Select(x => x.houseId).FirstOrDefault();
                        //int number = 1000;
                        //string refer = "SBA" + (number + id + 1);
                        // data.ReferanceId = refer;
                        var type = FillVehicalRegDetailsDataModel(data);
                        db.Vehical_QR_Master.Add(type);
                        db.SaveChanges();
                    }
                }
                var vqrid = db.Vehical_QR_Master.OrderByDescending(x => x.vqrId).Select(x => x.vqrId).FirstOrDefault();
                VehicalRegDetailsVM vv = GetVehicalRegDetails(vqrid);
                return vv;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<EmployeeHouseCollectionInnerOuter> getEmployeeHouseCollectionInnerOuter()
        {
            List<EmployeeHouseCollectionInnerOuter> obj = new List<EmployeeHouseCollectionInnerOuter>();
            int innerCount = 0;
            using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
            {
                var data = db.SP_EmployeeHouseCollectionCount().ToList();
                foreach (var x in data)
                {
                    innerCount = getInnerCountByUserId(x.userId, x.TodayDate);
                    obj.Add(new EmployeeHouseCollectionInnerOuter()
                    {
                        userId = x.userId,
                        userName = x.userName,
                        inTime = x.inTime,
                        TotalCount = x.TotHouseCount,
                        InnerCount = innerCount,
                        OuterCount = x.TotHouseCount - innerCount,
                        ToDate = x.TodayDate.ToString()
                    });

                }
            }
            return obj.OrderBy(c => c.userName).ToList();
        }

        public int getInnerCountByUserId(int userId, DateTime? todayDate)
        {
            int innerCount = 0;
            DateTime? fdate;
            DateTime? edate;
            List<List<coordinates>> lstPoly = new List<List<coordinates>>();

            using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
            {
                //  var att = db.Daily_Attendance.Where(a => a.userId == userId && a.daDate == todayDate).FirstOrDefault();
                //if (att != null)
                // {
                //      DateTime date = DateTime.Parse(att.startTime, System.Globalization.CultureInfo.CurrentCulture);
                //      string t = date.ToString("hh:mm:ss tt");
                //      string dt = Convert.ToDateTime(att.daDate).ToString("MM/dd/yyyy");
                //      fdate = Convert.ToDateTime(dt + " " + t);

                //      if (att.endTime == "" | att.endTime == null)
                //      {
                //          edate = DateTime.Now;
                //      }
                //      else
                //      {
                //          string Time2 = att.endTime;
                //          DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                //          string t2 = date2.ToString("hh:mm:ss tt");
                //          string dt2 = Convert.ToDateTime(att.daEndDate).ToString("MM/dd/yyyy");
                //          edate = Convert.ToDateTime(dt2 + " " + t2);
                //      }

                EmpBeatMapVM ebm = GetEmpBeatMapByUserId(userId);
                lstPoly = ebm.ebmLatLong;

                // var data = db.Locations.Where(c => c.userId == att.userId & c.datetime >= fdate & c.datetime <= edate & c.type == 1).OrderByDescending(a => a.datetime).ToList();

                //foreach (var x in data)
                //{
                //    if (x.type == 1)
                //    {

                var gcd = db.GarbageCollectionDetails.Where(c => (c.userId == userId & (c.houseId != null || c.dyId != null)) & EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(todayDate)).OrderBy(c => c.gcId).ToList();//.ToList();

                if (lstPoly != null && lstPoly.Count > 0)
                {
                    foreach (var d in gcd)
                    {
                        if (d.houseId != null)
                        {

                            var house = db.HouseMasters.Where(c => c.houseId == d.houseId).FirstOrDefault();
                            if (house != null)
                            {
                                coordinates p = new coordinates()
                                {
                                    lat = Convert.ToDouble(d.Lat),
                                    lng = Convert.ToDouble(d.Long)
                                };
                                if (lstPoly != null && lstPoly.Count > 0)
                                {
                                    foreach (var poly in lstPoly)
                                    {
                                        if (IsPointInPolygon(poly, p))
                                        {
                                            innerCount++;
                                        }
                                    }
                                }
                            }

                        }

                    }

                }
                //   }
                //  break;
                // }



                //  }

            }
            return innerCount;
        }
        public void SaveEmpBeatMap(EmpBeatMapVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.ebmId > 0)
                    {
                        var model = db.EmpBeatMaps.Where(x => x.ebmId == data.ebmId).FirstOrDefault();
                        if (model != null)
                        {
                            model.userId = data.userId;
                            model.Type = data.Type;
                            model.ebmLatLong = ConvertLatLongToString(data.ebmLatLong);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var type = fillEmpBeatMap(data);
                        db.EmpBeatMaps.Add(type);
                        db.SaveChanges();
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        public EmpBeatMapVM GetEmpBeatMap(int ebmId)
        {
            EmpBeatMapVM empBeatMap = new EmpBeatMapVM();
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (ebmId > 0)
                    {
                        var model = db.EmpBeatMaps.Where(x => x.ebmId == ebmId).FirstOrDefault();
                        if (model != null)
                        {
                            empBeatMap = fillEmpBeatMapVM(model);
                        }
                        else
                        {
                            empBeatMap.ebmId = -1;
                            empBeatMap.userId = -1;
                            empBeatMap.Type = "W";
                        }
                    }
                    else
                    {
                        empBeatMap.ebmId = -1;
                        empBeatMap.userId = -1;
                        empBeatMap.Type = "W";

                    }

                }
            }
            catch (Exception ex)
            {
                return empBeatMap;
            }

            return empBeatMap;
        }




        public EmpBeatMapVM GetEmpBeatMapByUserId(int userId)
        {
            EmpBeatMapVM empBeatMap = new EmpBeatMapVM();
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (userId > 0)
                    {
                        var model = db.EmpBeatMaps.Where(x => x.userId == userId).FirstOrDefault();
                        if (model != null)
                        {
                            empBeatMap = fillEmpBeatMapVM(model);
                        }
                        else
                        {
                            empBeatMap.ebmId = -1;
                            empBeatMap.userId = -1;
                        }
                    }
                    else
                    {
                        empBeatMap.ebmId = -1;
                        empBeatMap.userId = -1;

                    }

                }
            }
            catch (Exception ex)
            {
                return empBeatMap;
            }

            return empBeatMap;
        }

        public bool IsPointInPolygon(int ebmId, coordinates p)
        {
            bool inside = false;
            EmpBeatMapVM empBeatMap = new EmpBeatMapVM();
            empBeatMap = GetEmpBeatMap(ebmId);
            List<coordinates> poly = new List<coordinates>();
            if (empBeatMap.ebmId > 0)
            {
                poly = empBeatMap.ebmLatLong[0];
                double minX = poly[0].lat ?? 0;
                double maxX = poly[0].lat ?? 0;
                double minY = poly[0].lng ?? 0;
                double maxY = poly[0].lng ?? 0;

                for (int i = 1; i < poly.Count; i++)
                {
                    coordinates q = poly[i];
                    minX = Math.Min(q.lat ?? 0, minX);
                    maxX = Math.Max(q.lat ?? 0, maxX);
                    minY = Math.Min(q.lng ?? 0, minY);
                    maxY = Math.Max(q.lng ?? 0, maxY);
                }


                if (p.lat < minX || p.lat > maxX || p.lng < minY || p.lng > maxY)
                {
                    return false;
                }
                for (int i = 0, j = poly.Count - 1; i < poly.Count; j = i++)
                {
                    if ((poly[i].lng > p.lng) != (poly[j].lng > p.lng) &&
                         p.lat < (poly[j].lat - poly[i].lat) * (p.lng - poly[i].lng) / (poly[j].lng - poly[i].lng) + poly[i].lat)
                    {
                        inside = !inside;
                    }
                }
                return inside;

            }
            return inside;
        }

        public bool IsPointInPolygon(List<coordinates> poly, coordinates p)
        {
            bool inside = false;

            if (poly != null && poly.Count > 0)
            {
                double minX = poly[0].lat ?? 0;
                double maxX = poly[0].lat ?? 0;
                double minY = poly[0].lng ?? 0;
                double maxY = poly[0].lng ?? 0;

                for (int i = 1; i < poly.Count; i++)
                {
                    coordinates q = poly[i];
                    minX = Math.Min(q.lat ?? 0, minX);
                    maxX = Math.Max(q.lat ?? 0, maxX);
                    minY = Math.Min(q.lng ?? 0, minY);
                    maxY = Math.Max(q.lng ?? 0, maxY);
                }


                if (p.lat < minX || p.lat > maxX || p.lng < minY || p.lng > maxY)
                {
                    return false;
                }
                for (int i = 0, j = poly.Count - 1; i < poly.Count; j = i++)
                {
                    if ((poly[i].lng > p.lng) != (poly[j].lng > p.lng) &&
                         p.lat < (poly[j].lat - poly[i].lat) * (p.lng - poly[i].lng) / (poly[j].lng - poly[i].lng) + poly[i].lat)
                    {
                        inside = !inside;
                    }
                }
                return inside;

            }
            return inside;
        }

        public string ConvertLatLongToString(List<List<coordinates>> lstCord)
        {
            List<string> lstPoly = new List<string>();
            foreach (var p in lstCord)
            {
                List<string> lstLatLong = new List<string>();
                foreach (var s in p)
                {
                    lstLatLong.Add(s.lat + "," + s.lng);
                }
                lstPoly.Add(string.Join(";", lstLatLong));
            }
            return string.Join(":", lstPoly);
        }


        public List<List<coordinates>> ConvertStringToLatLong(string strCord)
        {
            List<List<coordinates>> lstCord = new List<List<coordinates>>();
            string[] lstPoly = strCord.Split(':');
            if (lstPoly.Length > 0)
            {
                for (var i = 0; i < lstPoly.Length; i++)
                {
                    List<coordinates> poly = new List<coordinates>();
                    string[] lstLatLong = lstPoly[i].Split(';');
                    if (lstLatLong.Length > 0)
                    {
                        for (var j = 0; j < lstLatLong.Length; j++)
                        {
                            coordinates cord = new coordinates();
                            string[] strLatLong = lstLatLong[j].Split(',');
                            if (strLatLong.Length == 2)
                            {
                                cord.lat = Convert.ToDouble(strLatLong[0]);
                                cord.lng = Convert.ToDouble(strLatLong[1]);
                            }
                            poly.Add(cord);
                        }

                    }
                    lstCord.Add(poly);
                }
            }
            return lstCord;
        }

        public List<coordinates> ConvertStringToLatLong1(string strCord)
        {

            List<coordinates> poly = new List<coordinates>();
            if (!string.IsNullOrEmpty(strCord))
            {
                string[] lstLatLong = strCord.Split(';');
                if (lstLatLong.Length > 0)
                {
                    for (var j = 0; j < lstLatLong.Length; j++)
                    {
                        coordinates cord = new coordinates();
                        string[] strLatLong = lstLatLong[j].Split(',');
                        if (strLatLong.Length == 2)
                        {
                            cord.lat = Convert.ToDouble(strLatLong[0]);
                            cord.lng = Convert.ToDouble(strLatLong[1]);
                        }
                        poly.Add(cord);
                    }

                }
            }
            return poly;
        }

        public void DeletHouseDetails(int teamId)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.HouseMasters.Where(x => x.houseId == teamId).FirstOrDefault();
                    if (Details != null)
                    {
                        db.HouseMasters.Remove(Details);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Employee
        public EmployeeDetailsVM GetEmployeeDetails(int teamId)
        {
            try
            {
                EmployeeDetailsVM type = new EmployeeDetailsVM();
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();

                string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.UserProfile + "/";
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.UserMasters.Where(x => x.userId == teamId).FirstOrDefault();
                    if (Details != null)
                    {
                        type = FillEmployeeViewModel(Details);
                        if (type.userProfileImage != null && type.userProfileImage != "")
                        {
                            type.userProfileImage = ThumbnaiUrlCMS + type.userProfileImage.Trim();
                        }
                        else
                        {
                            type.userProfileImage = "/Images/default_not_upload.png";
                        }
                        type.Shifts = GetEmpShifts(teamId);
                        return type;
                    }
                    else
                    {
                        type.userProfileImage = "/Images/add_image_square.png";
                        type.Shifts = GetEmpShifts(teamId);
                        return type;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Shift> GetEmpShifts(int teamId)
        {
            List<int> ShiftIds = new List<int>();
            List<Shift> ShiftList = new List<Shift>();
            using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
            {
                var Emp = db.UserMasters.Where(a => a.userId == teamId).FirstOrDefault();
                if (Emp != null && !string.IsNullOrEmpty(Emp.shiftIds))
                {
                    ShiftIds = Emp.shiftIds.Split(',').Select(x => Convert.ToInt32(x)).ToList();

                }
                ShiftList = db.EmpShifts.ToList().Select(x => new Shift
                {
                    shiftId = x.shiftId,
                    shiftName = x.shiftName + " (From : " + x.shiftStart + " To : " + x.shiftEnd + ")",
                    IsChecked = ShiftIds.Contains(x.shiftId)

                }).ToList();
            }

            return ShiftList;
        }

        public string GetEmpShiftIds(List<Shift> shiftList)
        {
            List<int> shiftIds = new List<int>();
            string strShiftIds = string.Empty;
            if (shiftList != null)
            {
                foreach (var shift in shiftList)
                {
                    if (shift.IsChecked == true)
                    {
                        if (shift.shiftId != 0)
                        {
                            shiftIds.Add(shift.shiftId);
                        }
                    }

                }
            }
            if (shiftIds.Count > 0)
            {
                strShiftIds = string.Join(",", shiftIds);
            }
            return strShiftIds;
        }

        public EmpShiftVM GetEmpShiftById(int teamId)
        {
            try
            {
                EmpShiftVM type = new EmpShiftVM();

                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.EmpShifts.Where(x => x.shiftId == teamId).FirstOrDefault();
                    if (Details != null)
                    {
                        type = FillEmpShiftViewModel(Details);

                        return type;
                    }
                    else
                    {
                        type.shiftId = -1;
                        return type;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public SBAAttendenceSettingsGridRow GetAttendenceEmployeeById(int teamId)
        {
            try
            {
                SBAAttendenceSettingsGridRow type = new SBAAttendenceSettingsGridRow();
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();

                string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.UserProfile + "/";
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.Daily_Attendance.Where(x => x.userId == teamId).FirstOrDefault();
                    if (Details != null)
                    {
                        type = FillAttendenceEmployeeViewModel(Details);

                        return type;
                    }
                    else
                    {
                        return type;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteEmployeeDetails(int teamId)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.UserMasters.Where(x => x.userId == teamId).FirstOrDefault();
                    if (Details != null)
                    {
                        db.UserMasters.Remove(Details);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void SaveEmployeeDetails(EmployeeDetailsVM data, string Emptype)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.userId > 0)
                    {
                        var model = db.UserMasters.Where(x => x.userId == data.userId).FirstOrDefault();
                        if (model != null)
                        {
                            if (data.userProfileImage != null)
                            {
                                model.userProfileImage = data.userProfileImage;
                            }
                            model.userId = data.userId;
                            model.userAddress = data.userAddress;
                            model.userLoginId = data.userLoginId;
                            model.userMobileNumber = data.userMobileNumber;
                            model.userName = data.userName;
                            model.userNameMar = data.userNameMar;
                            model.userPassword = data.userPassword;
                            model.userEmployeeNo = data.userEmployeeNo;
                            model.imoNo = data.imoNo;
                            model.isActive = data.isActive;
                            model.bloodGroup = data.bloodGroup;
                            model.gcTarget = data.gcTarget;
                            model.shiftIds = GetEmpShiftIds(data.Shifts);
                            //model.EmployeeType = Emptype;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var type = FillEmployeeDataModel(data, Emptype);
                        type.shiftIds = GetEmpShiftIds(data.Shifts);
                        db.UserMasters.Add(type);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveEmpShift(EmpShiftVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.shiftId > 0)
                    {
                        var model = db.EmpShifts.Where(x => x.shiftId == data.shiftId).FirstOrDefault();
                        if (model != null)
                        {
                            model.shiftName = data.shiftName;
                            model.shiftStart = data.shiftStart;
                            model.shiftEnd = data.shiftEnd;
                            //model.EmployeeType = Emptype;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var type = FillEmpShiftDataModel(data);
                        db.EmpShifts.Add(type);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveAttendenceSettingsDetail(SBAAttendenceSettingsGridRow data)
        {

            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    List<SBAAttendenceSettingsGridRow> datalist = new List<SBAAttendenceSettingsGridRow>();
                    var data1 = db.Vw_MsgNotification.ToList();
                    foreach (var x in data1)
                    {
                        datalist.Add(new SBAAttendenceSettingsGridRow()
                        {
                            userId = Convert.ToInt32(x.userId),
                            userName = db.UserMasters.Where(c => c.userId == x.userId).FirstOrDefault().userName,
                        });

                        string mes = "कचरा संकलन कर्मचारी" + db.UserMasters.Where(c => c.userId == x.userId).FirstOrDefault().userName + "आज ड्युटी वर गैरहजर आहे";
                        string housemob = "8830635095";
                        sendSMS(mes, housemob);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void sendSMS(string sms, string MobilNumber)
        {
            try
            {
                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://www.smsjust.com/sms/user/urlsms.php?username=ycagent&pass=yocc@5095&senderid=YOCCAG&dest_mobileno=" + MobilNumber + "&msgtype=UNI&message=" + sms + "&response=Y");
                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://www.smsjust.com/sms/user/urlsms.php?username=ycagent&pass=yocc@5095&senderid=YOCCAG&dest_mobileno=" + MobilNumber + "&message=" + sms + "&response=Y");
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://www.smsjust.com/sms/user/urlsms.php?username=artiyocc&pass=123456&senderid=BIGVCL&dest_mobileno=" + MobilNumber + "&msgtype=UNI&message=" + sms + "%20&response=Y");

                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://www.smsjust.com/sms/user/urlsms.php?username=ycagent&pass=yocc@5095&senderid=BIGVCL&dest_mobileno=" + MobilNumber + "&message=" + sms + "%20&response=Y");

                //Get response from Ozeki NG SMS Gateway Server and read the answer
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
            }
            catch { }

        }

        #endregion
        #region Compalint

        public ComplaintVM GetCompalint(int teamId)
        {
            ComplaintVM comp = new ComplaintVM();
            DevSwachhBharatMainEntities dbm = new DevSwachhBharatMainEntities();
            var appdetails = dbm.AppDetails.Where(c => c.AppId == AppID).FirstOrDefault();
            // string json = new WebClient().DownloadString("http://192.168.200.3:8077////api/Get/Complaint?appId=1");
            string json = new WebClient().DownloadString(appdetails.Grampanchayat_Pro + "/api/Get/Complaint?appId=1");
            List<ComplaintVM> obj2 = JsonConvert.DeserializeObject<List<ComplaintVM>>(json).ToList();
            var data = obj2.Where(c => c.complaintId == teamId).FirstOrDefault();
            comp.complaintId = data.complaintId;
            comp.status = data.status;
            string image = "";
            if (data.endImage == null || data.endImage == "")
            {
                image = "/Images/default_not_upload.png";
            }
            else
            {
                comp.endImage = data.endImage;
            }
            comp.comment = data.comment;

            return comp;

        }
        public void SaveComplaintStatus(ComplaintVM data)
        {
            //try
            //{
            //    using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
            //    {
            //        if (data.userId > 0)
            //        {
            //            var model = db.UserMasters.Where(x => x.userId == data.userId).FirstOrDefault();
            //            if (model != null)
            //            {
            //                if (data.userProfileImage != null)
            //                {
            //                    model.userProfileImage = data.userProfileImage;
            //                }
            //                model.userId = data.userId;
            //                model.userAddress = data.userAddress;
            //                model.userLoginId = data.userLoginId;
            //                model.userMobileNumber = data.userMobileNumber;
            //                model.userName = data.userName;
            //                model.userNameMar = data.userNameMar;
            //                model.userPassword = data.userPassword;
            //                model.userEmployeeNo = data.userEmployeeNo;
            //                model.imoNo = data.imoNo;
            //                db.SaveChanges();
            //            }
            //        }
            //        else
            //        {
            //            var type = FillEmployeeDataModel(data);
            //            db.UserMasters.Add(type);
            //            db.SaveChanges();
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        #endregion

        #region Location
        public string Address(string location)
        {
            if (location != string.Empty && location != null)
            {
                string lat = null, log = null;
                string[] arr = new string[2];
                arr = location.Split(',');
                lat = arr[0];
                log = arr[1];
                XmlDocument doc = new XmlDocument();
                string Address = "";

                //doc.Load("http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + lat + "," + log + "& sensor=false");
                doc.Load("https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + lat + "," + log + "&sensor=false&key=AIzaSyBy6BUqH6o1r7JBS8s1Tk7cmllapL6xuMA");

                XmlNode element = doc.SelectSingleNode("//GeocodeResponse/status");
                if (element.InnerText == "ZERO_RESULTS")
                {
                    Console.WriteLine("No data available for the specified location");
                }
                else
                {
                    XmlNode xnList1 = doc.SelectSingleNode("//GeocodeResponse/result/formatted_address");

                    if (xnList1 != null)
                    {
                        Address = xnList1.InnerText;
                    }
                    else
                    {
                        Address = "";
                    }
                }
                return Address;
            }
            else
            {
                return "";
            }


        }
        public SBALUserLocationMapView GetLocationDetails(int teamId, string Emptype)
        {
            try
            {
                if (Emptype == "NULL" || Emptype == null)
                {
                    using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                    {
                        //var Details = db.Locations.Where(c => c.EmployeeType == null).FirstOrDefault();
                        var Details = db.Locations.FirstOrDefault();

                        if (teamId > 0)
                        {
                            //Details = db.Locations.Where(c => c.locId == teamId && c.EmployeeType == null).FirstOrDefault();
                            Details = db.Locations.Where(c => c.locId == teamId).FirstOrDefault();
                        }
                        if (Details != null)
                        {
                            //var atten = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(Details.datetime) && c.userId == Details.userId && c.EmployeeType == null).FirstOrDefault();
                            var atten = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(Details.datetime) && c.userId == Details.userId).FirstOrDefault();
                            SBALUserLocationMapView loc = new SBALUserLocationMapView();
                            //var user = db.UserMasters.Where(c => c.userId == Details.userId && c.EmployeeType == null).FirstOrDefault();
                            var user = db.UserMasters.Where(c => c.userId == Details.userId).FirstOrDefault();
                            loc.userName = user.userName;
                            loc.date = Convert.ToDateTime(Details.datetime).ToString("dd/MM/yyyy");
                            loc.time = Convert.ToDateTime(Details.datetime).ToString("hh:mm tt");
                            loc.address = checkNull(Details.address).Replace("Unnamed Road, ", "");
                            loc.lat = Details.lat;
                            loc.log = Details.@long;
                            loc.UserList = ListUser(null);
                            loc.userMobile = user.userMobileNumber;
                            loc.type = Convert.ToInt32(user.Type);
                            try { loc.vehcileNumber = atten.vehicleNumber; } catch { loc.vehcileNumber = ""; }

                            return loc;
                        }
                        else
                        {
                            return new SBALUserLocationMapView();
                        }

                    }
                }
                else if (Emptype == "L")
                {
                    using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                    {
                        //var Details = db.Locations.Where(c => c.EmployeeType == Emptype).FirstOrDefault();
                        var Details = db.Locations.FirstOrDefault();

                        if (teamId > 0)
                        {
                            // Details = db.Locations.Where(c => c.locId == teamId && c.EmployeeType == Emptype).FirstOrDefault();
                            Details = db.Locations.Where(c => c.locId == teamId).FirstOrDefault();
                        }

                        if (Details != null)
                        {
                            //var atten = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(Details.datetime) && c.userId == Details.userId && c.EmployeeType == Emptype).FirstOrDefault();
                            var atten = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(Details.datetime) && c.userId == Details.userId).FirstOrDefault();
                            SBALUserLocationMapView loc = new SBALUserLocationMapView();
                            var user = db.UserMasters.Where(c => c.userId == Details.userId).FirstOrDefault();
                            loc.userName = user.userName;
                            loc.date = Convert.ToDateTime(Details.datetime).ToString("dd/MM/yyyy");
                            loc.time = Convert.ToDateTime(Details.datetime).ToString("hh:mm tt");
                            loc.address = checkNull(Details.address).Replace("Unnamed Road, ", "");
                            loc.lat = Details.lat;
                            loc.log = Details.@long;
                            loc.UserList = ListUser(Emptype);
                            loc.userMobile = user.userMobileNumber;
                            loc.type = Convert.ToInt32(user.Type);
                            try { loc.vehcileNumber = atten.vehicleNumber; } catch { loc.vehcileNumber = ""; }

                            return loc;
                        }
                        else
                        {
                            return new SBALUserLocationMapView();
                        }

                    }
                }
                else if (Emptype == "S")
                {
                    using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                    {
                        //var Details = db.Locations.Where(c => c.EmployeeType == Emptype).FirstOrDefault();
                        var Details = db.Locations.FirstOrDefault();

                        if (teamId > 0)
                        {
                            //Details = db.Locations.Where(c => c.locId == teamId && c.EmployeeType == Emptype).FirstOrDefault();
                            Details = db.Locations.Where(c => c.locId == teamId).FirstOrDefault();

                        }

                        if (Details != null)
                        {
                            //var atten = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(Details.datetime) && c.userId == Details.userId && c.EmployeeType == Emptype).FirstOrDefault();
                            var atten = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(Details.datetime) && c.userId == Details.userId).FirstOrDefault();
                            SBALUserLocationMapView loc = new SBALUserLocationMapView();
                            var user = db.UserMasters.Where(c => c.userId == Details.userId).FirstOrDefault();
                            loc.userName = user.userName;
                            loc.date = Convert.ToDateTime(Details.datetime).ToString("dd/MM/yyyy");
                            loc.time = Convert.ToDateTime(Details.datetime).ToString("hh:mm tt");
                            loc.address = checkNull(Details.address).Replace("Unnamed Road, ", "");
                            loc.lat = Details.lat;
                            loc.log = Details.@long;
                            loc.UserList = ListUser(Emptype);
                            loc.userMobile = user.userMobileNumber;
                            loc.type = Convert.ToInt32(user.Type);
                            try { loc.vehcileNumber = atten.vehicleNumber; } catch { loc.vehcileNumber = ""; }

                            return loc;
                        }
                        else
                        {
                            return new SBALUserLocationMapView();
                        }

                    }
                }
                else if (Emptype == "D")
                {
                    using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                    {
                        //var Details = db.Locations.Where(c => c.EmployeeType == null).FirstOrDefault();
                        var Details = db.Locations.FirstOrDefault();

                        if (teamId > 0)
                        {
                            //Details = db.Locations.Where(c => c.locId == teamId && c.EmployeeType == null).FirstOrDefault();
                            Details = db.Locations.Where(c => c.locId == teamId).FirstOrDefault();
                        }
                        if (Details != null)
                        {
                            //var atten = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(Details.datetime) && c.userId == Details.userId && c.EmployeeType == null).FirstOrDefault();
                            var atten = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(Details.datetime) && c.userId == Details.userId).FirstOrDefault();
                            SBALUserLocationMapView loc = new SBALUserLocationMapView();
                            //var user = db.UserMasters.Where(c => c.userId == Details.userId && c.EmployeeType == null).FirstOrDefault();
                            var user = db.UserMasters.Where(c => c.userId == Details.userId).FirstOrDefault();
                            loc.userName = user.userName;
                            loc.date = Convert.ToDateTime(Details.datetime).ToString("dd/MM/yyyy");
                            loc.time = Convert.ToDateTime(Details.datetime).ToString("hh:mm tt");
                            loc.address = checkNull(Details.address).Replace("Unnamed Road, ", "");
                            loc.lat = Details.lat;
                            loc.log = Details.@long;
                            loc.UserList = ListUser(Emptype);
                            loc.userMobile = user.userMobileNumber;
                            loc.type = Convert.ToInt32(user.Type);
                            try { loc.vehcileNumber = atten.vehicleNumber; } catch { loc.vehcileNumber = ""; }

                            return loc;
                        }
                        else
                        {
                            return new SBALUserLocationMapView();
                        }

                    }
                }
                else
                {
                    using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                    {
                        var Details = db.Locations.Where(c => c.EmployeeType == Emptype).FirstOrDefault();

                        if (teamId > 0)
                        {
                            //Details = db.Locations.Where(c => c.locId == teamId && c.EmployeeType == Emptype).FirstOrDefault();
                            Details = db.Locations.Where(c => c.locId == teamId).FirstOrDefault();

                        }

                        if (Details != null)
                        {
                            //var atten = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(Details.datetime) && c.userId == Details.userId && c.EmployeeType == Emptype).FirstOrDefault();
                            var atten = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(Details.datetime) && c.userId == Details.userId).FirstOrDefault();
                            SBALUserLocationMapView loc = new SBALUserLocationMapView();
                            var user = db.UserMasters.Where(c => c.userId == Details.userId).FirstOrDefault();
                            loc.userName = user.userName;
                            loc.date = Convert.ToDateTime(Details.datetime).ToString("dd/MM/yyyy");
                            loc.time = Convert.ToDateTime(Details.datetime).ToString("hh:mm tt");
                            loc.address = checkNull(Details.address).Replace("Unnamed Road, ", "");
                            loc.lat = Details.lat;
                            loc.log = Details.@long;
                            loc.UserList = ListUser(Emptype);
                            loc.userMobile = user.userMobileNumber;
                            loc.type = Convert.ToInt32(user.Type);
                            try { loc.vehcileNumber = atten.vehicleNumber; } catch { loc.vehcileNumber = ""; }

                            return loc;
                        }
                        else
                        {
                            return new SBALUserLocationMapView();
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                return new SBALUserLocationMapView();
            }
        }

        public List<SBALUserLocationMapView> GetAllUserLocation(string date, string Emptype)
        {
            List<SBALUserLocationMapView> userLocation = new List<SBALUserLocationMapView>();

            if (Emptype == null)
            {
                var data = db.CurrentAllUserLocationTest1().ToList();
                foreach (var x in data)
                {
                    //string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");
                    //string tim = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");
                    ////var atten = db.Daily_Attendance.Where(c => c.userId == x.userid && (c.daDate ==EntityFunctions.TruncateTime(x.datetime) && (c.endTime == null || c.endTime == ""))).FirstOrDefault();

                    //var atten = db.Daily_Attendance.Where(c => c.userId == x.userid && (c.endTime == null || c.endTime =="")).FirstOrDefault();

                    //if (atten != null)
                    //{
                    //    userLocation.Add(new SBALUserLocationMapView()
                    //    {
                    //        userId = Convert.ToInt32(x.userid),
                    //        userName = x.userName,
                    //        date = dat,
                    //        time = tim,
                    //        lat = x.lat,
                    //        log = x.@long,
                    //        address = Address(x.lat + "," + x.@long),
                    //        vehcileNumber = atten.vehicleNumber,
                    //        userMobile = x.userMobileNumber,
                    //    });
                    //}


                    userLocation.Add(new SBALUserLocationMapView()
                    {
                        userId = Convert.ToInt32(x.userid),
                        userName = x.userName,
                        date = Convert.ToDateTime(x.datt).ToString("dd/MM/yyyy"),
                        time = Convert.ToDateTime(x.datt).ToString("hh:mm tt"),
                        lat = x.lat,
                        log = x.lang,
                        address = checkNull(x.addr).Replace("Unnamed Road, ", ""),
                        vehcileNumber = x.v,
                        userMobile = x.mobile,
                    });
                }
            }
            else if (Emptype == "L")
            {
                var data = db.LiquidCurrentAllUserLocationTest1().ToList();
                foreach (var x in data)
                {
                    //string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");
                    //string tim = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");
                    ////var atten = db.Daily_Attendance.Where(c => c.userId == x.userid && (c.daDate ==EntityFunctions.TruncateTime(x.datetime) && (c.endTime == null || c.endTime == ""))).FirstOrDefault();

                    //var atten = db.Daily_Attendance.Where(c => c.userId == x.userid && (c.endTime == null || c.endTime =="")).FirstOrDefault();

                    //if (atten != null)
                    //{
                    //    userLocation.Add(new SBALUserLocationMapView()
                    //    {
                    //        userId = Convert.ToInt32(x.userid),
                    //        userName = x.userName,
                    //        date = dat,
                    //        time = tim,
                    //        lat = x.lat,
                    //        log = x.@long,
                    //        address = Address(x.lat + "," + x.@long),
                    //        vehcileNumber = atten.vehicleNumber,
                    //        userMobile = x.userMobileNumber,
                    //    });
                    //}


                    userLocation.Add(new SBALUserLocationMapView()
                    {
                        userId = Convert.ToInt32(x.userid),
                        userName = x.userName,
                        date = Convert.ToDateTime(x.datt).ToString("dd/MM/yyyy"),
                        time = Convert.ToDateTime(x.datt).ToString("hh:mm tt"),
                        lat = x.lat,
                        log = x.lang,
                        address = checkNull(x.addr).Replace("Unnamed Road, ", ""),
                        vehcileNumber = x.v,
                        userMobile = x.mobile,
                    });
                }
            }

            else if (Emptype == "S")
            {
                var data = db.StreetCurrentAllUserLocationTest1().ToList();
                foreach (var x in data)
                {

                    userLocation.Add(new SBALUserLocationMapView()
                    {
                        userId = Convert.ToInt32(x.userid),
                        userName = x.userName,
                        date = Convert.ToDateTime(x.datt).ToString("dd/MM/yyyy"),
                        time = Convert.ToDateTime(x.datt).ToString("hh:mm tt"),
                        lat = x.lat,
                        log = x.lang,
                        address = checkNull(x.addr).Replace("Unnamed Road, ", ""),
                        vehcileNumber = x.v,
                        userMobile = x.mobile,
                    });
                }
            }
            else
            {
                var data = db.CurrentAllUserLocationTest1().ToList();
                foreach (var x in data)
                {

                    userLocation.Add(new SBALUserLocationMapView()
                    {
                        userId = Convert.ToInt32(x.userid),
                        userName = x.userName,
                        date = Convert.ToDateTime(x.datt).ToString("dd/MM/yyyy"),
                        time = Convert.ToDateTime(x.datt).ToString("hh:mm tt"),
                        lat = x.lat,
                        log = x.lang,
                        address = checkNull(x.addr).Replace("Unnamed Road, ", ""),
                        vehcileNumber = x.v,
                        userMobile = x.mobile,
                    });
                }
            }
            return userLocation;
        }


        public List<SBALUserLocationMapView> GetAdminLocation()
        {
            List<SBALUserLocationMapView> userLocation = new List<SBALUserLocationMapView>();
            var data = dbMain.AppDetails.Where(c => c.IsActive == true && c.Latitude != null && c.Logitude != null).ToList();
            foreach (var x in data)
            {

                userLocation.Add(new SBALUserLocationMapView()
                {
                    userId = Convert.ToInt32(x.AppId),
                    userName = x.AppName,
                    date = DateTime.Now.ToString("dd/MM/yyyy"),
                    time = DateTime.Now.ToString("hh:mm:ss tt"),
                    lat = x.Latitude,
                    log = x.Logitude,
                    address = "",
                    vehcileNumber = "",
                    userMobile = "",
                });
            }

            return userLocation;
        }


        public List<SBALUserLocationMapView> GetUserWiseLocation(int userId, string date, string Emptype)
        {
            List<SBALUserLocationMapView> userLocation = new List<SBALUserLocationMapView>();
            DateTime newdate = DateTime.Now.Date;
            var datt = newdate;


            DateTime dt = DateTime.ParseExact(date, "d/M/yyyy", CultureInfo.InvariantCulture);
            // for both "1/1/2000" or "25/1/2000" formats
            //  string newString = dt.ToString("MM/dd/yyyy");
            //var dat1 = DateTime.ParseExact(date, "M/d/yyyy", CultureInfo.InvariantCulture);

            if (Emptype == null)
            {
                var data = db.Locations.Where(c => c.userId == userId && c.EmployeeType == null).ToList();
                foreach (var x in data.Where(c => Convert.ToDateTime(c.datetime).Date == Convert.ToDateTime(dt)))
                {
                    string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");
                    string tim = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");
                    var userName = db.UserMasters.Where(c => c.userId == userId).FirstOrDefault();
                    var atten = db.Daily_Attendance.Where(c => c.userId == x.userId && (c.daDate == EntityFunctions.TruncateTime(x.datetime))).FirstOrDefault();
                    string v = "";
                    //if (atten != null)
                    //{
                    //    v = atten.vehicleNumber;
                    //}
                    //else { v = ""; }

                    v = (string.IsNullOrEmpty(atten.vehicleNumber) ? "" : atten.vehicleNumber);

                    userLocation.Add(new SBALUserLocationMapView()
                    {
                        userName = userName.userName,
                        date = dat,
                        time = tim,
                        lat = x.lat,
                        log = x.@long,
                        address = checkNull(x.address).Replace("Unnamed Road, ", ""),
                        vehcileNumber = v,
                        userMobile = userName.userMobileNumber,
                    });
                }
            }
            else if (Emptype == "L")
            {
                var data = db.Locations.Where(c => c.userId == userId && c.EmployeeType == "L").ToList();
                foreach (var x in data.Where(c => Convert.ToDateTime(c.datetime).Date == Convert.ToDateTime(dt)))
                {
                    string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");
                    string tim = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");
                    var userName = db.UserMasters.Where(c => c.userId == userId).FirstOrDefault();
                    var atten = db.Daily_Attendance.Where(c => c.userId == x.userId && (c.daDate == EntityFunctions.TruncateTime(x.datetime))).FirstOrDefault();
                    string v = "";
                    //if (atten != null)
                    //{
                    //    v = atten.vehicleNumber;
                    //}
                    //else { v = ""; }

                    v = (string.IsNullOrEmpty(atten.vehicleNumber) ? "" : atten.vehicleNumber);

                    userLocation.Add(new SBALUserLocationMapView()
                    {
                        userName = userName.userName,
                        date = dat,
                        time = tim,
                        lat = x.lat,
                        log = x.@long,
                        address = checkNull(x.address).Replace("Unnamed Road, ", ""),
                        vehcileNumber = v,
                        userMobile = userName.userMobileNumber,
                    });
                }
            }
            else if (Emptype == "S")
            {
                var data = db.Locations.Where(c => c.userId == userId && c.EmployeeType == "S").ToList();
                foreach (var x in data.Where(c => Convert.ToDateTime(c.datetime).Date == Convert.ToDateTime(dt)))
                {
                    string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");
                    string tim = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");
                    var userName = db.UserMasters.Where(c => c.userId == userId).FirstOrDefault();
                    var atten = db.Daily_Attendance.Where(c => c.userId == x.userId && (c.daDate == EntityFunctions.TruncateTime(x.datetime))).FirstOrDefault();
                    string v = "";
                    //if (atten != null)
                    //{
                    //    v = atten.vehicleNumber;
                    //}
                    //else { v = ""; }

                    v = (string.IsNullOrEmpty(atten.vehicleNumber) ? "" : atten.vehicleNumber);

                    userLocation.Add(new SBALUserLocationMapView()
                    {
                        userName = userName.userName,
                        date = dat,
                        time = tim,
                        lat = x.lat,
                        log = x.@long,
                        address = checkNull(x.address).Replace("Unnamed Road, ", ""),
                        vehcileNumber = v,
                        userMobile = userName.userMobileNumber,
                    });
                }
            }
            return userLocation;
        }

        public List<SBALUserLocationMapView> GetUserAttenLocation(int daId)
        {
            List<SBALUserLocationMapView> userLocation = new List<SBALUserLocationMapView>();
            DateTime newdate = DateTime.Now.Date;
            var datt = newdate;
            var data = db.Daily_Attendance.Where(c => c.daID == daId).FirstOrDefault();
            var user = db.UserMasters.Where(c => c.userId == data.userId).FirstOrDefault();
            if (data.startLat.Trim() != null & data.startLat.Trim() != "")
            {
                userLocation.Add(new SBALUserLocationMapView()
                {
                    userName = user.userName,
                    date = Convert.ToDateTime(data.daDate).ToString("dd/MM/yyyy"),
                    time = data.startTime,
                    address = Address(data.startLat + "," + data.startLong),
                    lat = data.startLat,
                    log = data.startLong,
                    vehcileNumber = data.vehicleNumber,
                    userMobile = user.userMobileNumber

                });
            }
            if (data.endLat.Trim() != null & data.endLat.Trim() != "")
            {
                userLocation.Add(new SBALUserLocationMapView()
                {
                    userName = user.userName,
                    date = Convert.ToDateTime(data.daDate).ToString("dd/MM/yyyy"),
                    time = data.endTime,
                    address = Address(data.endLat + "," + data.endLong),
                    lat = data.endLat,
                    log = data.endLong,
                    vehcileNumber = data.vehicleNumber,
                    userMobile = user.userMobileNumber
                });
            }
            return userLocation;
        }

        //public List<SBALUserLocationMapView> GetUserAttenRoute(int daId)
        //{
        //    List<SBALUserLocationMapView> userLocation = new List<SBALUserLocationMapView>();
        //    DateTime newdate = DateTime.Now.Date;
        //    var datt = newdate;

        //    var att = db.Daily_Attendance.Where(c => c.daID == daId).FirstOrDefault();

        //    string Time = att.startTime;
        //    DateTime date = DateTime.Parse(Time, System.Globalization.CultureInfo.CurrentCulture);
        //    string t = date.ToString("hh:mm:ss tt");
        //    string dt = Convert.ToDateTime(att.daDate).ToString("MM/dd/yyyy");
        //    DateTime? fdate = Convert.ToDateTime(dt + " " + t);
        //    DateTime? edate;
        //    if (att.endTime == "" | att.endTime == null)
        //    {
        //        edate = DateTime.Now;
        //    }
        //    else
        //    {
        //        string Time2 = att.endTime;
        //        DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
        //        string t2 = date2.ToString("hh:mm:ss tt");
        //        string dt2 = Convert.ToDateTime(att.daEndDate).ToString("MM/dd/yyyy");
        //        edate = Convert.ToDateTime(dt2 + " " + t2);
        //    }

        //    var data = db.Locations.Where(c => c.userId == att.userId & c.datetime >= fdate & c.datetime <= edate).ToList();

        //    foreach (var x in data)
        //    {

        //        if (x.type == 1)
        //        {
        //            string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");
        //            string tim = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");
        //            var userName = db.UserMasters.Where(c => c.userId == att.userId).FirstOrDefault();

        //            var gcd = db.GarbageCollectionDetails.Where(c => (c.userId == x.userId & c.houseId != null) & EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(x.datetime)).FirstOrDefault();

        //            var house = db.HouseMasters.Where(c => c.houseId == gcd.houseId).FirstOrDefault();

        //            userLocation.Add(new SBALUserLocationMapView()
        //            {
        //                userName = userName.userName,
        //                date = dat,
        //                time = tim,
        //                lat = x.lat,
        //                log = x.@long,
        //                vehcileNumber = att.vehicleNumber,
        //                HouseOwnerName = house.houseOwner,
        //                OwnerMobileNo = house.houseOwnerMobile,
        //                HouseId = house.ReferanceId,
        //                HouseAddress = house.houseAddress,
        //                type = Convert.ToInt32(x.type),
        //            });

        //        }

        //        else
        //        {
        //            string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");
        //            string tim = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");
        //            var userName = db.UserMasters.Where(c => c.userId == att.userId).FirstOrDefault();

        //            //   var gcd = db.GarbageCollectionDetails.Where(c => c.userId == x.userId & EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(x.datetime)).FirstOrDefault();

        //            //var house = db.HouseMasters.Where(c => c.houseId == gcd.houseId).FirstOrDefault();

        //            userLocation.Add(new SBALUserLocationMapView()
        //            {
        //                userName = userName.userName,
        //                date = dat,
        //                time = tim,
        //                lat = x.lat,
        //                log = x.@long,
        //                vehcileNumber = att.vehicleNumber,
        //                HouseOwnerName = "",
        //                OwnerMobileNo = "",
        //                HouseId = "",
        //                address = x.address,
        //                type = 0,
        //            });

        //        }
        //    }
        //    return userLocation;
        //}

        //public list<sbaluserlocationmapview> getuserattenroute(int daid)
        //{
        //    list<sbaluserlocationmapview> userlocation = new list<sbaluserlocationmapview>();
        //    datetime newdate = datetime.now.date;
        //    var datt = newdate;
        //    var att = db.daily_attendance.where(c => c.daid == daid).firstordefault();
        //    string time = att.starttime;
        //    datetime date = datetime.parse(time, system.globalization.cultureinfo.currentculture);
        //    string t = date.tostring("hh:mm:ss tt");
        //    string dt = convert.todatetime(att.dadate).tostring("mm/dd/yyyy");
        //    datetime? fdate = convert.todatetime(dt + " " + t);
        //    datetime? edate;
        //    if (att.endtime == "" | att.endtime == null)
        //    {
        //        edate = datetime.now;
        //    }
        //    else {
        //        string time2 = att.endtime;
        //        datetime date2 = datetime.parse(time2, system.globalization.cultureinfo.currentculture);
        //        string t2 = date2.tostring("hh:mm:ss tt");
        //        string dt2 = convert.todatetime(att.daenddate).tostring("mm/dd/yyyy");
        //        edate = convert.todatetime(dt2 + " " + t2);
        //    }
        //    var data = db.locations.where(c => c.userid == att.userid & c.datetime >= fdate & c.datetime <= edate).tolist();
        //    //foreach (var x in data)
        //    //{
        //    //    string dat = convert.todatetime(x.datetime).tostring("dd/mm/yyyy");
        //    //    string tim = convert.todatetime(x.datetime).tostring("hh:mm tt");
        //    //    var username = db.usermasters.where(c => c.userid == att.userid).firstordefault();
        //    //    userlocation.add(new sbaluserlocationmapview()
        //    //    {
        //    //        username = username.username,
        //    //        date = dat,
        //    //        time = tim,
        //    //        lat = x.lat,
        //    //        log = x.@long,
        //    //        address = x.address,
        //    //        vehcilenumber = att.vehiclenumber,
        //    //        usermobile = username.usermobilenumber,
        //    //    });
        //    //}

        //    foreach (var x in data)
        //    {
        //        string dat = convert.todatetime(x.datetime).tostring("dd/mm/yyyy");
        //        string tim = convert.todatetime(x.datetime).tostring("hh:mm tt");
        //        var username = db.usermasters.where(c => c.userid == att.userid).firstordefault();

        //        var gcd = db.garbagecollectiondetails.where(c => c.userid == x.userid & entityfunctions.truncatetime(c.gcdate) == entityfunctions.truncatetime(x.datetime)).firstordefault();


        //        var house = db.housemasters.where(c => c.houseid == gcd.houseid).firstordefault();

        //        userlocation.add(new sbaluserlocationmapview()
        //        {
        //            username = username.username,
        //            date = dat,
        //            time = tim,
        //            lat = x.lat,
        //            log = x.@long,
        //            vehcilenumber = att.vehiclenumber,
        //            houseownername = house.houseowner,
        //            ownermobileno = house.houseownermobile,
        //            houseid = house.referanceid,
        //            houseaddress = house.houseaddress
        //        });
        //    }


        //    return userlocation;
        //}




        public List<SBALUserLocationMapView> GetUserAttenRoute(int daId)
        {
            //var lst = GetUserAttenRoute1(daId).Result;
            List<SBALUserLocationMapView> userLocation = new List<SBALUserLocationMapView>();
            DateTime newdate = DateTime.Now.Date;
            var datt = newdate;
            var att = db.Daily_Attendance.Where(c => c.daID == daId).FirstOrDefault();

            var useridnew = db.Daily_Attendance.Where(c => c.userId == att.userId && c.daDate == att.daDate).FirstOrDefault();


            string Time = useridnew.startTime;
            DateTime date = DateTime.Parse(Time, System.Globalization.CultureInfo.CurrentCulture);
            string t = date.ToString("hh:mm:ss tt");
            string dt = Convert.ToDateTime(att.daDate).ToString("MM/dd/yyyy");
            DateTime? fdate = Convert.ToDateTime(dt + " " + t);
            DateTime? edate;
            if (att.endTime == "" | att.endTime == null)
            {
                edate = DateTime.Now;
            }
            else
            {
                string Time2 = att.endTime;
                DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                string t2 = date2.ToString("hh:mm:ss tt");
                string dt2 = Convert.ToDateTime(att.daEndDate).ToString("MM/dd/yyyy");
                edate = Convert.ToDateTime(dt2 + " " + t2);
            }
            var data = db.Locations.Where(c => c.userId == att.userId & c.datetime >= fdate & c.datetime <= edate & c.type == null).ToList();


            foreach (var x in data)
            {

                string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");
                string tim = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");
                var userName = db.UserMasters.Where(c => c.userId == att.userId).FirstOrDefault();

                userLocation.Add(new SBALUserLocationMapView()
                {
                    userId = userName.userId,
                    userName = userName.userName,
                    datetime = Convert.ToDateTime(x.datetime).ToString("HH:mm"),
                    date = dat,
                    time = tim,
                    lat = x.lat,
                    log = x.@long,
                    address = checkNull(x.address).Replace("Unnamed Road, ", ""),
                    vehcileNumber = att.vehicleNumber,
                    userMobile = userName.userMobileNumber,
                    // type = Convert.ToInt32(x.type),

                });

            }

            return userLocation;
        }



        public async Task<List<SBALUserLocationMapView>> GetUserAttenRoute1(int daId)
        {
            List<SBALUserLocationMapView> userLocation = new List<SBALUserLocationMapView>();
            DateTime newdate = DateTime.Now.Date;
            var datt = newdate;
            var att = db.Daily_Attendance.Where(c => c.daID == daId).FirstOrDefault();

            var useridnew = db.Daily_Attendance.Where(c => c.userId == att.userId && c.daDate == att.daDate).FirstOrDefault();


            string Time = useridnew.startTime;
            DateTime date = DateTime.Parse(Time, System.Globalization.CultureInfo.CurrentCulture);
            string t = date.ToString("hh:mm:ss tt");
            string dt = Convert.ToDateTime(att.daDate).ToString("MM/dd/yyyy");
            DateTime? fdate = Convert.ToDateTime(dt + " " + t);
            DateTime? edate;
            if (att.endTime == "" | att.endTime == null)
            {
                edate = DateTime.Now;
            }
            else
            {
                string Time2 = att.endTime;
                DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                string t2 = date2.ToString("hh:mm:ss tt");
                string dt2 = Convert.ToDateTime(att.daEndDate).ToString("MM/dd/yyyy");
                edate = Convert.ToDateTime(dt2 + " " + t2);
            }
            var data = db.Locations.Where(c => c.userId == att.userId & c.datetime >= fdate & c.datetime <= edate & c.type == null).ToList();

            //Start code for Emp route scan 
            List<coordinates> lstResult = new List<coordinates>();
            List<coordinates> lstPoints = new List<coordinates>();
            int iSize = 11;
            foreach (var x in data)
            {
                coordinates p = new coordinates();
                p.lat = Convert.ToDouble(x.lat);
                p.lng = Convert.ToDouble(x.@long);
                lstPoints.Add(p);
            }
            List<List<coordinates>> lstPP = new List<List<coordinates>>();
            List<coordinates> subList = new List<coordinates>();

            string baseURL = "https://maps.googleapis.com/maps/api/directions/xml";
            string apiKey = "AIzaSyB7w-l0RWEUVqhXyshtsgdkqJDNxaKQ5lY";
            //var tasks = new List<List<coordinates>>();


            for (int j = 0; j < lstPoints.Count - 1; j++)
            {
                coordinates source = new coordinates();
                coordinates dest = new coordinates();
                List<coordinates> lstp = new List<coordinates>();
                string strWayPoints = string.Empty;
                //string strSubURL = string.Empty;
                string mainURL = string.Empty;
                source = lstPoints[j];
                dest = lstPoints[j + 1];
                if (source.lat != dest.lat && source.lng != dest.lng)
                {
                    lstp.Add(source);
                    mainURL = $"{baseURL}?origin={HttpUtility.UrlEncode(source.lat + "," + source.lng)}&destination={HttpUtility.UrlEncode(dest.lat + "," + dest.lng)}&key={apiKey}";

                    List<coordinates> lstTemp = await GetAllCoordinate(mainURL).ConfigureAwait(false);
                    if (lstTemp.Count > 0)
                    {
                        lstp.AddRange(lstTemp);
                    }

                    lstp.Add(dest);
                }
                if (lstp.Count > 0)
                {
                    //lstResult.AddRange(lstp);
                    lstPP.Add(lstp);

                }

            }


            string LineString = string.Empty;
            string ResultLine = string.Empty;
            List<string> lstLine = new List<string>();
            foreach (var line in lstPP)
            {
                List<string> lstP = new List<string>();
                foreach (var p in line)
                {
                    lstP.Add(p.lng.ToString() + " " + p.lat.ToString());
                }
                LineString = string.Join(",", lstP);
                lstLine.Add(LineString);
            }
            List<SP_CalcMinRouteDist_Result> resultLst = new List<SP_CalcMinRouteDist_Result>();
            db.Database.CommandTimeout = 6000;
            //foreach (var strLine in lstLine)
            //{
            //    var houselst = db.SP_CalcMinRouteDist(strLine, 20).ToList();
            //    resultLst.AddRange(houselst);
            //}

            List<Task> tasks = new List<Task>();
            int counter = 0;

            //db.Database.CommandTimeout = 6000;
            foreach (var strLine in lstLine)
            {
                var strLine1 = strLine;
                Task t1 = Task.Run(() =>
                {
                    using (var db1 = new DevChildSwachhBharatNagpurEntities(AppID))
                    {
                        db1.Database.CommandTimeout = 6000;
                        var houselst = db1.SP_CalcMinRouteDist(strLine1, 20).ToList();
                        resultLst.AddRange(houselst);
                    }
                });
                tasks.Add(t1);
                counter++;
                if (tasks.Count == 10)
                {
                    await Task.WhenAll(tasks);
                    tasks.Clear();
                }
            }


            if (tasks.Any())
            {
                await Task.WhenAll(tasks);
                tasks.Clear();
            }

            //Task.WaitAll(tasks.ToArray());



            var houseList = resultLst.Select(a => a.houseId).Distinct();
            //ResultLine = string.Join(";", lstLine);
            //db.Database.CommandTimeout = 6000;
            //var houselst = db.SP_CalcMinRouteDist(ResultLine, 20).ToList();
            //End code for Emp route scan 

            foreach (var x in data)
            {

                string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");
                string tim = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");
                var userName = db.UserMasters.Where(c => c.userId == att.userId).FirstOrDefault();

                userLocation.Add(new SBALUserLocationMapView()
                {
                    userId = userName.userId,
                    userName = userName.userName,
                    datetime = Convert.ToDateTime(x.datetime).ToString("HH:mm"),
                    date = dat,
                    time = tim,
                    lat = x.lat,
                    log = x.@long,
                    address = checkNull(x.address).Replace("Unnamed Road, ", ""),
                    vehcileNumber = att.vehicleNumber,
                    userMobile = userName.userMobileNumber,
                    // type = Convert.ToInt32(x.type),

                });

            }

            return userLocation;
        }


        public async Task<List<coordinates>> GetAllCoordinate(string mainURL)
        {

            List<coordinates> lstResult = new List<coordinates>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(mainURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/xml"));

                    var response = await client.GetAsync(mainURL).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        using (var sr = new System.IO.StreamReader(await response.Content.ReadAsStreamAsync()))
                        {
                            XDocument xmlDoc = new XDocument();
                            xmlDoc = XDocument.Parse(sr.ReadToEnd());
                            string status = xmlDoc.Root.Element("status").Value;
                            if (status == "OK")
                            {
                                foreach (XElement rt in xmlDoc.Root.Descendants("route"))
                                {
                                    foreach (XElement lg in rt.Descendants("leg"))
                                    {
                                        foreach (XElement st in lg.Descendants("step"))
                                        {
                                            coordinates p = new coordinates();
                                            p.lat = Convert.ToDouble(st.Element("start_location").Element("lat").Value);
                                            p.lng = Convert.ToDouble(st.Element("start_location").Element("lng").Value);
                                            lstResult.Add(p);
                                            p.lat = Convert.ToDouble(st.Element("end_location").Element("lat").Value);
                                            p.lng = Convert.ToDouble(st.Element("end_location").Element("lng").Value);
                                            lstResult.Add(p);
                                        }
                                    }
                                }
                            }
                        }


                    }


                }
            }
            catch (Exception ex)
            {
                return lstResult;
            }
            return lstResult;


        }



        // Added By Saurabh (11 July 2019)
        public List<SBALUserLocationMapView> GetHouseAttenRoute(int daId, int areaid)
        {
            List<SBALUserLocationMapView> userLocation = new List<SBALUserLocationMapView>();
            DateTime newdate = DateTime.Now.Date;
            var datt = newdate;
            var att = db.Daily_Attendance.Where(c => c.daID == daId).FirstOrDefault();

            var useridnew = db.Daily_Attendance.Where(c => c.userId == att.userId && c.daDate == att.daDate).FirstOrDefault();
            string Time = useridnew.startTime;
            //string Time = att.startTime;
            DateTime date = DateTime.Parse(Time, System.Globalization.CultureInfo.CurrentCulture);
            string t = date.ToString("hh:mm:ss tt");
            string dt = Convert.ToDateTime(att.daDate).ToString("MM/dd/yyyy");
            DateTime? fdate = Convert.ToDateTime(dt + " " + t);
            DateTime? edate;
            if (att.endTime == "" | att.endTime == null)
            {
                edate = DateTime.Now;
            }
            else
            {
                string Time2 = att.endTime;
                DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                string t2 = date2.ToString("hh:mm:ss tt");
                string dt2 = Convert.ToDateTime(att.daEndDate).ToString("MM/dd/yyyy");
                edate = Convert.ToDateTime(dt2 + " " + t2);
            }
            var data = db.Locations.Where(c => c.userId == att.userId & c.datetime >= fdate & c.datetime <= edate & c.type == 1).OrderByDescending(a => a.datetime).ToList();

            foreach (var x in data)
            {
                if (x.type == 1)
                {

                    // string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");
                    //string tim = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");
                    var userName = db.UserMasters.Where(c => c.userId == att.userId).FirstOrDefault();
                    //var gcd = db.GarbageCollectionDetails.Where(c => (c.userId == x.userId & c.houseId != null) & EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(x.datetime)).FirstOrDefault();

                    //var gcd = db.GarbageCollectionDetails.Where(c => (c.userId == x.userId & c.houseId != null) & EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(x.datetime)).OrderBy(c => c.gcDate).ToList();//.ToList();

                    var gcd = db.GarbageCollectionDetails.Where(c => (c.userId == x.userId & (c.houseId != null || c.dyId != null)) & EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(x.datetime)).OrderBy(c => c.gcId).ToList();//.ToList();


                    foreach (var d in gcd)
                    {
                        //DateTime dt = DateTime.Parse(x.gcDate == null ? DateTime.Now.ToString() : x.gcDate.ToString());
                        string dat = Convert.ToDateTime(d.gcDate).ToString("dd/MM/yyyy");
                        string tim = Convert.ToDateTime(d.gcDate).ToString("hh:mm tt");
                        if (d.houseId != null)
                        {
                            if (areaid != 0)
                            {
                                var house = db.HouseMasters.Where(c => c.houseId == d.houseId & c.AreaId == areaid).FirstOrDefault();
                                if (house != null)
                                {
                                    userLocation.Add(new SBALUserLocationMapView()
                                    {
                                        userId = userName.userId,
                                        userName = userName.userName,
                                        datetime = Convert.ToDateTime(d.gcDate).ToString("HH:mm"),
                                        date = dat,
                                        time = tim,
                                        lat = d.Lat,
                                        log = d.Long,
                                        address = x.address,
                                        vehcileNumber = att.vehicleNumber,
                                        userMobile = userName.userMobileNumber,
                                        type = Convert.ToInt32(x.type),
                                        HouseId = house.ReferanceId,
                                        HouseAddress = (house.houseAddress == null ? "" : house.houseAddress.Replace("Unnamed Road, ", "")),
                                        HouseOwnerName = house.houseOwner,
                                        OwnerMobileNo = house.houseOwnerMobile,
                                        WasteType = d.garbageType.ToString(),
                                        gpBeforImage = d.gpBeforImage,
                                        gpAfterImage = d.gpAfterImage,
                                        ZoneList = ListZone(),

                                    });
                                }

                            }
                            else
                            {
                                var house = db.HouseMasters.Where(c => c.houseId == d.houseId).FirstOrDefault();
                                userLocation.Add(new SBALUserLocationMapView()
                                {
                                    userId = userName.userId,
                                    userName = userName.userName,
                                    datetime = Convert.ToDateTime(d.gcDate).ToString("HH:mm"),
                                    date = dat,
                                    time = tim,
                                    lat = d.Lat,
                                    log = d.Long,
                                    address = x.address,
                                    vehcileNumber = att.vehicleNumber,
                                    userMobile = userName.userMobileNumber,
                                    type = Convert.ToInt32(x.type),
                                    HouseId = house.ReferanceId,
                                    HouseAddress = (house.houseAddress == null ? "" : house.houseAddress.Replace("Unnamed Road, ", "")),
                                    HouseOwnerName = house.houseOwner,
                                    OwnerMobileNo = house.houseOwnerMobile,
                                    WasteType = d.garbageType.ToString(),
                                    gpBeforImage = d.gpBeforImage,
                                    gpAfterImage = d.gpAfterImage,
                                    ZoneList = ListZone(),

                                });
                            }


                        }
                        if (d.dyId != null)
                        {
                            if (areaid != 0)
                            {
                                var dump = db.DumpYardDetails.Where(c => c.dyId == d.dyId & c.areaId == areaid).FirstOrDefault();
                                if (dump != null)
                                {
                                    userLocation.Add(new SBALUserLocationMapView()
                                    {
                                        userId = userName.userId,
                                        userName = userName.userName,
                                        datetime = Convert.ToDateTime(d.gcDate).ToString("HH:mm"),
                                        date = dat,
                                        time = tim,
                                        lat = d.Lat,
                                        log = d.Long,
                                        address = x.address,
                                        vehcileNumber = att.vehicleNumber,
                                        userMobile = userName.userMobileNumber,
                                        type = Convert.ToInt32(x.type),
                                        DyId = dump.ReferanceId,
                                        DumpAddress = (dump.dyAddress == null ? "" : dump.dyAddress.Replace("Unnamed Road, ", "")),
                                        DumpYardName = dump.dyName,
                                        OwnerMobileNo = dump.dyNameMar,
                                        WasteType = d.garbageType.ToString(),
                                        gpBeforImage = d.gpBeforImage,
                                        gpAfterImage = d.gpAfterImage,
                                        DryWaste = d.totalDryWeight.ToString(),
                                        WetWaste = d.totalWetWeight.ToString(),
                                        TotWaste = d.totalGcWeight.ToString(),
                                        ZoneList = ListZone(),

                                    });
                                }

                            }
                            else
                            {
                                var dump = db.DumpYardDetails.Where(c => c.dyId == d.dyId).FirstOrDefault();
                                userLocation.Add(new SBALUserLocationMapView()
                                {
                                    userId = userName.userId,
                                    userName = userName.userName,
                                    datetime = Convert.ToDateTime(d.gcDate).ToString("HH:mm"),
                                    date = dat,
                                    time = tim,
                                    lat = d.Lat,
                                    log = d.Long,
                                    address = x.address,
                                    vehcileNumber = att.vehicleNumber,
                                    userMobile = userName.userMobileNumber,
                                    type = Convert.ToInt32(x.type),
                                    DyId = dump.ReferanceId,
                                    DumpAddress = (dump.dyAddress == null ? "" : dump.dyAddress.Replace("Unnamed Road, ", "")),
                                    DumpYardName = dump.dyName,
                                    OwnerMobileNo = dump.dyNameMar,
                                    WasteType = d.garbageType.ToString(),
                                    gpBeforImage = d.gpBeforImage,
                                    gpAfterImage = d.gpAfterImage,
                                    DryWaste = d.totalDryWeight.ToString(),
                                    WetWaste = d.totalWetWeight.ToString(),
                                    TotWaste = d.totalGcWeight.ToString(),
                                    ZoneList = ListZone(),

                                });
                            }


                        }







                    }
                    break;
                }

            }




            return userLocation;
        }

        public List<SBALUserLocationMapView> GetDumpAttenRoute(int daId)
        {
            List<SBALUserLocationMapView> userLocation = new List<SBALUserLocationMapView>();
            DateTime newdate = DateTime.Now.Date;
            var datt = newdate;
            var att = db.Daily_Attendance.Where(c => c.daID == daId).FirstOrDefault();

            var useridnew = db.Daily_Attendance.Where(c => c.userId == att.userId && c.daDate == att.daDate).FirstOrDefault();
            string Time = useridnew.startTime;
            //string Time = att.startTime;
            DateTime date = DateTime.Parse(Time, System.Globalization.CultureInfo.CurrentCulture);
            string t = date.ToString("hh:mm:ss tt");
            string dt = Convert.ToDateTime(att.daDate).ToString("MM/dd/yyyy");
            DateTime? fdate = Convert.ToDateTime(dt + " " + t);
            DateTime? edate;
            if (att.endTime == "" | att.endTime == null)
            {
                edate = DateTime.Now;
            }
            else
            {
                string Time2 = att.endTime;
                DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                string t2 = date2.ToString("hh:mm:ss tt");
                string dt2 = Convert.ToDateTime(att.daEndDate).ToString("MM/dd/yyyy");
                edate = Convert.ToDateTime(dt2 + " " + t2);
            }
            var data = db.Locations.Where(c => c.userId == att.userId & c.datetime >= fdate & c.datetime <= edate & c.type == 1).OrderByDescending(a => a.datetime).ToList();

            foreach (var x in data)
            {
                if (x.type == 1)
                {

                    // string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");
                    //string tim = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");
                    var userName = db.UserMasters.Where(c => c.userId == att.userId).FirstOrDefault();
                    //var gcd = db.GarbageCollectionDetails.Where(c => (c.userId == x.userId & c.houseId != null) & EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(x.datetime)).FirstOrDefault();

                    //var gcd = db.GarbageCollectionDetails.Where(c => (c.userId == x.userId & c.houseId != null) & EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(x.datetime)).OrderBy(c => c.gcDate).ToList();//.ToList();

                    var gcd = db.GarbageCollectionDetails.Where(c => (c.userId == x.userId & (c.vqrid != null || c.dyId != null)) & EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(x.datetime)).OrderBy(c => c.gcId).ToList();//.ToList();


                    foreach (var d in gcd)
                    {
                        //DateTime dt = DateTime.Parse(x.gcDate == null ? DateTime.Now.ToString() : x.gcDate.ToString());
                        string dat = Convert.ToDateTime(d.gcDate).ToString("dd/MM/yyyy");
                        string tim = Convert.ToDateTime(d.gcDate).ToString("hh:mm tt");
                        if (d.vqrid != null)
                        {

                            var house = db.Vehical_QR_Master.Where(c => c.vqrId == d.vqrid).FirstOrDefault();
                            userLocation.Add(new SBALUserLocationMapView()
                            {
                                userId = userName.userId,
                                userName = userName.userName,
                                datetime = Convert.ToDateTime(d.gcDate).ToString("HH:mm"),
                                date = dat,
                                time = tim,
                                lat = d.Lat,
                                log = d.Long,
                                address = x.address,
                                vehcileNumber = att.vehicleNumber,
                                userMobile = userName.userMobileNumber,
                                type = Convert.ToInt32(x.type),
                                HouseId = house.ReferanceId,
                                HouseOwnerName = house.VehicalNumber,
                                WasteType = d.garbageType.ToString(),
                                gpBeforImage = d.gpBeforImage,
                                gpAfterImage = d.gpAfterImage,
                                ZoneList = ListZone(),

                            });



                        }


                    }
                    break;
                }

            }




            return userLocation;
        }

        public List<SelectListItem> ListBeatMapArea(int daId, int areaid)
        {
            List<SelectListItem> Area = new List<SelectListItem>();
            List<List<coordinates>> lstPoly = new List<List<coordinates>>();

            var att = db.Daily_Attendance.Where(c => c.daID == daId).FirstOrDefault();
            if (att != null)
            {
                var userName = db.UserMasters.Where(c => c.userId == att.userId).FirstOrDefault();
                EmpBeatMapVM ebm = GetEmpBeatMapByUserId(userName.userId);
                lstPoly = ebm.ebmLatLong;
                if (lstPoly != null && lstPoly.Count > 0)
                {
                    for (var i = 0; i < lstPoly.Count; i++)
                    {
                        SelectListItem item = new SelectListItem();
                        item.Text = "Area-" + (i + 1).ToString();
                        item.Value = i.ToString();
                        Area.Add(item);
                    }

                }
            }
            return Area;
        }

        public HouseAttenRouteVM GetBeatHouseAttenRoute(int daId, int areaid, int polyId)
        {
            HouseAttenRouteVM houseAtten = new HouseAttenRouteVM();
            List<SBALUserLocationMapView> userLocation = new List<SBALUserLocationMapView>();
            List<List<coordinates>> lstPoly = new List<List<coordinates>>();
            int houseCount = 0;
            //List<coordinates> poly = new List<coordinates>();
            DateTime newdate = DateTime.Now.Date;
            var datt = newdate;
            var att = db.Daily_Attendance.Where(c => c.daID == daId).FirstOrDefault();

            var useridnew = db.Daily_Attendance.Where(c => c.userId == att.userId && c.daDate == att.daDate).FirstOrDefault();
            string Time = useridnew.startTime;
            //string Time = att.startTime;
            DateTime date = DateTime.Parse(Time, System.Globalization.CultureInfo.CurrentCulture);
            string t = date.ToString("hh:mm:ss tt");
            string dt = Convert.ToDateTime(att.daDate).ToString("MM/dd/yyyy");
            DateTime? fdate = Convert.ToDateTime(dt + " " + t);
            DateTime? edate;
            if (att.endTime == "" | att.endTime == null)
            {
                edate = DateTime.Now;
            }
            else
            {
                string Time2 = att.endTime;
                DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                string t2 = date2.ToString("hh:mm:ss tt");
                string dt2 = Convert.ToDateTime(att.daEndDate).ToString("MM/dd/yyyy");
                edate = Convert.ToDateTime(dt2 + " " + t2);
            }
            var userName = db.UserMasters.Where(c => c.userId == att.userId).FirstOrDefault();
            EmpBeatMapVM ebm = GetEmpBeatMapByUserId(userName.userId);
            lstPoly = ebm.ebmLatLong;
            //if (lstPoly != null && lstPoly.Count > polyId)
            //{
            //    poly = lstPoly[polyId];
            //}
            var Hdata = db.SP_HouseOnMapDetailsForEmpBitMap(userName.EmployeeType ?? "W").ToList();
            foreach (var h in Hdata)
            {
               

                coordinates p = new coordinates()
                {
                    lat = Convert.ToDouble(h.houseLat),
                    lng = Convert.ToDouble(h.houseLong)
                };
                if (lstPoly != null && lstPoly.Count > 0)
                {
                    foreach (var poly in lstPoly)
                    {
                        if (IsPointInPolygon(poly, p))
                        {
                            houseCount++;
                        }
                    }
                }

            }
            var data = db.Locations.Where(c => c.userId == att.userId & c.datetime >= fdate & c.datetime <= edate & c.type == 1).OrderByDescending(a => a.datetime).ToList();

            foreach (var x in data)
            {
                if (x.type == 1)
                {

                    var gcd = db.GarbageCollectionDetails.Where(c => (c.userId == x.userId & (c.houseId != null || c.dyId != null)) & EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(x.datetime)).OrderBy(c => c.gcId).ToList();//.ToList();


                    foreach (var d in gcd)
                    {
                        //DateTime dt = DateTime.Parse(x.gcDate == null ? DateTime.Now.ToString() : x.gcDate.ToString());
                        string dat = Convert.ToDateTime(d.gcDate).ToString("dd/MM/yyyy");
                        string tim = Convert.ToDateTime(d.gcDate).ToString("hh:mm tt");
                        bool bIsIn = false;

                        coordinates p = new coordinates()
                        {
                            lat = Convert.ToDouble(d.Lat),
                            lng = Convert.ToDouble(d.Long)
                        };
                        if (lstPoly != null && lstPoly.Count > 0)
                        {
                            foreach (var poly in lstPoly)
                            {
                                if (IsPointInPolygon(poly, p))
                                {
                                    bIsIn = true;
                                }
                            }
                        }
                        if (d.houseId != null)
                        {
                            if (areaid != 0)
                            {
                                var house = db.HouseMasters.Where(c => c.houseId == d.houseId & c.AreaId == areaid).FirstOrDefault();
                                if (house != null)
                                {
                                    userLocation.Add(new SBALUserLocationMapView()
                                    {
                                        userId = userName.userId,
                                        userName = userName.userName,
                                        datetime = Convert.ToDateTime(d.gcDate).ToString("HH:mm"),
                                        date = dat,
                                        time = tim,
                                        lat = d.Lat,
                                        log = d.Long,
                                        address = x.address,
                                        vehcileNumber = att.vehicleNumber,
                                        userMobile = userName.userMobileNumber,
                                        type = Convert.ToInt32(x.type),
                                        HouseId = house.ReferanceId,
                                        HouseAddress = (house.houseAddress == null ? "" : house.houseAddress.Replace("Unnamed Road, ", "")),
                                        HouseOwnerName = house.houseOwner,
                                        OwnerMobileNo = house.houseOwnerMobile,
                                        WasteType = d.garbageType.ToString(),
                                        gpBeforImage = d.gpBeforImage,
                                        gpAfterImage = d.gpAfterImage,
                                        ZoneList = ListZone(),
                                        //IsIn = IsPointInPolygon(poly, p)
                                        IsIn = bIsIn

                                    });
                                }

                            }
                            else
                            {
                                var house = db.HouseMasters.Where(c => c.houseId == d.houseId).FirstOrDefault();
                                userLocation.Add(new SBALUserLocationMapView()
                                {
                                    userId = userName.userId,
                                    userName = userName.userName,
                                    datetime = Convert.ToDateTime(d.gcDate).ToString("HH:mm"),
                                    date = dat,
                                    time = tim,
                                    lat = d.Lat,
                                    log = d.Long,
                                    address = x.address,
                                    vehcileNumber = att.vehicleNumber,
                                    userMobile = userName.userMobileNumber,
                                    type = Convert.ToInt32(x.type),
                                    HouseId = house.ReferanceId,
                                    HouseAddress = (house.houseAddress == null ? "" : house.houseAddress.Replace("Unnamed Road, ", "")),
                                    HouseOwnerName = house.houseOwner,
                                    OwnerMobileNo = house.houseOwnerMobile,
                                    WasteType = d.garbageType.ToString(),
                                    gpBeforImage = d.gpBeforImage,
                                    gpAfterImage = d.gpAfterImage,
                                    ZoneList = ListZone(),
                                    //IsIn = IsPointInPolygon(poly, p)
                                    IsIn = bIsIn
                                });
                            }


                        }
                        if (d.dyId != null)
                        {
                            if (areaid != 0)
                            {
                                var dump = db.DumpYardDetails.Where(c => c.dyId == d.dyId & c.areaId == areaid).FirstOrDefault();
                                if (dump != null)
                                {
                                    userLocation.Add(new SBALUserLocationMapView()
                                    {
                                        userId = userName.userId,
                                        userName = userName.userName,
                                        datetime = Convert.ToDateTime(d.gcDate).ToString("HH:mm"),
                                        date = dat,
                                        time = tim,
                                        lat = d.Lat,
                                        log = d.Long,
                                        address = x.address,
                                        vehcileNumber = att.vehicleNumber,
                                        userMobile = userName.userMobileNumber,
                                        type = Convert.ToInt32(x.type),
                                        DyId = dump.ReferanceId,
                                        DumpAddress = (dump.dyAddress == null ? "" : dump.dyAddress.Replace("Unnamed Road, ", "")),
                                        DumpYardName = dump.dyName,
                                        OwnerMobileNo = dump.dyNameMar,
                                        WasteType = d.garbageType.ToString(),
                                        gpBeforImage = d.gpBeforImage,
                                        gpAfterImage = d.gpAfterImage,
                                        DryWaste = d.totalDryWeight.ToString(),
                                        WetWaste = d.totalWetWeight.ToString(),
                                        TotWaste = d.totalGcWeight.ToString(),
                                        ZoneList = ListZone(),
                                        //IsIn = IsPointInPolygon(poly, p)
                                        IsIn = bIsIn

                                    });
                                }

                            }
                            else
                            {
                                var dump = db.DumpYardDetails.Where(c => c.dyId == d.dyId).FirstOrDefault();
                                userLocation.Add(new SBALUserLocationMapView()
                                {
                                    userId = userName.userId,
                                    userName = userName.userName,
                                    datetime = Convert.ToDateTime(d.gcDate).ToString("HH:mm"),
                                    date = dat,
                                    time = tim,
                                    lat = d.Lat,
                                    log = d.Long,
                                    address = x.address,
                                    vehcileNumber = att.vehicleNumber,
                                    userMobile = userName.userMobileNumber,
                                    type = Convert.ToInt32(x.type),
                                    DyId = dump.ReferanceId,
                                    DumpAddress = (dump.dyAddress == null ? "" : dump.dyAddress.Replace("Unnamed Road, ", "")),
                                    DumpYardName = dump.dyName,
                                    OwnerMobileNo = dump.dyNameMar,
                                    WasteType = d.garbageType.ToString(),
                                    gpBeforImage = d.gpBeforImage,
                                    gpAfterImage = d.gpAfterImage,
                                    DryWaste = d.totalDryWeight.ToString(),
                                    WetWaste = d.totalWetWeight.ToString(),
                                    TotWaste = d.totalGcWeight.ToString(),
                                    ZoneList = ListZone(),
                                    //IsIn = IsPointInPolygon(poly, p)
                                    IsIn = bIsIn

                                });
                            }


                        }







                    }
                    break;
                }

            }


            houseAtten.poly = lstPoly;
            houseAtten.lstUserLocation = userLocation;
            houseAtten.TotalHouseCount = houseCount;
            return houseAtten;
        }


        public EmpBeatMapCountVM GetbeatMapCount(int daId, int areaid, int polyId)
        {
            EmpBeatMapCountVM ebmCount = new EmpBeatMapCountVM();
            List<coordinates> houseCord = new List<coordinates>();
            List<List<coordinates>> lstPoly = new List<List<coordinates>>();

            List<coordinates> poly = new List<coordinates>();
            List<SBALUserLocationMapView> userLocation = new List<SBALUserLocationMapView>();
            int innerCount = 0;
            int outerCount = 0;
            DateTime newdate = DateTime.Now.Date;
            var datt = newdate;
            var att = db.Daily_Attendance.Where(c => c.daID == daId).FirstOrDefault();

            var useridnew = db.Daily_Attendance.Where(c => c.userId == att.userId && c.daDate == att.daDate).FirstOrDefault();
            string Time = useridnew.startTime;
            //string Time = att.startTime;
            DateTime date = DateTime.Parse(Time, System.Globalization.CultureInfo.CurrentCulture);
            string t = date.ToString("hh:mm:ss tt");
            string dt = Convert.ToDateTime(att.daDate).ToString("MM/dd/yyyy");
            DateTime? fdate = Convert.ToDateTime(dt + " " + t);
            DateTime? edate;
            if (att.endTime == "" | att.endTime == null)
            {
                edate = DateTime.Now;
            }
            else
            {
                string Time2 = att.endTime;
                DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                string t2 = date2.ToString("hh:mm:ss tt");
                string dt2 = Convert.ToDateTime(att.daEndDate).ToString("MM/dd/yyyy");
                edate = Convert.ToDateTime(dt2 + " " + t2);
            }
            var userName = db.UserMasters.Where(c => c.userId == att.userId).FirstOrDefault();
            EmpBeatMapVM ebm = GetEmpBeatMapByUserId(userName.userId);
            lstPoly = ebm.ebmLatLong;
            if (lstPoly != null && lstPoly.Count > polyId)
            {
                poly = lstPoly[polyId];
                if (poly != null && poly.Count > 0)
                {
                    var data = db.Locations.Where(c => c.userId == att.userId & c.datetime >= fdate & c.datetime <= edate & c.type == 1).OrderByDescending(a => a.datetime).ToList();

                    foreach (var x in data)
                    {
                        if (x.type == 1)
                        {

                            var gcd = db.GarbageCollectionDetails.Where(c => (c.userId == x.userId & (c.houseId != null || c.dyId != null)) & EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(x.datetime)).OrderBy(c => c.gcId).ToList();//.ToList();


                            foreach (var d in gcd)
                            {
                                //DateTime dt = DateTime.Parse(x.gcDate == null ? DateTime.Now.ToString() : x.gcDate.ToString());
                                string dat = Convert.ToDateTime(d.gcDate).ToString("dd/MM/yyyy");
                                string tim = Convert.ToDateTime(d.gcDate).ToString("hh:mm tt");
                                if (d.houseId != null)
                                {
                                    if (areaid != 0)
                                    {
                                        var house = db.HouseMasters.Where(c => c.houseId == d.houseId & c.AreaId == areaid).FirstOrDefault();
                                        if (house != null)
                                        {
                                            coordinates p = new coordinates()
                                            {
                                                lat = Convert.ToDouble(d.Lat),
                                                lng = Convert.ToDouble(d.Long)
                                            };
                                            if (IsPointInPolygon(poly, p))
                                            {
                                                innerCount++;
                                            }
                                            else
                                            {
                                                outerCount++;
                                            }

                                        }

                                    }
                                    else
                                    {
                                        var house = db.HouseMasters.Where(c => c.houseId == d.houseId).FirstOrDefault();
                                        if (house != null)
                                        {
                                            coordinates p = new coordinates()
                                            {
                                                lat = Convert.ToDouble(d.Lat),
                                                lng = Convert.ToDouble(d.Long)
                                            };
                                            if (IsPointInPolygon(poly, p))
                                            {
                                                innerCount++;
                                            }
                                            else
                                            {
                                                outerCount++;
                                            }
                                        }

                                    }


                                }


                            }
                        }
                        break;
                    }

                }
            }
            ebmCount.poly = poly;
            ebmCount.innerCount = innerCount;
            ebmCount.outerCount = outerCount;
            return ebmCount;
        }
        public List<SBALUserLocationMapView> GetLiquidAttenRoute(int daId, int areaid)
        {
            List<SBALUserLocationMapView> userLocation = new List<SBALUserLocationMapView>();
            DateTime newdate = DateTime.Now.Date;
            var datt = newdate;
            var att = db.Daily_Attendance.Where(c => c.daID == daId).FirstOrDefault();
            string Time = att.startTime;
            DateTime date = DateTime.Parse(Time, System.Globalization.CultureInfo.CurrentCulture);
            string t = date.ToString("hh:mm:ss tt");
            string dt = Convert.ToDateTime(att.daDate).ToString("MM/dd/yyyy");
            DateTime? fdate = Convert.ToDateTime(dt + " " + t);
            DateTime? edate;
            if (att.endTime == "" | att.endTime == null)
            {
                edate = DateTime.Now;
            }
            else
            {
                string Time2 = att.endTime;
                DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                string t2 = date2.ToString("hh:mm:ss tt");
                string dt2 = Convert.ToDateTime(att.daEndDate).ToString("MM/dd/yyyy");
                edate = Convert.ToDateTime(dt2 + " " + t2);
            }
            var data = db.Locations.Where(c => c.userId == att.userId & c.datetime >= fdate & c.datetime <= edate & c.type == 1).OrderByDescending(a => a.datetime).ToList();

            foreach (var x in data)
            {
                if (x.type == 1)
                {

                    // string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");
                    //string tim = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");
                    var userName = db.UserMasters.Where(c => c.userId == att.userId).FirstOrDefault();
                    //var gcd = db.GarbageCollectionDetails.Where(c => (c.userId == x.userId & c.houseId != null) & EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(x.datetime)).FirstOrDefault();

                    //var gcd = db.GarbageCollectionDetails.Where(c => (c.userId == x.userId & c.houseId != null) & EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(x.datetime)).OrderBy(c => c.gcDate).ToList();//.ToList();

                    var gcd = db.GarbageCollectionDetails.Where(c => (c.userId == x.userId & (c.LWId != null || c.dyId != null)) & EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(x.datetime)).OrderBy(c => c.gcId).ToList();//.ToList();


                    foreach (var d in gcd)
                    {
                        //DateTime dt = DateTime.Parse(x.gcDate == null ? DateTime.Now.ToString() : x.gcDate.ToString());
                        string dat = Convert.ToDateTime(d.gcDate).ToString("dd/MM/yyyy");
                        string tim = Convert.ToDateTime(d.gcDate).ToString("hh:mm tt");
                        if (d.LWId != null)
                        {
                            if (areaid != 0)
                            {
                                var house = db.LiquidWasteDetails.Where(c => c.LWId == d.LWId & c.areaId == areaid).FirstOrDefault();
                                if (house != null)
                                {
                                    userLocation.Add(new SBALUserLocationMapView()
                                    {
                                        userName = userName.userName,
                                        datetime = Convert.ToDateTime(d.gcDate).ToString("HH:mm"),
                                        date = dat,
                                        time = tim,
                                        lat = d.Lat,
                                        log = d.Long,
                                        address = x.address,
                                        vehcileNumber = att.vehicleNumber,
                                        userMobile = userName.userMobileNumber,
                                        type = Convert.ToInt32(x.type),
                                        HouseId = house.ReferanceId,
                                        HouseAddress = (house.LWAddreLW == null ? "" : house.LWAddreLW.Replace("Unnamed Road, ", "")),
                                        HouseOwnerName = house.LWName,
                                        //OwnerMobileNo = house.houseOwnerMobile,
                                        WasteType = d.gcType.ToString(),
                                        gpBeforImage = d.gpBeforImage,
                                        gpAfterImage = d.gpAfterImage,
                                        ZoneList = ListZone(),

                                    });
                                }

                            }
                            else
                            {
                                var house = db.LiquidWasteDetails.Where(c => c.LWId == d.LWId).FirstOrDefault();
                                userLocation.Add(new SBALUserLocationMapView()
                                {
                                    userName = userName.userName,
                                    datetime = Convert.ToDateTime(d.gcDate).ToString("HH:mm"),
                                    date = dat,
                                    time = tim,
                                    lat = d.Lat,
                                    log = d.Long,
                                    address = x.address,
                                    vehcileNumber = att.vehicleNumber,
                                    userMobile = userName.userMobileNumber,
                                    type = Convert.ToInt32(x.type),
                                    HouseId = house.ReferanceId,
                                    HouseAddress = (house.LWAddreLW == null ? "" : house.LWAddreLW.Replace("Unnamed Road, ", "")),
                                    HouseOwnerName = house.LWName,
                                    //OwnerMobileNo = house.houseOwnerMobile,
                                    WasteType = d.gcType.ToString(),
                                    gpBeforImage = d.gpBeforImage,
                                    gpAfterImage = d.gpAfterImage,
                                    ZoneList = ListZone(),

                                });
                            }


                        }
                        //if (d.dyId != null)
                        //{
                        //    if (areaid != 0)
                        //    {
                        //        var dump = db.DumpYardDetails.Where(c => c.dyId == d.dyId & c.areaId == areaid).FirstOrDefault();
                        //        if (dump != null)
                        //        {
                        //            userLocation.Add(new SBALUserLocationMapView()
                        //            {
                        //                userName = userName.userName,
                        //                datetime = Convert.ToDateTime(d.gcDate).ToString("dd/MM/yyyy HH:mm"),
                        //                date = dat,
                        //                time = tim,
                        //                lat = d.Lat,
                        //                log = d.Long,
                        //                address = x.address,
                        //                vehcileNumber = att.vehicleNumber,
                        //                userMobile = userName.userMobileNumber,
                        //                type = Convert.ToInt32(x.type),
                        //                DyId = dump.ReferanceId,
                        //                DumpAddress = (dump.dyAddress == null ? "" : dump.dyAddress.Replace("Unnamed Road, ", "")),
                        //                DumpYardName = dump.dyName,
                        //                OwnerMobileNo = dump.dyNameMar,
                        //                WasteType = d.garbageType.ToString(),
                        //                gpBeforImage = d.gpBeforImage,
                        //                gpAfterImage = d.gpAfterImage,
                        //                DryWaste = d.totalDryWeight.ToString(),
                        //                WetWaste = d.totalWetWeight.ToString(),
                        //                TotWaste = d.totalGcWeight.ToString(),
                        //                ZoneList = ListZone(),

                        //            });
                        //        }

                        //    }
                        //    else
                        //    {
                        //        var dump = db.DumpYardDetails.Where(c => c.dyId == d.dyId).FirstOrDefault();
                        //        userLocation.Add(new SBALUserLocationMapView()
                        //        {
                        //            userName = userName.userName,
                        //            datetime = Convert.ToDateTime(d.gcDate).ToString("dd/MM/yyyy HH:mm"),
                        //            date = dat,
                        //            time = tim,
                        //            lat = d.Lat,
                        //            log = d.Long,
                        //            address = x.address,
                        //            vehcileNumber = att.vehicleNumber,
                        //            userMobile = userName.userMobileNumber,
                        //            type = Convert.ToInt32(x.type),
                        //            DyId = dump.ReferanceId,
                        //            DumpAddress = (dump.dyAddress == null ? "" : dump.dyAddress.Replace("Unnamed Road, ", "")),
                        //            DumpYardName = dump.dyName,
                        //            OwnerMobileNo = dump.dyNameMar,
                        //            WasteType = d.garbageType.ToString(),
                        //            gpBeforImage = d.gpBeforImage,
                        //            gpAfterImage = d.gpAfterImage,
                        //            DryWaste = d.totalDryWeight.ToString(),
                        //            WetWaste = d.totalWetWeight.ToString(),
                        //            TotWaste = d.totalGcWeight.ToString(),
                        //            ZoneList = ListZone(),

                        //        });
                        //    }


                        //}







                    }
                    break;
                }

            }




            return userLocation;
        }

        public List<SBALUserLocationMapView> GetStreetAttenRoute(int daId, int areaid)
        {
            List<SBALUserLocationMapView> userLocation = new List<SBALUserLocationMapView>();
            DateTime newdate = DateTime.Now.Date;
            var datt = newdate;
            var att = db.Daily_Attendance.Where(c => c.daID == daId).FirstOrDefault();
            string Time = att.startTime;
            DateTime date = DateTime.Parse(Time, System.Globalization.CultureInfo.CurrentCulture);
            string t = date.ToString("hh:mm:ss tt");
            string dt = Convert.ToDateTime(att.daDate).ToString("MM/dd/yyyy");
            DateTime? fdate = Convert.ToDateTime(dt + " " + t);
            DateTime? edate;
            if (att.endTime == "" | att.endTime == null)
            {
                edate = DateTime.Now;
            }
            else
            {
                string Time2 = att.endTime;
                DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                string t2 = date2.ToString("hh:mm:ss tt");
                string dt2 = Convert.ToDateTime(att.daEndDate).ToString("MM/dd/yyyy");
                edate = Convert.ToDateTime(dt2 + " " + t2);
            }
            var data = db.Locations.Where(c => c.userId == att.userId & c.datetime >= fdate & c.datetime <= edate & c.type == 1).OrderByDescending(a => a.datetime).ToList();

            foreach (var x in data)
            {
                if (x.type == 1)
                {

                    // string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");
                    //string tim = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");
                    var userName = db.UserMasters.Where(c => c.userId == att.userId).FirstOrDefault();
                    //var gcd = db.GarbageCollectionDetails.Where(c => (c.userId == x.userId & c.houseId != null) & EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(x.datetime)).FirstOrDefault();

                    //var gcd = db.GarbageCollectionDetails.Where(c => (c.userId == x.userId & c.houseId != null) & EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(x.datetime)).OrderBy(c => c.gcDate).ToList();//.ToList();

                    var gcd = db.GarbageCollectionDetails.Where(c => (c.userId == x.userId & (c.SSId != null || c.dyId != null)) & EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(x.datetime)).OrderBy(c => c.gcId).ToList();//.ToList();


                    foreach (var d in gcd)
                    {
                        //DateTime dt = DateTime.Parse(x.gcDate == null ? DateTime.Now.ToString() : x.gcDate.ToString());
                        string dat = Convert.ToDateTime(d.gcDate).ToString("dd/MM/yyyy");
                        string tim = Convert.ToDateTime(d.gcDate).ToString("hh:mm tt");
                        if (d.SSId != null)
                        {
                            if (areaid != 0)
                            {
                                var house = db.StreetSweepingDetails.Where(c => c.SSId == d.SSId & c.areaId == areaid).FirstOrDefault();
                                if (house != null)
                                {
                                    userLocation.Add(new SBALUserLocationMapView()
                                    {
                                        userName = userName.userName,
                                        datetime = Convert.ToDateTime(d.gcDate).ToString("HH:mm"),
                                        date = dat,
                                        time = tim,
                                        lat = d.Lat,
                                        log = d.Long,
                                        address = x.address,
                                        vehcileNumber = att.vehicleNumber,
                                        userMobile = userName.userMobileNumber,
                                        type = Convert.ToInt32(x.type),
                                        HouseId = house.ReferanceId,
                                        HouseAddress = (house.SSAddress == null ? "" : house.SSAddress.Replace("Unnamed Road, ", "")),
                                        HouseOwnerName = house.SSName,
                                        //OwnerMobileNo = house.houseOwnerMobile,
                                        WasteType = d.gcType.ToString(),
                                        gpBeforImage = d.gpBeforImage,
                                        gpAfterImage = d.gpAfterImage,
                                        ZoneList = ListZone(),

                                    });
                                }

                            }
                            else
                            {
                                var house = db.StreetSweepingDetails.Where(c => c.SSId == d.SSId).FirstOrDefault();
                                userLocation.Add(new SBALUserLocationMapView()
                                {
                                    userName = userName.userName,
                                    datetime = Convert.ToDateTime(d.gcDate).ToString("HH:mm"),
                                    date = dat,
                                    time = tim,
                                    lat = d.Lat,
                                    log = d.Long,
                                    address = x.address,
                                    vehcileNumber = att.vehicleNumber,
                                    userMobile = userName.userMobileNumber,
                                    type = Convert.ToInt32(x.type),
                                    HouseId = house.ReferanceId,
                                    HouseAddress = (house.SSAddress == null ? "" : house.SSAddress.Replace("Unnamed Road, ", "")),
                                    HouseOwnerName = house.SSName,
                                    //OwnerMobileNo = house.houseOwnerMobile,
                                    WasteType = d.gcType.ToString(),
                                    gpBeforImage = d.gpBeforImage,
                                    gpAfterImage = d.gpAfterImage,
                                    ZoneList = ListZone(),

                                });
                            }


                        }
                        //if (d.dyId != null)
                        //{
                        //    if (areaid != 0)
                        //    {
                        //        var dump = db.DumpYardDetails.Where(c => c.dyId == d.dyId & c.areaId == areaid).FirstOrDefault();
                        //        if (dump != null)
                        //        {
                        //            userLocation.Add(new SBALUserLocationMapView()
                        //            {
                        //                userName = userName.userName,
                        //                datetime = Convert.ToDateTime(d.gcDate).ToString("dd/MM/yyyy HH:mm"),
                        //                date = dat,
                        //                time = tim,
                        //                lat = d.Lat,
                        //                log = d.Long,
                        //                address = x.address,
                        //                vehcileNumber = att.vehicleNumber,
                        //                userMobile = userName.userMobileNumber,
                        //                type = Convert.ToInt32(x.type),
                        //                DyId = dump.ReferanceId,
                        //                DumpAddress = (dump.dyAddress == null ? "" : dump.dyAddress.Replace("Unnamed Road, ", "")),
                        //                DumpYardName = dump.dyName,
                        //                OwnerMobileNo = dump.dyNameMar,
                        //                WasteType = d.garbageType.ToString(),
                        //                gpBeforImage = d.gpBeforImage,
                        //                gpAfterImage = d.gpAfterImage,
                        //                DryWaste = d.totalDryWeight.ToString(),
                        //                WetWaste = d.totalWetWeight.ToString(),
                        //                TotWaste = d.totalGcWeight.ToString(),
                        //                ZoneList = ListZone(),

                        //            });
                        //        }

                        //    }
                        //    else
                        //    {
                        //        var dump = db.DumpYardDetails.Where(c => c.dyId == d.dyId).FirstOrDefault();
                        //        userLocation.Add(new SBALUserLocationMapView()
                        //        {
                        //            userName = userName.userName,
                        //            datetime = Convert.ToDateTime(d.gcDate).ToString("dd/MM/yyyy HH:mm"),
                        //            date = dat,
                        //            time = tim,
                        //            lat = d.Lat,
                        //            log = d.Long,
                        //            address = x.address,
                        //            vehcileNumber = att.vehicleNumber,
                        //            userMobile = userName.userMobileNumber,
                        //            type = Convert.ToInt32(x.type),
                        //            DyId = dump.ReferanceId,
                        //            DumpAddress = (dump.dyAddress == null ? "" : dump.dyAddress.Replace("Unnamed Road, ", "")),
                        //            DumpYardName = dump.dyName,
                        //            OwnerMobileNo = dump.dyNameMar,
                        //            WasteType = d.garbageType.ToString(),
                        //            gpBeforImage = d.gpBeforImage,
                        //            gpAfterImage = d.gpAfterImage,
                        //            DryWaste = d.totalDryWeight.ToString(),
                        //            WetWaste = d.totalWetWeight.ToString(),
                        //            TotWaste = d.totalGcWeight.ToString(),
                        //            ZoneList = ListZone(),

                        //        });
                        //    }

                        //}

                    }
                    break;
                }

            }




            return userLocation;
        }

        // Added By Saurabh (06 June 2019)

        public List<SBALHouseLocationMapView> GetAllHouseLocation(string date, int userid, int areaid, int wardNo, string SearchString, int? GarbageType, int FilterType, string Emptype)
        {

            List<SBALHouseLocationMapView> houseLocation = new List<SBALHouseLocationMapView>();
            var zoneId = 0;
            DateTime dt1 = DateTime.ParseExact(date, "d/M/yyyy", CultureInfo.InvariantCulture);
            if (Emptype == null)
            {
                var data = db.SP_HouseOnMapDetails(Convert.ToDateTime(dt1), userid == -1 ? 0 : userid, zoneId, areaid, wardNo, GarbageType, FilterType).ToList();
                foreach (var x in data)
                {

                    DateTime dt = DateTime.Parse(x.gcDate == null ? DateTime.Now.ToString() : x.gcDate.ToString());
                    //string gcTime = x.gcDate.ToString();
                    houseLocation.Add(new SBALHouseLocationMapView()
                    {
                        dyid = Convert.ToInt32(x.dyId),
                        ssid = Convert.ToInt32(x.SSId),
                        lwid = Convert.ToInt32(x.LWId),
                        houseId = Convert.ToInt32(x.houseId),
                        ReferanceId = x.ReferanceId,
                        houseOwnerName = (x.houseOwner == null ? "" : x.houseOwner),
                        houseOwnerMobile = (x.houseOwnerMobile == null ? "" : x.houseOwnerMobile),
                        houseAddress = checkNull(x.houseAddress).Replace("Unnamed Road, ", ""),
                        gcDate = dt.ToString("dd-MM-yyyy"),
                        gcTime = dt.ToString("h:mm tt"), // 7:00 AM // 12 hour clock
                                                         //string gcTime = x.gcDate.ToString(),
                                                         //gcTime = x.gcDate.ToString("hh:mm tt"),
                                                         //myDateTime.ToString("HH:mm:ss")
                        ///date = Convert.ToDateTime(x.datt).ToString("dd/MM/yyyy"),
                        //time = Convert.ToDateTime(x.datt).ToString("hh:mm:ss tt"),
                        houseLat = x.houseLat,
                        houseLong = x.houseLong,
                        // address = x.houseAddress,
                        //vehcileNumber = x.v,
                        //userMobile = x.mobile,
                        garbageType = x.garbageType,
                    });
                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    // var abc = db.HouseMasters.ToList();
                    var model = houseLocation.Where(c => c.houseOwnerName.Contains(SearchString) || c.ReferanceId.Contains(SearchString)
                                                         || c.houseOwnerName.ToLower().Contains(SearchString) || c.ReferanceId.ToLower().Contains(SearchString)).ToList();

                    //var model = houseLocation.Where(c => ((string.IsNullOrEmpty(c.ReferanceId) ? " " : c.houseOwnerName) + " " +
                    //                                     (string.IsNullOrEmpty(c.houseOwnerName) ? " " : c.houseOwnerName) + " " +
                    //                                     (string.IsNullOrEmpty(c.houseOwnerMobile) ? " " : c.houseOwnerMobile) + " " +
                    //                                     (string.IsNullOrEmpty(c.houseAddress) ? " " : c.houseAddress)).ToLower().Contains(SearchString)).ToList();

                    houseLocation = model.ToList();

                    //var model = data.Where(c => ((string.IsNullOrEmpty(c.WardNo) ? " " : c.WardNo) + " " +
                    //                        (string.IsNullOrEmpty(c.zone) ? " " : c.zone) + " " +
                    //                        (string.IsNullOrEmpty(c.Area) ? " " : c.Area) + " " +
                    //                        (string.IsNullOrEmpty(c.Name) ? " " : c.Name) + " " +
                    //                        (string.IsNullOrEmpty(c.houseNo) ? " " : c.houseNo) + " " +
                    //                        (string.IsNullOrEmpty(c.Mobile) ? " " : c.Mobile) + " " +
                    //                        (string.IsNullOrEmpty(c.Address) ? " " : c.Address) + " " +
                    //                        (string.IsNullOrEmpty(c.ReferanceId) ? " " : c.ReferanceId) + " " +
                    //                        (string.IsNullOrEmpty(c.QRCode) ? " " : c.QRCode)).ToUpper().Contains(SearchString.ToUpper())).ToList();

                }
            }
            else if (Emptype == "L")
            {
                var data = db.SP_LiquidWasteOnMapDetails(Convert.ToDateTime(dt1), userid == -1 ? 0 : userid, zoneId, areaid, wardNo, GarbageType, FilterType).ToList();
                foreach (var x in data)
                {

                    DateTime dt = DateTime.Parse(x.gcDate == null ? DateTime.Now.ToString() : x.gcDate.ToString());
                    //string gcTime = x.gcDate.ToString();
                    houseLocation.Add(new SBALHouseLocationMapView()
                    {
                        houseId = Convert.ToInt32(x.LWId),
                        ReferanceId = x.ReferanceId,
                        houseOwnerName = (x.LWName == null ? "" : x.LWName),
                        //houseOwnerMobile = (x.houseOwnerMobile == null ? "" : x.houseOwnerMobile),
                        houseAddress = checkNull(x.LWAddreLW).Replace("Unnamed Road, ", ""),
                        gcDate = dt.ToString("dd-MM-yyyy"),
                        gcTime = dt.ToString("h:mm tt"), // 7:00 AM // 12 hour clock
                                                         //string gcTime = x.gcDate.ToString(),
                                                         //gcTime = x.gcDate.ToString("hh:mm tt"),
                                                         //myDateTime.ToString("HH:mm:ss")
                        ///date = Convert.ToDateTime(x.datt).ToString("dd/MM/yyyy"),
                        //time = Convert.ToDateTime(x.datt).ToString("hh:mm:ss tt"),
                        houseLat = x.LWLat,
                        houseLong = x.LWLong,
                        // address = x.houseAddress,
                        //vehcileNumber = x.v,
                        //userMobile = x.mobile,
                        garbageType = x.garbageType,
                        gcType = x.gcType,

                    });
                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    // var abc = db.HouseMasters.ToList();
                    var model = houseLocation.Where(c => c.houseOwnerName.Contains(SearchString) || c.ReferanceId.Contains(SearchString)
                                                         || c.houseOwnerName.ToLower().Contains(SearchString) || c.ReferanceId.ToLower().Contains(SearchString)).ToList();

                    houseLocation = model.ToList();

                }
            }

            else if (Emptype == "S")
            {
                var data = db.SP_StreetSweepingOnMapDetails(Convert.ToDateTime(dt1), userid == -1 ? 0 : userid, zoneId, areaid, wardNo, GarbageType, FilterType).ToList();
                foreach (var x in data)
                {

                    DateTime dt = DateTime.Parse(x.gcDate == null ? DateTime.Now.ToString() : x.gcDate.ToString());
                    //string gcTime = x.gcDate.ToString();
                    houseLocation.Add(new SBALHouseLocationMapView()
                    {
                        houseId = Convert.ToInt32(x.SSId),
                        ReferanceId = x.ReferanceId,
                        houseOwnerName = (x.SSName == null ? "" : x.SSName),
                        //houseOwnerMobile = (x.houseOwnerMobile == null ? "" : x.houseOwnerMobile),
                        houseAddress = checkNull(x.SSAddress).Replace("Unnamed Road, ", ""),
                        gcDate = dt.ToString("dd-MM-yyyy"),
                        gcTime = dt.ToString("h:mm tt"), // 7:00 AM // 12 hour clock
                                                         //string gcTime = x.gcDate.ToString(),
                                                         //gcTime = x.gcDate.ToString("hh:mm tt"),
                                                         //myDateTime.ToString("HH:mm:ss")
                        ///date = Convert.ToDateTime(x.datt).ToString("dd/MM/yyyy"),
                        //time = Convert.ToDateTime(x.datt).ToString("hh:mm:ss tt"),
                        houseLat = x.SSLat,
                        houseLong = x.SSLong,
                        // address = x.houseAddress,
                        //vehcileNumber = x.v,
                        //userMobile = x.mobile,
                        garbageType = x.garbageType,
                        gcType = x.gcType,
                        BeatId = db.StreetSweepingBeats.Where(s => s.ReferanceId1 == x.ReferanceId || s.ReferanceId2 == x.ReferanceId || s.ReferanceId3 == x.ReferanceId || s.ReferanceId4 == x.ReferanceId || s.ReferanceId5 == x.ReferanceId).Select(a => a.BeatId).FirstOrDefault()

                    });
                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    // var abc = db.HouseMasters.ToList();
                    var model = houseLocation.Where(c => c.houseOwnerName.Contains(SearchString) || c.ReferanceId.Contains(SearchString)
                                                         || c.houseOwnerName.ToLower().Contains(SearchString) || c.ReferanceId.ToLower().Contains(SearchString)).ToList();

                    houseLocation = model.ToList();

                }
            }


            else
            {
                var data = db.SP_HouseOnMapDetails(Convert.ToDateTime(dt1), userid == -1 ? 0 : userid, zoneId, areaid, wardNo, GarbageType, FilterType).ToList();
                foreach (var x in data)
                {

                    DateTime dt = DateTime.Parse(x.gcDate == null ? DateTime.Now.ToString() : x.gcDate.ToString());
                    //string gcTime = x.gcDate.ToString();
                    houseLocation.Add(new SBALHouseLocationMapView()
                    {
                        houseId = Convert.ToInt32(x.houseId),
                        ReferanceId = x.ReferanceId,
                        houseOwnerName = (x.houseOwner == null ? "" : x.houseOwner),
                        houseOwnerMobile = (x.houseOwnerMobile == null ? "" : x.houseOwnerMobile),
                        houseAddress = checkNull(x.houseAddress).Replace("Unnamed Road, ", ""),
                        gcDate = dt.ToString("dd-MM-yyyy"),
                        gcTime = dt.ToString("h:mm tt"), // 7:00 AM // 12 hour clock
                                                         //string gcTime = x.gcDate.ToString(),
                                                         //gcTime = x.gcDate.ToString("hh:mm tt"),
                                                         //myDateTime.ToString("HH:mm:ss")
                        ///date = Convert.ToDateTime(x.datt).ToString("dd/MM/yyyy"),
                        //time = Convert.ToDateTime(x.datt).ToString("hh:mm:ss tt"),
                        houseLat = x.houseLat,
                        houseLong = x.houseLong,
                        // address = x.houseAddress,
                        //vehcileNumber = x.v,
                        //userMobile = x.mobile,
                        garbageType = x.garbageType,
                    });
                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    // var abc = db.HouseMasters.ToList();
                    var model = houseLocation.Where(c => c.houseOwnerName.Contains(SearchString) || c.ReferanceId.Contains(SearchString)
                                                         || c.houseOwnerName.ToLower().Contains(SearchString) || c.ReferanceId.ToLower().Contains(SearchString)).ToList();

                    //var model = houseLocation.Where(c => ((string.IsNullOrEmpty(c.ReferanceId) ? " " : c.houseOwnerName) + " " +
                    //                                     (string.IsNullOrEmpty(c.houseOwnerName) ? " " : c.houseOwnerName) + " " +
                    //                                     (string.IsNullOrEmpty(c.houseOwnerMobile) ? " " : c.houseOwnerMobile) + " " +
                    //                                     (string.IsNullOrEmpty(c.houseAddress) ? " " : c.houseAddress)).ToLower().Contains(SearchString)).ToList();

                    houseLocation = model.ToList();

                    //var model = data.Where(c => ((string.IsNullOrEmpty(c.WardNo) ? " " : c.WardNo) + " " +
                    //                        (string.IsNullOrEmpty(c.zone) ? " " : c.zone) + " " +
                    //                        (string.IsNullOrEmpty(c.Area) ? " " : c.Area) + " " +
                    //                        (string.IsNullOrEmpty(c.Name) ? " " : c.Name) + " " +
                    //                        (string.IsNullOrEmpty(c.houseNo) ? " " : c.houseNo) + " " +
                    //                        (string.IsNullOrEmpty(c.Mobile) ? " " : c.Mobile) + " " +
                    //                        (string.IsNullOrEmpty(c.Address) ? " " : c.Address) + " " +
                    //                        (string.IsNullOrEmpty(c.ReferanceId) ? " " : c.ReferanceId) + " " +
                    //                        (string.IsNullOrEmpty(c.QRCode) ? " " : c.QRCode)).ToUpper().Contains(SearchString.ToUpper())).ToList();

                }
            }
            return houseLocation;

        }


        //public List<SBALHouseLocationMapView> GetAllHouseLocationForEmpBitMap(string date, int userid, int areaid, int wardNo, string SearchString, int? GarbageType, int FilterType, string Emptype)
        //{

        //    List<SBALHouseLocationMapView> houseLocation = new List<SBALHouseLocationMapView>();
        //    var zoneId = 0;
        //    DateTime dt1 = DateTime.ParseExact(date, "d/M/yyyy", CultureInfo.InvariantCulture);
        //    if (Emptype == null)
        //    {
        //        var data = db.SP_HouseOnMapDetailsForEmpBitMap(Convert.ToDateTime(dt1), userid == -1 ? 0 : userid, zoneId, areaid, wardNo, GarbageType, FilterType).ToList();
        //        foreach (var x in data)
        //        {

        //            DateTime dt = DateTime.Parse(x.gcDate == null ? DateTime.Now.ToString() : x.gcDate.ToString());
        //            //string gcTime = x.gcDate.ToString();
        //            houseLocation.Add(new SBALHouseLocationMapView()
        //            {
        //                dyid = Convert.ToInt32(x.dyId),
        //                houseId = Convert.ToInt32(x.houseId),
        //                ReferanceId = x.ReferanceId,
        //                houseOwnerName = (x.houseOwner == null ? "" : x.houseOwner),
        //                houseOwnerMobile = (x.houseOwnerMobile == null ? "" : x.houseOwnerMobile),
        //                houseAddress = checkNull(x.houseAddress).Replace("Unnamed Road, ", ""),
        //                gcDate = dt.ToString("dd-MM-yyyy"),
        //                gcTime = dt.ToString("h:mm tt"), // 7:00 AM // 12 hour clock
        //                                                 //string gcTime = x.gcDate.ToString(),
        //                                                 //gcTime = x.gcDate.ToString("hh:mm tt"),
        //                                                 //myDateTime.ToString("HH:mm:ss")
        //                ///date = Convert.ToDateTime(x.datt).ToString("dd/MM/yyyy"),
        //                //time = Convert.ToDateTime(x.datt).ToString("hh:mm:ss tt"),
        //                houseLat = x.houseLat,
        //                houseLong = x.houseLong,
        //                // address = x.houseAddress,
        //                //vehcileNumber = x.v,
        //                //userMobile = x.mobile,
        //                garbageType = x.garbageType,
        //            });
        //        }
        //        if (!string.IsNullOrEmpty(SearchString))
        //        {
        //            // var abc = db.HouseMasters.ToList();
        //            var model = houseLocation.Where(c => c.houseOwnerName.Contains(SearchString) || c.ReferanceId.Contains(SearchString)
        //                                                 || c.houseOwnerName.ToLower().Contains(SearchString) || c.ReferanceId.ToLower().Contains(SearchString)).ToList();

        //            //var model = houseLocation.Where(c => ((string.IsNullOrEmpty(c.ReferanceId) ? " " : c.houseOwnerName) + " " +
        //            //                                     (string.IsNullOrEmpty(c.houseOwnerName) ? " " : c.houseOwnerName) + " " +
        //            //                                     (string.IsNullOrEmpty(c.houseOwnerMobile) ? " " : c.houseOwnerMobile) + " " +
        //            //                                     (string.IsNullOrEmpty(c.houseAddress) ? " " : c.houseAddress)).ToLower().Contains(SearchString)).ToList();

        //            houseLocation = model.ToList();

        //            //var model = data.Where(c => ((string.IsNullOrEmpty(c.WardNo) ? " " : c.WardNo) + " " +
        //            //                        (string.IsNullOrEmpty(c.zone) ? " " : c.zone) + " " +
        //            //                        (string.IsNullOrEmpty(c.Area) ? " " : c.Area) + " " +
        //            //                        (string.IsNullOrEmpty(c.Name) ? " " : c.Name) + " " +
        //            //                        (string.IsNullOrEmpty(c.houseNo) ? " " : c.houseNo) + " " +
        //            //                        (string.IsNullOrEmpty(c.Mobile) ? " " : c.Mobile) + " " +
        //            //                        (string.IsNullOrEmpty(c.Address) ? " " : c.Address) + " " +
        //            //                        (string.IsNullOrEmpty(c.ReferanceId) ? " " : c.ReferanceId) + " " +
        //            //                        (string.IsNullOrEmpty(c.QRCode) ? " " : c.QRCode)).ToUpper().Contains(SearchString.ToUpper())).ToList();

        //        }
        //    }
        //    else if (Emptype == "L")
        //    {
        //        var data = db.SP_LiquidWasteOnMapDetails(Convert.ToDateTime(dt1), userid == -1 ? 0 : userid, zoneId, areaid, wardNo, GarbageType, FilterType).ToList();
        //        foreach (var x in data)
        //        {

        //            DateTime dt = DateTime.Parse(x.gcDate == null ? DateTime.Now.ToString() : x.gcDate.ToString());
        //            //string gcTime = x.gcDate.ToString();
        //            houseLocation.Add(new SBALHouseLocationMapView()
        //            {
        //                houseId = Convert.ToInt32(x.LWId),
        //                ReferanceId = x.ReferanceId,
        //                houseOwnerName = (x.LWName == null ? "" : x.LWName),
        //                //houseOwnerMobile = (x.houseOwnerMobile == null ? "" : x.houseOwnerMobile),
        //                houseAddress = checkNull(x.LWAddreLW).Replace("Unnamed Road, ", ""),
        //                gcDate = dt.ToString("dd-MM-yyyy"),
        //                gcTime = dt.ToString("h:mm tt"), // 7:00 AM // 12 hour clock
        //                                                 //string gcTime = x.gcDate.ToString(),
        //                                                 //gcTime = x.gcDate.ToString("hh:mm tt"),
        //                                                 //myDateTime.ToString("HH:mm:ss")
        //                ///date = Convert.ToDateTime(x.datt).ToString("dd/MM/yyyy"),
        //                //time = Convert.ToDateTime(x.datt).ToString("hh:mm:ss tt"),
        //                houseLat = x.LWLat,
        //                houseLong = x.LWLong,
        //                // address = x.houseAddress,
        //                //vehcileNumber = x.v,
        //                //userMobile = x.mobile,
        //                garbageType = x.garbageType,
        //                gcType = x.gcType,

        //            });
        //        }
        //        if (!string.IsNullOrEmpty(SearchString))
        //        {
        //            // var abc = db.HouseMasters.ToList();
        //            var model = houseLocation.Where(c => c.houseOwnerName.Contains(SearchString) || c.ReferanceId.Contains(SearchString)
        //                                                 || c.houseOwnerName.ToLower().Contains(SearchString) || c.ReferanceId.ToLower().Contains(SearchString)).ToList();

        //            houseLocation = model.ToList();

        //        }
        //    }

        //    else if (Emptype == "S")
        //    {
        //        var data = db.SP_StreetSweepingOnMapDetails(Convert.ToDateTime(dt1), userid == -1 ? 0 : userid, zoneId, areaid, wardNo, GarbageType, FilterType).ToList();
        //        foreach (var x in data)
        //        {

        //            DateTime dt = DateTime.Parse(x.gcDate == null ? DateTime.Now.ToString() : x.gcDate.ToString());
        //            //string gcTime = x.gcDate.ToString();
        //            houseLocation.Add(new SBALHouseLocationMapView()
        //            {
        //                houseId = Convert.ToInt32(x.SSId),
        //                ReferanceId = x.ReferanceId,
        //                houseOwnerName = (x.SSName == null ? "" : x.SSName),
        //                //houseOwnerMobile = (x.houseOwnerMobile == null ? "" : x.houseOwnerMobile),
        //                houseAddress = checkNull(x.SSAddress).Replace("Unnamed Road, ", ""),
        //                gcDate = dt.ToString("dd-MM-yyyy"),
        //                gcTime = dt.ToString("h:mm tt"), // 7:00 AM // 12 hour clock
        //                                                 //string gcTime = x.gcDate.ToString(),
        //                                                 //gcTime = x.gcDate.ToString("hh:mm tt"),
        //                                                 //myDateTime.ToString("HH:mm:ss")
        //                ///date = Convert.ToDateTime(x.datt).ToString("dd/MM/yyyy"),
        //                //time = Convert.ToDateTime(x.datt).ToString("hh:mm:ss tt"),
        //                houseLat = x.SSLat,
        //                houseLong = x.SSLong,
        //                // address = x.houseAddress,
        //                //vehcileNumber = x.v,
        //                //userMobile = x.mobile,
        //                garbageType = x.garbageType,
        //                gcType = x.gcType,
        //                BeatId = db.StreetSweepingBeats.Where(s => s.ReferanceId1 == x.ReferanceId || s.ReferanceId2 == x.ReferanceId || s.ReferanceId3 == x.ReferanceId || s.ReferanceId4 == x.ReferanceId || s.ReferanceId5 == x.ReferanceId).Select(a => a.BeatId).FirstOrDefault()

        //            });
        //        }
        //        if (!string.IsNullOrEmpty(SearchString))
        //        {
        //            // var abc = db.HouseMasters.ToList();
        //            var model = houseLocation.Where(c => c.houseOwnerName.Contains(SearchString) || c.ReferanceId.Contains(SearchString)
        //                                                 || c.houseOwnerName.ToLower().Contains(SearchString) || c.ReferanceId.ToLower().Contains(SearchString)).ToList();

        //            houseLocation = model.ToList();

        //        }
        //    }


        //    else
        //    {
        //        var data = db.SP_HouseOnMapDetails(Convert.ToDateTime(dt1), userid == -1 ? 0 : userid, zoneId, areaid, wardNo, GarbageType, FilterType).ToList();
        //        foreach (var x in data)
        //        {

        //            DateTime dt = DateTime.Parse(x.gcDate == null ? DateTime.Now.ToString() : x.gcDate.ToString());
        //            //string gcTime = x.gcDate.ToString();
        //            houseLocation.Add(new SBALHouseLocationMapView()
        //            {
        //                houseId = Convert.ToInt32(x.houseId),
        //                ReferanceId = x.ReferanceId,
        //                houseOwnerName = (x.houseOwner == null ? "" : x.houseOwner),
        //                houseOwnerMobile = (x.houseOwnerMobile == null ? "" : x.houseOwnerMobile),
        //                houseAddress = checkNull(x.houseAddress).Replace("Unnamed Road, ", ""),
        //                gcDate = dt.ToString("dd-MM-yyyy"),
        //                gcTime = dt.ToString("h:mm tt"), // 7:00 AM // 12 hour clock
        //                                                 //string gcTime = x.gcDate.ToString(),
        //                                                 //gcTime = x.gcDate.ToString("hh:mm tt"),
        //                                                 //myDateTime.ToString("HH:mm:ss")
        //                ///date = Convert.ToDateTime(x.datt).ToString("dd/MM/yyyy"),
        //                //time = Convert.ToDateTime(x.datt).ToString("hh:mm:ss tt"),
        //                houseLat = x.houseLat,
        //                houseLong = x.houseLong,
        //                // address = x.houseAddress,
        //                //vehcileNumber = x.v,
        //                //userMobile = x.mobile,
        //                garbageType = x.garbageType,
        //            });
        //        }
        //        if (!string.IsNullOrEmpty(SearchString))
        //        {
        //            // var abc = db.HouseMasters.ToList();
        //            var model = houseLocation.Where(c => c.houseOwnerName.Contains(SearchString) || c.ReferanceId.Contains(SearchString)
        //                                                 || c.houseOwnerName.ToLower().Contains(SearchString) || c.ReferanceId.ToLower().Contains(SearchString)).ToList();



        //            houseLocation = model.ToList();



        //        }
        //    }
        //    return houseLocation;

        //}

        // Code Optimization(code)
        //public SBALHouseLocationMapView1 GetAllHouseLocation(string date, int userid, int areaid, int wardNo, string SearchString, string start)
        //{

        //    List<SBALHouseLocationMapView> houseLocation = new List<SBALHouseLocationMapView>();

        //    SBALHouseLocationMapView1 houses = new SBALHouseLocationMapView1();

        //    DateTime dt1 = DateTime.ParseExact(date, "d/M/yyyy", CultureInfo.InvariantCulture);
        //    Int16 length = 3000;
        //    List<SP_HouseOnMapDetails_Result> data = new List<SP_HouseOnMapDetails_Result>();
        //    var houseCount = db.SP_HouseOnMapDetails(Convert.ToDateTime(dt1), userid == -1 ? 0 : userid, areaid, wardNo).Count();
        //    if (SearchString == null || SearchString == "")
        //    {
        //        data = db.SP_HouseOnMapDetails(Convert.ToDateTime(dt1), userid == -1 ? 0 : userid, areaid, wardNo).Skip(Convert.ToInt32(start)).Take(Convert.ToInt32(length)).ToList();
        //    }
        //    else
        //    {
        //        data = db.SP_HouseOnMapDetails(Convert.ToDateTime(dt1), userid == -1 ? 0 : userid, areaid, wardNo).ToList();
        //    }
        //    foreach (var x in data)
        //    {

        //        DateTime dt = DateTime.Parse(x.gcDate == null ? DateTime.Now.ToString() : x.gcDate.ToString());
        //        //string gcTime = x.gcDate.ToString();
        //        houseLocation.Add(new SBALHouseLocationMapView()
        //        {
        //            houseId = Convert.ToInt32(x.houseId),
        //            ReferanceId = x.ReferanceId,
        //            houseOwnerName = (x.houseOwner == null ? "" : x.houseOwner),
        //            houseOwnerMobile = (x.houseOwnerMobile == null ? "" : x.houseOwnerMobile),
        //            houseAddress = checkNull(x.houseAddress).Replace("Unnamed Road, ", ""),
        //            gcDate = dt.ToString("dd-MM-yyyy"),
        //            gcTime = dt.ToString("h:mm tt"), // 7:00 AM // 12 hour clock
        //            //string gcTime = x.gcDate.ToString(),
        //            //gcTime = x.gcDate.ToString("hh:mm tt"),
        //            //myDateTime.ToString("HH:mm:ss")
        //            ///date = Convert.ToDateTime(x.datt).ToString("dd/MM/yyyy"),
        //            //time = Convert.ToDateTime(x.datt).ToString("hh:mm:ss tt"),
        //            houseLat = x.houseLat,
        //            houseLong = x.houseLong,
        //            // address = x.houseAddress,
        //            //vehcileNumber = x.v,
        //            //userMobile = x.mobile,
        //            garbageType = x.garbageType,

        //        });
        //    }
        //    if (!string.IsNullOrEmpty(SearchString))
        //    {
        //        // var abc = db.HouseMasters.ToList();
        //        var model = houseLocation.Where(c => c.houseOwnerName.Contains(SearchString) || c.ReferanceId.Contains(SearchString)
        //                                             || c.houseOwnerName.ToLower().Contains(SearchString) || c.ReferanceId.ToLower().Contains(SearchString)).ToList();

        //        //var model = houseLocation.Where(c => ((string.IsNullOrEmpty(c.ReferanceId) ? " " : c.houseOwnerName) + " " +
        //        //                                     (string.IsNullOrEmpty(c.houseOwnerName) ? " " : c.houseOwnerName) + " " +
        //        //                                     (string.IsNullOrEmpty(c.houseOwnerMobile) ? " " : c.houseOwnerMobile) + " " +
        //        //                                     (string.IsNullOrEmpty(c.houseAddress) ? " " : c.houseAddress)).ToLower().Contains(SearchString)).ToList();

        //        houseLocation = model.ToList();

        //        //var model = data.Where(c => ((string.IsNullOrEmpty(c.WardNo) ? " " : c.WardNo) + " " +
        //        //                        (string.IsNullOrEmpty(c.zone) ? " " : c.zone) + " " +
        //        //                        (string.IsNullOrEmpty(c.Area) ? " " : c.Area) + " " +
        //        //                        (string.IsNullOrEmpty(c.Name) ? " " : c.Name) + " " +
        //        //                        (string.IsNullOrEmpty(c.houseNo) ? " " : c.houseNo) + " " +
        //        //                        (string.IsNullOrEmpty(c.Mobile) ? " " : c.Mobile) + " " +
        //        //                        (string.IsNullOrEmpty(c.Address) ? " " : c.Address) + " " +
        //        //                        (string.IsNullOrEmpty(c.ReferanceId) ? " " : c.ReferanceId) + " " +
        //        //                        (string.IsNullOrEmpty(c.QRCode) ? " " : c.QRCode)).ToUpper().Contains(SearchString.ToUpper())).ToList();

        //    }
        //    houses.houseCount = houseCount;
        //    houses.houseList = houseLocation;
        //    return houses;
        //}

        //(old code)
        //public List<SBALHouseLocationMapView> GetAllHouseLocation(string date, int userid, int areaid)
        //{

        //    List<SBALHouseLocationMapView> houseLocation = new List<SBALHouseLocationMapView>();


        //     var data = db.SP_HouseOnMapDetails(Convert.ToDateTime(date), userid == -1 ? 0 : userid, areaid).ToList();
        //    foreach (var x in data)
        //        {

        //            DateTime dt = DateTime.Parse(x.gcDate == null ? DateTime.Now.ToString() : x.gcDate.ToString());
        //            //string gcTime = x.gcDate.ToString();
        //            houseLocation.Add(new SBALHouseLocationMapView()
        //            {
        //                houseId = Convert.ToInt32(x.houseId),
        //                ReferanceId = x.ReferanceId,
        //                houseOwnerName = x.houseOwner,
        //                houseOwnerMobile = x.houseOwnerMobile,
        //                houseAddress = x.houseAddress,
        //                gcDate = dt.ToString("dd-MM-yyyy"),
        //                gcTime = dt.ToString("h:mm tt"), // 7:00 AM // 12 hour clock
        //                //string gcTime = x.gcDate.ToString(),
        //                //gcTime = x.gcDate.ToString("hh:mm tt"),
        //                //myDateTime.ToString("HH:mm:ss")
        //                ///date = Convert.ToDateTime(x.datt).ToString("dd/MM/yyyy"),
        //                //time = Convert.ToDateTime(x.datt).ToString("hh:mm:ss tt"),
        //                houseLat = x.houseLat,
        //                houseLong = x.houseLong,
        //                // address = x.houseAddress,
        //                //vehcileNumber = x.v,
        //                //userMobile = x.mobile,
        //                garbageType = x.garbageType,
        //            });
        //        }

        //         return houseLocation;

        //}
        // Added By Saurabh (02 July 2019)

        public List<SBALHouseLocationMapView> GetAllHouseLocationForEmpBitMap(string Emptype, int ebmId)
        {

            List<SBALHouseLocationMapView> houseLocation = new List<SBALHouseLocationMapView>();
            List<List<coordinates>> lstPoly = new List<List<coordinates>>();

            EmpBeatMapVM ebm = GetEmpBeatMap(ebmId);
            lstPoly = ebm.ebmLatLong;
            var data = db.SP_HouseOnMapDetailsForEmpBitMap(Emptype).ToList();
            foreach (var x in data)
            {
                bool bIsIn = false;

                coordinates p = new coordinates()
                {
                    lat = Convert.ToDouble(x.houseLat),
                    lng = Convert.ToDouble(x.houseLong)
                };
                if (lstPoly != null && lstPoly.Count > 0)
                {
                    foreach (var poly in lstPoly)
                    {
                        if (IsPointInPolygon(poly, p))
                        {
                            bIsIn = true;
                        }
                    }
                }
                DateTime dt = DateTime.Parse(x.gcDate == null ? DateTime.Now.ToString() : x.gcDate.ToString());
                //string gcTime = x.gcDate.ToString();
                houseLocation.Add(new SBALHouseLocationMapView()
                {
                    dyid = Convert.ToInt32(x.dyId),
                    houseId = Convert.ToInt32(x.houseId),
                    ReferanceId = x.ReferanceId,
                    houseOwnerName = (x.houseOwner == null ? "" : x.houseOwner),
                    houseOwnerMobile = (x.houseOwnerMobile == null ? "" : x.houseOwnerMobile),
                    houseAddress = checkNull(x.houseAddress).Replace("Unnamed Road, ", ""),
                    gcDate = dt.ToString("dd-MM-yyyy"),
                    gcTime = dt.ToString("h:mm tt"), // 7:00 AM // 12 hour clock
                                                     //string gcTime = x.gcDate.ToString(),
                                                     //gcTime = x.gcDate.ToString("hh:mm tt"),
                                                     //myDateTime.ToString("HH:mm:ss")
                    ///date = Convert.ToDateTime(x.datt).ToString("dd/MM/yyyy"),
                    //time = Convert.ToDateTime(x.datt).ToString("hh:mm:ss tt"),
                    houseLat = x.houseLat,
                    houseLong = x.houseLong,
                    // address = x.houseAddress,
                    //vehcileNumber = x.v,
                    //userMobile = x.mobile,
                    garbageType = x.garbageType,
                    IsIn = bIsIn
                });
            }


            return houseLocation;

        }

        public DashBoardVM GetHouseOnMapDetails()
        {
            DashBoardVM model = new DashBoardVM();
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {

                    DevSwachhBharatMainEntities dbm = new DevSwachhBharatMainEntities();
                    //var appdetails = dbm.AppDetails.Where(c => c.AppId == AppID).FirstOrDefault();
                    //List<ComplaintVM> obj = new List<ComplaintVM>();
                    //if (AppID == 1)
                    //{
                    //    string json = new WebClient().DownloadString(appdetails.Grampanchayat_Pro + "/api/Get/Complaint?appId=1");
                    //    obj = JsonConvert.DeserializeObject<List<ComplaintVM>>(json).Where(c => Convert.ToDateTime(c.createdDate2).ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")).ToList();
                    //}


                    var data = db.SP_HouseScanify_Count(AppID).First();

                    //var date = DateTime.Today;
                    //var houseCount = db.SP_TotalHouseCollection_Count(date).FirstOrDefault();
                    if (data != null)
                    {

                        model.TotalHouseCount = data.TotalHouseCount;
                        model.HouseCollection = data.TotalHouseLatLongCount;
                        model.TotalScanHouseCount = data.TotalScanHouseCount;
                        model.MixedCount = data.MixedCount;
                        model.BifurgatedCount = data.BifurgatedCount;
                        model.NotCollected = data.NotCollected;
                        model.NotSpecified = data.NotSpecified;
                        model.LiquidCollection = data.TotalLiquidLatLongCount;
                        model.StreetCollection = data.TotalStreetLatLongCount;
                        model.DumpYardCount = data.TotalDumpYardLatLongCount;
                        return model;
                    }

                    // String.Format("{0:0.00}", 123.4567); 

                    else
                    {
                        return model;
                    }
                }
            }
            catch (Exception ex)
            {
                return model;
            }
        }


        public DashBoardVM GetLiquidWasteDetails()
        {
            DashBoardVM model = new DashBoardVM();
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    DevSwachhBharatMainEntities dbm = new DevSwachhBharatMainEntities();
                    var data = db.SP_LWaste_Count().First();
                    if (data != null)
                    {
                        model.LiquidWasteCollection = data.TotalLiquidLatLongCount;
                        model.LiquidWasteScanedHouse = data.TotalScanHouseCount;
                        //model.TotalScanHouseCount = data.TotalScanHouseCount;
                        //model.MixedCount = data.MixedCount;
                        //model.BifurgatedCount = data.BifurgatedCount;
                        //model.NotCollected = data.NotCollected;
                        //model.NotSpecified = data.NotSpecified;
                        return model;
                    }
                    else
                    {
                        return model;
                    }
                }
            }
            catch (Exception ex)
            {
                return model;
            }
        }


        public DashBoardVM GetStreetSweepingDetails()
        {
            DashBoardVM model = new DashBoardVM();
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    DevSwachhBharatMainEntities dbm = new DevSwachhBharatMainEntities();
                    var data = db.SP_SSweeping_Count().First();
                    if (data != null)
                    {
                        model.StreetWasteCollection = data.TotalStreetLatLongCount;
                        model.StreetWasteScanedHouse = data.TotalScanHouseCount;
                        //model.TotalScanHouseCount = data.TotalScanHouseCount;
                        //model.MixedCount = data.MixedCount;
                        //model.BifurgatedCount = data.BifurgatedCount;
                        //model.NotCollected = data.NotCollected;
                        //model.NotSpecified = data.NotSpecified;
                        return model;
                    }
                    else
                    {
                        return model;
                    }
                }
            }
            catch (Exception ex)
            {
                return model;
            }
        }
        #endregion

        #region Garbage Point Details
        public GarbagePointDetailsVM GetGarbagePointDetails(int teamId)
        {
            try
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();

                string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.PointQRCode + "/";
                GarbagePointDetailsVM point = new GarbagePointDetailsVM();

                var Details = db.GarbagePointDetails.Where(x => x.gpId == teamId).FirstOrDefault();
                if (Details != null)
                {
                    point = FillGarbagePointDetailsViewModel(Details);
                    if (point.qrCode != null && point.qrCode != "")
                    {
                        HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(ThumbnaiUrlCMS + point.qrCode.Trim());
                        HttpWebResponse httpRes = null;
                        try
                        {
                            httpRes = (HttpWebResponse)httpReq.GetResponse(); // Error 404 right here,
                            if (httpRes.StatusCode == HttpStatusCode.NotFound)
                            {
                                point.qrCode = "/Images/default_not_upload.png";
                            }
                            else
                            {
                                point.qrCode = ThumbnaiUrlCMS + point.qrCode.Trim();
                            }
                        }
                        catch (Exception e) { point.qrCode = "/Images/default_not_upload.png"; }

                        //  point.qrCode = ThumbnaiUrlCMS + point.qrCode.Trim();
                    }
                    else
                    {
                        point.qrCode = "/Images/default_not_upload.png";
                    }

                    // house.WardList = ListWardNo();
                    point.AreaList = LoadListArea(Convert.ToInt32(point.WardNo)); //ListArea();
                    point.ZoneList = ListZone();
                    point.WardList = LoadListWardNo(Convert.ToInt32(point.ZoneId)); //ListWardNo();

                    return point;
                }
                else
                {
                    var id = db.GarbagePointDetails.OrderByDescending(x => x.gpId).Select(x => x.gpId).FirstOrDefault();
                    int number = 1000;
                    string refer = "GPSBA" + (number + id + 1);
                    point.ReferanceId = refer;
                    point.qrCode = "/Images/QRcode.png";
                    point.WardList = ListWardNo();
                    point.AreaList = ListArea();
                    point.ZoneList = ListZone();
                    return point;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public GarbagePointDetailsVM SaveGarbagePointDetails(GarbagePointDetailsVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.gpId > 0)
                    {
                        var model = db.GarbagePointDetails.Where(x => x.gpId == data.gpId).FirstOrDefault();
                        if (model != null)
                        {
                            model.zoneId = data.ZoneId;
                            model.wardId = data.WardNo;
                            model.areaId = data.areaId;
                            model.gpAddress = data.gpAddress;
                            model.gpLat = data.gpLat;
                            model.gpLong = data.gpLong;
                            model.gpName = data.gpName;
                            model.gpNameMar = data.gpNameMar;
                            model.qrCode = data.qrCode;
                            model.ReferanceId = data.ReferanceId;
                            model.modified = DateTime.Now;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var type = FillGarbagePointDetailsDataModel(data);
                        db.GarbagePointDetails.Add(type);
                        db.SaveChanges();
                    }
                }
                var gpid = db.GarbagePointDetails.OrderByDescending(x => x.gpId).Select(x => x.gpId).FirstOrDefault();
                GarbagePointDetailsVM vv = GetGarbagePointDetails(gpid);
                return vv;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void DeletGarbagePointDetails(int teamId)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.GarbagePointDetails.Where(x => x.gpId == teamId).FirstOrDefault();
                    if (Details != null)
                    {
                        db.GarbagePointDetails.Remove(Details);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region DataModel
        private TeritoryMaster FillAreaDataModel(AreaVM data)
        {

            TeritoryMaster model = new TeritoryMaster();

            var modelt = db.TeritoryMasters.Where(x => x.Area == data.Name).FirstOrDefault();

            if (modelt == null)
            {
                model.Id = data.Id;
                model.Area = data.Name;
                model.AreaMar = data.NameMar;
                model.wardId = data.wardId;

                return model;
            }

            return model;
        }

        private TeritoryMaster LiquidFillAreaDataModel(AreaVM data)
        {
            TeritoryMaster model = new TeritoryMaster();
            model.Id = data.LWId;
            model.Area = data.LWName;
            model.AreaMar = data.LWNameMar;
            model.wardId = data.LWwardId;

            return model;
        }

        private TeritoryMaster StreetFillAreaDataModel(AreaVM data)
        {
            TeritoryMaster model = new TeritoryMaster();
            model.Id = data.SSId;
            model.Area = data.SSName;
            model.AreaMar = data.SSNameMar;
            model.wardId = data.SSwardId;

            return model;
        }
        private VehicleType FillVehicleDataModel(VehicleTypeVM data)
        {
            VehicleType model = new VehicleType();
            model.vtId = data.Id;
            model.description = data.description;
            model.descriptionMar = data.descriptionMar;
            model.isActive = data.isActive;
            return model;
        }
        private WardNumber FillWardDataModel(WardNumberVM data)
        {
            WardNumber model = new WardNumber();
            model.Id = data.Id;
            model.WardNo = data.WardNo;
            model.zoneId = data.zoneId;
            return model;
        }

        private MasterQR FillMasterQRDataModel(MasterQRDetailsVM data)
        {
            MasterQR model = new MasterQR();
            model.MasterId = data.masterId;
            model.ReferanceId = data.ReferanceId;
            model.ISActive = data.isActive;
            return model;
        }
        private WardNumber LiquidFillWardDataModel(WardNumberVM data)
        {
            WardNumber model = new WardNumber();
            model.Id = data.LWId;
            model.WardNo = data.LWWardNo;
            model.zoneId = data.LWzoneId;
            return model;
        }

        private WardNumber StreetFillWardDataModel(WardNumberVM data)
        {
            WardNumber model = new WardNumber();
            model.Id = data.SSId;
            model.WardNo = data.SSWardNo;
            model.zoneId = data.SSzoneId;
            return model;
        }

        private EmpBeatMap fillEmpBeatMap(EmpBeatMapVM data)
        {
            EmpBeatMap model = new EmpBeatMap();
            model.userId = data.userId;
            model.Type = data.Type;
            model.ebmLatLong = ConvertLatLongToString(data.ebmLatLong);

            return model;
        }
        private EmpBeatMapVM fillEmpBeatMapVM(EmpBeatMap data)
        {
            EmpBeatMapVM model = new EmpBeatMapVM();
            model.ebmId = data.ebmId;
            model.userId = data.userId;
            model.Type = data.Type;
            model.userName = db.UserMasters.Where(x => x.userId == data.userId).Select(x => x.userName).FirstOrDefault();
            model.ebmLatLong = ConvertStringToLatLong(data.ebmLatLong);

            return model;
        }

        private AppAreaMapVM fillAppAreaMapVM(AppDetail data)
        {
            AppAreaMapVM model = new AppAreaMapVM();
            model.AppId = data.AppId;
            model.AppName = data.AppName;
            model.AppLat = data.Latitude;
            model.AppLong = data.Logitude;
            model.AppAreaLatLong = ConvertStringToLatLong1(data.AppAreaLatLong);

            return model;
        }
        private HouseMaster FillHouseDetailsDataModel(HouseDetailsVM data)
        {
            HouseMaster model = new HouseMaster();
            model.houseId = data.houseId;
            model.WardNo = data.WardNo;
            model.AreaId = data.AreaId;
            model.houseOwner = data.houseOwner;
            model.houseOwnerMar = data.houseOwnerMar;
            model.houseAddress = data.houseAddress;
            model.houseOwnerMobile = data.houseMobile;
            model.houseNumber = data.houseNumber;
            model.houseQRCode = data.houseQRCode;
            model.houseLat = data.houseLat;
            model.houseLong = data.houseLong;
            model.ZoneId = data.ZoneId;
            model.ReferanceId = data.ReferanceId;
            model.modified = DateTime.Now;
            model.OccupancyStatus = data.OccupancyStatus;
            model.Property_Type = data.Property_Type;
            //if (data.WasteType == "DW")
            //{
            //    model.WasteType = data.WasteType;
            //}
            //if (data.WasteType == "WW")
            //{
            //    model.WasteType = data.WasteType;
            //}
            // model.userId = data.userId;
            return model;
        }

        private Vehical_QR_Master FillVehicalRegDetailsDataModel(VehicalRegDetailsVM data)
        {
            Vehical_QR_Master model = new Vehical_QR_Master();
            model.vqrId = data.vqrId;
            model.VehicalNumber = data.Vehican_No;
            model.VehicalQRCode = data.vehicalQRCode;
            model.ReferanceId = data.ReferanceId;
            model.modified = DateTime.Now;
            model.VehicalType = data.Vehical_Type;

            return model;
        }
        private GarbagePointDetail FillGarbagePointDetailsDataModel(GarbagePointDetailsVM data)
        {
            GarbagePointDetail model = new GarbagePointDetail();
            model.zoneId = data.ZoneId;
            model.wardId = data.WardNo;
            model.areaId = data.areaId;
            model.gpId = data.gpId;
            model.gpAddress = data.gpAddress;
            model.gpLat = data.gpLat;
            model.gpLong = data.gpLong;
            model.gpName = data.gpName;
            model.gpNameMar = data.gpNameMar;
            model.qrCode = data.qrCode;
            model.ReferanceId = data.ReferanceId;
            model.modified = DateTime.Now;
            return model;
        }
        private UserMaster FillEmployeeDataModel(EmployeeDetailsVM data, string Emptype)
        {
            UserMaster model = new UserMaster();
            model.userId = data.userId;
            model.userAddress = data.userAddress;
            model.userLoginId = data.userLoginId;
            model.userMobileNumber = data.userMobileNumber;
            model.userName = data.userName;
            model.userNameMar = data.userNameMar;
            model.userPassword = data.userPassword;
            model.userProfileImage = data.userProfileImage;
            model.userEmployeeNo = data.userEmployeeNo;
            model.bloodGroup = data.bloodGroup;
            model.isActive = data.isActive;
            model.gcTarget = data.gcTarget;
            model.EmployeeType = Emptype;
            return model;
        }

        private EmpShift FillEmpShiftDataModel(EmpShiftVM data)
        {
            EmpShift model = new EmpShift();
            model.shiftName = data.shiftName;
            model.shiftStart = data.shiftStart;
            model.shiftEnd = data.shiftEnd;
            return model;
        }
        // Added By Saurabh

        private DumpYardDetail FillDumpYardDetailsDataModel(DumpYardDetailsVM data, string Emptype)
        {
            DumpYardDetail model = new DumpYardDetail();
            model.areaId = data.areaId;
            model.wardId = data.WardNo;
            model.zoneId = data.ZoneId;
            model.dyId = data.dyId;
            model.dyAddress = data.dyAddress;
            model.dyLat = data.dyLat;
            model.dyLong = data.dyLong;
            model.dyName = data.dyName;
            model.dyNameMar = data.dyNameMar;
            model.dyQRCode = data.dyQRCode;
            model.ReferanceId = data.ReferanceId;
            model.lastModifiedDate = DateTime.Now;
            model.EmployeeType = Emptype;
            return model;
        }

        private StreetSweepingDetail FillStreetSweepDetailsDataModel(StreetSweepVM data)
        {
            StreetSweepingDetail model = new StreetSweepingDetail();
            model.areaId = data.areaId;
            model.wardId = data.WardNo;
            model.zoneId = data.ZoneId;
            model.SSId = data.SSId;
            model.SSAddress = data.SSAddress;
            model.SSLat = data.SSLat;
            model.SSLong = data.SSLong;
            model.SSName = data.SSName;
            model.SSNameMar = data.SSNameMar;
            model.SSQRCode = data.SSQRCode;
            model.ReferanceId = data.ReferanceId;
            model.lastModifiedDate = DateTime.Now;
            return model;
        }

        private LiquidWasteDetail FillLiquidWasteDetailsDataModel(LiquidWasteVM data)
        {
            LiquidWasteDetail model = new LiquidWasteDetail();
            model.areaId = data.areaId;
            model.wardId = data.WardNo;
            model.zoneId = data.ZoneId;
            model.LWId = data.LWId;
            model.LWAddreLW = data.LWAddress;
            model.LWLat = data.LWLat;
            model.LWLong = data.LWLong;
            model.LWName = data.LWName;
            model.LWNameMar = data.LWNameMar;
            model.LWQRCode = data.LWQRCode;
            model.ReferanceId = data.ReferanceId;
            model.lastModifiedDate = DateTime.Now;
            return model;
        }

        // Added By saurabh (04 June 2019)
        private QrEmployeeMaster FillHSEmployeeDataModel(HouseScanifyEmployeeDetailsVM data)
        {
            QrEmployeeMaster model = new QrEmployeeMaster();
            model.qrEmpId = data.qrEmpId;
            model.qrEmpAddress = data.qrEmpAddress;
            model.qrEmpLoginId = data.qrEmpLoginId;
            model.qrEmpPassword = data.qrEmpPassword;
            model.qrEmpMobileNumber = data.qrEmpMobileNumber;
            model.qrEmpName = data.qrEmpName;
            model.qrEmpNameMar = data.qrEmpNameMar;
            model.bloodGroup = data.bloodGroup;
            model.isActive = data.isActive;
            model.typeId = 1;
            model.type = "Employee";
            return model;
        }

        private EmployeeMaster FillUREmployeeDataModel(UREmployeeDetailsVM data)
        {
            EmployeeMaster model = new EmployeeMaster();
            model.EmpId = data.EmpId;
            model.EmpAddress = data.EmpAddress;
            model.LoginId = data.LoginId;
            model.Password = data.Password;
            model.EmpMobileNumber = data.EmpMobileNumber;
            model.EmpName = data.EmpName;
            model.EmpNameMar = data.EmpNameMar;
            model.isActive = data.isActive;
            model.type = data.type;
            model.lastModifyDateEntry = DateTime.Now;
            return model;
        }


        private SauchalayAddress FillSauchalayDetailsDataModel(SauchalayDetailsVM data)
        {
            SauchalayAddress model = new SauchalayAddress();
            model.Id = data.Id;
            model.SauchalayID = data.SauchalayID;
            model.ImageUrl = data.Image;
            model.QrImageUrl = data.QrImage;
            model.Name = data.Name;
            model.Address = data.Address;
            model.Lat = data.Lat;
            model.Long = data.Long;
            model.Mobile = data.Mobile;
            model.CreatedDate = DateTime.Now;
            // model.userId = data.userId;
            return model;
        }
        #endregion

        #region List

        //public List<SelectListItem> ListLanguage()
        //{
        //    var lstLanguage = new List<SelectListItem>();
        //    SelectListItem itemAdd = new SelectListItem() { Text = "Select Language", Value = "-1" };

        //    try
        //    {
        //        lstLanguage = db.LanguageInfoes.AsNoTracking()
        //            .Select(x => new SelectListItem
        //            {
        //                Text = x.languageType,
        //                Value = x.id.ToString()
        //            }).OrderBy(t => t.Text).ToList();

        //        lstLanguage.Insert(0, itemAdd);
        //    }
        //    catch (Exception ex) { throw ex; }

        //    return lstLanguage;
        //}
        public List<SelectListItem> ListArea()
        {
            var Area = new List<SelectListItem>();
            SelectListItem itemAdd = new SelectListItem() { Text = "Select Area", Value = "0" };

            try
            {
                Area = db.TeritoryMasters.ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.Area,
                        Value = x.Id.ToString()
                    }).OrderBy(t => t.Text).ToList();

                Area.Insert(0, itemAdd);
            }
            catch (Exception ex) { throw ex; }

            return Area;
        }
        public List<SelectListItem> ListVehicle()
        {
            var Vehicle = new List<SelectListItem>();
            SelectListItem itemAdd = new SelectListItem() { Text = "--Select Vehicle--", Value = "0" };

            try
            {
                Vehicle = db.VehicleTypes.ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.description,
                        Value = x.vtId.ToString()
                    }).OrderBy(t => t.Text).ToList();

                Vehicle.Insert(0, itemAdd);
            }
            catch (Exception ex) { throw ex; }

            return Vehicle;
        }
        public List<SelectListItem> ListWardNo()
        {
            var WardNo = new List<SelectListItem>();
            SelectListItem itemAdd = new SelectListItem() { Text = "Select Ward / Prabhag", Value = "0" };

            try
            {

                WardNo = db.WardNumbers.ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.WardNo + " (" + db.ZoneMasters.Where(c => c.zoneId == x.zoneId).FirstOrDefault().name + ")",
                        Value = x.Id.ToString()
                    }).OrderBy(t => t.Text).ToList();

                WardNo.Insert(0, itemAdd);
            }
            catch (Exception ex) { throw ex; }

            return WardNo;
        }
        public List<SelectListItem> ListZone()
        {
            var Zone = new List<SelectListItem>();
            SelectListItem itemAdd = new SelectListItem() { Text = "--Select Zone--", Value = "0" };

            try
            {
                Zone = db.ZoneMasters.ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.name,
                        Value = x.zoneId.ToString()
                    }).OrderBy(t => t.Text).ToList();

                Zone.Insert(0, itemAdd);
            }
            catch (Exception ex) { throw ex; }

            return Zone;
        }

        public List<SelectListItem> ListHouse(int teamId, string houseId)
        {
            var House = new List<SelectListItem>();
            SelectListItem itemAdd = new SelectListItem() { Text = "--Select HouseId--", Value = "0" };

            try
            {
                if (houseId == null)
                {
                    if (teamId > 0)
                    {

                        House = db.HouseLists.Where(x => x.AreaId > 0).ToList()
                           .Select(x => new SelectListItem
                           {
                               Text = x.ReferanceId,
                               Value = x.ReferanceId.ToString()
                           }).OrderBy(t => t.Text).ToList();

                        House.Insert(0, itemAdd);
                    }
                    else
                    {
                        House = db.HouseLists.Where(p => db.MasterQRs.All(x => x.ReferanceId != p.ReferanceId & p.AreaId > 0)).ToList()
                           .Select(x => new SelectListItem
                           {
                               Text = x.ReferanceId,
                               Value = x.ReferanceId.ToString()
                           }).OrderBy(t => t.Text).ToList();

                        House.Insert(0, itemAdd);
                    }
                }
                else
                {
                    House = db.HouseLists.Where(p => db.MasterQRs.All(x => x.ReferanceId != p.ReferanceId & p.AreaId > 0)).ToList()
                       .Select(x => new SelectListItem
                       {
                           Text = x.ReferanceId,
                           Value = x.ReferanceId.ToString(),
                       }).OrderBy(t => t.Text).ToList();

                    House.Insert(0, itemAdd);
                    // House.Select(houseId);
                }
            }
            catch (Exception ex) { throw ex; }

            return House;
        }

        public List<SelectListItem> VehicleList()
        {
            var Vehical = new List<SelectListItem>();
            SelectListItem itemAdd = new SelectListItem() { Text = "--Select Vehical--", Value = "0" };

            try
            {
                Vehical = db.VehicleTypes.ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.description,
                        Value = x.vtId.ToString()
                    }).OrderBy(t => t.Text).ToList();

                Vehical.Insert(0, itemAdd);
            }
            catch (Exception ex) { throw ex; }

            return Vehical;
        }
        public List<SelectListItem> ListUser(string Emptype)
        {
            var user = new List<SelectListItem>();
            SelectListItem itemAdd = new SelectListItem() { Text = "--Select Employee--", Value = "0" };

            try
            {
                user = db.UserMasters.Where(c => c.isActive == true && c.EmployeeType == Emptype).ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.userName,
                        Value = x.userId.ToString()
                    }).OrderBy(t => t.Text).ToList();

            }
            catch (Exception ex) { throw ex; }

            return user;
        }

        public List<SelectListItem> ListUserBeatMap(string Emptype)
        {
            var user = new List<SelectListItem>();
            SelectListItem itemAdd = new SelectListItem() { Text = "--Select Employee--", Value = "0" };

            try
            {
                user = db.UserMasters.Where(c => c.isActive == true && c.EmployeeType == Emptype).Where(c => !db.EmpBeatMaps.Any(d => d.userId == c.userId))
                    .Select(x => new SelectListItem
                    {
                        Text = x.userName,
                        Value = x.userId.ToString()
                    }).OrderBy(t => t.Text).ToList();

            }
            catch (Exception ex) { throw ex; }

            return user;
        }


        public List<SelectListItem> LoadListWardNo(Int32 ZoneId)
        {
            var WardNo = new List<SelectListItem>();
            if (ZoneId == 0)
            {
                WardNo = ListWardNo();
                return WardNo;
            }
            else
            {
                try
                {
                    SelectListItem itemAdd = new SelectListItem() { Text = "--Select Ward No.--", Value = "0" };
                    WardNo = db.WardNumbers.Where(c => c.zoneId == ZoneId).ToList()
                        .Select(x => new SelectListItem
                        {
                            Text = x.WardNo + " (" + db.ZoneMasters.Where(c => c.zoneId == x.zoneId).FirstOrDefault().name + ")",
                            Value = x.Id.ToString()
                        }).OrderBy(t => t.Text).ToList();
                    WardNo.Insert(0, itemAdd);
                }
                catch (Exception ex) { throw ex; }
                return WardNo;
            }
        }

        public List<SelectListItem> LoadListArea(Int32 WardNo)
        {
            var Area = new List<SelectListItem>();
            if (WardNo == 0)
            {
                Area = ListArea();
                return Area;
            }
            else
            {
                try
                {
                    SelectListItem itemAdd = new SelectListItem() { Text = "--Select Area--", Value = "0" };
                    Area = db.TeritoryMasters.Where(c => c.wardId == WardNo).ToList()
                        .Select(x => new SelectListItem
                        {
                            Text = x.Area,
                            Value = x.Id.ToString()
                        }).OrderBy(t => t.Text).ToList();
                    Area.Insert(0, itemAdd);
                }
                catch (Exception ex) { throw ex; }
                return Area;
            }
        }
        #endregion

        #region ViewModel 
        private AreaVM FillAreaViewModel(TeritoryMaster data)
        {
            AreaVM model = new AreaVM();
            model.Id = data.Id;
            model.Name = data.Area;
            model.NameMar = data.AreaMar;
            model.wardId = data.wardId;
            return model;
        }

        private HouseScanifyEmployeeDetailsVM FillUserViewModel(QrEmployeeMaster data, UserMaster data1)
        {
            try
            {
                HouseScanifyEmployeeDetailsVM model = new HouseScanifyEmployeeDetailsVM();
                // model.qrEmpId = (data.qrEmpId == null ? 0 : data.qrEmpId) ;
                if (data != null)
                {
                    model.qrEmpLoginId = (data.qrEmpLoginId == null ? "" : data.qrEmpLoginId);
                }
                else
                {
                    model.qrEmpLoginId = "";
                }
                if (data1 != null)
                {
                    model.LoginId = (data1.userLoginId.ToString() == "" ? "" : data1.userLoginId);
                }
                else
                {
                    model.LoginId = "";
                }

                //model.NameMar = data.AreaMar;
                //model.wardId = data.wardId;
                return model;
            }
            catch
            {
                throw;
            }
        }



        private HouseScanifyEmployeeDetailsVM FillUserNameViewModel(QrEmployeeMaster data)
        {
            try
            {
                HouseScanifyEmployeeDetailsVM model = new HouseScanifyEmployeeDetailsVM();
                // model.qrEmpId = (data.qrEmpId == null ? 0 : data.qrEmpId) ;
                if (data != null)
                {
                    model.qrEmpName = (data.qrEmpName == null ? "" : data.qrEmpName);
                }
                else
                {
                    model.qrEmpName = "";
                }


                //model.NameMar = data.AreaMar;
                //model.wardId = data.wardId;
                return model;
            }
            catch
            {
                throw;
            }
        }

        private UserMasterVM FillUserMasterViewModel(UserMaster data)
        {
            UserMasterVM model = new UserMasterVM();
            model.LoginId = data.userLoginId;
            // model.qrEmpLoginId = data.qrEmpLoginId;
            //model.NameMar = data.AreaMar;
            //model.wardId = data.wardId;
            return model;
        }
        private VehicleTypeVM FillVehicleViewModel(VehicleType data)
        {
            VehicleTypeVM model = new VehicleTypeVM();
            model.Id = data.vtId;
            model.description = data.description;
            model.descriptionMar = data.descriptionMar;
            model.isActive = data.isActive;
            return model;
        }
        private WardNumberVM FillWardViewModel(WardNumber data)
        {
            WardNumberVM model = new WardNumberVM();
            model.Id = data.Id;
            model.WardNo = data.WardNo;
            model.zoneId = data.zoneId;
            return model;
        }
        private HouseDetailsVM FillHouseDetailsViewModel(HouseMaster data)
        {

            HouseDetailsVM model = new HouseDetailsVM();
            model.houseId = data.houseId;
            model.WardNo = data.WardNo;
            model.AreaId = data.AreaId;
            model.ZoneId = data.ZoneId;
            model.houseOwner = data.houseOwner;
            model.houseOwnerMar = data.houseOwnerMar;
            model.houseAddress = data.houseAddress;
            model.houseMobile = data.houseOwnerMobile;
            model.houseNumber = data.houseNumber;
            model.houseQRCode = data.houseQRCode;
            model.houseLat = data.houseLat;
            model.houseLong = data.houseLong;
            model.ReferanceId = data.ReferanceId;
            model.OccupancyStatus = data.OccupancyStatus;
            model.Property_Type = data.Property_Type;
            using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
            {
                if (data.AreaId > 0)
                {
                    model.areaName = db.TeritoryMasters.Where(c => c.Id == data.AreaId).FirstOrDefault().Area;
                }
                else
                {
                    model.areaName = "";
                }


                if (data.WardNo > 0)
                {
                    model.wardName = db.WardNumbers.Where(c => c.Id == data.WardNo).FirstOrDefault().WardNo;
                }
                else
                {
                    model.wardName = "";
                }




            }

            return model;
        }

        private MasterQRDetailsVM FillMasterQRDetailsViewModel(MasterQR data)
        {

            MasterQRDetailsVM model = new MasterQRDetailsVM();
            model.masterId = data.MasterId;
            model.ReferanceId = data.ReferanceId;
            model.QRList = data.QRList;
            model.isActive = data.ISActive;


            return model;
        }

        private VehicalRegDetailsVM FillVehicalRegDetailsViewModel(Vehical_QR_Master data)
        {

            VehicalRegDetailsVM model = new VehicalRegDetailsVM();
            model.vqrId = data.vqrId;
            model.Vehican_No = data.VehicalNumber;
            model.vehicalQRCode = data.VehicalQRCode;
            model.ReferanceId = data.ReferanceId;
            model.Vehical_Type = data.VehicalType;


            return model;
        }
        private SBALUserLocationMapView FillHouseDetailsViewModelforMap(HouseMaster data)
        {

            SBALUserLocationMapView model = new SBALUserLocationMapView();
            model.houseId = data.houseId;
            model.WardNo = data.WardNo;
            model.AreaId = data.AreaId;
            model.ZoneId = data.ZoneId;
            model.houseOwner = data.houseOwner;
            model.houseOwnerMar = data.houseOwnerMar;
            model.houseAddress = data.houseAddress;
            model.houseMobile = data.houseOwnerMobile;
            model.houseNumber = data.houseNumber;
            model.houseQRCode = data.houseQRCode;
            model.houseLat = data.houseLat;
            model.houseLong = data.houseLong;
            model.ReferanceId = data.ReferanceId;
            using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
            {
                if (data.AreaId > 0)
                {
                    model.areaName = db.TeritoryMasters.Where(c => c.Id == data.AreaId).FirstOrDefault().Area;
                }
                else
                {
                    model.areaName = "";
                }


                if (data.WardNo > 0)
                {
                    model.wardName = db.WardNumbers.Where(c => c.Id == data.WardNo).FirstOrDefault().WardNo;
                }
                else
                {
                    model.wardName = "";
                }




            }

            return model;
        }

        private GarbagePointDetailsVM FillGarbagePointDetailsViewModel(GarbagePointDetail data)
        {
            GarbagePointDetailsVM model = new GarbagePointDetailsVM();
            model.areaId = data.areaId;
            model.WardNo = data.wardId;
            model.ZoneId = data.zoneId;
            model.gpId = data.gpId;
            model.gpAddress = data.gpAddress;
            model.gpLat = data.gpLat;
            model.gpLong = data.gpLong;
            model.gpName = data.gpName;
            model.gpNameMar = data.gpNameMar;
            model.qrCode = data.qrCode;
            model.ReferanceId = data.ReferanceId;
            return model;
        }
        private EmployeeDetailsVM FillEmployeeViewModel(UserMaster data)
        {
            EmployeeDetailsVM model = new EmployeeDetailsVM();
            model.userId = data.userId;
            model.userAddress = data.userAddress;
            model.userLoginId = data.userLoginId;
            model.userMobileNumber = data.userMobileNumber;
            model.userName = data.userName;
            model.userNameMar = data.userNameMar;
            model.userPassword = data.userPassword;
            model.userProfileImage = data.userProfileImage;
            model.userEmployeeNo = data.userEmployeeNo;
            model.imoNo = data.imoNo;
            model.isActive = data.isActive;
            model.bloodGroup = data.bloodGroup;
            model.gcTarget = data.gcTarget;
            model.EmployeeType = data.EmployeeType;
            return model;
        }


        private EmpShiftVM FillEmpShiftViewModel(EmpShift data)
        {
            EmpShiftVM model = new EmpShiftVM();
            model.shiftId = data.shiftId;
            model.shiftName = data.shiftName;
            model.shiftStart = data.shiftStart;
            model.shiftEnd = data.shiftEnd;
            return model;
        }
        //private EmployeeDetailsVM FillLiquidEmployeeViewModel(UserMaster_Liquid data)
        //{
        //    EmployeeDetailsVM model = new EmployeeDetailsVM();
        //    model.userId = data.userId;
        //    model.userAddress = data.userAddress;
        //    model.userLoginId = data.userLoginId;
        //    model.userMobileNumber = data.userMobileNumber;
        //    model.userName = data.userName;
        //    model.userNameMar = data.userNameMar;
        //    model.userPassword = data.userPassword;
        //    model.userProfileImage = data.userProfileImage;
        //    model.userEmployeeNo = data.userEmployeeNo;
        //    model.imoNo = data.imoNo;
        //    model.isActive = data.isActive;
        //    model.bloodGroup = data.bloodGroup;
        //    model.gcTarget = data.gcTarget;
        //    model.EmployeeType = data.EmployeeType;
        //    return model;
        //}

        private SBAAttendenceSettingsGridRow FillAttendenceEmployeeViewModel(Daily_Attendance data)
        {
            SBAAttendenceSettingsGridRow model = new SBAAttendenceSettingsGridRow();
            model.userId = data.userId;
            model.userName = db.UserMasters.Where(c => c.userId == data.userId).FirstOrDefault().userName;
            model.startTime = data.startTime;
            model.NotificationTime = data.startTime;
            model.NotificationMobileNumber = db.UserMasters.Where(c => c.userId == data.userId).FirstOrDefault().userMobileNumber;
            return model;
        }


        // Added By Saurabh

        private DumpYardDetailsVM FillDumpYardDetailsViewModel(DumpYardDetail data)
        {
            DumpYardDetailsVM model = new DumpYardDetailsVM();
            model.areaId = data.areaId;
            model.WardNo = data.wardId;
            model.ZoneId = data.zoneId;
            model.dyId = data.dyId;
            model.dyAddress = data.dyAddress;
            model.dyLat = data.dyLat;
            model.dyLong = data.dyLong;
            model.dyName = data.dyName;
            model.dyNameMar = data.dyNameMar;
            model.dyQRCode = data.dyQRCode;
            model.ReferanceId = data.ReferanceId;
            //model.lastModifiedDate = data.lastModifiedDate;
            return model;
        }

        private StreetSweepVM FillStreetSweepDetailsViewModel(StreetSweepingDetail data)
        {
            StreetSweepVM model = new StreetSweepVM();
            model.areaId = data.areaId;
            model.WardNo = data.wardId;
            model.ZoneId = data.zoneId;
            model.SSId = data.SSId;
            model.SSAddress = data.SSAddress;
            model.SSLat = data.SSLat;
            model.SSLong = data.SSLong;
            model.SSName = data.SSName;
            model.SSNameMar = data.SSNameMar;
            model.SSQRCode = data.SSQRCode;
            model.ReferanceId = data.ReferanceId;
            //model.lastModifiedDate = data.lastModifiedDate;
            return model;
        }

        private LiquidWasteVM FillLiquidWasteDetailsViewModel(LiquidWasteDetail data)
        {
            LiquidWasteVM model = new LiquidWasteVM();
            model.areaId = data.areaId;
            model.WardNo = data.wardId;
            model.ZoneId = data.zoneId;
            model.LWId = data.LWId;
            model.LWAddress = data.LWAddreLW;
            model.LWLat = data.LWLat;
            model.LWLong = data.LWLong;
            model.LWName = data.LWName;
            model.LWNameMar = data.LWNameMar;
            model.LWQRCode = data.LWQRCode;
            model.ReferanceId = data.ReferanceId;
            //model.lastModifiedDate = data.lastModifiedDate;
            return model;
        }


        //Added By Saurabh (04 June 2019)
        private HouseScanifyEmployeeDetailsVM FillHSEmployeeViewModel(QrEmployeeMaster data)
        {
            HouseScanifyEmployeeDetailsVM model = new HouseScanifyEmployeeDetailsVM();
            model.qrEmpId = data.qrEmpId;
            model.qrEmpAddress = data.qrEmpAddress;
            model.qrEmpLoginId = data.qrEmpLoginId;
            model.qrEmpPassword = data.qrEmpPassword;
            model.qrEmpMobileNumber = data.qrEmpMobileNumber;
            model.qrEmpName = data.qrEmpName;
            model.qrEmpNameMar = data.qrEmpNameMar;
            model.imoNo = data.imoNo;
            model.isActive = data.isActive;
            model.bloodGroup = data.bloodGroup;
            model.userEmployeeNo = data.userEmployeeNo;
            return model;
        }

        private UREmployeeDetailsVM FillUREmployeeViewModel(EmployeeMaster data)
        {
            UREmployeeDetailsVM model = new UREmployeeDetailsVM();
            model.EmpId = data.EmpId;
            model.EmpAddress = data.EmpAddress;
            model.LoginId = data.LoginId;
            model.Password = data.Password;
            model.EmpMobileNumber = data.EmpMobileNumber;
            model.EmpName = data.EmpName;
            model.EmpNameMar = data.EmpNameMar;
            model.isActiveULB = data.isActiveULB;
            model.isActive = data.isActive;
            model.lastModifyDate = data.lastModifyDateEntry;
            model.type = data.type;
            return model;
        }

        private SS_1_4_ANSWER FillSS1Point4DataModel(OnePoint4VM data)
        {
            SS_1_4_ANSWER model = new SS_1_4_ANSWER();
            model.ANS_ID = data.ANS_ID;
            model.Q_ID = data.Q_ID;
            model.TOTAL_COUNT = data.TOTAL_COUNT.ToString();
            int INSERT_ID = db.SS_1_4_ANSWER.Max(p => p.INSERT_ID) == null ? 0 : db.SS_1_4_ANSWER.Max(p => p.INSERT_ID.Value);
            if (HttpContext.Current.Session["DateTime"] == null)
            {
                HttpContext.Current.Session["DateTime"] = DateTime.Now;
                HttpContext.Current.Session["INSERT_ID"] = INSERT_ID + 1;
            }
            model.INSERT_DATE = Convert.ToDateTime(HttpContext.Current.Session["DateTime"]);
            model.INSERT_ID = Convert.ToInt32(HttpContext.Current.Session["INSERT_ID"]);
            return model;
        }

        private SS_1_4_ANSWER FillSS1Point5DataModel(OnePoint5VM data)
        {
            SS_1_4_ANSWER model = new SS_1_4_ANSWER();
            model.ANS_ID = data.ANS_ID;
            model.TOTAL_COUNT = data.TOTAL_COUNT;
            model.Q_ID = data.Q_ID;
            if (HttpContext.Current.Session["DateTime"] == null)
            {
                HttpContext.Current.Session["DateTime"] = DateTime.Now;
            }
            model.INSERT_DATE = Convert.ToDateTime(HttpContext.Current.Session["DateTime"]);
            return model;
        }
        private SS_1_7_ANSWER FillSS1Point7DataModel(OnePointSevenVM data)
        {
            SS_1_7_ANSWER model = new SS_1_7_ANSWER();
            model.id = data.id;
            model.No_water_bodies = data.No_water_bodies;
            model.No_drain_nallas = data.No_drain_nallas;
            model.No_locations = data.No_locations;
            model.No_outlets = data.No_outlets;
            int INSERT_ID = db.SS_1_7_ANSWER.Max(p => p.INSERT_ID) == null ? 0 : db.SS_1_7_ANSWER.Max(p => p.INSERT_ID.Value);
            if (HttpContext.Current.Session["INSERT_ID"] == null)
            {
                HttpContext.Current.Session["INSERT_ID"] = INSERT_ID + 1;
                HttpContext.Current.Session["DateTime"] = DateTime.Now;
            }

            model.INSERT_ID = Convert.ToInt32(HttpContext.Current.Session["INSERT_ID"]);
            model.INSERT_DATE = Convert.ToDateTime(HttpContext.Current.Session["DateTime"]);
            return model;
        }

        private SauchalayDetailsVM FillSauchalayDetailsViewModel(SauchalayAddress data)
        {

            SauchalayDetailsVM model = new SauchalayDetailsVM();
            model.Id = data.Id;
            model.SauchalayID = data.SauchalayID;
            model.Lat = data.Lat;
            model.Long = data.Long;
            model.Name = data.Name;
            model.Address = data.Address;
            model.Mobile = data.Mobile;
            model.Image = data.ImageUrl;
            model.QrImage = data.QrImageUrl;
            model.CreatedDate = data.CreatedDate;

            return model;
        }
        #endregion

        #region Ward Number
        public ZoneVM GetZone(int teamId)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.ZoneMasters.Where(x => x.zoneId == teamId).FirstOrDefault();
                    if (Details != null)
                    {
                        ZoneVM obj = new ZoneVM();
                        obj.id = Details.zoneId;
                        obj.name = Details.name;
                        return obj;
                    }
                    else
                    {
                        return new ZoneVM();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ZoneVM StreetGetZone(int teamId)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.ZoneMasters.Where(x => x.zoneId == teamId).FirstOrDefault();
                    if (Details != null)
                    {
                        ZoneVM obj = new ZoneVM();
                        obj.id = Details.zoneId;
                        obj.name = Details.name;
                        return obj;
                    }
                    else
                    {
                        return new ZoneVM();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SaveZone(ZoneVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.id > 0)
                    {
                        var model = db.ZoneMasters.Where(x => x.zoneId == data.id).FirstOrDefault();
                        if (model != null)
                        {
                            model.zoneId = data.id;
                            model.name = data.name;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        ZoneMaster obj = new ZoneMaster();
                        obj.name = data.name;
                        obj.zoneId = data.id;
                        db.ZoneMasters.Add(obj);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void StreetSaveZone(ZoneVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.SSid > 0)
                    {
                        var model = db.ZoneMasters.Where(x => x.zoneId == data.SSid).FirstOrDefault();
                        if (model != null)
                        {
                            model.zoneId = data.SSid;
                            model.name = data.SSname;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        ZoneMaster obj = new ZoneMaster();
                        obj.name = data.SSname;
                        obj.zoneId = data.SSid;
                        db.ZoneMasters.Add(obj);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ZoneVM GetValidZone(string name, int zoneId)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.ZoneMasters.Where(x => x.name == name || x.zoneId == zoneId).FirstOrDefault();
                    if (Details != null)
                    {
                        ZoneVM obj = new ZoneVM();
                        obj.id = Details.zoneId;
                        obj.name = Details.name;
                        return obj;
                    }
                    else
                    {
                        return new ZoneVM();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        //Added By Saurabh

        #region Dump Yard Details
        public DumpYardDetailsVM GetDumpYardtDetails(int teamId)
        {
            try
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();

                string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.DumpYardQRCode + "/";
                DumpYardDetailsVM dumpYard = new DumpYardDetailsVM();

                var Details = db.DumpYardDetails.Where(x => x.dyId == teamId).FirstOrDefault();
                if (Details != null)
                {
                    dumpYard = FillDumpYardDetailsViewModel(Details);
                    if (dumpYard.dyQRCode != null && dumpYard.dyQRCode != "")
                    {
                        HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(ThumbnaiUrlCMS + dumpYard.dyQRCode.Trim());
                        HttpWebResponse httpRes = null;
                        try
                        {
                            httpRes = (HttpWebResponse)httpReq.GetResponse(); // Error 404 right here,
                            if (httpRes.StatusCode == HttpStatusCode.NotFound)
                            {
                                dumpYard.dyQRCode = "/Images/default_not_upload.png";
                            }
                            else
                            {
                                dumpYard.dyQRCode = ThumbnaiUrlCMS + dumpYard.dyQRCode.Trim();
                            }
                        }
                        catch (Exception e) { dumpYard.dyQRCode = "/Images/default_not_upload.png"; }

                        //  point.qrCode = ThumbnaiUrlCMS + point.qrCode.Trim();
                    }
                    else
                    {
                        dumpYard.dyQRCode = "/Images/default_not_upload.png";
                    }

                    // house.WardList = ListWardNo();
                    dumpYard.AreaList = LoadListArea(Convert.ToInt32(dumpYard.WardNo));//ListArea();
                    dumpYard.ZoneList = ListZone();
                    dumpYard.WardList = LoadListWardNo(Convert.ToInt32(dumpYard.ZoneId));//ListWardNo();
                                                                                         //var plainText = "0xFFD8FFE000104A46494600010100000100010000FFE202284943435F50524F46494C450001010000021800000000021000006D6E74725247422058595A2000000000000000000000000061637370000000000000000000000000000000000000000000000000000000010000F6D6000100000000D32D0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000964657363000000F0000000747258595A00000164000000146758595A00000178000000146258595A0000018C0000001472545243000001A00000002867545243000001A00000002862545243000001A00000002877747074000001C80000001463707274000001DC0000003C6D6C756300000000000000010000000C656E5553000000580000001C0073005200470042000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000058595A200000000000006FA2000038F50000039058595A2000000000000062990000B785000018DA58595A2000000000000024A000000F840000B6CF706172610000000000040000000266660000F2A700000D59000013D000000A5B000000000000000058595A20000000000000F6D6000100000000D32D6D6C756300000000000000010000000C656E5553000000200000001C0047006F006F0067006C006500200049006E0063002E00200032003000310036FFDB0043002016181C1814201C1A1C24222026305034302C2C3062464A3A5074667A787266706E8090B89C8088AE8A6E70A0DAA2AEBEC4CED0CE7C9AE2F2E0C8F0B8CACEC6FFDB004301222424302A305E34345EC6847084C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6C6FFC0001108028001E003012200021101031101FFC4001A000101010101010100000000000000000000010203040506FFC40049100002020102040304080402070703050000010203110412213141510513611422719115233242525381A1334392B135622463727374D1F03482B2C1C2E1F106164425364546A2FFC4001801010101010100000000000000000000000001020304FFC4001C1101010100030003000000000000000000000111021221223141FFDA000C03010002110311003F00F7E0D11064004006820000000A000A8017A819C6194D00202802140020280210D1008500000008C1480000000EA5026060A503252900000080A00981828004C1401000008540000008000042900008A04052010868806C9918EE5087200145210A000005042900A0A15014800000002017F4045EA500428004280000080002A32140029001410A00992808850028C852014800000010148000001E4992924D2600A42A004290014CE4710290710000006819DC38BE211A32E46B1DC98289C4D000464346790452E4CF22A036522045526400214CFDE341421A23032689828101400000442800400A042678F22940000010A402826001410A00851FA810BD000AC94B8000C9A20000011999F73A118188AE1C4D0281085EA00CE16726800042990040CA03069CB04E832113326CB15964E3835D0A260358030117292030402157321B8A0294023410A400C000000007EA00000000000042800000000400000000000000000000000000245E40A42900A42902A90A40217A10BD0080A4000A4004291811F30C0611A08A503200CE0A8C949C587CF080D0332CE70801A4B89B2148A0002842900000010A3000A428021480085004052002801000000000000004280000000000000008C0A404028200AA42802000014800000010A00CF50C750C0D319E381CE4665C1F12A2E49CD973B92E03A842445DC3E2CBC58138E781A517CD8C1A0B14101154001021405405200290A0010A1022290000000214000402821400000850000264642A800203F500010A4000A40202B20500011485205091965B58E45000000000102148C080A40AD479B0D2650113A035818032545C14010A0280850214850810A02842900A00020290002900A400200000429001400000000002140000000000000020290010A0080A0080A4000000000A00020428021194815D00000148022DB45200290A00000200000000A80649940504C8C8141323701419DC377A046819DDE83701A213713215A298C8DC06819CB19606C18CB2E581A0672C640D0339006823200D03200D0213645F402EE59C6514E6D253E08D84000001001485004000000000000000195921B32C2B79192002E464995DC6E5DD0172C64CF990FC4BE64F36BEB38FCC0DE58393BE95CEC893DAA95FCC4076048C94A2A4B9300504396A3511D3A4E49BCF603B03C5F4943F048C3F135F96FE607BC1E2AB5FE6D9186CC67A9ED93C45BEC001F325E236278D9130FC4AEED103EA83E4FD217BFC3F223D7EA3F12F901F5C1F19EB7519FB7FB13DB6FF00CC183ED03E23D55EFF0098CDD17DB2BA2A53935903EB80500429895908FDA68836432AD8497066A2F72C81410A5000014199CE308E66D24615F07DD7C501D411B4A39310BA3278E317EA07400CDB62AAB72971034538D776E92528ED6F91D408D7BD93466439F36C0D10612E09F1EA0205200290A400000000000000000046B8148079B5F29C694E126B8F43E779D63FBF2F99F4F5B1DDA597A1F3B4918CEF8C66B29F42AB2EC9BFBEFE66773FC4FE67D8F67A57F2E26BC9AD7DC8FC868F8B9630FB33EDEC82FB8BE45DB1FC2BE4347C3D8DFDD7F22630F8A3EF617647C7D62DBA990D1F4F4B2DDA783F4353B615F09C9238F87CB3A75E879FC4E3EF41907ADEAE85F7D1E4D76A2BB6B8C60F2F278B0FF0BF90DB2E7B5FC8B8355552B67B60B89DA7A1BA1073938E11341251D42CBC2C1F4351755E4CD6F8F2EE07CAD3CB17C1FA9F71F18FE87C083C5917EA7DE8F18AF80A3E15BC2D92F53D54683CDA94DD98CF448F3EA9635335EA7D2D03CE963E80725E190FCC7F22FD195FE391DEED55744B6CF3938BF11A7FCDF2207D1B4AFBD21F4751FE6F993E92A7B48D55AF85B628462F8818BB434C2A94A29E52EE7CEA5E2C8FC4FB972CD325E87C183C4D7A328FD02E48A663C60BE024F116CC8E36D929D9E555FABEC621545EFDD16F1D59AD2C93CF0E2DF166EBC666F77322393AB7422A2D27D0DD52954D427FA1E7B2728D91697D9475DEEFA5CB1868618F527959470BAEB2BE3B63FAC8DD1272AD3395BA69CE537171F7BABE858A47516CA718C543DE449EA6C8C9C538CA4BA24152EBB211826D28E1C8AEA71E1E4EFECF251CEDF3A51AEC9B8E73C238164EE9C9D5C5BF446FC9B9571584E5BB2F8F23ABAAC5373849272E79039E9D5B2DD1766DDBC391CDC253BA4A5636E0B3C0EB569E7B66AC7F69F34214D95C2504E387D7AB039A9CBCB87BF394E5C964CDF19A86D9293973CEEE08EF0D3C9423EF2DD1E4D1A95339D6E32B38BEB8038AAD2BEAE32795C789ED382D3BF323273FB2B1C8EC055F690B23B59966B3B9FBCC091E650B0020000290000000000000000098E392802148062E8EEA64BD0F8FA77B6F8FC4FB5CD60F873F72F97A48AAFBBD0F993D7DCA725C3833E941E6117DD1F17551D9A89AF520EDEDF7FE25F233EDB7FE33CC528FB1A2B1DB4664F2F278BC4963519EE8EFE192FAB92ECCC78A2F7A0C0E9E192CD725D99ECDA9F347CEF0B97BF38FA1EAD64E70D3B941E1A20EF85D91CB531CE9E6BD0F94F577FE632C6F9CE718CE6DC5BE3EA5C1C320FBAA9AB1FC38FC8BE5C3A423F21A3E0E70CFBD4BCD307E87C4D42DB7CD7A9F5F472CE961F0147CDF1058D54BD4F67864B3A76BB33CBE28B1A84FBA3B7853F726BD47E0CF8AAF7E2CF064FA3E2ABDD83F53E6640A76D24B1AA87C4E193A69DE2F83F503EECF8C25F03E03E137E8CFD03FB3FA1F9FB785D25FE6241F7AA79AA2FD04D660D18D33CE9E0FD0B7C7306F2F81071A6A4EB6F7CBE08515C2CDDC649AF5394673AA5DB3DCE9545C65B95B159799138D662C74F5ECDCD36F3DCD595571A9CB1D3B91E2557BB349E4E1658E525152CC57EEC5AB7C8F5D2D42B8A6F895DF05D5FC8C69E0F2E525C7A3394EA9C9B5896E72FB59E091611DFDA6B79C6EE1CF813DAABE185279E5C0E15D5638B8C73B53EBC3258533AA55FDE9678BE88AAEEB509D9B364B2CE939EC839359C1C211B3DA37589CBB7448F44E2A7169F5038FB4CB629F952C3E5C4E71F32CBA5BE1B9AE4B77046ACA2D6A294B724F97234AAB1D937F673CB0C0E0AFB2A73CC56174C9D3DA6CCC56209CBA16AD3B51719E39E7775625A5CD9192967BB6067CFBA6FEAD2924FED743D51CED59E2CF3CB4F66CD90B311F81E88ADB14BB009161CB249163269704058F201721C8200000000000000000000042802000087C7D6C76EAA67D83E5F89C717A7DD162BDFA596ED341FA1F3BC4A3B7559EE8F6F874B3A6C76671F12A673B22E1172E1D00F9C53AAD2DEFF9722AD1EA3F2D81E8F0B97D64A3DD1DBC523F5317D99CF45A6BAABB338E238EE77D7ADDA597A01E3F0D963538EE8FA56D6ADADC1BC64F91A3963530F89F69BE1914789F86D4A2DEE933E6AF767F067D19789438C5419CE3E1D29ADEEC4B3C71803E8C1E6117E85335C764231CE708F1EA35F2A6E7055E71EA41E3D72DBAA99F47C3A59D2AF4670869D6BB374E5B73D11EBD3D11D3D7B22DB28F178B2F7A0C9E14FEB26BD0E9E2CBEAA32ECCF3F85CB1A8C77407ABC557FA3A7D99F20FD0DB542E8ED9ACA38FB069BF07EE41F10D54F1645FA9F67D874DF968D2D1E9D7F2E25D1DA3C62BE07C1D52C6A2C5EA7DEF81F0F5EB1AB9920FABA279D2C3E0773CBE1AF3A589E8B2BF316374A3F0039DFA656BCE70CF33D14D7297EE7A7D9A3F8E7FD43D96BEAE7FD44C4EAF32D24FABFDCEB5E9211E3292F99D3D96AED2FEA2FB2D3F83F72754EB1D37422B198AFD49E6D7F8E3F338CA3A583C38C726E15512598C22FF0042B4D79F57E647E64F68A7F3226BCAAFF047E4315C5A5B6397E8518F6AA7F18F6AA7F13F91D76C57DD5F2185D80E5ED55F453FE91ED31E909FF49D801C3DA3B5367C879F3E944C97EA954F6A59918AB5BBA589AC17AD4ED1D3CEBBA50FE674AE5392CCE3B4D279E408A31C36F064971448F4E441D000540000000000000000000008469BEB828020C800783C523EEC247BCF2F894776973D98572F0B9A519A6CF779905F7E3F33F3D97D0659707E83CFAFF323F333ED34FE647E67C1E3D9976CBB4BE4307DC7ABA3F32271D46AF4F2A27153CB6B91F2B64FF04BE4554D9F972F900A65B6E83F53EFF35FA1F0569EECA6AA97C8FBB5E7CB8E561E051F0AF5B6E9AED23ED69A5BB4F07E87CDD5696E9EA272856DC5BE67AE8B63A6A235DEF6CBB01BD46B61A7B36CA2DF03CDE43D7C9DD16A31E49135517ADB54B4EB728AE2CF5686AB29A5C6C4B99070763F0EAD41ADF9677D26AFDA6525B76E0CEBF4D3D4462A18CAEE6343A4B34F64A5371C35D00DF89C73A5CF667CBA2E7459BE2B2CFB5A9A9DD4CA11E0D9E0FA2ACEB647E45187E2977E1893E94BFB47E475FA25FE6AF917E89FF5DFB0F11C3E92D47F97E465F88EA3F12F91EAFA263F9AFE45FA2A1D6C90F078FDBF53F99FB1C2739592729BCB67D45E154FE3932FD1747797CC0BE14FFD1B1D99ED3951A7869E2E30CE1F73A11426E59C09A6E3889C6353CE64D116477CFA9CEFB142B6F386F919F2B8F35F31A9A94EBCF65C02729E78F9F2971E675D2DDB6DC37C0E50A9CFECAE46ABAF1A88C249733323CF375F513CACA39CFF008D59D0E76FF12BF89B7A1D0E7E771C28FEE746F08E1BE2E5B974E64591A8DEE5CA26EC96C839359C185647A40E8BDE8F15CC15F2AC9B9C9BEE67A9DA55EEBA4B925CD999C23B374338CF53D32C71C7BF4D273A936B0753352C5715E868E17EDD2230961AE030DB5834DB72C3E8455200114000011A7D18028000000085040291F1452013801D40024A319C712594523E4F01585454B9571F91A55C3F047E47C99EBF50A4D6E5C1F630F5FA8FC7FB1707DAD91FC2BE45C2EC8F86F5BA8FCC667DAEF7FCD91307DEC03E07B4DCFF009B2F99F5F41273D2C5B79607A0657747C7F1294A1AA7894B0D773C9BE4FEF4BE607E8F72FC4BE67CCF138F996C367BCF1C71C4F04774BEF7EE7D0F0897BD6228DF85C670DEA5192F8A3DD39C60B33924BD4A793C4E39D237D9907A237D5396D8CE2D9B949422E52784B9B3E27874B1AB8FA9F5B53283D3CD3947977022D669DB4A3626D9DCFCED4F16C5FA9F7BCEAB6F1B23CBB81C5EBF4EA5B773CE7B1E95C567B9F9EB64BCF938BCADDC19F7EA79AA2FD00F3D9E214D7370929657A1E8AAC56D6A71E4CF97AAD25D66A272841B8B7CCF7E8A13AF4D18CD62480CEA75B0D3CF64A0DF037A5D4AD4C5C9476E0F3788696DD45B175A4F0BAB269BFF00D3EB7ED1C373E18E207D0070A3575DF3DB5E78773B81271DD1C1CA34BDB86CEBBA1F897CC9E6D6BEFC7E64595CFC9F81D26BEAE4BD09E755F991F99977D38C798816EBC319C766C96571E874AA4A7AA8ED5C12333AAB726E372F91BA15354F739CA4FD22C98E32727B8E76FDBAFE24F698769BFF00BA6657464D3F2EC78FF29A75777C4CAAE31E48C79EFA5367C879D3E944C8BAE9B238C60A91CBCDB7A50FE637DDF92BFA826BC77C9C3512C1CDD929ED8F0E7D0F64EAB2C799530FEA2468B20F31AEB4FE393ACE5318BC6BD315EEAF814E58D477AD7E86A0AC5FC4927F04736964F0B2229E33C849670740ACACE3894A408A0100A084934B8B60519245E789400032000005200008398021489A6B280578E5E1B54E6E4E52E2CF997C15774E0B9459FA03C57F87C6EB65639B59E8914634DA1A2CA23396E6DAE3C4EDF47E9BF07EE70B6F96814698ADCB1CD9CFE94B7A422072F10A2145D1508E22D1CEBD4DD5436C27B627B288AF1094A5770DBC923BAF0DD3F693FD40E7A08C353094EE5BE79E6CE5E294C2B841C22A3C78E0EBA95EC34A7A7E197C73C4E3A5B1EB6ED9A8F7A2965203E79A8CE50FB3271CF667DBF61D37E5235EC9A7FCA88D1F2F416CDEAE3BA4DE7D4FA7AD8EED2D8BD04B4F54212942118C92E0D1F26ABEC9DD1859394A2E5C536079F3D9972FB9FA1F22BDAF15C570EC7E7E7EED925DA4119C7C41FA1A630F260D463CBB1F175CB6EAEC5EA15C0FD0691EED356FD0FCF9F6FC3E6BD9219690A3D12B2107894E2BE2CCFB453F991F99F2FC59C5EA22E2D3E078A1094DE21172F82260FD03D550BF9B1F99E4D7CA3AA8C6143F3249F43E64AAB211DD2AE497768F67843FF4892EE8B88EDE1DA6BA9B9B9C36A68FA128A9AC496514115CFC8A7F2E25F26AFCB8FC8E800C7975F4847E425E5C17BCA2BF43679B5B9F2A2E38DDB9632076DD0DFB163776347825E7577C9B9A94DC3861722464FEC5564E6F6E64DF403E80CFA9E056798D28B970871C239D4ACF2EC84137C38BE207D34D3E4CC46E8CA535C9479B679F4317194B83DBF0C1CECAA7294DB839477E71DC0F5D572B65251E51EBDCE879B49192959275BAD37C133D2403164F64782CC9F246CE56F09465D132879537C656BDDE9C8B093DCE13E6BA9BCA6B3939C1A9DB29478C570CF720E8FA173C4CC9F146CA80000000011A4F994805042800080500002024B970019E38453297CCA073FB16A4B94BA1D0E50DD3B3CC9476C52C463D7E2750AE1AAD52D36DCC1CB279BE955F97FB9DF5BA796A2118C3194FA9E3FA32DFC7103A457D256665EE282E9D4DFD175F5B2466AAA5E1F195B36A49F443E958F4ADFCCA3D3A6D2434CDB8B6F3DCEE78A8F10575D1AFCBC67AE4F69062EA617C36CF3839D3A3A689EF8279F89D6D93857294565A47CE8789D93B231708ACB03E8DAE4AA938F0925C0F915EBB512B22A53E0DF13ECB598E3BA3C6BC3684F3EF7CC0F6738FE87E76DCC2F9639A9703D976BEFAAD94138E22F0B81E9A74545D546C9A7294B8B7928F9AF557BFE6CBE672E67DC5A0D37E5FEE7CAD7551AB5328C16238E0073F3ACE5BE5F330DE5E5BCB67D5F0FD3D166994A75A72CF16CF5FB2E9FF00263F21A8FCF8CBEECFD02D3D2BF951F91AF26BFCB8FC86ABF3A7D2F07E12B11F47CB87E08FC8D28A8F28A5F01A3C7E2B172D2F059E278FC32328EAB8C5F15D8FB1CC1000CAEE8E7A99B869E72873480EA43C776AB6E92328D91F3199BA774B510846EF2E2E396C0F70714F9AC9E0AAFB76DF08CFCC705C246289B728E352DCF3EF2901F4B1C7380925C9247CCBE51F6C9AB6E9C638E0A3D4EFA0B3742C6E4DC53F77773C01ECE1C8725D91F2E9B651D479F297BB64B0964F46BE364EBCC64A35478B5D6407B39914A2F386B8733CD7DCE1A78460D6FB1617A1E5AA2E5A39D7196EF7B8E3A81F4A36427F624A5F01BE3E66CCFBDD8F05128FB5C63541C22E3D57337555E578861CDCA4E396D81E99EA298D9B2535BBB1D3835E8CF9B7C65542E5E5EE7279DFD8F7519F2219EC01D15BE8FE674492588AC2400199F43A2394FA1D23F642280402820028214085200000000140CA8F1CC8D10000130040081540383D669D73B101AD4D4EED3CA11E6CF9FF0045DDF8E27B7DBB4DF9867E90D37E37F203853E1D6576C67E647833E89E4FA4B4FDDFC89F49D1FE6F901EB92DD16BBA3C11F0B4A4A5E6F27D8DBF14A3B489F4AD3F82407BBD0F9F7F894EABA5055AF75F73DB4D8ADAE334B099F1FC4A3B7592F54079EDB1DB6CA72E6D9F6BC3A5BB471F43C1A4D02D455E639EDE3C923E9E9685A7AB62939712D1D8F91E2B1FF494FBC4F5EB7592D2CA2A304F28E354578949CEDF776F04A241BF09966892ECCF79F3EE5F46D59A78EE7C771AD0EB27A9B5C67B5617403DC78FC4ED9D54C655C9C78F43AEB6D9D3A794E18CA3C1A6B65AEBBCBD43CC52CE101E5F6BD47E6C8F4F87DF64F5494E7292C7567BFD834CBF948DD7A5A6A96E85693EE513579F66B36B69E0F83E6D9D6C97CCFD1CA2A4B12594CC2D3D2B9530F9107C1A66FCE8664DF1EE7DEBD3969E492E712AAAB4F8571F91B03C1EC705A1E14FD6E3B712CF4CEDBA9F320DC547DE3D2ED9E70A993F5C8F36EFC87FD403C955D32850956DF63C93AB516ED83A611C3E3664F5EFBFF257F50DDA8FCB87F501C2745D1D4BB2B842598E3DE648E96FDB6B94A0A7670E1C91DDCB50965AAD7C599F32DC677D580397D1B4AAF0B3BD2FB4D9DE74CE7A5F2A4D6EC7332E56A8EE775697C047CC943779F1C7FB206A5A6AEC8C5591DDB518AF451AE1628BC39755D0D4A338FDBD463F43194FFF00C9971F40354697CBB3CC9D92B258C2CF446FC94EFF003B2F2960C382D9BDEA2CDA73DD4E32EEB1FEA0749E8A139E652961BCB8E7833BF04B08F0C67549E3EB3FA9852A1AE309E7E207B72BBA194FA9E5A215591949D58C7A9DE98C143308A8E482CF8E0E8B9189349AC9D160A81305004290A008520141014520040282000C00211B48A65C16EDD90294802A9F9EBE3B6E9AED23F427C4F108EDD5CFD44188E935128A71AA587C8D7B0EA7F2DFCCFADA49EFD2D6FD05BAAA6A96D9CF1228F95F47EA5FDCFDCBF46EA7F0C7E67D0FA434DF8FF627D25A6FC4FE40787E8CD4768FCCF35F4CF4F66C9E338E87DDA3510D426EBCF0EE7CEF178FD7425DD01ECF0C9EED1C7D19CF5BA19EA6E538CA31C2EA67C1E59AA71ECCF4EAB531D3414A5172CBE841E68CBE8DAB6D9EFEE7C31D0EFA4D647552928C5C707CED6EB63AA845460E387CCC68B55ECB3949C7765147D4D5E93DA9C7DEDB8F42E9349ECBBBDFDDBBD0F2FD2DFEABF73AE975EF5172AF628FEA41E8D4E9E3A9AD424F6E1F439E9B430D3D9BE339378EA75D458E9A2538ACB8AEA7868F12B2DBA30708A52607AF5D1DDA4B3E07CBF0D963591F53EC6A1668B17F94FCFD564A9B14E38CAEE20FD05F394299CA3F692E07C8FA4F53F8A3F224FC47513838B92C3EC8F2C12728A7C9B2E0F53F12D4FE3FD891D7EA5C966CE193EA4743A6DBFC25C8F876AD97492E9208FD245E629FA14C52F34C1FA1B22B13DCE71C72EA70955A8797BDAECB27A8E73BA1096D6F32EC9710387B3DF8FE27FF00E8EB6D329D718A9E31CDF70B510970AD39CBB224E528E37CDE5F2856B2C0E7EC927CE68AB478CFBCB8FA1BA6EE719EE5C70B276784B2DE101E6F6358E325F23AAAB156CC8F35CB8551DDFE67C1154A6A694F6F1EC066FABCC8A49F14661A64A3EF49E7AE0EE67CD8671B9013CA83AD56D6628CFB355D8D79B5F723BA09E32FE403C8A92FB3CBD4C4BD9EBE385C781CF556FBA9AE5D11CB4F276D989C7315C4CDAE9C786CD7B631825EEAE0CD2492C238D37B9CA4A51DA93E0762B9B13E85595C84965A3A22A32B2528019000000000014000400000008000004040154F95E2D1C5D197747D53C1E2F0CD509766074F0B9674B8ECCF2F8BC717425DD1D3C227C2C8FEA5F178E6A84BB328F940100FA5E0F2F7EC8F7593A78C47EAEB9766797C2A5B75697747BFC523BB48DF660797C1E58B671EE8F578AC73A4CF66783C2E5B75897747D4D747768EC5E841F9F3A515BBAD8D69E32CE674A27E55D09BFBACA3D97F863A69958ECCED5CB07934D779172B319C1F46CF10AF51074C212CCF826CE6FC266A2DBB570F40337F89BB6A943CB4B72EE78AAB3CAB2334B3B5994B32DBEB83E9AF085B72EEE9D80E73F16B65171508AC9F3CD4E3B2C947B3C1F4EAF0BAE75C64EC9715903E5C78C92F53ED43C374FB62F126F1DCF8F38ECB6515D19FA2A5E6A83F403E45BAFD442D942334945E1703DD4E8F4F65519CE1BA4D65B6F99F2B5AB6EAEC5EA7DAD13DDA4AFE00768C54629457045008249B516D713C718B51CC94A7B9E5A89EC7249664F08E2DECF7AB9C76BE8C0CB93C2946BD8D3C25DCB194D4AC96CDD676F4109294BCCB2C8BDBC9479213B6A93CC6CC4BB902529BAE5BE0A3148D5752718CAC6E72C75E9FA1CE52529415B63927C5452C23AF9F0766C59C947426D5BB775280070CBE915F23B9C375BC709F322CA7BFB738FD84B2F835F01F58F9E7F41B6C6E3C3910D4D5C3755EA8E7A69BF26508ADB248E9B2C72F7B89CF510BE52C57F65F60D77F8E3CD0B25BF126D9F46B79AD3671D3E97CA7BE7C65FD8F488E711FDA46CE7D51D0D0000000000000000010000520000000000325200AA797C4A3BB472F4E27A4C6A23BF4F647BC40F99E153C6A5AEE8F7788C1D9A5928A6DA7C11F2B453D9AAADBEE7DCF36B5F7E3F32D1F03D96FFCA9FC8D7B26A3F2647DCF3EAFCD8FCC9ED347E747E64D1F2B49A5D457A984DD72493E27D3D6C77E96C5E81EAF4EBF9D1F998B359A675C979A9E501F23472DBAAADFA9F7AE5BAA9AEE8FCEC25B6C8BECCFD1AF7A3F145A3F35C9E01BBA3B6E9C7B499808FA14F86DC9C2CDD15D4F44FC56A8EE8EC936B812AF13A6354632526D2E3C0E1F46D97BF36328C54F8A4C2BC0E599E71D727D35E2D1514BCA93C2EE7CEBEA74DD2ADBCB8F53D9A7F0C95D4C6CF31473D30078AC979964A78C6E793F41A496ED2D6FFCA7857847FAEFD8F7E9EAF2298D79DD8EA41E69F86573B25394E5993385DAEB34B679318C5C61C1367D43C97F87D77DAEC94A49BEC072A3495EAEBF3ED72DD2E78E08F7555469AD570E4BB9F3AFD44F40E3454938E39C8F5687513D452E73C673D00F3F886B2ED3DFB60D28E3B1E8F0FBA77D0E5379964DDDA4A6F9EEB536FE26A9A6BA23B6B584C0DCA119AC4913CA86D51DAB08D00279704B1B5609E5C3F047E468A0676AEC8B85CD200000008504029C2ED47956461B739EA76332AE129294A29B5C80E13D5A8456E8E1B9631D91BD3DCEE8C9B4961F43A6C8FE1414545622B1F0034400823E68E873CFBD83A1502148050428000010000000000000000010019290A1418CAC77000F85A8A5C35128C53DBDCE5E5D9F825F23F45C3B21C00FCEF936BFE5CBE46BD9EE7FCA97C8FD06467D4BA3F3EB4B7FE4CBE46BD8F50FF9523EF67D467D49A3E12D0EA7F2D9F6E9CAA60A4BDE4B89AC8C81F2B51A0BECD44E708ADADF5673FA3352FA47E67D9C8CFC7E407C7FA2F51DE1F33EB69E32AA88425C6515C4DE7D1FC867D181F3F55E1D66A3512B2328C53EE7AF4B54A8A235C9EE6BA9D78F6638FE1605E3D89C471FC23DEFC3FB80E2388C4BB7EE312ECBE6079F51A286A66A7393CA5D0DE9F4F1D341C6126D37D4EB897A0C4BD0071EE38F72ED977436CBBAF900036CBBFEC36BFC4001763FC4C6CF560405D8BBBF98D8BD7E60641AD8BFE98D91EC064995DCD6C8F643647F0819CAEE86E5DD1AC47B170BB2039EE5DC6E5DCE98400E7B90DDE8FE474007249B9E70CEA00408520029001400040000000000000001014804DABD4BB513DEEE871EFF00B00DABB176AEC4E3DC7BDE805DABB170BB132FAA2E530185D86176000BC013A940148000000A0800A0100A53250290000000000028000020028000100004013000000000008500429000000000001D48050000202810A080520004C80001724005041D70051C0850271E85DDC70C078600A662FA33405042800000CAE40000520028214010A40000028200294C94000000040282000C2E63E0390552027508A42FE8401F0002004000A40001485004000A40667251E60681172C94000000040000026577299DB1EC86101A219E3FA1AE61541001A0401125C93EC68CCFECFC4D014100140000A40050428000000000CE4000000000000000000008CA40292450008520000000001014800100141001410A00113280042810A400520C800400085200066594F28D124F11606B994CC3EC20051D4199379C479F7EC05E72F45FDCD112C2C2281410014A4000000504E8502820005200000028200282002821401000000019042800000042810000010013A948C055000408688C2808B91420000000009B00814000464A428024B8F0EE1B4B8B2478B727D7901AE85063739708F2EE0572E3B63CFF00B163C104925C02E41540011413200A00028214014800148000050202800000000000000000042900A085000002732820000010148C0020028214007C811F2011E2B81400A000200802A900603A1CE996FAF2DF136F9609B17DDE18005E4B246F1CCCB697197E88235CDE5FE847251E7F226652E4B6AF5E66A304BE3DC0893971972FC26800A300040A67AF33400A4000A428000014000010A0014010A00000002000500000000214019EA689D4A150A004080A04000020004042855000440501500000A080000038E400042800728C1F394B8FA1B8C52E5F338C2524B317B9763A46D849E3387D981B000400014010081485000000320602A800205214088A0000001480055040052000504284429001402001C400A060300085027503A800060010000002810148800280000004280200502004038C3DDB65179F7B8A3738465CD71EE62DCAC35D19D30073DF3AF8496E8F73A466A6B316539CE2E12DF0E5D501D0A8CC64A71CA332F7F293C47FB81ADF15CDA2EF8F7114A2B0921B53E680A531B1673178312BB64F634DBC65B5D00EC08B8ACA28400014293726F09F14500520028264640A0800A0800A0002820000A400014080140801400230000201485200000100000018E39028000000000000200008DA5CD8DDD930290997BD4787A9A038D99941A35079847E04B1E2B9342AE35C581B21401E7CBA6728F46B81D92C4629238EAE3EEC5F5C9DD3E5F002843A002BE3C38A38D51FADB573C2C659D8E557F12CC72C816A6D438F14B81D177315F094FE26B97C00D18B1B6D571E0E5CDF646CE75FBD294F1D7080B384547DD496391B4F2B2665C22C2E8BB01A03A138814CF99052C6EE256D46397D0E75453A7125CF881D7031D99CEA6F8C658CC4D81789481BC26FB105072AF29C5C9E772C9D31C4A283164B661259937C11CA5192BEB529EE7CF041E82E4990514A67208282710514A6725C8019200290E765AA12DA965F5F43A2E280A09D4A04409D47500320105064D14085000852100A40514800000963DB5C9FA019AF8EE97766C91588A450318E183147F0D67B9AE8628FB32E1F780EA4050395EB728AC7365A9F0C3E71E045EF6A7D20BF72CBDC96FE9D40E810400A72D3FD893EAE4CEA72A1FD5CB3DD816AE2E7FED1D0C551FAB4FABE26C0CE711797C912A4E35C512F5F54CD37B63C3F4013E2F1DB8B1159E622B877EECDE000000C5D854CB3D8D47ECAF812D5F533F812AE35C7E0047EEDF17F89713A67D0E6BDEB9F68AC1D0018B9BF2F0B9C9E0D9CE6F36D71C7A81B92C38FA3164D570727D04BED47E2633E6CF1F762FE6C0D5717C273FB4D7C8CAE3AA7E91E27538E9FDE764FBC80EE4C033649423BE59E004BA71A6B737CFA2EE4AA33DBBA6F74A5F2471BE0F6FBCFDFB25FD2BB1EA4B09202179229383209945E273BEC5547DD5993E48DC7728ADDCFA802378E32E8578673BE5B2993CF17C101CA09D8F8F39F3F448EE935D4CD55EC8F1FB4F99BC30297264BCC0BD41064003329A8F3E7DBA9374DFDCC7C581B0637B5F6A38F5349A92CA28A01C7CD93F797D9DD84BB81D80040000000140C5BF652EF246CC4FEDC17A81B33278E5CDF22939D8FD1019C76E0620F6CA49F73A995FC592EE80A8964B641B5C5F45DD999E2BE29F17CA2BA88E5CF75BC25F757602D50D91C7DE7C5B35CD712900E6B353EF0FEC754F2B2818F2F0F316E20743CF5BF73CB8F3727FA23AFD6778FC8C511D939AE6DF1C81DB90264A066D4DD524BB12B7BA2A4FB702BF797A7F73107B25B3A7403AA2911409C720A00925BA2D3EA8E75CD468EED70C7A9D25251FB4FFF00738D69ABDB9FDEE315D80EB5C36C78FDA7C5FC4D0000E7F6B52FB462753954BEB2D7EA02D961A49FBCF91AAE1B22A3CFBBEE66BFACB653E91E4750274672D36155CBA9D67F625F03347F063F0036728E6DB7737EE47ECFABEE2E6DE2B8E54A5D7B2EA748A518A8C56120385FC6FA9671C4F41C3539DF5BFF0031DC01513072BA6F0E11CE71993EC066A5E65B2B5FD9CFBBEA7A3A1CE949530F81B934936DF040664D24DC9E12EA79ADCCA75B6F1EF7BB13B2CDB2DD24D457D94FA92CE3A9AD76E2075C80C013A71092C70E052904E1D4CD92DB0E1C5B3525939CFDEBA30E8B8B01186DB1C9B797D4D97EF00275C1CF842EC7DD9743AF4316FF000DF7E8FB145B65B2B93F438C5351AAB78FC4CD5EFF00D1B39E66A1179DF35C7184BB01D080841A0428000140E73FE347E0CE8739FF001A3FEC81B313CC65BE3C7BA364039798BEE6E7E983329D9BE2DC547A6799D7A892CC40908C5714F74BABEA6A493E689852E380F72E5C7E204E30F58FF636B0D7031BD72970657EE7BD15C3A81A044D3592810C4BDD9A9FCCD802EE8E339433BBD17F733849E708D730065ACAE46C806333874DD11E6AFC13F91D0018F373CAB9BFD066C9724A0BD78B37D0A0661051E3CE4F9B7CC96477478735C8D8033096F8E7AF53461C1EEDD1C67FB977CBEF425FA01B3CDB9A56A8AC66476DD29725B7E3CCCC6AC58DE78672901B8476C144B9290049662FE073AA4A3A7CB7C8EA708D72DCE325EE6EDDF10374C6496E9FDA973F43A1001CF5316E0A49FD8793A45EE8A97741A4D34FA9CE12956B6384A58FB2D01B9CDC52496652FB28461B60E39CB7CDF764841E77CF8CDFEC6C0E55C9C21B650936B960D6D94DE6CE4B9457FD71360088E32C3D62EFB4EC70FF00F2F97403BB2900131EF1A224520899CED7B6EAE59E7C0E9D496477C71D7A144C36CB87DCC6F943DDB163B4BA0F36B5CE688349BEA4B64BCA96EE41DBB9660B87E2970462107396F9BCAE9C00C5B9943324DB8BE08F4744CCDB87097C0B571AE2FD00AC149D0014000080A29CE7FC483F89D0E76F383ED20360060611513F42A209F61FA3344E67CEAB4744B41A0B1D7EF5D7A84DE5F15B9947D1238E3ECBC1CDE9344EDD453A5AE755F447729A936B97AB38D3AE764AB83AE719CD6536B09F0E8075DD2AA5EF2F75F54758C949662F27CBB74B4C7C1F4B7A862DB2C7193CBE2B2FFE47B9786E8FE98950E9FAA546FDBB9F3CFC4A8EFC89C4F3AD3D165345DA1DFA78DD6797359CF0EFF131ACD3787C2BD4D55669BA94B8CA7FC4EB8C67881EC4B89A39CFC3FC3FDB7D9214CEBB766F538CDF0FDCF3F85E92AD73BEED6454E5B94134DAE297A107B01F36BC7BBA495DE454A765729359CE3FF93AD9A0AA3E330D2D59AEA9D7EFE1F16BFE9228F710F1D9469969EED4787C674D94CF63CCB2A5C71D4EDF47E8BDABD936D9ED3E5F99E76E7CC0EE0F0D34E9E7A6A751AF8CEEB2FB3CB8E1E36F1C743BD30746A6FD36E72AEADBB1BE6935C883B80028320002229101A000400230AA085018290A00000085200383FF00B5ACFE1E077384DEDD556DF55803B0236B1C781956C7EEE65F0406CA63749FF2DFEAC7D63FC31FDC0D7DE29CF64BAD92FD10F2A3F7B32F8B02CA708FDA68CE5B5EE412E3CE46D4631E5148A061438EE9BDCFF646B25260037C3919AB0AB5C4D61F53308FBA06CCBCE4D2E1C03209D00C0CF70007EA0019B566B7E9C4D3274C004F314CA6297F56BD0D9463A0450414F251FE17E15FF12BFF00133D67820F551D2E969F65CFB3DBE667CC5EF716F1FB951ECA7FC63C4BFDDFFE48F1E9B36CB4D29EAE3375C310AF0935C391E8B753A89C6DF2B430AACB638959E626F91E6AA99ABF4CFD9153E545A9C9493DEF1CCA3BD53D3C3C0B48F5554AD8BB1E1278C3CC8F6F0FFEE0B33CBD97FF0051F2271D4CB41468FD9B0EA939EEDEB8F3E9FA9EAF69D57D212D57B173ABCBDBE6AEF9CE40E37DB17A4D3E9BC3A36C92B14A3392C665C791BD6571F10D1DB7B82AF57A7E16C7B8A28B23A4A526A17552DF1CF159CBE66F517EA6EA2E857A28532B57BF2534DC80FA165D5BF14F67F2F6DD3A72AE4F8AF43C7A0A6DA34142AA0E6DEA7336BF0A786FF62CF5DA87679B1F0D82BB1853762783CB6D9E232D3D15D55CEA7567738DABDF605D7C14751AC8B8C7DDB2138BC71F7B19FEC7B27FFEE7AFFDD7FCCF0EA5EA3577D96F90EBCC22B67989E5A927FD8DD93D5D9E231D646855CA11C28B9A9297A7EE06B4FF00E1BE21FF0011FF00A91EBFFF00B33FF70796E9DDA9AA542D3C3495D92DD39292936FF43A7B5EAF39F63ABCFC6CF3F72E5DF1CC0F2E82AAA15FB76A16DAE993DAB2DEF964F468E6AE76DEE719596B52928BCED5D1339D13BABD157A6BBC395D1836F32B173CBFF99AD25728EA2FB1E9D5109EDDB0524D2C01EB00845504000A42806320803A9400000028E042811BEC164A3800E25249A8ACB692473C3B78BCC63D175605958B2D453935CF070B77F9B5CA4D478E381DD28A7B570F81CB53FC34FB480EBE5439C96EFF006B895705C11798028222810A0011F6293A9408014088A4080000080A1A20981F12F12000F3C369994A11E3B92FD4CF9CA5F621297EC8055C2538F6659B696773F4491CDBB1DFD2B525F16758C1478F16DF36D94620F9A6F8A66B24CA5D513CD82E724456CF9F0AE51D168F55E7DCE56DDB65173F77197FF23D7E7C3A3CFC11E7FF00F84F0EEBF5FF00FAA4547A34DA45ADD46A252D45D08C24A3155CF0B38E272D069E9D4CE74D9AAD52BE1BB728CF8613C763B686C70D1D36C22FEBF5797E8B913470F2FF00FA8B591EF5B7F3DACA8F14355A6AB58DC3516CE970E7636DEEC9EB696A2EBA33BA74D3A78A94DC39B6CF0E9A9D45BE1AEA8469F2EC96EDD2CEEFFAE077969AEBB572A6BB65195C93B12FB2A2B86581DDE9AC76515E9B533953A94DEF9F194708CDB5428A2CD4E975375CA996DB216BCA7F03B517D7678AE9AAA3F81442508BEEF1C7FF0023CF1FF08F13FF007CFF00BA03BEA27647CB55382959351CCF92E679F53A5BB4BAAD25556A6C9CEE586E52CC53EE91AD5D119E9A6E7294B6A72597D706B58A6F51E14AB694B6AC3F9016CD1D71F3BD9F5774F51A75BA4A6F2996BD2C2D54ABF597C351A88EE8460F115C0EF73A5BF115A54D6A367D637C9F0E878FC29D2B595AB6ABA36B5F52ECCE170E202BADCEBBAED6EA6CAE144FCA7E5706DF7FEC67515C349ACF2B53ACD4792EBDD5CA32E3CFA99A28B353A8BA8B6C9C6B85B29EA1E710E7D3FEBA1BD65BED3E7EAA31CD7B3CAA97759C67E606AFD3C551A7B749ABD4B775AA11F32595F1C1ABB4918D5A9F67D6DF2BB4F1CCD49F0E467C49C29BF47A46A725557F661CDC9F0FFC869B6BF0BD646A8CE1A955FD77989F1E7CBF700EAD353A5D3D9AAD66AE32B61B928CF2BFB19F0FD445C2CDD64E4B7BDAE5C5E0F4E9B57194F43A57A7CA955B5CAC8633C3A7A18D2C763BEA5C235DD28C5764074F3EBFF37F4B1E72FC167F49D0115CFCD7D2A997CD97E54CD8031E6FFAB98F31F4AE46CA07176CBF2646BCD7F9533A0039F996259F25BF4DC3CD9FE4CBE6742019F3659C7952279B67E44BE68E8503979967E44BFA90F326B3F532E1EBCCEA3881E795929D897933C478B5EA6DDEE3CEA98AB3E658B39E3CCE984D3038F9ADACAA6673BA7274BFAB9717D4F4C7ECF2317FF0F1DD813CDDA96EAE6932C6F8BFBB3FE9373C702F24073F3E38FE1D8F1FE50AF8F0CC27C7FCA7523786073F688AE709FF0048F68AFB4FFA4EA4607357C738DB3E7CF687A882E93FE93A0039FB4432D6D9FF004B1E7ACE3CBB3FA4E8503979FF00EA6CF90F367D2897CCEC40397996BE54FCE4375F8FE1C3FA8EA00E5FE90F3FC38FCD936DEFED5D15FECC4EA00E5E549F3BA7FD89E447AB93F8C8EC40311AAB8F28236001894374A2FB1AE652600E5E5C3F085543AC51B41A20A92E88F1D16D0FC3A9D2EAB49AA9BADB97B90F57EBEA7AE278A32D4AD2E9752F51BA375BB1C362E1C5F5FD0A8DDBAFBA9A69A7C3E9BEB841BCA9D7CF8FEBEA6D6B52F159EABD9753B2556CC797C7392D50BB555C6FF6A8E9ABB25B6B8CA2A4E44D3E9F5B76A35154B52AB9D3B561569A965147CC8E9E3EC538CB4D7FB46EF765B5EDC7FD64FA755D1D3EA758ADA2F9C6E518A9551E98E3C4E0BDB7D9A8BA37E656D9E5F96E0961E7BFE87A6CA353442CB16B217CA95BACAB6A585F103CF0955A7D653768B49A84A2A5BD58B9AC74FDCD6AAF8CF47751A4D1DF0F365BE6E6BD7A1E886A299E142D8B6F92CF133542ED4D51BDEAA1A6858F6D7171527203C6AAAD5D73D3517575BA2492B1716CEB75D65D768A54D16C654478F991C278C75FD0ED1AF556596D365B0A150B365D8CA7DB817EB74F7575DB62BE17272AEC8AC72013D5425E77B3E92F85FA85B6529AC4570E62BD5C2B54BBF477CEFD3C76C250598BE048AB351E658B511D35154B6394927BA43C9D67B4FB1F990CEDDFE7E3EEFC3B81CE8D44569F5156AF4BA96EEB5CE5E5C7874E19FD0F33AE986BE9B69D2EA9510C3945C72DB4FF00F83D962D450AB957743575DD2D90925B712FD0B769F555556CA1AEAEDB2A8E675EC4B1C39818979BA8BACD5C6328CD5B19C61358CA8AE0BF73766A616C2F5A7D1DD0BB52B6CE53588A269EB96A1C230F158799359D8AA4D9886E73B236F8A42A9426E18752E38EA075AF550A952EFD1DF3BF4F1DB0941662F81AD3C27184A562C4EC9B9C96793671D43BB43A89C2CD42BB14EF5EE638E70B916FAF5BA55A772B95B3B9EDF2DC14526D7703B5B98C94D714B9A3A27959385BA7D4535D962D5D77BA566DAB6A585F13544B2B09E62D6E4FD08AEC08502820000A0001919EE00A42802900189A717BA31CB2F989F4C3ECCD7C4CCE119734023C1186F758B1F6573F88F2E3EB8ED9349638258480BC370C712F5C940004E6008F9893508B93E823CB32E6C07305E60014850009CCA0000040000000021401083018100E00060F1AFF0005F0DFF88FFD523DA7829BF43678569F4F7EA6554EB9397BB1794F2FD3D423A4FF00C2FC2BFDEAFEE7BD5AA8D6F8858F94554DFC30782BD5787CB4F5536DF351D3D9BA12DAFDE5F239DBE214D8BC45CA4E0EE8C557169F1C228FA3AAA9532D1C5727AADCBF5CB3CB753395BE27655A85069353AF66772DBDFA7539DDE275D9A5D0ED939DD54E2E7049E5E160D5FACD0AAF57669ECB2CBB530C38B8BF77801C34D073B34B6434CAA8D70C4A6A49EF78E67AF4B5D56F8668BDAE7E56DB3EAF1F7B89E1D34AB8DFA58D3A8B27BA0FCC83CE22F6F23D30B74AB4D469F5D3955669A5B96D8E54901E8BE53957E2DB961A492F86D394DE34FE159FC12FFC247AFA6CB755ED0A7569F5314A13C71E08E3A9D4E96D9E92985D6434F4C5A7724D3E5FF5F30377FF00816A7FE25FF73E93FF00B6BFF84FFCCF93A6D4E925A3BB4BA9B6C8D6ED72859B5BDDFF00BFFCCF57D235FB67B46C9FB23ABCAF331EBCF1DBA01341FE15A1FF00895FDD9BD4D5A7BECF1055AB21A884774A4A4D2970E58383D4530A68A341BEF8D1679B26D638763A4B59A36F533D2CE766A3551DBB36F278039785A842E9EA76A4A8D32CF0E726B3FD8E74D11B5E8A1282765B3F32524B9AE6FF00B98D36A2AFA325A656BF3EFB2317C1F08F047A61ABD3E9BC4272B24E2AAAB6C238E0DBE3C31E980270D6F8F5AE4A3E552F749BE8A3FF00B9D7C47511BA8D16A27375C5CE52528F35D8F3E821ABD3A95B4BA67E74537E667E3D3E275B3C5A3751A58D708CED5252B2118B58C71E00674B28CBC33597D76BB751657F5AA5C36F3374E230D3B8E70E097EC27AAD135AA7A59CECBF571DBB36E31C038F950A21CF6E239FD00F4820CE165F24455048BDD152C7328004CB28149803E205C6500001499280204FA00274E244F269FA936F1E000AB0D90B1E405042819B16E8E3B97A00F9000445024EB8CD71E6B93EC484DFD99E14BBF7364714F9AC8141CB12A9FBBEF47B3E86E3252E4068321408000042800400011948064A40414E352D5EAE2EDD346A8D59C45DADE65F03A9E69F0F0CF0BFF7CBFB96237096AEEB254D754216D7FC4958FDD5F02EEBA9BD53AA8C54A6B309438C59E8B23E76A7C474EDEC8CA116EC7C97BBD4F36B60E0F41A78DF0AD42126AF9636BE051A9CEC9DD1A34F18CAE6B77BDCA31EECE5A87AED3BAE338D12F364A319C5B71C9D7409C75BA8CEA21A87ECFF006EBC6171E5C0E7A6C7D0FA1726925AA8BCBE9C581ABEAD769EBB2D9CF49EEACB8A72C87A950D22BA58E3052635DA6D1EB5EB6CAE5377D51DCDE7DD7C3A7C8F15F5B8F87EEF6FA66B6C7EAA38DDD387E807B634F88795E6F97461ADDE5E5EFC18A21ACD6A57D31AA15F28F9ADF1F91D34B7CE5A8BFC4B50E0ABAEB705B3949FA64E763CF86F85BEF727FB8160F5575B2D3D754236D7FC5737EEA23F6BD35AB4F3AE33B2DFE1383F75975F6DB1D7EB28A63093D428430F9BE1D0D6A6D7A5B74B5D7284A5A3ADBB1CB3B72D63005B7DAF4B156EAA354A9CE24EA6F31F895CAEBAF953A58C1CA0B3394F8451CA509D7E172AEC58BF5B66E50FC2BB9D17FD9BC57FD98FFE103163D7D7AAAB4CE346FB32E33CBDAC6ADEB7494CAC9CB4B25178DB1726CEF538AFA22529249425C5BFF29E3D6E9749768F51ABD3B9EFAECF79BE52CBFF00DC0F44A5A9B35AB4FA7F2B3E5EFCCF3DFD0C6A25ADD3BAD4A5A597993505B1B7C47873D355E2564AB859455E43E16F079CA31EC9A48AD16AF4AE6A12BE3196F7D33CFF00603B5F5EBB4F54E739E93DC59714E59239BB34F4D92E0DED7C3D46BB4DA3D6BD6D95CA6EFAA3B9BCFBAF874F91CD62CD2A92588A4B6C73C9600F5E57468C5CF2A30EB27FB071AF6EE94524738427193B22B9F28BE69115E8E80C42C52787EECBB33605041902A290014192817A0E409C00A42900CCE5B671E3C1F03462E4DC32B9C5E4D26A4B2B9302AE79346401A23182750293390000E011407101F222034625179DD1787FB33447CC0919B7C1AE25C91C7286EDBC25FF00C81A06773E8BE638BE72F9700340CED4B8B7CBBB32B2F8C6385DDFFC80D831B65F8FF61BA51FB4B72F4036422945F5E650320811053851E45BA2D3537EA21459A6B3328CDF3E3D0EE62555763CCEB849F792C9447ABA35366B6B958AA85E92AEC9708BC2C3316F9363D269AA9C7511A22F7C9715CB81D6554271509422E2B926B821084611C42318AED15808E744AAD26B9EF51AAAB6AD9B92C252C98B67A7D368F4DA68DB1D4AAEDF32CD8B72DBD7FB9DE69496D945493E8D188D70AF3B211877DA804A5A4D357AC9D1A8AA7ED31DB0AAB5C53C76FD4F2EAA8855E1EFEAEB5646314E4A2BD0F5469AE2F746B845AEAA28DCA3192C492927D1A033ACAA8BD530A7C474F5554A5B63B93E3DF99CB4B3D3EA741A484F535D33D3CF73537CF89BF66A3F26BFE945F229784E9AF872F75145A353A6BBC475BA88DF5D72DB185539BC2E5C5FEC79AAA6AA75DA78DBADA6FAA5395927B961492EACF43A296F8D55BFFBA87B351F915FF4A03878866365BACAFC4299CF9461169BDB9E47784EB53D669EF9AA56A22B64E5CB96187A5A1AC7935AFF00BA8D251B23B2C8C6587CA4B2073B6FD2C2DD153270BE9A22E364F6EE8ACAC239EAA5A6D27866A28AB515DB2BACDD150E8B2BFE47AA35D718384611517CD25C199F67A16714D7C79FBA80F2D4E887893AF577D3A88CEAC2B36A718BCFFF00274B67A7D3E8F4BA68DB0D4AAEDF32CD8B72DBC73FDCEAB4F4ACE29AF8F3F751A8575D79D908C33CF6AC01894B49A6AF593A2FAA6F531DB5D55AE29E3B7EA48D1B68506F8A8252C75C1B8D1542598D708B5D5452364562A5BA1194F8CB1D7A1BE4629FB18ECCE806670535EF231EFD4D71728F5F43A8E8048C94A39468E4E0D665078F4EE6A1353E0F9F603633C41259DBEEE33EA05C94C453C7BD2CFE86B2051804CE00A1B249A5C5BE04DD26BDD8FEAD81AEA72AFDC9BAFA7346B74FA28989B9AC4B6AE0FA303B05C8C6F7F8196138CB93036886672DB1CAE2CD6400190002609902E464C79B14F124E3D9B37C0001943981252E8B99971EADBDDDCB1EAFD4D6409179E7D0A65F09A7DF814093F7A518BE5CFE268E737B6519F45CCE80400640E3852BA49AC65703A464F389735FB99B5E36CFB3E22DFB3B9738F103FFD9";
                                                                                         //var plainTextBytes = System.Text.Encoding.ASCII.GetBytes(plainText);
                                                                                         //var base64 = System.Convert.ToBase64String(plainTextBytes);



                    return dumpYard;
                }
                else
                {
                    var id = db.DumpYardDetails.OrderByDescending(x => x.dyId).Select(x => x.dyId).FirstOrDefault();
                    int number = 1000;
                    string refer = "DYSBA" + (number + id + 1);
                    dumpYard.ReferanceId = refer;
                    dumpYard.dyQRCode = "/Images/QRcode.png";
                    dumpYard.WardList = ListWardNo();
                    dumpYard.AreaList = ListArea();
                    dumpYard.ZoneList = ListZone();

                    return dumpYard;
                }

            }

            catch (Exception)
            {
                throw;
            }


        }


        public StreetSweepVM GetStreetSweepDetails(int teamId)
        {
            try
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();

                string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.StreetQRCode + "/";
                StreetSweepVM StreetSweep = new StreetSweepVM();

                var Details = db.StreetSweepingDetails.Where(x => x.SSId == teamId).FirstOrDefault();
                if (Details != null)
                {
                    StreetSweep = FillStreetSweepDetailsViewModel(Details);
                    if (StreetSweep.SSQRCode != null && StreetSweep.SSQRCode != "")
                    {
                        HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(ThumbnaiUrlCMS + StreetSweep.SSQRCode.Trim());
                        HttpWebResponse httpRes = null;
                        try
                        {
                            httpRes = (HttpWebResponse)httpReq.GetResponse(); // Error 404 right here,
                            if (httpRes.StatusCode == HttpStatusCode.NotFound)
                            {
                                StreetSweep.SSQRCode = "/Images/default_not_upload.png";
                            }
                            else
                            {
                                StreetSweep.SSQRCode = ThumbnaiUrlCMS + StreetSweep.SSQRCode.Trim();
                            }
                        }
                        catch (Exception e) { StreetSweep.SSQRCode = "/Images/default_not_upload.png"; }

                        //  point.qrCode = ThumbnaiUrlCMS + point.qrCode.Trim();
                    }
                    else
                    {
                        StreetSweep.SSQRCode = "/Images/default_not_upload.png";
                    }

                    // house.WardList = ListWardNo();
                    StreetSweep.AreaList = LoadListArea(Convert.ToInt32(StreetSweep.WardNo));//ListArea();
                    StreetSweep.ZoneList = ListZone();
                    StreetSweep.WardList = LoadListWardNo(Convert.ToInt32(StreetSweep.ZoneId));//ListWardNo();

                    return StreetSweep;
                }
                else
                {
                    var id = db.StreetSweepingDetails.OrderByDescending(x => x.SSId).Select(x => x.SSId).FirstOrDefault();
                    int number = 1000;
                    string refer = "SSSBA" + (number + id + 1);
                    StreetSweep.ReferanceId = refer;
                    StreetSweep.SSQRCode = "/Images/QRcode.png";
                    StreetSweep.WardList = ListWardNo();
                    StreetSweep.AreaList = ListArea();
                    StreetSweep.ZoneList = ListZone();
                    return StreetSweep;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public LiquidWasteVM GetLiquidWasteDetails(int teamId)
        {
            try
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();

                string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.LiquidQRCode + "/";
                LiquidWasteVM LiquidWaste = new LiquidWasteVM();

                var Details = db.LiquidWasteDetails.Where(x => x.LWId == teamId).FirstOrDefault();
                if (Details != null)
                {
                    LiquidWaste = FillLiquidWasteDetailsViewModel(Details);
                    if (LiquidWaste.LWQRCode != null && LiquidWaste.LWQRCode != "")
                    {
                        HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(ThumbnaiUrlCMS + LiquidWaste.LWQRCode.Trim());
                        HttpWebResponse httpRes = null;
                        try
                        {
                            httpRes = (HttpWebResponse)httpReq.GetResponse(); // Error 404 right here,
                            if (httpRes.StatusCode == HttpStatusCode.NotFound)
                            {
                                LiquidWaste.LWQRCode = "/Images/default_not_upload.png";
                            }
                            else
                            {
                                LiquidWaste.LWQRCode = ThumbnaiUrlCMS + LiquidWaste.LWQRCode.Trim();
                            }
                        }
                        catch (Exception e) { LiquidWaste.LWQRCode = "/Images/default_not_upload.png"; }

                        //  point.qrCode = ThumbnaiUrlCMS + point.qrCode.Trim();
                    }
                    else
                    {
                        LiquidWaste.LWQRCode = "/Images/default_not_upload.png";
                    }

                    // house.WardList = ListWardNo();
                    LiquidWaste.AreaList = LoadListArea(Convert.ToInt32(LiquidWaste.WardNo));//ListArea();
                    LiquidWaste.ZoneList = ListZone();
                    LiquidWaste.WardList = LoadListWardNo(Convert.ToInt32(LiquidWaste.ZoneId));//ListWardNo();

                    return LiquidWaste;
                }
                else
                {
                    var id = db.LiquidWasteDetails.OrderByDescending(x => x.LWId).Select(x => x.LWId).FirstOrDefault();
                    int number = 1000;
                    string refer = "LWSBA" + (number + id + 1);
                    LiquidWaste.ReferanceId = refer;
                    LiquidWaste.LWQRCode = "/Images/QRcode.png";
                    LiquidWaste.WardList = ListWardNo();
                    LiquidWaste.AreaList = ListArea();
                    LiquidWaste.ZoneList = ListZone();
                    return LiquidWaste;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public DumpYardDetailsVM SaveDumpYardtDetails(DumpYardDetailsVM data, string Emptype)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.dyId > 0)
                    {
                        var model = db.DumpYardDetails.Where(x => x.dyId == data.dyId).FirstOrDefault();
                        if (model != null)
                        {
                            model.zoneId = data.ZoneId;
                            model.wardId = data.WardNo;
                            model.areaId = data.areaId;
                            model.dyAddress = data.dyAddress;
                            model.dyLat = data.dyLat;
                            model.dyLong = data.dyLong;
                            model.dyName = data.dyName;
                            model.dyNameMar = data.dyNameMar;
                            model.dyQRCode = data.dyQRCode;
                            model.ReferanceId = data.ReferanceId;
                            //model.lastModifiedDate = DateTime.Now;
                            model.DataEntryDate = DateTime.Now;
                            //model.EmployeeType = data.EmployeType;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var type = FillDumpYardDetailsDataModel(data, Emptype);
                        db.DumpYardDetails.Add(type);
                        db.SaveChanges();
                    }
                }
                var dyId = db.DumpYardDetails.OrderByDescending(x => x.dyId).Select(x => x.dyId).FirstOrDefault();
                DumpYardDetailsVM vv = GetDumpYardtDetails(dyId);
                return vv;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public StreetSweepVM SaveStreetSweepDetails(StreetSweepVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.SSId > 0)
                    {
                        var model = db.StreetSweepingDetails.Where(x => x.SSId == data.SSId).FirstOrDefault();
                        if (model != null)
                        {
                            model.zoneId = data.ZoneId;
                            model.wardId = data.WardNo;
                            model.areaId = data.areaId;
                            model.SSAddress = data.SSAddress;
                            model.SSLat = data.SSLat;
                            model.SSLong = data.SSLong;
                            model.SSName = data.SSName;
                            model.SSNameMar = data.SSNameMar;
                            model.SSQRCode = data.SSQRCode;
                            model.ReferanceId = data.ReferanceId;
                            // model.lastModifiedDate = DateTime.Now;
                            model.DataEntryDate = DateTime.Now;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var type = FillStreetSweepDetailsDataModel(data);
                        db.StreetSweepingDetails.Add(type);
                        db.SaveChanges();
                    }
                }
                var SSId = db.StreetSweepingDetails.OrderByDescending(x => x.SSId).Select(x => x.SSId).FirstOrDefault();
                StreetSweepVM vv = GetStreetSweepDetails(SSId);
                return vv;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public LiquidWasteVM SaveLiquidWasteDetails(LiquidWasteVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.LWId > 0)
                    {
                        var model = db.LiquidWasteDetails.Where(x => x.LWId == data.LWId).FirstOrDefault();
                        if (model != null)
                        {
                            model.zoneId = data.ZoneId;
                            model.wardId = data.WardNo;
                            model.areaId = data.areaId;
                            model.LWAddreLW = data.LWAddress;
                            model.LWLat = data.LWLat;
                            model.LWLong = data.LWLong;
                            model.LWName = data.LWName;
                            model.LWNameMar = data.LWNameMar;
                            model.LWQRCode = data.LWQRCode;
                            model.ReferanceId = data.ReferanceId;
                            // model.lastModifiedDate = DateTime.Now;
                            model.DataEntryDate = DateTime.Now;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var type = FillLiquidWasteDetailsDataModel(data);
                        db.LiquidWasteDetails.Add(type);
                        db.SaveChanges();
                    }
                }
                var LWId = db.LiquidWasteDetails.OrderByDescending(x => x.LWId).Select(x => x.LWId).FirstOrDefault();
                LiquidWasteVM vv = GetLiquidWasteDetails(LWId);
                return vv;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void DeletDumpYardtDetails(int teamId)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.GarbagePointDetails.Where(x => x.gpId == teamId).FirstOrDefault();
                    if (Details != null)
                    {
                        db.GarbagePointDetails.Remove(Details);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion


        // Added By Saurabh (27 May 2019)
        #region HouseScanify

        public List<QrEmployeeMaster> GetUserList(int AppId, int teamId)
        {
            //try
            //{
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
            using (DevChildSwachhBharatNagpurEntities db = new DevChildSwachhBharatNagpurEntities(AppId))
            //using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
            {
                // var Details = db.Locations.FirstOrDefault();

                // if (AppId > 0)
                // {
                //     Details = db.Locations.Where(c => c.locId == teamId).FirstOrDefault();

                // }

                //// var atten = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(Details.datetime) && c.userId == Details.userId).FirstOrDefault();
                // if (Details != null)
                // {
                //SBALUserLocationMapView loc = new SBALUserLocationMapView();
                List<QrEmployeeMaster> user = new List<QrEmployeeMaster>();
                user = db.QrEmployeeMasters.OrderBy(x => x.qrEmpName).ToList();
                //loc.userName = user.userName;
                //loc.date = Convert.ToDateTime(Details.datetime).ToString("dd/MM/yyyy");
                //loc.time = Convert.ToDateTime(Details.datetime).ToString("hh:mm tt");
                //loc.address = Details.address;
                //loc.lat = Details.lat;
                //loc.log = Details.@long;
                //loc.UserList = ListUser();
                //loc.userMobile = user.userMobileNumber;
                // try { loc.vehcileNumber = atten.vehicleNumber; } catch { loc.vehcileNumber = ""; }

                return user;
                //}
                //else
                //{
                //    return new SBALUserLocationMapView();
                //}

            }
            //}
            //catch (Exception ex)
            //{
            //    return new SBALUserLocationMapView();
            //}
        }


        //Added By Saurabh (03 June 2019)
        public HouseScanifyEmployeeDetailsVM GetHSEmployeeDetails(int teamId)
        {
            try
            {
                HouseScanifyEmployeeDetailsVM type = new HouseScanifyEmployeeDetailsVM();
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();

                string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.UserProfile + "/";
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.QrEmployeeMasters.Where(x => x.qrEmpId == teamId).FirstOrDefault();
                    if (Details != null)
                    {
                        type = FillHSEmployeeViewModel(Details);

                        return type;
                    }
                    else
                    {

                        return type;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UREmployeeDetailsVM GetUREmployeeDetails(int teamId)
        {
            try
            {
                UREmployeeDetailsVM TypeDetail = new UREmployeeDetailsVM();
                //  DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();

                string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.UserProfile + "/";
                using (var db = new DevSwachhBharatMainEntities())
                {
                    var Details = db.EmployeeMasters.Where(x => x.EmpId == teamId).FirstOrDefault();
                    if (Details != null)
                    {
                        TypeDetail = FillUREmployeeViewModel(Details);

                        TypeDetail.CheckAppDs = db.CheckAppDs.Where(x => x.IsActive == true).ToList<CheckAppD>();
                        if (TypeDetail.isActiveULB != null)
                        {
                            string s = TypeDetail.isActiveULB;
                            string[] values = s.Split(',');
                            for (int i = 0; i < values.Length; i++)
                            {
                                values[i] = values[i].Trim();
                                int u = 0;
                                if (values[i] != "")
                                {
                                    u = Convert.ToInt32(values[i]);
                                }
                                string state1 = "";
                                foreach (var v in TypeDetail.CheckAppDs)
                                {
                                    if (v.AppId == u)
                                    {

                                        v.IsCheked = true;

                                    }
                                }
                            }

                        }
                        return TypeDetail;
                    }
                    else
                    {

                        TypeDetail.CheckAppDs = db.CheckAppDs.Where(x => x.IsActive == true).OrderBy(x => x.App_Name).ToList<CheckAppD>();
                        return TypeDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<AppDetail> GetAppName()
        {
            List<AppDetail> appNames = new List<AppDetail>();
            appNames = dbMain.AppDetails.Where(x => x.IsActive == true && x.AppName != "Thane Mahanagar Palika").OrderBy(x => x.AppName).ToList();
            //appNames = dbMain.AppDetails.ToList();
            //var appNames= dbMain.AppDetails.Where(row => row.)
            return appNames.OrderBy(x => x.AppName).ToList();
        }


        //public List<AppDetails> ListULB()
        //{
        //    List<AppDetails> users = new List<AppDetails>();
        //    SelectListItem itemAdd = new SelectListItem() { Text = "--Select Employee--", Value = "0" };
        //    DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
        //    try
        //    {
        //        users = dbMain..ToList();

        //    }
        //    catch (Exception ex) { throw ex; }

        //    return users;
        //}
        public void SaveHSEmployeeDetails(HouseScanifyEmployeeDetailsVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.qrEmpId > 0)
                    {
                        var model = db.QrEmployeeMasters.Where(x => x.qrEmpId == data.qrEmpId).FirstOrDefault();
                        if (model != null)
                        {

                            model.qrEmpId = data.qrEmpId;
                            model.qrEmpAddress = data.qrEmpAddress;
                            model.qrEmpLoginId = data.qrEmpLoginId;
                            model.qrEmpMobileNumber = data.qrEmpMobileNumber;
                            model.qrEmpName = data.qrEmpName;
                            model.qrEmpNameMar = data.qrEmpNameMar;
                            model.qrEmpPassword = data.qrEmpPassword;
                            model.imoNo = data.imoNo;
                            model.isActive = data.isActive;
                            model.bloodGroup = data.bloodGroup;
                            model.userEmployeeNo = data.userEmployeeNo;
                            model.typeId = 1;
                            model.type = "Employee";
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var type = FillHSEmployeeDataModel(data);
                        db.QrEmployeeMasters.Add(type);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void SaveHSQRStatusHouse(int houseId, string QRStatus)
        {
            bool? bQRStatus = null;
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (houseId > 0 && !string.IsNullOrEmpty(QRStatus))
                    {
                        if (QRStatus == "1")
                        {
                            bQRStatus = true;
                        }
                        else if (QRStatus == "0")
                        {
                            bQRStatus = false;
                        }
                        else
                        {
                            bQRStatus = null;
                        }

                        var model = db.HouseMasters.Where(x => x.houseId == houseId).FirstOrDefault();
                        if (model != null)
                        {
                            model.QRStatus = bQRStatus;
                            model.QRStatusDate = DateTime.Now;
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void SaveQRStatusDump(int dumpId, string QRStatus)
        {
            bool? bQRStatus = null;
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (dumpId > 0 && !string.IsNullOrEmpty(QRStatus))
                    {
                        if (QRStatus == "1")
                        {
                            bQRStatus = true;
                        }
                        else if (QRStatus == "0")
                        {
                            bQRStatus = false;
                        }
                        else
                        {
                            bQRStatus = null;
                        }

                        var model = db.DumpYardDetails.Where(x => x.dyId == dumpId).FirstOrDefault();
                        if (model != null)
                        {
                            model.QRStatus = bQRStatus;
                            model.QRStatusDate = DateTime.Now;
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveQRStatusLiquid(int liquidId, string QRStatus)
        {
            bool? bQRStatus = null;
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (liquidId > 0 && !string.IsNullOrEmpty(QRStatus))
                    {
                        if (QRStatus == "1")
                        {
                            bQRStatus = true;
                        }
                        else if (QRStatus == "0")
                        {
                            bQRStatus = false;
                        }
                        else
                        {
                            bQRStatus = null;
                        }

                        var model = db.LiquidWasteDetails.Where(x => x.LWId == liquidId).FirstOrDefault();
                        if (model != null)
                        {
                            model.QRStatus = bQRStatus;
                            model.QRStatusDate = DateTime.Now;
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void SaveQRStatusStreet(int streetId, string QRStatus)
        {
            bool? bQRStatus = null;
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (streetId > 0 && !string.IsNullOrEmpty(QRStatus))
                    {
                        if (QRStatus == "1")
                        {
                            bQRStatus = true;
                        }
                        else if (QRStatus == "0")
                        {
                            bQRStatus = false;
                        }
                        else
                        {
                            bQRStatus = null;
                        }

                        var model = db.StreetSweepingDetails.Where(x => x.SSId == streetId).FirstOrDefault();
                        if (model != null)
                        {
                            model.QRStatus = bQRStatus;
                            model.QRStatusDate = DateTime.Now;
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<int> GetHSHouseDetailsID(DateTime? fromDate, DateTime? toDate, int userId, string searchString, int QRStatus, string sortColumn, string sortOrder)
        {
            List<int> lstIDs = new List<int>() { };

            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var data = db.SP_GetHSHouseDetailsID(fromDate, toDate, userId, QRStatus, sortColumn, sortOrder, searchString).ToList();
                    if (data != null && data.Count > 0)
                    {
                        foreach (var i in data)
                        {
                            lstIDs.Add(i ?? 0);
                        }
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            return lstIDs;
        }



        public List<int> GetHSDumpDetailsID(DateTime? fromDate, DateTime? toDate, int userId, string searchString, int QRStatus, string sortColumn, string sortOrder)
        {


            List<int> lstIDs = new List<int>() { };
            bool? bQRStatus = null;
            if (QRStatus == 1)
            {
                bQRStatus = true;
            }
            else if (QRStatus == 2)
            {
                bQRStatus = false;

            }
            else
            {
                bQRStatus = null;
            }

            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var model = db.DumpYardDetails
                         .GroupJoin(db.QrEmployeeMasters,
                                      a => a.userId,
                                      b => b.qrEmpId,
                                      (a, b) => new { c = a, d = b.DefaultIfEmpty() })
                           .SelectMany(r => r.d.DefaultIfEmpty(),
                                       (p, b) => new
                                       {
                                           modifiedDate = p.c.lastModifiedDate,
                                           userId = p.c.userId,
                                           houseId = p.c.dyId,
                                           Name = b.qrEmpName,
                                           HouseLat = p.c.dyLat,
                                           HouseLong = p.c.dyLong,
                                           QRCodeImage = string.IsNullOrEmpty(p.c.QRCodeImage) ? "/Images/default_not_upload.png" : p.c.QRCodeImage,
                                           ReferanceId = p.c.ReferanceId,
                                           QRStatus = p.c.QRStatus,
                                           QRStatusDate = p.c.QRStatusDate
                                       }).Where(c => ((bQRStatus != null && c.QRStatus == bQRStatus) || bQRStatus == null) && (c.modifiedDate >= fromDate && c.modifiedDate <= toDate) && c.HouseLat != null && c.HouseLong != null).OrderBy(c => c.houseId).ToList();

                    if (QRStatus == 3)
                    {
                        model = model.Where(c => (c.QRStatus == null)).ToList();
                    }

                    if (fromDate != null && toDate != null)
                    {
                        if (Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy") == Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy"))
                        {
                            model = model.Where(c => (Convert.ToDateTime(c.modifiedDate).ToString("dd/MM/yyyy") == Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy"))).ToList();
                        }
                        else
                        {

                            model = model.Where(c => (c.modifiedDate >= fromDate && c.modifiedDate <= toDate)).ToList();
                        }
                    }
                    if (userId > 0)
                    {
                        model = model.Where(c => c.userId == userId).ToList();

                    }

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        model = model.Where(c => ((string.IsNullOrEmpty(c.Name) ? " " : c.Name) + " " + (string.IsNullOrEmpty(c.ReferanceId) ? " " : c.ReferanceId)).ToUpper().Contains(searchString.ToUpper())
                         ).ToList();

                    }

                    lstIDs = model.Select(x => x.houseId).ToList();

                }

            }
            catch (Exception)
            {
                throw;
            }
            return lstIDs;
        }

        public List<int> GetHSLiquidDetailsID(DateTime? fromDate, DateTime? toDate, int userId, string searchString, int QRStatus, string sortColumn, string sortOrder)
        {


            List<int> lstIDs = new List<int>() { };
            bool? bQRStatus = null;
            if (QRStatus == 1)
            {
                bQRStatus = true;
            }
            else if (QRStatus == 2)
            {
                bQRStatus = false;

            }
            else
            {
                bQRStatus = null;
            }

            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var model = db.LiquidWasteDetails
                         .GroupJoin(db.QrEmployeeMasters,
                                      a => a.userId,
                                      b => b.qrEmpId,
                                      (a, b) => new { c = a, d = b.DefaultIfEmpty() })
                           .SelectMany(r => r.d.DefaultIfEmpty(),
                                       (p, b) => new
                                       {
                                           modifiedDate = p.c.lastModifiedDate,
                                           userId = p.c.userId,
                                           houseId = p.c.LWId,
                                           Name = b.qrEmpName,
                                           HouseLat = p.c.LWLat,
                                           HouseLong = p.c.LWLong,
                                           QRCodeImage = string.IsNullOrEmpty(p.c.QRCodeImage) ? "/Images/default_not_upload.png" : p.c.QRCodeImage,
                                           ReferanceId = p.c.ReferanceId,
                                           QRStatus = p.c.QRStatus,
                                           QRStatusDate = p.c.QRStatusDate
                                       }).Where(c => ((bQRStatus != null && c.QRStatus == bQRStatus) || bQRStatus == null) && (c.modifiedDate >= fromDate && c.modifiedDate <= toDate) && c.HouseLat != null && c.HouseLong != null).OrderBy(c => c.houseId).ToList();

                    if (QRStatus == 3)
                    {
                        model = model.Where(c => (c.QRStatus == null)).ToList();
                    }


                    if (fromDate != null && toDate != null)
                    {
                        if (Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy") == Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy"))
                        {
                            model = model.Where(c => (Convert.ToDateTime(c.modifiedDate).ToString("dd/MM/yyyy") == Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy"))).ToList();
                        }
                        else
                        {

                            model = model.Where(c => (c.modifiedDate >= fromDate && c.modifiedDate <= toDate)).ToList();
                        }
                    }
                    if (userId > 0)
                    {
                        model = model.Where(c => c.userId == userId).ToList();

                    }

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        model = model.Where(c => ((string.IsNullOrEmpty(c.Name) ? " " : c.Name) + " " + (string.IsNullOrEmpty(c.ReferanceId) ? " " : c.ReferanceId)).ToUpper().Contains(searchString.ToUpper())
                         ).ToList();

                    }

                    lstIDs = model.Select(x => x.houseId).ToList();

                }

            }
            catch (Exception)
            {
                throw;
            }
            return lstIDs;
        }


        public List<int> GetHSStreetDetailsID(DateTime? fromDate, DateTime? toDate, int userId, string searchString, int QRStatus, string sortColumn, string sortOrder)
        {


            List<int> lstIDs = new List<int>() { };
            bool? bQRStatus = null;
            if (QRStatus == 1)
            {
                bQRStatus = true;
            }
            else if (QRStatus == 2)
            {
                bQRStatus = false;

            }
            else
            {
                bQRStatus = null;
            }

            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var model = db.StreetSweepingDetails
                         .GroupJoin(db.QrEmployeeMasters,
                                      a => a.userId,
                                      b => b.qrEmpId,
                                      (a, b) => new { c = a, d = b.DefaultIfEmpty() })
                           .SelectMany(r => r.d.DefaultIfEmpty(),
                                       (p, b) => new
                                       {
                                           modifiedDate = p.c.lastModifiedDate,
                                           userId = p.c.userId,
                                           houseId = p.c.SSId,
                                           Name = b.qrEmpName,
                                           HouseLat = p.c.SSLat,
                                           HouseLong = p.c.SSLong,
                                           QRCodeImage = string.IsNullOrEmpty(p.c.QRCodeImage) ? "/Images/default_not_upload.png" : p.c.QRCodeImage,
                                           ReferanceId = p.c.ReferanceId,
                                           QRStatus = p.c.QRStatus,
                                           QRStatusDate = p.c.QRStatusDate
                                       }).Where(c => ((bQRStatus != null && c.QRStatus == bQRStatus) || bQRStatus == null) && (c.modifiedDate >= fromDate && c.modifiedDate <= toDate) && c.HouseLat != null && c.HouseLong != null).OrderBy(c => c.houseId).ToList();

                    if (QRStatus == 3)
                    {
                        model = model.Where(c => (c.QRStatus == null)).ToList();
                    }

                    if (fromDate != null && toDate != null)
                    {
                        if (Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy") == Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy"))
                        {
                            model = model.Where(c => (Convert.ToDateTime(c.modifiedDate).ToString("dd/MM/yyyy") == Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy"))).ToList();
                        }
                        else
                        {

                            model = model.Where(c => (c.modifiedDate >= fromDate && c.modifiedDate <= toDate)).ToList();
                        }
                    }
                    if (userId > 0)
                    {
                        model = model.Where(c => c.userId == userId).ToList();

                    }

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        model = model.Where(c => ((string.IsNullOrEmpty(c.Name) ? " " : c.Name) + " " + (string.IsNullOrEmpty(c.ReferanceId) ? " " : c.ReferanceId)).ToUpper().Contains(searchString.ToUpper())
                         ).ToList();

                    }

                    lstIDs = model.Select(x => x.houseId).ToList();

                }

            }
            catch (Exception)
            {
                throw;
            }
            return lstIDs;
        }


        public SBAHSHouseDetailsGrid GetHouseDetailsById(int houseId)
        {
            SBAHSHouseDetailsGrid data = null;
            using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
            {
                data = db.SP_GetHSHouseDetailsById(houseId).Select(x => new SBAHSHouseDetailsGrid
                {
                    houseId = x.houseId,
                    Name = x.qrEmpName,
                    HouseLat = x.houseLat,
                    HouseLong = x.houseLong,
                    QRCodeImage = x.QRCodeImage,
                    ReferanceId = x.ReferanceId,
                    modifiedDate = x.modified.HasValue ? Convert.ToDateTime(x.modified).ToString("dd/MM/yyyy hh:mm tt") : "",
                    QRStatusDate = x.QRStatusDate.HasValue ? Convert.ToDateTime(x.QRStatusDate).ToString("dd/MM/yyyy hh:mm tt") : "",
                    QRStatus = x.QRStatus
                }).FirstOrDefault();
            }
            return data;
        }

        public SBAHSDumpyardDetailsGrid GetDumpDetailsById(int dumpId)
        {
            SBAHSDumpyardDetailsGrid data = null;
            using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
            {
                var model = db.DumpYardDetails
                         .GroupJoin(db.QrEmployeeMasters,
                                      a => a.userId,
                                      b => b.qrEmpId,
                                      (a, b) => new { c = a, d = b.DefaultIfEmpty() })
                           .SelectMany(r => r.d.DefaultIfEmpty(),
                                       (p, b) => new
                                       {
                                           modifiedDate = p.c.lastModifiedDate,
                                           userId = p.c.userId,
                                           dumpId = p.c.dyId,
                                           Name = b.qrEmpName,
                                           HouseLat = p.c.dyLat,
                                           HouseLong = p.c.dyLong,
                                           //QRCodeImage = string.IsNullOrEmpty(p.c.QRCodeImage) ? "/Images/default_not_upload.png" : p.c.QRCodeImage,
                                           QRCodeImage = p.c.BinaryQrCodeImage,
                                           ReferanceId = p.c.ReferanceId,
                                           QRStatus = p.c.QRStatus,
                                           QRStatusDate = p.c.QRStatusDate
                                       }).Where(a => a.dumpId == dumpId && a.HouseLat != null && a.HouseLong != null).FirstOrDefault();

                if (model != null)
                {
                    data = new SBAHSDumpyardDetailsGrid()
                    {
                        dumpId = model.dumpId,
                        Name = model.Name,
                        HouseLat = model.HouseLat,
                        HouseLong = model.HouseLong,
                        //QRCodeImage = model.QRCodeImage,
                        QRCodeImage = (model.QRCodeImage == null || model.QRCodeImage.Length == 0) ? "/Images/default_not_upload.png" : ("data:image/jpeg;base64," + System.Convert.ToBase64String(model.QRCodeImage)),
                        ReferanceId = model.ReferanceId,
                        modifiedDate = model.modifiedDate.HasValue ? Convert.ToDateTime(model.modifiedDate).ToString("dd/MM/yyyy hh:mm tt") : "",
                        QRStatusDate = model.QRStatusDate.HasValue ? Convert.ToDateTime(model.QRStatusDate).ToString("dd/MM/yyyy hh:mm tt") : "",
                        QRStatus = model.QRStatus
                    };
                }
            }
            return data;
        }


        public SBAHSLiquidDetailsGrid GetLiquidDetailsById(int liquidId)
        {
            SBAHSLiquidDetailsGrid data = null;
            using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
            {
                var model = db.LiquidWasteDetails
                         .GroupJoin(db.QrEmployeeMasters,
                                      a => a.userId,
                                      b => b.qrEmpId,
                                      (a, b) => new { c = a, d = b.DefaultIfEmpty() })
                           .SelectMany(r => r.d.DefaultIfEmpty(),
                                       (p, b) => new
                                       {
                                           modifiedDate = p.c.lastModifiedDate,
                                           userId = p.c.userId,
                                           liquid = p.c.LWId,
                                           Name = b.qrEmpName,
                                           HouseLat = p.c.LWLat,
                                           HouseLong = p.c.LWLong,
                                           //QRCodeImage = string.IsNullOrEmpty(p.c.QRCodeImage) ? "/Images/default_not_upload.png" : p.c.QRCodeImage,
                                           QRCodeImage = p.c.BinaryQrCodeImage,
                                           ReferanceId = p.c.ReferanceId,
                                           QRStatus = p.c.QRStatus,
                                           QRStatusDate = p.c.QRStatusDate
                                       }).Where(a => a.liquid == liquidId).FirstOrDefault();

                if (model != null)
                {
                    data = new SBAHSLiquidDetailsGrid()
                    {
                        liquidId = model.liquid,
                        Name = model.Name,
                        HouseLat = model.HouseLat,
                        HouseLong = model.HouseLong,
                        //QRCodeImage = model.QRCodeImage,
                        QRCodeImage = (model.QRCodeImage == null || model.QRCodeImage.Length == 0) ? "/Images/default_not_upload.png" : ("data:image/jpeg;base64," + System.Convert.ToBase64String(model.QRCodeImage)),
                        ReferanceId = model.ReferanceId,
                        modifiedDate = model.modifiedDate.HasValue ? Convert.ToDateTime(model.modifiedDate).ToString("dd/MM/yyyy hh:mm tt") : "",
                        QRStatusDate = model.QRStatusDate.HasValue ? Convert.ToDateTime(model.QRStatusDate).ToString("dd/MM/yyyy hh:mm tt") : "",
                        QRStatus = model.QRStatus
                    };
                }
            }
            return data;
        }

        public SBAHSStreetDetailsGrid GetStreetDetailsById(int streetId)
        {
            SBAHSStreetDetailsGrid data = null;
            using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
            {
                var model = db.StreetSweepingDetails
                         .GroupJoin(db.QrEmployeeMasters,
                                      a => a.userId,
                                      b => b.qrEmpId,
                                      (a, b) => new { c = a, d = b.DefaultIfEmpty() })
                           .SelectMany(r => r.d.DefaultIfEmpty(),
                                       (p, b) => new
                                       {
                                           modifiedDate = p.c.lastModifiedDate,
                                           userId = p.c.userId,
                                           liquid = p.c.SSId,
                                           Name = b.qrEmpName,
                                           HouseLat = p.c.SSLat,
                                           HouseLong = p.c.SSLong,
                                           //QRCodeImage = string.IsNullOrEmpty(p.c.QRCodeImage) ? "/Images/default_not_upload.png" : p.c.QRCodeImage,
                                           QRCodeImage = p.c.BinaryQrCodeImage,
                                           ReferanceId = p.c.ReferanceId,
                                           QRStatus = p.c.QRStatus,
                                           QRStatusDate = p.c.QRStatusDate
                                       }).Where(a => a.liquid == streetId).FirstOrDefault();

                if (model != null)
                {
                    data = new SBAHSStreetDetailsGrid()
                    {
                        streetId = model.liquid,
                        Name = model.Name,
                        HouseLat = model.HouseLat,
                        HouseLong = model.HouseLong,
                        //QRCodeImage = model.QRCodeImage,
                        QRCodeImage = (model.QRCodeImage == null || model.QRCodeImage.Length == 0) ? "/Images/default_not_upload.png" : ("data:image/jpeg;base64," + System.Convert.ToBase64String(model.QRCodeImage)),
                        ReferanceId = model.ReferanceId,
                        modifiedDate = model.modifiedDate.HasValue ? Convert.ToDateTime(model.modifiedDate).ToString("dd/MM/yyyy hh:mm tt") : "",
                        QRStatusDate = model.QRStatusDate.HasValue ? Convert.ToDateTime(model.QRStatusDate).ToString("dd/MM/yyyy hh:mm tt") : "",
                        QRStatus = model.QRStatus
                    };
                }
            }
            return data;
        }


        public void SaveUREmployeeDetails(UREmployeeDetailsVM data)
        {
            try
            {
                using (var db = new DevSwachhBharatMainEntities())
                {
                    if (data.EmpId > 0)
                    {
                        var model = db.EmployeeMasters.Where(x => x.EmpId == data.EmpId).FirstOrDefault();
                        if (model != null)
                        {

                            model.EmpId = data.EmpId;
                            model.EmpName = data.EmpName;
                            model.EmpNameMar = data.EmpNameMar;
                            model.LoginId = data.LoginId;
                            model.Password = data.Password;
                            model.EmpAddress = data.EmpAddress;
                            model.isActive = data.isActive;
                            model.EmpMobileNumber = data.EmpMobileNumber;
                            model.lastModifyDateEntry = DateTime.Now;
                            string state1 = "";
                            foreach (var s in data.CheckAppDs)
                            {
                                if (s.IsCheked == true)
                                {

                                    state1 += s.AppId + ",";

                                }
                            }
                            if (data.type == "A")
                            {
                                model.isActiveULB = null;
                                model.type = data.type;
                            }
                            else
                            {
                                model.isActiveULB = state1;
                                model.type = data.type;
                            }
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var type = FillUREmployeeDataModel(data);

                        //arr[CheckAppD] myArray = data.CheckAppDs.ToArray();

                        string state1 = "";
                        foreach (var s in data.CheckAppDs)
                        {
                            if (s.IsCheked == true)
                            {

                                state1 += s.AppId + ",";

                            }
                        }

                        if (data.type == "A")
                        {
                            type.isActiveULB = null;
                            type.type = data.type;
                        }
                        else
                        {
                            type.isActiveULB = state1;
                            type.type = data.type;
                        }

                        db.EmployeeMasters.Add(type);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        public HSDashBoardVM GetHSDashBoardDetails()
        {
            HSDashBoardVM model = new HSDashBoardVM();
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {

                    DevSwachhBharatMainEntities dbm = new DevSwachhBharatMainEntities();
                    var appdetails = dbm.AppDetails.Where(c => c.AppId == AppID).FirstOrDefault();

                    var data = db.SP_HouseScanifyDetails(AppID).First();


                    if (data != null)
                    {

                        model.TotalHouse = data.TotalHouse;
                        model.TotalHouseUpdated = data.TotalHouseUpdated;
                        model.TotalHouseUpdated_CurrentDay = data.TotalHouseUpdated_CurrentDay;
                        model.TotalPoint = data.TotalPoint;
                        model.TotalPointUpdated = data.TotalPointUpdated;
                        model.TotalPointUpdated_CurrentDay = data.TotalPointUpdated_CurrentDay;
                        model.TotalDump = data.TotalDump;
                        model.TotalDumpUpdated = data.TotalDumpUpdated;
                        model.TotalDumpUpdated_CurrentDay = data.TotalDumpUpdated_CurrentDay;

                        model.TotalLiquid = data.TotalLiquid;
                        model.TotalLiquidUpdated = data.TotalLiquidUpdated;
                        model.TotalLiquidUpdated_CurrentDay = data.TotalLiquidUpdated_CurrentDay;

                        model.TotalStreet = data.TotalStreet;
                        model.TotalStreetUpdated = data.TotalStreetUpdated;
                        model.TotalStreetUpdated_CurrentDay = data.TotalStreetUpdated_CurrentDay;

                        return model;
                    }

                    else
                    {
                        return model;
                    }
                }
            }
            catch (Exception)
            {
                return model;
            }
        }


        public HSDashBoardVM GetURDashBoardDetails()
        {
            HSDashBoardVM model = new HSDashBoardVM();
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {

                    DevSwachhBharatMainEntities dbm = new DevSwachhBharatMainEntities();
                    var appdetails = dbm.AppDetails.Where(c => c.AppId == AppID).FirstOrDefault();

                    var data = db.SP_HouseScanifyDetails(AppID).First();


                    if (data != null)
                    {

                        model.TotalHouse = data.TotalHouse;
                        model.TotalHouseUpdated = data.TotalHouseUpdated;
                        model.TotalHouseUpdated_CurrentDay = data.TotalHouseUpdated_CurrentDay;
                        model.TotalPoint = data.TotalPoint;
                        model.TotalPointUpdated = data.TotalPointUpdated;
                        model.TotalPointUpdated_CurrentDay = data.TotalPointUpdated_CurrentDay;
                        model.TotalDump = data.TotalDump;
                        model.TotalDumpUpdated = data.TotalDumpUpdated;
                        model.TotalDumpUpdated_CurrentDay = data.TotalDumpUpdated_CurrentDay;

                        model.TotalLiquid = data.TotalLiquid;
                        model.TotalLiquidUpdated = data.TotalLiquidUpdated;
                        model.TotalLiquidUpdated_CurrentDay = data.TotalLiquidUpdated_CurrentDay;

                        model.TotalStreet = data.TotalStreet;
                        model.TotalStreetUpdated = data.TotalStreetUpdated;
                        model.TotalStreetUpdated_CurrentDay = data.TotalStreetUpdated_CurrentDay;

                        return model;
                    }

                    else
                    {
                        return model;
                    }
                }
            }
            catch (Exception)
            {
                return model;
            }

        }

        public HouseScanifyEmployeeDetailsVM GetUserDetails(int teamId, string name)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.QrEmployeeMasters.Where(x => x.qrEmpId == teamId || x.qrEmpLoginId.ToUpper()
                    == name.ToUpper()).FirstOrDefault();

                    var Details1 = db.UserMasters.Where(x => x.userId == teamId || x.userLoginId.ToUpper()
                    == name.ToUpper()).FirstOrDefault();

                    if (Details != null || Details1 != null)
                    {
                        HouseScanifyEmployeeDetailsVM user = FillUserViewModel(Details, Details1);
                        // area.WardList = ListWardNo();
                        return user;
                    }
                    //if (Details1 != null)
                    //{
                    //    HouseScanifyEmployeeDetailsVM user = FillUserViewModel(Details1);
                    //    // area.WardList = ListWardNo();
                    //    return user;
                    //}
                    else
                    {
                        HouseScanifyEmployeeDetailsVM user = new HouseScanifyEmployeeDetailsVM();
                        // area.WardList = ListWardNo();

                        return user;
                    }
                }
            }
            catch (Exception)
            {

                return new HouseScanifyEmployeeDetailsVM();
            }
        }



        public HouseScanifyEmployeeDetailsVM GetUserNameDetails(int teamId, string name)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.QrEmployeeMasters.Where(x => x.qrEmpId == teamId || x.qrEmpName.ToUpper()
                    == name.ToUpper()).FirstOrDefault();



                    if (Details != null)
                    {
                        HouseScanifyEmployeeDetailsVM user = FillUserNameViewModel(Details);
                        // area.WardList = ListWardNo();
                        return user;
                    }
                    //if (Details1 != null)
                    //{
                    //    HouseScanifyEmployeeDetailsVM user = FillUserViewModel(Details1);
                    //    // area.WardList = ListWardNo();
                    //    return user;
                    //}
                    else
                    {
                        HouseScanifyEmployeeDetailsVM user = new HouseScanifyEmployeeDetailsVM();
                        // area.WardList = ListWardNo();

                        return user;
                    }
                }
            }
            catch (Exception)
            {

                return new HouseScanifyEmployeeDetailsVM();
            }
        }

        public List<SBALHSUserLocationMapView> GetHSUserAttenRoute(int qrEmpDaId)
        {
            List<SBALHSUserLocationMapView> userLocation = new List<SBALHSUserLocationMapView>();
            DateTime newdate = DateTime.Now.Date;
            var datt = newdate;
            var att = db.Qr_Employee_Daily_Attendance.Where(c => c.qrEmpDaId == qrEmpDaId).FirstOrDefault();
            string Time = att.startTime;
            DateTime date = DateTime.Parse(Time, System.Globalization.CultureInfo.CurrentCulture);
            string t = date.ToString("hh:mm:ss tt");
            string dt = Convert.ToDateTime(att.startDate).ToString("MM/dd/yyyy");
            DateTime? fdate = Convert.ToDateTime(dt + " " + t);
            DateTime? edate;
            if (att.endTime == "" | att.endTime == null)
            {
                edate = DateTime.Now;
            }
            else
            {
                string Time2 = att.endTime;
                DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                string t2 = date2.ToString("hh:mm:ss tt");
                string dt2 = Convert.ToDateTime(att.endDate).ToString("MM/dd/yyyy");
                edate = Convert.ToDateTime(dt2 + " " + t2);
            }


            var data = (from t1 in db.Qr_Location.Where(c => c.empId == att.qrEmpId & c.datetime >= fdate & c.datetime <= edate)
                        join t2 in db.QrEmployeeMasters on t1.empId equals t2.qrEmpId
                        select new { t1.locId, t1.batteryStatus, t1.datetime, t1.lat, t1.@long, t1.address, t2.qrEmpName, t2.qrEmpMobileNumber }).ToList();

            // var data =   db.Qr_Location.Where(c => c.empId == att.qrEmpId & c.datetime >= fdate & c.datetime <= edate).ToList();


            foreach (var x in data)
            {

                string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");
                string tim = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");
                // var userName = db.UserMasters.Where(c => c.userId == att.userId).FirstOrDefault();

                userLocation.Add(new SBALHSUserLocationMapView()
                {
                    userName = x.qrEmpName,
                    date = dat,
                    time = tim,
                    lat = x.lat,
                    log = x.@long,
                    address = checkNull(x.address).Replace("Unnamed Road, ", ""),
                    //vehcileNumber = att.vehicleNumber,
                    userMobile = x.qrEmpMobileNumber,
                    // type = Convert.ToInt32(x.type),

                });

            }

            return userLocation;
        }


        //Added by milind 09-03-2022
        public async Task<List<SBAHSHouseDetailsGrid>> GetHSQRCodeImageByDate1(int type, int UserId, DateTime fDate, DateTime tDate, string QrStatus)
        {
            bool? bQRStatus = null;
            if (QrStatus == "1")
            {
                bQRStatus = true;
            }
            else if (QrStatus == "2")
            {
                bQRStatus = false;

            }

            List<SBAHSHouseDetailsGrid> data = new List<SBAHSHouseDetailsGrid>();
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    //db.Configuration.ProxyCreationEnabled = false;
                    if (UserId > 0)
                    {
                        if (type == 0)
                        {
                            //  data = db.HouseMasters.Where(a => a.modified > fDate && a.modified < tDate && !string.IsNullOrEmpty(a.houseLat) && !string.IsNullOrEmpty(a.houseLong) && !string.IsNullOrEmpty(a.QRCodeImage) && a.userId == UserId));
                            if (QrStatus == "-1")
                            {
                                IQueryable<VW_HSGetHouseDetails> query = db.VW_HSGetHouseDetails.Where(a => a.modified > fDate && a.modified < tDate && !string.IsNullOrEmpty(a.houseLat) && !string.IsNullOrEmpty(a.houseLong) && !string.IsNullOrEmpty(a.BinaryQrCodeImage) && a.userId == UserId);
                                data = query.Select(x => new SBAHSHouseDetailsGrid
                                {
                                    houseId = x.houseId,
                                    Name = x.houseOwner,
                                    HouseLat = x.houseLat,
                                    HouseLong = x.houseLong,
                                    QRCodeImage = x.BinaryQrCodeImage,
                                    ReferanceId = x.ReferanceId
                                }).OrderBy(a => a.houseId).ToList();
                            }
                            else if (QrStatus == "1" || QrStatus == "2")
                            {
                                IQueryable<VW_HSGetHouseDetails> query = db.VW_HSGetHouseDetails.Where(a => a.modified > fDate && a.modified < tDate && !string.IsNullOrEmpty(a.houseLat) && !string.IsNullOrEmpty(a.houseLong) && !string.IsNullOrEmpty(a.BinaryQrCodeImage) && a.userId == UserId && a.QRStatus == bQRStatus);
                                data = query.Select(x => new SBAHSHouseDetailsGrid
                                {
                                    houseId = x.houseId,
                                    Name = x.houseOwner,
                                    HouseLat = x.houseLat,
                                    HouseLong = x.houseLong,
                                    QRCodeImage = x.BinaryQrCodeImage,
                                    ReferanceId = x.ReferanceId
                                }).OrderBy(a => a.houseId).ToList();

                            }
                        }
                        else if (type == 1)
                        {
                            // data = db.DumpYardDetails.Where(a => a.lastModifiedDate > fDate && a.lastModifiedDate < tDate && !string.IsNullOrEmpty(a.dyLat) && !string.IsNullOrEmpty(a.dyLong) && !string.IsNullOrEmpty(a.QRCodeImage) && a.userId == UserId).Select(x => new SBAHSHouseDetailsGrid

                            if (QrStatus == "-1")
                            {
                                IQueryable<VW_HSGetDumpyardDetails> query = db.VW_HSGetDumpyardDetails.Where(a => a.lastModifiedDate > fDate && a.lastModifiedDate < tDate && !string.IsNullOrEmpty(a.dyLat) && !string.IsNullOrEmpty(a.dyLong) && !string.IsNullOrEmpty(a.BinaryQrCodeImage) && a.userId == UserId);
                                data = query.Select(x => new SBAHSHouseDetailsGrid
                                {
                                    houseId = x.dyId,
                                    Name = x.qrEmpName,
                                    HouseLat = x.dyLat,
                                    HouseLong = x.dyLong,
                                    QRCodeImage = x.BinaryQrCodeImage,
                                    ReferanceId = x.ReferanceId
                                }).OrderBy(a => a.houseId).ToList();
                            }
                            else if (QrStatus == "1" || QrStatus == "2")
                            {
                                IQueryable<VW_HSGetDumpyardDetails> query = db.VW_HSGetDumpyardDetails.Where(a => a.lastModifiedDate > fDate && a.lastModifiedDate < tDate && !string.IsNullOrEmpty(a.dyLat) && !string.IsNullOrEmpty(a.dyLong) && !string.IsNullOrEmpty(a.BinaryQrCodeImage) && a.userId == UserId && a.QRStatus == bQRStatus);
                                data = query.Select(x => new SBAHSHouseDetailsGrid
                                {
                                    houseId = x.dyId,
                                    Name = x.qrEmpName,
                                    HouseLat = x.dyLat,
                                    HouseLong = x.dyLong,
                                    QRCodeImage = x.BinaryQrCodeImage,
                                    ReferanceId = x.ReferanceId
                                }).OrderBy(a => a.houseId).ToList();
                            }


                        }
                        else if (type == 2)
                        {
                            // data = db.LiquidWasteDetails.Where(a => a.lastModifiedDate > fDate && a.lastModifiedDate < tDate && !string.IsNullOrEmpty(a.LWLat) && !string.IsNullOrEmpty(a.LWLong) && !string.IsNullOrEmpty(a.QRCodeImage) && a.userId == UserId).Select(x => new SBAHSHouseDetailsGrid

                            if (QrStatus == "-1")
                            {
                                IQueryable<VW_HSGetLiquidDetails> query = db.VW_HSGetLiquidDetails.Where(a => a.lastModifiedDate > fDate && a.lastModifiedDate < tDate && !string.IsNullOrEmpty(a.LWLat) && !string.IsNullOrEmpty(a.LWLong) && !string.IsNullOrEmpty(a.BinaryQrCodeImage) && a.userId == UserId);
                                data = query.Select(x => new SBAHSHouseDetailsGrid
                                {
                                    houseId = x.LWId,
                                    Name = x.qrEmpName,
                                    HouseLat = x.LWLat,
                                    HouseLong = x.LWLong,
                                    QRCodeImage = x.BinaryQrCodeImage,
                                    ReferanceId = x.ReferanceId
                                }).OrderBy(a => a.houseId).ToList();
                            }
                            else if (QrStatus == "1" || QrStatus == "2")
                            {
                                IQueryable<VW_HSGetLiquidDetails> query = db.VW_HSGetLiquidDetails.Where(a => a.lastModifiedDate > fDate && a.lastModifiedDate < tDate && !string.IsNullOrEmpty(a.LWLat) && !string.IsNullOrEmpty(a.LWLong) && !string.IsNullOrEmpty(a.BinaryQrCodeImage) && a.userId == UserId && a.QRStatus == bQRStatus);
                                data = query.Select(x => new SBAHSHouseDetailsGrid
                                {
                                    houseId = x.LWId,
                                    Name = x.qrEmpName,
                                    HouseLat = x.LWLat,
                                    HouseLong = x.LWLong,
                                    QRCodeImage = x.BinaryQrCodeImage,
                                    ReferanceId = x.ReferanceId
                                }).OrderBy(a => a.houseId).ToList();
                            }


                        }
                        else if (type == 3)
                        {
                            //data = db.StreetSweepingDetails.Where(a => a.lastModifiedDate > fDate && a.lastModifiedDate < tDate && !string.IsNullOrEmpty(a.SSLat) && !string.IsNullOrEmpty(a.SSLong) && !string.IsNullOrEmpty(a.QRCodeImage) && a.userId == UserId).Select(x => new SBAHSHouseDetailsGrid
                            if (QrStatus == "-1")
                            {
                                IQueryable<VW_HSGetStreetDetails> query = db.VW_HSGetStreetDetails.Where(a => a.lastModifiedDate > fDate && a.lastModifiedDate < tDate && !string.IsNullOrEmpty(a.SSLat) && !string.IsNullOrEmpty(a.SSLong) && !string.IsNullOrEmpty(a.BinaryQrCodeImage) && a.userId == UserId);
                                data = query.Select(x => new SBAHSHouseDetailsGrid
                                {
                                    houseId = x.SSId,
                                    Name = x.qrEmpName,
                                    HouseLat = x.SSLat,
                                    HouseLong = x.SSLong,
                                    QRCodeImage = x.BinaryQrCodeImage,
                                    ReferanceId = x.ReferanceId
                                }).OrderBy(a => a.houseId).ToList();
                            }
                            else if (QrStatus == "1" || QrStatus == "2")
                            {
                                IQueryable<VW_HSGetStreetDetails> query = db.VW_HSGetStreetDetails.Where(a => a.lastModifiedDate > fDate && a.lastModifiedDate < tDate && !string.IsNullOrEmpty(a.SSLat) && !string.IsNullOrEmpty(a.SSLong) && !string.IsNullOrEmpty(a.BinaryQrCodeImage) && a.userId == UserId && a.QRStatus == bQRStatus);
                                data = query.Select(x => new SBAHSHouseDetailsGrid
                                {
                                    houseId = x.SSId,
                                    Name = x.qrEmpName,
                                    HouseLat = x.SSLat,
                                    HouseLong = x.SSLong,
                                    QRCodeImage = x.BinaryQrCodeImage,
                                    ReferanceId = x.ReferanceId
                                }).OrderBy(a => a.houseId).ToList();
                            }


                        }
                    }
                    else
                    {
                        //int  skip = 0;
                        //int  take = 10000;
                        //  var list = (from usr in db.HouseMasters
                        //              select usr).OrderBy(i => i.houseId).Skip(skip).Take(take).ToList();

                        //var   userlist = (from i in list
                        //               select new UserInfo(i, true)).ToList();


                        // IQueryable<HouseMaster> query = db.HouseMasters.Where(tt => tt.modified>fDate && tt.modified<tDate).OrderByDescending(tt => tt.houseId);

                        //  var data1 = await  (from p in db.HouseMasters  where p.modified == fDate select p).ToListAsync();
                        //     data = await  query.ToListAsync();

                        //  var data1 = await Task.Run(() => db.HouseMasters.Where(tt => tt.modified == fDate).ToList());

                        //var data1 = await Task.Run( async() => db.HouseMasters.Where(tt => tt.modified == fDate).ToList());


                        // var data1 = await  db.HouseMasters.Where(tt => tt.modified == fDate).ToListAsync();

                        //var tasks = db.HouseMasters.ToListAsync();
                        //var data1 = await Task.WhenAll(tasks);

                        //data = users.Select(x => new SBAHSHouseDetailsGrid
                        //{
                        //    houseId = x.houseId,
                        //    Name = x.houseOwner,
                        //    HouseLat = x.houseLat,
                        //    HouseLong = x.houseLong,
                        //    QRCodeImage = x.QRCodeImage,
                        //    ReferanceId = x.ReferanceId
                        //}).OrderBy(a => a.houseId).ToList();

                        //  List<SBAHSHouseDetailsGrid> ResultValues = query.ToList();


                        if (type == 0)
                        {

                            if (QrStatus == "-1")
                            {
                                IQueryable<VW_HSGetHouseDetails> query = db.VW_HSGetHouseDetails.Where(a => a.modified > fDate && a.modified < tDate && !string.IsNullOrEmpty(a.houseLat) && !string.IsNullOrEmpty(a.houseLong) && !string.IsNullOrEmpty(a.BinaryQrCodeImage));
                                data = query.Select(x => new SBAHSHouseDetailsGrid
                                {
                                    houseId = x.houseId,
                                    Name = x.qrEmpName,
                                    HouseLat = x.houseLat,
                                    HouseLong = x.houseLong,
                                    QRCodeImage = x.BinaryQrCodeImage,
                                    ReferanceId = x.ReferanceId
                                }).OrderBy(a => a.houseId).ToList();
                            }
                            else if (QrStatus == "1" || QrStatus == "2")
                            {
                                IQueryable<VW_HSGetHouseDetails> query = db.VW_HSGetHouseDetails.Where(a => a.modified > fDate && a.modified < tDate && !string.IsNullOrEmpty(a.houseLat) && !string.IsNullOrEmpty(a.houseLong) && !string.IsNullOrEmpty(a.BinaryQrCodeImage) && a.QRStatus == bQRStatus);
                                data = query.Select(x => new SBAHSHouseDetailsGrid
                                {
                                    houseId = x.houseId,
                                    Name = x.qrEmpName,
                                    HouseLat = x.houseLat,
                                    HouseLong = x.houseLong,
                                    QRCodeImage = x.BinaryQrCodeImage,
                                    ReferanceId = x.ReferanceId
                                }).OrderBy(a => a.houseId).ToList();
                            }


                        }
                        else if (type == 1)
                        {
                            //data = db.DumpYardDetails.Where(a => a.lastModifiedDate > fDate && a.lastModifiedDate < tDate && !string.IsNullOrEmpty(a.dyLat) && !string.IsNullOrEmpty(a.dyLong) && !string.IsNullOrEmpty(a.QRCodeImage)).Select(x => new SBAHSHouseDetailsGrid

                            if (QrStatus == "-1")
                            {
                                IQueryable<VW_HSGetDumpyardDetails> query = db.VW_HSGetDumpyardDetails.Where(a => a.lastModifiedDate > fDate && a.lastModifiedDate < tDate && !string.IsNullOrEmpty(a.dyLat) && !string.IsNullOrEmpty(a.dyLong) && !string.IsNullOrEmpty(a.BinaryQrCodeImage));
                                data = query.Select(x => new SBAHSHouseDetailsGrid
                                {
                                    houseId = x.dyId,
                                    Name = x.dyName,
                                    HouseLat = x.dyLat,
                                    HouseLong = x.dyLong,
                                    QRCodeImage = x.BinaryQrCodeImage,
                                    ReferanceId = x.ReferanceId
                                }).OrderBy(a => a.houseId).ToList();
                            }
                            else if (QrStatus == "1" || QrStatus == "2")
                            {
                                IQueryable<VW_HSGetDumpyardDetails> query = db.VW_HSGetDumpyardDetails.Where(a => a.lastModifiedDate > fDate && a.lastModifiedDate < tDate && !string.IsNullOrEmpty(a.dyLat) && !string.IsNullOrEmpty(a.dyLong) && !string.IsNullOrEmpty(a.BinaryQrCodeImage) && a.QRStatus == bQRStatus);
                                data = query.Select(x => new SBAHSHouseDetailsGrid
                                {
                                    houseId = x.dyId,
                                    Name = x.dyName,
                                    HouseLat = x.dyLat,
                                    HouseLong = x.dyLong,
                                    QRCodeImage = x.BinaryQrCodeImage,
                                    ReferanceId = x.ReferanceId
                                }).OrderBy(a => a.houseId).ToList();
                            }


                        }
                        else if (type == 2)
                        {
                            //data = db.LiquidWasteDetails.Where(a => a.lastModifiedDate > fDate && a.lastModifiedDate < tDate && !string.IsNullOrEmpty(a.LWLat) && !string.IsNullOrEmpty(a.LWLong) && !string.IsNullOrEmpty(a.QRCodeImage)).Select(x => new SBAHSHouseDetailsGrid

                            if (QrStatus == "-1")
                            {
                                IQueryable<VW_HSGetLiquidDetails> query = db.VW_HSGetLiquidDetails.Where(a => a.lastModifiedDate > fDate && a.lastModifiedDate < tDate && !string.IsNullOrEmpty(a.LWLat) && !string.IsNullOrEmpty(a.LWLong) && !string.IsNullOrEmpty(a.BinaryQrCodeImage));
                                data = query.Select(x => new SBAHSHouseDetailsGrid
                                {
                                    houseId = x.LWId,
                                    Name = x.qrEmpName,
                                    HouseLat = x.LWLat,
                                    HouseLong = x.LWLong,
                                    QRCodeImage = x.BinaryQrCodeImage,
                                    ReferanceId = x.ReferanceId
                                }).OrderBy(a => a.houseId).ToList();
                            }
                            else if (QrStatus == "1" || QrStatus == "2")
                            {
                                IQueryable<VW_HSGetLiquidDetails> query = db.VW_HSGetLiquidDetails.Where(a => a.lastModifiedDate > fDate && a.lastModifiedDate < tDate && !string.IsNullOrEmpty(a.LWLat) && !string.IsNullOrEmpty(a.LWLong) && !string.IsNullOrEmpty(a.BinaryQrCodeImage) && a.QRStatus == bQRStatus);
                                data = query.Select(x => new SBAHSHouseDetailsGrid
                                {
                                    houseId = x.LWId,
                                    Name = x.qrEmpName,
                                    HouseLat = x.LWLat,
                                    HouseLong = x.LWLong,
                                    QRCodeImage = x.BinaryQrCodeImage,
                                    ReferanceId = x.ReferanceId
                                }).OrderBy(a => a.houseId).ToList();
                            }


                        }
                        else if (type == 3)
                        {
                            //data = db.StreetSweepingDetails.Where(a => a.lastModifiedDate > fDate && a.lastModifiedDate < tDate && !string.IsNullOrEmpty(a.SSLat) && !string.IsNullOrEmpty(a.SSLong) && !string.IsNullOrEmpty(a.QRCodeImage)).Select(x => new SBAHSHouseDetailsGrid

                            if (QrStatus == "-1")
                            {
                                IQueryable<VW_HSGetStreetDetails> query = db.VW_HSGetStreetDetails.Where(a => a.lastModifiedDate > fDate && a.lastModifiedDate < tDate && !string.IsNullOrEmpty(a.SSLat) && !string.IsNullOrEmpty(a.SSLong) && !string.IsNullOrEmpty(a.BinaryQrCodeImage));
                                data = query.Select(x => new SBAHSHouseDetailsGrid
                                {
                                    houseId = x.SSId,
                                    Name = x.qrEmpName,
                                    HouseLat = x.SSLat,
                                    HouseLong = x.SSLong,
                                    QRCodeImage = x.BinaryQrCodeImage,
                                    ReferanceId = x.ReferanceId
                                }).OrderBy(a => a.houseId).ToList();
                            }
                            else if (QrStatus == "1" || QrStatus == "2")
                            {
                                IQueryable<VW_HSGetStreetDetails> query = db.VW_HSGetStreetDetails.Where(a => a.lastModifiedDate > fDate && a.lastModifiedDate < tDate && !string.IsNullOrEmpty(a.SSLat) && !string.IsNullOrEmpty(a.SSLong) && !string.IsNullOrEmpty(a.BinaryQrCodeImage) && a.QRStatus == bQRStatus);
                                data = query.Select(x => new SBAHSHouseDetailsGrid
                                {
                                    houseId = x.SSId,
                                    Name = x.qrEmpName,
                                    HouseLat = x.SSLat,
                                    HouseLong = x.SSLong,
                                    QRCodeImage = x.BinaryQrCodeImage,
                                    ReferanceId = x.ReferanceId
                                }).OrderBy(a => a.houseId).ToList();

                            }


                        }


                    }
                }
            }
            catch (Exception ex)
            {
                return data;
            }
            return data;
        }

        public DataTable getHousesList(int option, int type)
        {
            string query = string.Empty;

            if (type == 1)
            {
                if (option == 1)
                {
                    query = "select ReferanceId StreetId, SSName StreetName,SSAddress StreetAddress,SSLat,SSLong,W.WardNo,T.Area, z.name ZoneName from StreetSweepingDetails S left join TeritoryMaster T on S.areaId = T.Id left join WardNumber W on S.wardId = W.Id left join ZoneMaster Z on S.zoneId = Z.zoneId where S.SSLat is not null AND S.SSLat <> '' AND S.SSLong is not null AND S.SSLong <> '' AND (S.SSName is null OR S.SSName = '') ORDER BY S.SSId asc";
                }
                else if (option == 2)
                {
                    query = "select ReferanceId StreetId, SSName StreetName,SSAddress StreetAddress,SSLat,SSLong,W.WardNo,T.Area, z.name ZoneName from StreetSweepingDetails S left join TeritoryMaster T on S.areaId = T.Id left join WardNumber W on S.wardId = W.Id left join ZoneMaster Z on S.zoneId = Z.zoneId where S.SSName is not null AND S.SSName <> '' ORDER BY S.SSId asc";

                }
                else
                {
                    query = "select ReferanceId StreetId, SSName StreetName,SSAddress StreetAddress,SSLat,SSLong,W.WardNo,T.Area, z.name ZoneName from StreetSweepingDetails S left join TeritoryMaster T on S.areaId = T.Id left join WardNumber W on S.wardId = W.Id left join ZoneMaster Z on S.zoneId = Z.zoneId ORDER BY S.SSId asc";

                }

            }
            else if (type == 2)
            {
                if (option == 1)
                {
                    query = "select ReferanceId LiquidId,LWName LiquidName,LWAddreLW LiquidAddress,LWLat,LWLong, W.WardNo,T.Area, z.name ZoneName from LiquidWasteDetails L left join TeritoryMaster T on L.areaId = T.Id left join WardNumber W on L.wardId = W.Id left join ZoneMaster Z on L.zoneId = Z.zoneId where L.LWLat is not null AND L.LWLat <> '' AND L.LWLong is not null AND L.LWLong <> '' AND (L.LWName is null OR L.LWName = '') ORDER BY L.LWId asc";
                }
                else if (option == 2)
                {
                    query = "select ReferanceId LiquidId,LWName LiquidName,LWAddreLW LiquidAddress,LWLat,LWLong, W.WardNo,T.Area, z.name ZoneName from LiquidWasteDetails L left join TeritoryMaster T on L.areaId = T.Id left join WardNumber W on L.wardId = W.Id left join ZoneMaster Z on L.zoneId = Z.zoneId where L.LWName is not null AND L.LWName <> '' ORDER BY L.LWId asc";

                }
                else
                {
                    query = "select ReferanceId LiquidId,LWName LiquidName,LWAddreLW LiquidAddress,LWLat,LWLong, W.WardNo,T.Area, z.name ZoneName from LiquidWasteDetails L left join TeritoryMaster T on L.areaId = T.Id left join WardNumber W on L.wardId = W.Id left join ZoneMaster Z on L.zoneId = Z.zoneId ORDER BY L.LWId asc";

                }
            }
            else if (type == 3)
            {
                if (option == 1)
                {
                    query = "select ReferanceId DumpId, dyName DumpName,dyAddress DumpAddress, dyLat,dyLong, W.WardNo,T.Area, z.name ZoneName  from DumpYardDetails D left join TeritoryMaster T on D.areaId = T.Id left join WardNumber W on D.wardId = W.Id left join ZoneMaster Z on D.zoneId = Z.zoneId where D.dyLat is not null AND D.dyLat <> '' AND D.dyLong is not null AND D.dyLong <> '' AND (D.dyName is null OR D.dyName = '') ORDER BY D.dyId asc";
                }
                else if (option == 2)
                {
                    query = "select ReferanceId DumpId, dyName DumpName,dyAddress DumpAddress, dyLat,dyLong, W.WardNo,T.Area, z.name ZoneName  from DumpYardDetails D left join TeritoryMaster T on D.areaId = T.Id left join WardNumber W on D.wardId = W.Id left join ZoneMaster Z on D.zoneId = Z.zoneId where D.dyName is not null AND D.dyName <> '' ORDER BY D.dyId asc";

                }
                else
                {
                    query = "select ReferanceId DumpId, dyName DumpName,dyAddress DumpAddress, dyLat,dyLong, W.WardNo,T.Area, z.name ZoneName  from DumpYardDetails D left join TeritoryMaster T on D.areaId = T.Id left join WardNumber W on D.wardId = W.Id left join ZoneMaster Z on D.zoneId = Z.zoneId ORDER BY D.dyId asc";

                }
            }
            else
            {
                if (option == 1)
                {
                    query = "select ReferanceId HouseId, houseOwner,OccupancyStatus OwnerType, Property_Type,houseOwnerMobile,houseLat,houseLong,houseAddress,W.WardNo,T.Area, z.name ZoneName from HouseMaster H left join TeritoryMaster T on H.AreaId = T.Id left join WardNumber W on W.Id = H.WardNo left join ZoneMaster Z on z.zoneId = h.ZoneId where H.houseLat is not null AND H.houseLat <> '' AND H.houseLong is not null AND H.houseLong <> '' AND (H.houseOwner is null OR H.houseOwner = '') order BY H.houseId asc";
                }
                else if (option == 2)
                {
                    query = "select ReferanceId HouseId, houseOwner,OccupancyStatus OwnerType, Property_Type,houseOwnerMobile,houseLat,houseLong,houseAddress,W.WardNo,T.Area, z.name ZoneName from HouseMaster H left join TeritoryMaster T on H.AreaId = T.Id left join WardNumber W on W.Id = H.WardNo left join ZoneMaster Z on z.zoneId = h.ZoneId where H.houseOwner is not null AND H.houseOwner <> '' order BY H.houseId asc";

                }
                else
                {
                    query = "select ReferanceId HouseId, houseOwner,OccupancyStatus OwnerType, Property_Type,houseOwnerMobile,houseLat,houseLong,houseAddress,W.WardNo,T.Area, z.name ZoneName from HouseMaster H left join TeritoryMaster T on H.AreaId = T.Id left join WardNumber W on W.Id = H.WardNo left join ZoneMaster Z on z.zoneId = h.ZoneId ORDER BY H.houseId asc";

                }
            }




            var data = new DataTable();
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var conn = db.Database.Connection;
                    var connectionState = conn.State;
                    try
                    {
                        if (connectionState != ConnectionState.Open) conn.Open();
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = query;
                            cmd.CommandType = CommandType.Text;
                            using (var reader = cmd.ExecuteReader())
                            {
                                data.Load(reader);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // error handling
                        return data;
                    }
                    finally
                    {
                        if (connectionState != ConnectionState.Closed) conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                return data;
            }
            return data;
        }

        public List<SBAHSHouseDetailsGrid> GetHSQRCodeImageByDate(int type, int UserId, DateTime fDate, DateTime tDate, string QrStatus)
        {

            bool? bQRStatus = null;
            if (QrStatus == "1")
            {
                bQRStatus = true;
            }
            else if (QrStatus == "2")
            {
                bQRStatus = false;

            }
            else
            {
                bQRStatus = null;
            }
            List<SBAHSHouseDetailsGrid> data = new List<SBAHSHouseDetailsGrid>();

            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (type == 0)
                    {
                        data = db.HouseMasters.Where(a => ((bQRStatus != null && a.QRStatus == bQRStatus) || bQRStatus == null) && (a.modified >= fDate && a.modified <= tDate) && !string.IsNullOrEmpty(a.houseLat) && !string.IsNullOrEmpty(a.houseLong) && ((UserId > 0 && a.userId == UserId) || UserId <= 0) && (a.BinaryQrCodeImage != null)).Select(x => new SBAHSHouseDetailsGrid
                        {
                            houseId = x.houseId,
                            Name = x.houseOwner,
                            HouseLat = x.houseLat,
                            HouseLong = x.houseLong,
                            //QRCodeImage = x.QRCodeImage,
                            BinaryQrCodeImage = x.BinaryQrCodeImage,
                            ReferanceId = x.ReferanceId
                        }).OrderBy(a => a.houseId).ToList();
                    }
                    else if (type == 1)
                    {
                        data = db.DumpYardDetails.Where(a => ((bQRStatus != null && a.QRStatus == bQRStatus) || bQRStatus == null) && (a.lastModifiedDate >= fDate && a.lastModifiedDate <= tDate) && !string.IsNullOrEmpty(a.dyLat) && !string.IsNullOrEmpty(a.dyLong) && ((UserId > 0 && a.userId == UserId) || UserId <= 0) && (a.BinaryQrCodeImage != null)).Select(x => new SBAHSHouseDetailsGrid
                        {
                            houseId = x.dyId,
                            Name = x.dyName,
                            HouseLat = x.dyLat,
                            HouseLong = x.dyLong,
                            //QRCodeImage = x.QRCodeImage,
                            BinaryQrCodeImage = x.BinaryQrCodeImage,
                            ReferanceId = x.ReferanceId
                        }).OrderBy(a => a.houseId).ToList();

                    }
                    else if (type == 2)
                    {
                        data = db.LiquidWasteDetails.Where(a => ((bQRStatus != null && a.QRStatus == bQRStatus) || bQRStatus == null) && (a.lastModifiedDate >= fDate && a.lastModifiedDate <= tDate) && !string.IsNullOrEmpty(a.LWLat) && !string.IsNullOrEmpty(a.LWLong) && ((UserId > 0 && a.userId == UserId) || UserId <= 0) && (a.BinaryQrCodeImage != null)).Select(x => new SBAHSHouseDetailsGrid
                        {
                            houseId = x.LWId,
                            Name = x.LWName,
                            HouseLat = x.LWLat,
                            HouseLong = x.LWLong,
                            //QRCodeImage = x.QRCodeImage,
                            BinaryQrCodeImage = x.BinaryQrCodeImage,
                            ReferanceId = x.ReferanceId
                        }).OrderBy(a => a.houseId).ToList();
                    }
                    else if (type == 3)
                    {
                        data = db.StreetSweepingDetails.Where(a => ((bQRStatus != null && a.QRStatus == bQRStatus) || bQRStatus == null) && (a.lastModifiedDate >= fDate && a.lastModifiedDate <= tDate) && !string.IsNullOrEmpty(a.SSLat) && !string.IsNullOrEmpty(a.SSLong) && ((UserId > 0 && a.userId == UserId) || UserId <= 0) && (a.BinaryQrCodeImage != null)).Select(x => new SBAHSHouseDetailsGrid
                        {
                            houseId = x.SSId,
                            Name = x.SSName,
                            HouseLat = x.SSLat,
                            HouseLong = x.SSLong,
                            //QRCodeImage = x.QRCodeImage,
                            BinaryQrCodeImage = x.BinaryQrCodeImage,
                            ReferanceId = x.ReferanceId
                        }).OrderBy(a => a.houseId).ToList();

                    }
                }
            }
            catch (Exception ex)
            {
                return data;
            }
            return data;
        }

        public string getbinarytobase64(string plainText)
        {

            var plainTextBytes = System.Text.Encoding.ASCII.GetBytes(plainText);
            var base64 = System.Convert.ToBase64String(plainTextBytes);
            return base64;
        }

        #endregion

        // Added By Neha(12 July 2019)
        #region idleTime map
        public List<SBAEmplyeeIdelGrid> GetIdleTimeRoute(int userId, string date)
        {
            DateTime date1 = DateTime.ParseExact(date, "dd/MM/yyyy", null);
            List<SBAEmplyeeIdelGrid> obj = new List<SBAEmplyeeIdelGrid>();
            var data = db.SP_IdelTime(userId, date1, date1).Where(c => c.IdelTime != null & c.IdelTime > 15).ToList();
            foreach (var x in data)
            {
                TimeSpan spWorkMin = TimeSpan.FromMinutes(Convert.ToDouble(x.IdelTime));
                string workHours = spWorkMin.ToString(@"hh\:mm");
                obj.Add(new SBAEmplyeeIdelGrid()
                {
                    UserName = x.userName,
                    Date = Convert.ToDateTime(x.date).ToString("dd/MM/yyyy"),
                    StartTime = x.StartTime,
                    EndTime = x.LastTime,
                    StartAddress = checkNull(x.StartAddress).Replace("Unnamed Road, ", ""),
                    EndAddress = checkNull(x.address).Replace("Unnamed Road, ", ""),
                    IdelTime = workHours,
                    startLat = x.StarLat,
                    startLong = x.StartLog,
                    EndLat = x.lat,
                    EndLong = x.@long,
                    userId = x.userId

                });


            }
            return obj;
        }
        #endregion

        public StreetSweepVM GetBeatDetails(int teamId)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.SP_StreetSweepList().Where(x => x.SSId == teamId).FirstOrDefault();
                    if (Details != null)
                    {
                        StreetSweepVM vechile = new StreetSweepVM();
                        vechile.BeatList = ListBeat();
                        return vechile;
                    }
                    else
                    {
                        StreetSweepVM vechile = new StreetSweepVM();
                        vechile.BeatList = ListBeat();
                        return vechile;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> ListBeat()
        {
            var Vehicle = new List<SelectListItem>();
            SelectListItem itemAdd = new SelectListItem() { Text = "--Select Beat--", Value = "0" };

            try
            {
                Vehicle = db.SP_StreetSweepList().ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.ReferanceId,
                        Value = x.ReferanceId.ToString()
                    }).OrderBy(t => t.Text).ToList();

                Vehicle.Insert(0, itemAdd);
            }
            catch (Exception ex) { throw ex; }

            return Vehicle;
        }

        public StreetSweepVM SaveStreetBeatDetails(StreetSweepVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.BeatId > 0)
                    {
                        var model = db.StreetSweepingBeats.Where(x => x.BeatId == data.BeatId).FirstOrDefault();
                        if (model != null)
                        {
                            model.CreateDate = DateTime.Now;
                            model.ReferanceId1 = data.SSBeatone;
                            model.ReferanceId2 = data.SSBeattwo;
                            model.ReferanceId3 = data.SSBeatthree;
                            model.ReferanceId4 = data.SSBeatfour;
                            model.ReferanceId5 = data.SSBeatfive;

                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var type = FillStreetBeatDetailsDataModel(data);
                        db.StreetSweepingBeats.Add(type);
                        db.SaveChanges();
                    }

                }
                var SSId = db.StreetSweepingBeats.OrderByDescending(x => x.BeatId).Select(x => x.BeatId).FirstOrDefault();
                StreetSweepVM vv = GetBeatDetails(SSId);
                return vv;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        private StreetSweepingBeat FillStreetBeatDetailsDataModel(StreetSweepVM data)
        {
            StreetSweepingBeat model = new StreetSweepingBeat();
            model.CreateDate = DateTime.Now;
            model.ReferanceId1 = data.SSBeatone;
            model.ReferanceId2 = data.SSBeattwo;
            model.ReferanceId3 = data.SSBeatthree;
            model.ReferanceId4 = data.SSBeatfour;
            model.ReferanceId5 = data.SSBeatfive;

            return model;
        }

        public void Save1Point7(List<OnePointSevenVM> data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {

                    foreach (var item in data)
                    {
                        var type = FillSS1Point7DataModel(item);
                        db.SS_1_7_ANSWER.Add(type);
                        db.SaveChanges();
                    }
                    HttpContext.Current.Session["INSERT_ID"] = null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<OnePoint7QuestionVM> GetOnePointSevenQuestions()
        {
            using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
            {
                var data = db.RPT_1point7_questions().Select(x => new OnePoint7QuestionVM
                {
                    Id = x.Id,
                    Area = x.Area,
                    wardId = x.wardId.Value,
                }).ToList();
                return data;
            }
        }

        public List<OnePoint7QuestionVM> GetOnePointSevenAnswers(int INSERT_ID)
        {
            using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
            {
                var data = db.RPT_1POINT7_DETAILS(INSERT_ID).Select(x => new OnePoint7QuestionVM
                {
                    Id = x.Id,
                    Area = x.Area,
                    No_drain_nallas = x.No_drain_nallas.ToString() == "0" ? "" : x.No_drain_nallas.ToString(),
                    No_Water_Bodies = x.No_water_bodies.ToString() == "0" ? "" : x.No_water_bodies.ToString(),
                    No_locations = x.No_locations.ToString() == "0" ? "" : x.No_locations.ToString(),
                    No_outlets = x.No_outlets.ToString() == "0" ? "" : x.No_outlets.ToString(),
                    INSERT_ID = x.INSERT_ID.Value

                }).ToList();
                return data;
            }
        }

        public void Save1Point4(List<OnePoint4VM> data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {

                    foreach (var item in data)
                    {
                        var type = FillSS1Point4DataModel(item);
                        db.SS_1_4_ANSWER.Add(type);
                        db.SaveChanges();
                    }
                    HttpContext.Current.Session["DateTime"] = null;
                    HttpContext.Current.Session["INSERT_ID"] = null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Save1Point5(List<OnePoint5VM> data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {

                    foreach (var item in data)
                    {
                        var type = FillSS1Point5DataModel(item);
                        db.SS_1_4_ANSWER.Add(type);
                        db.SaveChanges();

                    }
                    HttpContext.Current.Session["DateTime"] = null;
                    HttpContext.Current.Session["INSERT_ID"] = null;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }
        }


        public List<OnePoint4VM> GetQuetions(string ReportName)
        {
            using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
            {
                var data = db.SS_1_4_QUESTION.Where(p => p.Q_NO == ReportName).Select(x => new OnePoint4VM
                {
                    Q_ID = x.Q_ID,
                    QUESTIOND = x.QUESTIOND,
                }).ToList();
                return data;
            }
        }

        public List<OnePoint5VM> GetQuetions1pointfive()
        {
            using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
            {
                var data = db.SS_1_4_QUESTION.Where(p => p.Q_NO == "1.5").Select(x => new OnePoint5VM
                {
                    Q_ID = x.Q_ID,
                    QUESTIOND = x.QUESTIOND,
                }).ToList();

                return data;
            }
        }
        public List<OnePoint4VM> GetOnepointfourEditData(int INSERT_ID)
        {
            using (var dbMain = new DevChildSwachhBharatNagpurEntities(AppID))
            {
                var data = dbMain.SS_1_4_ANSWER.Where(m => m.INSERT_ID == INSERT_ID).
                    Select(x => new OnePoint4VM
                    {
                        INSERT_DATE = x.INSERT_DATE.Value,
                        INSERT_ID = x.INSERT_ID.Value,
                        ANS_ID = x.ANS_ID,
                        Q_ID = x.Q_ID,
                        TOTAL_COUNT = x.TOTAL_COUNT
                    }).ToList();

                return data;
            }
        }

        public OnePoint4VM GetTotalCountDetails(int ANS_ID)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    OnePoint4VM model = new OnePoint4VM();
                    var Data = db.SS_1_4_ANSWER.Where(m => m.ANS_ID == ANS_ID).SingleOrDefault();
                    if (Data != null)
                    {
                        model.TOTAL_COUNT = Data.TOTAL_COUNT;
                        model.INSERT_ID = Data.INSERT_ID.Value;
                    }
                    return model;
                }
            }
            catch (Exception)
            {
                return new OnePoint4VM();
            }
        }
        public OnePoint4VM GetMaxINSERTID()
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    OnePoint4VM model = new OnePoint4VM();
                    var Data = db.SS_1_4_ANSWER.OrderByDescending(p => p.INSERT_ID).FirstOrDefault();


                    if (Data != null)
                    {
                        model.INSERT_ID = Data.INSERT_ID.Value;
                    }
                    return model;
                }
            }
            catch (Exception)
            {
                return new OnePoint4VM();
            }
        }

        public void SaveTotalCount(OnePoint4VM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {

                    if (data.ANS_ID > 0)
                    {
                        var model = db.SS_1_4_ANSWER.Where(x => x.ANS_ID == data.ANS_ID).FirstOrDefault();
                        if (model != null)
                        {
                            model.TOTAL_COUNT = data.TOTAL_COUNT;

                            db.SaveChanges();
                        }
                    }

                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public void EditOnePointSeven(List<OnePoint7QuestionVM> data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    foreach (var item in data)
                    {
                        //var type = FillSS1Point7DataModel(item);
                        var model = db.SS_1_7_ANSWER.Where(x => x.id == item.Id && x.INSERT_ID == item.INSERT_ID).FirstOrDefault();
                        if (model != null)
                        {
                            model.No_drain_nallas = Convert.ToInt32(item.No_drain_nallas);
                            model.No_water_bodies = Convert.ToInt32(item.No_Water_Bodies);
                            model.No_locations = Convert.ToInt32(item.No_locations);
                            model.No_outlets = Convert.ToInt32(item.No_outlets);
                            db.SaveChanges();
                        }
                    }

                }
            }
            catch (Exception e)
            {
                throw;
            }
        }



        #region Infotainment

        //public InfotainmentDetailsVW GetInfotainmentDetailsById(int ID)
        //{
        //    try
        //    {
        //        InfotainmentDetailsVW type = new InfotainmentDetailsVW();
        //        DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();

        //        //using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
        //        //{
        //        var Details = dbMain.GameDetails.Where(x => x.GameDetailsID == ID).FirstOrDefault();
        //            if (Details != null)
        //            {
        //                type = FillInfotainmentViewModel(Details);
        //                if (type.Image != null && type.Image != "")
        //                {
        //                    type.Image = type.Image.Trim(); //ThumbnaiUrlCMS + type.Image.Trim();
        //                }
        //                else
        //                {
        //                    type.Image = "/Images/default_not_upload.png";
        //                }
        //                return type;
        //            }
        //            else
        //            {
        //                type.GameMasterList = LoadGameList();
        //                type.SloganList = LoadSloganList();
        //                type.AnswerTypeList = LoadAnswerTypeList();
        //                type.Image = "/Images/add_image_square.png";
        //                return type;
        //            }
        //        //}
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        #endregion

        #region Sauchalay
        public SauchalayDetailsVM GetSauchalayDetails(int teamId)
        {
            try
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();

                string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.HouseQRCode + "/";
                SauchalayDetailsVM data = new SauchalayDetailsVM();

                var Details = db.SauchalayAddresses.Where(x => x.Id == teamId).FirstOrDefault();
                if (Details != null)
                {
                    data = FillSauchalayDetailsViewModel(Details);
                    if (data.Image != null && data.Image != "")
                    {
                        data.Image = data.Image.Trim(); //ThumbnaiUrlCMS + type.Image.Trim();
                    }
                    else
                    {
                        data.Image = "/Images/default_not_upload.png";
                    }
                    if (data.QrImage != null && data.QrImage != "")
                    {
                        data.QrImage = data.QrImage.Trim(); //ThumbnaiUrlCMS + type.Image.Trim();
                    }
                    else
                    {
                        data.QrImage = "/Images/default_not_upload.png";
                    }
                    return data;
                }
                else if (teamId == -2 && AppID != 3086 && AppID != 3099)
                {
                    var id = db.SauchalayAddresses.OrderByDescending(x => x.SauchalayID).Select(x => x.SauchalayID).FirstOrDefault();
                    if (id == null)
                    {
                        string appName = (appDetails.AppName).Split(' ').First();
                        string name = appName + '_' + 'S' + '_' + ("0" + 1);
                        data.SauchalayID = name;
                        data.Image = "/Images/add_image_square.png";
                        data.QrImage = "/Images/add_image_square.png";
                        data.Id = 0;
                    }
                    else
                    {
                        var sId = id.Split('_').Last();
                        string appName = (appDetails.AppName).Split(' ').First();
                        string name = Convert.ToInt32(sId) < 9 ? appName + '_' + 'S' + '_' + ("0" + (Convert.ToInt32(sId) + 1)) : appName + '_' + 'S' + '_' + ((Convert.ToInt32(sId)) + (1));
                        data.SauchalayID = name;
                        data.Id = 0;
                        data.Image = "/Images/add_image_square.png";
                        data.QrImage = "/Images/add_image_square.png";

                    }
                    return data;
                }


                else if (teamId == -2 && AppID == 3086)
                {
                    var id = db.SauchalayAddresses.OrderByDescending(x => x.SauchalayID).Select(x => x.SauchalayID).FirstOrDefault();
                    if (id == null)
                    {
                        string appName = (appDetails.AppName).Split(' ').First();
                        string name = "DMC" + '-' + ("0" + 1);
                        data.SauchalayID = name;
                        data.Image = "/Images/add_image_square.png";
                        data.QrImage = "/Images/add_image_square.png";
                        data.Id = 0;
                    }
                    else
                    {
                        var sId = id.Split('-').Last();
                        string appName = (appDetails.AppName).Split(' ').First();

                        string name = Convert.ToInt32(sId) < 9 ? "DMC" + '-' + ("0" + (Convert.ToInt32(sId) + 1)) : "DMC" + '-' + ((Convert.ToInt32(sId)) + (1));
                        data.SauchalayID = name;
                        data.Id = 0;
                        data.Image = "/Images/add_image_square.png";
                        data.QrImage = "/Images/add_image_square.png";

                    }
                    return data;
                }




                else if (teamId == -2 && AppID == 3099)
                {
                    var id = db.SauchalayAddresses.OrderByDescending(x => x.SauchalayID).Select(x => x.SauchalayID).FirstOrDefault();
                    if (id == null)
                    {
                        string appName = (appDetails.AppName).Split(' ').First();
                        string name = "802784PTB00" + ("0" + 1);
                        data.SauchalayID = name;
                        data.Image = "/Images/add_image_square.png";
                        data.QrImage = "/Images/add_image_square.png";
                        data.Id = 0;
                    }
                    else
                    {
                        var sId = id.Substring(id.Length - 2);
                        // var sId = id.Split('0').Last();
                        string appName = (appDetails.AppName).Split(' ').First();

                        string name = Convert.ToInt32(sId) < 9 ? "802784PTB00" + ("0" + (Convert.ToInt32(sId) + 1)) : "802784PTB00" + ((Convert.ToInt32(sId)) + (1));
                        data.SauchalayID = name;
                        data.Id = 0;
                        data.Image = "/Images/add_image_square.png";
                        data.QrImage = "/Images/add_image_square.png";

                    }
                    return data;
                }
                else
                {

                    if (AppID != 3086 && AppID != 3099)
                    {
                        var id = db.SauchalayAddresses.OrderByDescending(x => x.SauchalayID).Select(x => x.SauchalayID).FirstOrDefault();

                        if (id == null)
                        {
                            string appName = (appDetails.AppName).Split(' ').First();
                            string name = appName + '_' + 'S' + '_' + ("0" + 1);
                            data.SauchalayID = name;
                            data.Id = 0;
                        }
                        else
                        {
                            var sId = id.Split('_').Last();
                            string appName = (appDetails.AppName).Split(' ').First();
                            string name = Convert.ToInt32(sId) < 9 ? appName + '_' + 'S' + '_' + ("0" + (Convert.ToInt32(sId) + 1)) : appName + '_' + 'S' + '_' + ((Convert.ToInt32(sId)) + (1));
                            data.Id = Convert.ToInt32(sId);
                        }
                    }

                    if (AppID == 3086)
                    {
                        var id = db.SauchalayAddresses.OrderByDescending(x => x.SauchalayID).Select(x => x.SauchalayID).FirstOrDefault();

                        if (id == null)
                        {
                            string appName = (appDetails.AppName).Split(' ').First();
                            string name = "DMC" + '-' + ("0" + 1);
                            data.SauchalayID = name;
                            data.Id = 0;
                        }
                        else
                        {
                            var sId = id.Split('-').Last();
                            string appName = (appDetails.AppName).Split(' ').First();
                            string name = Convert.ToInt32(sId) < 9 ? "DMC" + '-' + ("0" + (Convert.ToInt32(sId) + 1)) : "DMC" + '-' + ((Convert.ToInt32(sId)) + (1));
                            data.SauchalayID = name;
                            data.Id = Convert.ToInt32(sId);
                        }
                    }

                    if (AppID == 3099)
                    {
                        var id = db.SauchalayAddresses.OrderByDescending(x => x.SauchalayID).Select(x => x.SauchalayID).FirstOrDefault();

                        if (id == null)
                        {
                            string appName = (appDetails.AppName).Split(' ').First();
                            string name = "802784PTB00" + ("0" + 1);
                            data.SauchalayID = name;
                            data.Id = 0;
                        }
                        else
                        {
                            var sId = id.Substring(id.Length - 2);
                            //   var sId = id.Split('0').Last();
                            string appName = (appDetails.AppName).Split(' ').First();
                            string name = Convert.ToInt32(sId) < 9 ? "802784PTB00" + ("0" + (Convert.ToInt32(sId) + 1)) : "802784PTB00" + ((Convert.ToInt32(sId)) + (1));
                            data.SauchalayID = name;
                            data.Id = Convert.ToInt32(sId);
                        }
                    }
                    return data;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public SauchalayDetailsVM SaveSauchalayDetails(SauchalayDetailsVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.Id > 0)
                    {
                        var model = db.SauchalayAddresses.Where(x => x.Id == data.Id).FirstOrDefault();
                        if (model != null)
                        {
                            if (data.Image != null)
                            {
                                model.ImageUrl = data.Image;
                            }
                            if (data.QrImage != null)
                            {
                                model.QrImageUrl = data.QrImage;
                            }
                            model.SauchalayID = data.SauchalayID;
                            model.Name = data.Name;
                            model.Address = data.Address;
                            model.Lat = data.Lat;
                            model.Long = data.Long;
                            model.Mobile = data.Mobile;
                            model.CreatedDate = DateTime.Now;
                            //model.userId = data.userId;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var type = FillSauchalayDetailsDataModel(data);
                        db.SauchalayAddresses.Add(type);
                        db.SaveChanges();
                    }
                }

                var id = db.SauchalayAddresses.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                SauchalayDetailsVM vv = GetSauchalayDetails(id);
                return vv;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SauchalayDetailsVM> GetCTPTLocation()
        {
            List<SauchalayDetailsVM> ctptLocation = new List<SauchalayDetailsVM>();
            var data = db.SauchalayAddresses.Where(c => c.Lat != null && c.Long != null).ToList();
            foreach (var x in data)
            {
                ctptLocation.Add(new SauchalayDetailsVM()
                {
                    SauchalayID = x.SauchalayID,
                    Address = x.Address,
                    Lat = x.Lat,
                    Long = x.Long,
                    Name = x.Name,
                    Image = (string.IsNullOrEmpty(x.ImageUrl) ? "" : x.ImageUrl),
                    QrImage = (string.IsNullOrEmpty(x.QrImageUrl) ? "" : x.QrImageUrl),
                    Mobile = x.Mobile
                });
            }

            return ctptLocation;
        }

        #endregion

        #region WasteManagement

        public List<SelectListItem> WasteCategoryList()
        {
            var Category = new List<SelectListItem>();
            SelectListItem itemAdd = new SelectListItem() { Text = "--Select Category--", Value = "0" };

            try
            {
                Category = db.WM_GarbageCategory.ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.Category,
                        Value = x.CategoryID.ToString()
                    }).OrderBy(t => t.Text).ToList();

                Category.Insert(0, itemAdd);
            }
            catch (Exception ex) { throw ex; }

            return Category;
        }
        public List<SelectListItem> WasteSubCategoryList()
        {
            var SubCategory = new List<SelectListItem>();
            SelectListItem itemAdd = new SelectListItem() { Text = "Select Sub-Category", Value = "0" };

            try
            {
                //WardNo = db.WardNumbers.ToList()
                //    .Select(x => new SelectListItem
                //    {
                //        Text = x.WardNo + " (" + db.ZoneMasters.Where(c => c.zoneId == x.zoneId).FirstOrDefault().name + ")",
                //        Value = x.Id.ToString()
                //    }).OrderBy(t => t.Text).ToList();

                SubCategory = db.WM_GarbageSubCategory.ToList()
                   .Select(x => new SelectListItem
                   {
                       Text = x.SubCategory + " (" + db.WM_GarbageCategory.Where(c => c.CategoryID == x.CategoryID).FirstOrDefault().Category + ")",
                       Value = x.SubCategoryID.ToString()
                   }).OrderBy(t => t.Text).ToList();

                SubCategory.Insert(0, itemAdd);
            }
            catch (Exception ex) { throw ex; }

            return SubCategory;
        }
        public List<SelectListItem> LoadListSubCategory(Int32 categoryId)
        {
            var SubCategory = new List<SelectListItem>();
            try
            {
                SelectListItem itemAdd = new SelectListItem() { Text = "--Select Sub-Category--", Value = "0" };
                SubCategory = db.WM_GarbageSubCategory.Where(c => c.CategoryID == categoryId).ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.SubCategory,
                        Value = x.SubCategoryID.ToString()
                    }).OrderBy(t => t.Text).ToList();
                SubCategory.Insert(0, itemAdd);
            }
            catch (Exception ex) { throw ex; }
            return SubCategory;
        }

        public List<SelectListItem> GetWMUserDetails()
        {
            var Category = new List<SelectListItem>();
            //var Category1 = new SelectList();
            SelectListItem itemAdd = new SelectListItem() { Text = "--Select Category--", Value = "0" };
            //Category.Add(new SelectListItem() { Text = "--Select Category--", Value = "0" });
            //Category.Add(new SelectListItem() { Text = "--Select Category1--", Value = "1" });
            try
            {

                Category = db.UserMasters.Where(c => c.Type == "2").ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.userName,
                        Value = x.userId.ToString()
                    })
                    .OrderBy(t => t.Text).ToList();
                //Category.Add(new SelectListItem() { Text = "--Select Category--", Value = "0" });
                //Category.Add(new SelectListItem() { Text = "Admin", Value = "-3" });
                //Category.Add(new SelectListItem() { Text = "Employees", Value = "-2" });
                //Category1 = new SelectList(Category, "Value", "Text");
                // Category.Insert(0, itemAdd);

                // return Category1;
            }
            catch (Exception ex) { throw ex; }

            return Category;
        }
        public WasteDetailsVM GetWasteDetails(int teamId)
        {
            try
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();

                WasteDetailsVM waste = new WasteDetailsVM();

                if (teamId == -2)
                {
                    var WWWW = new List<SelectListItem>();
                    SelectListItem itemAdd = new SelectListItem() { Text = "Select Category", Value = "0" };
                    WWWW.Insert(0, itemAdd);

                    var ARRR = new List<SelectListItem>();
                    SelectListItem itemAddARR = new SelectListItem() { Text = "Select Sub-Category", Value = "0" };
                    ARRR.Insert(0, itemAddARR);

                    waste.WasteCategoryList = WWWW;
                    waste.WasteSubCategoryList = ARRR;

                    return waste;
                }

                else
                {
                    waste.WasteCategoryList = WasteCategoryList();
                    waste.WasteSubCategoryList = WasteSubCategoryList();
                    return waste;
                }


            }
            catch (Exception)
            {
                throw;
            }
        }
        //public List<WasteDetailsVM> SaveWasteDetails(List<WasteDetailsVM> data)
        //{
        //    try
        //    {
        //        DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
        //        var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();

        //        WasteDetailsVM waste = new WasteDetailsVM();
        //        List<WasteDetailsVM> details = new List<WasteDetailsVM>();

        //        string appId = (appDetails.AppId).ToString();
        //        foreach (var x in data)
        //        {
        //            //bool IsChecked = (bool)((dat))
        //            x.ID = data.IndexOf(x);
        //            x.SubCategoryID = x.SubCategoryID;
        //            x.Weight = x.Weight;
        //            x.UserID = x.UserID;

        //            details.Add(new WasteDetailsVM()
        //            {
        //                ID = x.ID,
        //                SubCategoryID = x.SubCategoryID,
        //                Weight = x.Weight,
        //                UserID = x.UserID
        //        });
        //        }
        //        try
        //        {
        //            WebClient client = new WebClient();
        //            client.Headers["Content-type"] = "application/json";
        //            client.Headers.Add("AppId", appId);
        //            string data1 = JsonConvert.SerializeObject(details);
        //            string json = client.UploadString(appDetails.baseImageUrl + "api/Save/GarbageDetails", data1);
        //            List<WasteDetailsVM> obj2 = JsonConvert.DeserializeObject<List<WasteDetailsVM>>(json).ToList();

        //        }
        //        catch (WebException ex)
        //        {
        //            var statusCode = ((HttpWebResponse)ex.Response).StatusCode;
        //            return "An error occurred, status code: " + statusCode;
        //            // failed...
        //            using (StreamReader r = new StreamReader(
        //                ex.Response.GetResponseStream()))
        //            {
        //                string responseContent = r.ReadToEnd();
        //                // ... do whatever ...
        //            }
        //        }
        //        //var statusCode = ((HttpWebResponse)ex.Response).StatusCode;


        //        return details;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public string SaveWasteDetails(string data)
        {
            try
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();

                WasteDetailsVM waste = new WasteDetailsVM();
                List<WasteDetailsVM> details = new List<WasteDetailsVM>();
                var abcd = JsonConvert.DeserializeObject<List<WasteDetailsVM>>(data).ToList();
                string appId = (appDetails.AppId).ToString();
                foreach (var x in abcd)
                {
                    //bool IsChecked = (bool)((dat))
                    x.ID = abcd.IndexOf(x);
                    x.SubCategoryID = x.SubCategoryID;
                    x.Weight = x.Weight;
                    x.UnitID = x.UnitID;
                    x.Source = 1;

                    details.Add(new WasteDetailsVM()
                    {
                        ID = x.ID,
                        SubCategoryID = x.SubCategoryID,
                        Weight = x.Weight,
                        UnitID = x.UnitID,
                        Source = x.Source

                    });
                }
                try
                {
                    string URI = appDetails.baseImageUrl + "/api/Save/GarbageDetails";
                    WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Headers.Add("AppId", appId);
                    string data1 = JsonConvert.SerializeObject(details);
                    string json = client.UploadString(URI, data1);
                    List<WasteDetailsVM> obj2 = JsonConvert.DeserializeObject<List<WasteDetailsVM>>(json).ToList();

                    return "Success";
                }
                catch (WebException ex)
                {
                    var statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                    return "An error occurred, status code: " + statusCode;

                    // failed...
                    //using (StreamReader r = new StreamReader(
                    //    ex.Response.GetResponseStream()))
                    //{
                    //    string responseContent = r.ReadToEnd();
                    //    // ... do whatever ...
                    //}
                }
                //var statusCode = ((HttpWebResponse)ex.Response).StatusCode;



            }
            catch (Exception)
            {
                throw;
            }
        }

        public string SaveSalesWasteDetails(string data)
        {
            try
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();

                WasteDetailsVM waste = new WasteDetailsVM();
                List<WasteDetailsVM> details = new List<WasteDetailsVM>();
                var abcd = JsonConvert.DeserializeObject<List<WasteDetailsVM>>(data).ToList();
                string appId = (appDetails.AppId).ToString();
                foreach (var x in abcd)
                {
                    //bool IsChecked = (bool)((dat))
                    x.ID = abcd.IndexOf(x);
                    x.SubCategoryID = x.SubCategoryID;
                    x.PartyName = x.PartyName;
                    x.SalesWeight = x.SalesWeight;
                    x.Amount = x.Amount;
                    x.UnitID = x.UnitID;
                    x.Source = 1;

                    details.Add(new WasteDetailsVM()
                    {
                        ID = x.ID,
                        SubCategoryID = x.SubCategoryID,
                        PartyName = x.PartyName,
                        SalesWeight = x.SalesWeight,
                        Amount = x.Amount,
                        UnitID = x.UnitID,
                        Source = x.Source

                    });
                }
                try
                {
                    string URI = appDetails.baseImageUrl + "api/Save/GarbageSales";
                    WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Headers.Add("AppId", appId);
                    string data1 = JsonConvert.SerializeObject(details);
                    string json = client.UploadString(URI, data1);
                    List<WasteDetailsVM> obj2 = JsonConvert.DeserializeObject<List<WasteDetailsVM>>(json).ToList();

                    return "Success";
                }
                catch (WebException ex)
                {
                    var statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                    return "An error occurred, status code: " + statusCode;

                    // failed...
                    //using (StreamReader r = new StreamReader(
                    //    ex.Response.GetResponseStream()))
                    //{
                    //    string responseContent = r.ReadToEnd();
                    //    // ... do whatever ...
                    //}
                }
                //var statusCode = ((HttpWebResponse)ex.Response).StatusCode;



            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        public List<LogVM> GetLogString()
        {
            List<LogVM> _LogVM = new List<LogVM>();

            var data = db.SP_GetLiveTracking().ToList();

            foreach (var x in data)
            {
                _LogVM.Add(new LogVM()
                {
                    Logstring = x.gcId + "   |   " + x.ReferanceId.ToString() + "   |   " + x.Name + "   |   " + x.userName + "   |   " + x.gcDate,
                    gcId = x.gcId,
                });
            }

            return _LogVM;
        }

        public List<SBAEmplyeeIdelGrid> GetIdelTimeNotification()
        {
            List<SBAEmplyeeIdelGrid> obj = new List<SBAEmplyeeIdelGrid>();
            // DateTime? fdate = null
            string dt = DateTime.Now.ToString("MM/dd/yyyy");
            DateTime fdate = Convert.ToDateTime(dt + " " + "00:00:00");
            DateTime tdate = Convert.ToDateTime(dt + " " + "23:59:59");

            var data = db.SP_IdelTime(0, fdate, tdate).Where(c => c.IdelTime != null & c.IdelTime > 15).ToList().OrderByDescending(c => c.StartTime);
            //var data = db.SP_IdelTime(0, fdate, fdate).Where(c => c.IdelTime != null & c.IdelTime > 15).ToList();
            foreach (var x in data)
            {
                TimeSpan spWorkMin = TimeSpan.FromMinutes(Convert.ToDouble(x.IdelTime));
                string workHours = spWorkMin.ToString(@"hh\:mm");

                string displayTime = Convert.ToDateTime(x.date).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                string time = Convert.ToDateTime(x.StartTime).ToString("HH:mm:ss");

                obj.Add(new SBAEmplyeeIdelGrid()
                {
                    UserName = x.userName,
                    Date = Convert.ToDateTime(x.date).ToString("dd/MM/yyyy"),
                    StartTime = x.StartTime,
                    EndTime = x.LastTime,
                    StartAddress = checkNull(x.StartAddress).Replace("Unnamed Road, ", ""),
                    EndAddress = checkNull(x.address).Replace("Unnamed Road, ", ""),
                    IdelTime = workHours,
                    userId = x.userId,
                    startLat = x.StarLat,
                    startLong = x.StartLog,
                    EndLat = x.lat,
                    EndLong = x.@long,
                    daDateTIme = (displayTime + " " + time)

                });
            }
            return obj;
        }

        public List<SBAEmplyeeIdelGrid> GetLiquidIdelTimeNotification()
        {
            List<SBAEmplyeeIdelGrid> obj = new List<SBAEmplyeeIdelGrid>();
            // DateTime? fdate = null
            string dt = DateTime.Now.ToString("MM/dd/yyyy");
            DateTime fdate = Convert.ToDateTime(dt + " " + "00:00:00");
            DateTime tdate = Convert.ToDateTime(dt + " " + "23:59:59");

            var data = db.SP_IdelTimeLiquid(0, fdate, tdate).Where(c => c.IdelTime != null & c.IdelTime > 15).ToList().OrderByDescending(c => c.StartTime);
            //var data = db.SP_IdelTime(0, fdate, fdate).Where(c => c.IdelTime != null & c.IdelTime > 15).ToList();
            foreach (var x in data)
            {
                TimeSpan spWorkMin = TimeSpan.FromMinutes(Convert.ToDouble(x.IdelTime));
                string workHours = spWorkMin.ToString(@"hh\:mm");

                string displayTime = Convert.ToDateTime(x.date).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                string time = Convert.ToDateTime(x.StartTime).ToString("HH:mm:ss");

                obj.Add(new SBAEmplyeeIdelGrid()
                {
                    UserName = x.userName,
                    Date = Convert.ToDateTime(x.date).ToString("dd/MM/yyyy"),
                    StartTime = x.StartTime,
                    EndTime = x.LastTime,
                    StartAddress = checkNull(x.StartAddress).Replace("Unnamed Road, ", ""),
                    EndAddress = checkNull(x.address).Replace("Unnamed Road, ", ""),
                    IdelTime = workHours,
                    userId = x.userId,
                    startLat = x.StarLat,
                    startLong = x.StartLog,
                    EndLat = x.lat,
                    EndLong = x.@long,
                    daDateTIme = (displayTime + " " + time)

                });
            }
            return obj;
        }

        public List<SBAEmplyeeIdelGrid> GetStreetIdelTimeNotification()
        {
            List<SBAEmplyeeIdelGrid> obj = new List<SBAEmplyeeIdelGrid>();
            // DateTime? fdate = null
            string dt = DateTime.Now.ToString("MM/dd/yyyy");
            DateTime fdate = Convert.ToDateTime(dt + " " + "00:00:00");
            DateTime tdate = Convert.ToDateTime(dt + " " + "23:59:59");

            var data = db.SP_IdelTimestreet(0, fdate, tdate).Where(c => c.IdelTime != null & c.IdelTime > 15).ToList().OrderByDescending(c => c.StartTime);
            //var data = db.SP_IdelTime(0, fdate, fdate).Where(c => c.IdelTime != null & c.IdelTime > 15).ToList();
            foreach (var x in data)
            {
                TimeSpan spWorkMin = TimeSpan.FromMinutes(Convert.ToDouble(x.IdelTime));
                string workHours = spWorkMin.ToString(@"hh\:mm");

                string displayTime = Convert.ToDateTime(x.date).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                string time = Convert.ToDateTime(x.StartTime).ToString("HH:mm:ss");

                obj.Add(new SBAEmplyeeIdelGrid()
                {
                    UserName = x.userName,
                    Date = Convert.ToDateTime(x.date).ToString("dd/MM/yyyy"),
                    StartTime = x.StartTime,
                    EndTime = x.LastTime,
                    StartAddress = checkNull(x.StartAddress).Replace("Unnamed Road, ", ""),
                    EndAddress = checkNull(x.address).Replace("Unnamed Road, ", ""),
                    IdelTime = workHours,
                    userId = x.userId,
                    startLat = x.StarLat,
                    startLong = x.StartLog,
                    EndLat = x.lat,
                    EndLong = x.@long,
                    daDateTIme = (displayTime + " " + time)

                });
            }
            return obj;
        }

        public List<SBALUserLocationMapView> GetUserTimeWiseRoute(string adate = "", DateTime? fTime = null, DateTime? tTime = null, int? userId = null)
        {

            // SBALUserLocationMapView userLocation = new SBALUserLocationMapView();
            // date = "03-09-2020";
            // //fTime = "10:00 AM";
            //// tTime = "10:30 AM";

            //DateTime a = Convert.ToDateTime(fTime);s
            //DateTime b = Convert.ToDateTime(tTime);
            //int result = DateTime.Compare(a, b);

            DateTime dateTime = new DateTime();
            // dateTime = Convert.ToDateTime(DateTime.ParseExact("date", "MM/dd/yyyy", CultureInfo.InvariantCulture));

            var dateAndTime = DateTime.Now;
            var date = dateAndTime.Date;

            DateTime firstdate = DateTime.ParseExact(adate,
                                         "dd/MM/yyyy",
                                         CultureInfo.InvariantCulture);

            var firstDateString = firstdate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);


            // string cc = Convert.ToDateTime(adate).ToString("MM/dd/yyyy");
            //string dt1 = Convert.ToDateTime(firstDateString).ToString("MM/dd/yyyy");
            //string displayTime = Convert.ToDateTime(firstDateString).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            // string dt = Convert.ToDateTime(date).ToString("MM/dd/yyyy");
            string ft = Convert.ToDateTime(fTime).ToString("HH:mm:ss");
            string tt = Convert.ToDateTime(tTime).ToString("HH:mm:ss");
            DateTime fdate = Convert.ToDateTime(firstDateString + " " + ft);
            DateTime tdate = Convert.ToDateTime(firstDateString + " " + tt);

            var data = db.Locations.Where(c => c.userId == userId & c.datetime >= fdate & c.datetime <= tdate & c.type == null).ToList();

            List<SBALUserLocationMapView> userLocation = new List<SBALUserLocationMapView>();
            //DateTime newdate = DateTime.Now.Date;
            //var datt = newdate;
            //var att = db.Daily_Attendance.Where(c => c.daID == userId).FirstOrDefault();
            //string Time = att.startTime;
            //DateTime date = DateTime.Parse(Time, System.Globalization.CultureInfo.CurrentCulture);
            //string t = date.ToString("hh:mm:ss tt");
            //string dt = Convert.ToDateTime(att.daDate).ToString("MM/dd/yyyy");
            //DateTime? fdate = Convert.ToDateTime(dt + " " + t);
            //DateTime? edate;
            //if (att.endTime == "" | att.endTime == null)
            //{
            //    edate = DateTime.Now;
            //}
            //else {
            //    string Time2 = att.endTime;
            //    DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
            //    string t2 = date2.ToString("hh:mm:ss tt");
            //    string dt2 = Convert.ToDateTime(att.daEndDate).ToString("MM/dd/yyyy");
            //    edate = Convert.ToDateTime(dt2 + " " + t2);
            //}
            //var data = db.Locations.Where(c => c.userId == att.userId & c.datetime >= fdate & c.datetime <= edate & c.type == null).ToList();


            foreach (var x in data)
            {

                string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");
                string tim = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");
                var userName = db.UserMasters.Where(c => c.userId == userId).FirstOrDefault();

                userLocation.Add(new SBALUserLocationMapView()
                {
                    userName = userName.userName,
                    date = dat,
                    time = tim,
                    lat = x.lat,
                    log = x.@long,
                    address = checkNull(x.address).Replace("Unnamed Road, ", ""),
                    // vehcileNumber = att.vehicleNumber,
                    userMobile = userName.userMobileNumber,
                    // type = Convert.ToInt32(x.type),

                });

            }


            return userLocation;
        }

        public List<SBALUserLocationMapView> GetHouseTimeWiseRoute(string adate = "", DateTime? fTime = null, DateTime? tTime = null, int? userId = null)
        {
            DateTime dateTime = new DateTime();
            var dateAndTime = DateTime.Now;
            var date = dateAndTime.Date;

            DateTime firstdate = DateTime.ParseExact(adate,
                                         "dd/MM/yyyy",
                                         CultureInfo.InvariantCulture);

            var firstDateString = firstdate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

            string ft = Convert.ToDateTime(fTime).ToString("HH:mm:ss");
            string tt = Convert.ToDateTime(tTime).ToString("HH:mm:ss");
            DateTime fdate = Convert.ToDateTime(firstDateString + " " + ft);
            DateTime tdate = Convert.ToDateTime(firstDateString + " " + tt);

            //var data = db.Locations.Where(c => c.userId == userId & c.datetime >= fdate & c.datetime <= tdate & c.type == 1).ToList();
            var data = db.GarbageCollectionDetails.Where(c => c.userId == userId & c.gcDate >= fdate & c.gcDate <= tdate).ToList();
            List<SBALUserLocationMapView> userLocation = new List<SBALUserLocationMapView>();
            foreach (var x in data)
            {
                string dat = Convert.ToDateTime(x.gcDate).ToString("dd/MM/yyyy");
                string tim = Convert.ToDateTime(x.gcDate).ToString("hh:mm tt");
                var userName = db.UserMasters.Where(c => c.userId == userId).FirstOrDefault();
                var house = db.HouseMasters.Where(h => h.houseId == x.houseId).FirstOrDefault();
                var d = db.GarbageCollectionDetails.Where(a => a.houseId == house.houseId).FirstOrDefault();
                userLocation.Add(new SBALUserLocationMapView()
                {
                    //userName = userName.userName,
                    //date = dat,
                    //time = tim,
                    //lat = x.lat,
                    //log = x.@long,
                    //address = checkNull(x.address).Replace("Unnamed Road, ", ""),
                    //userMobile = userName.userMobileNumber,
                    //// type = Convert.ToInt32(x.type),
                    ///
                    userId = userName.userId,
                    userName = userName.userName,
                    date = dat,
                    time = tim,
                    lat = d.Lat,
                    log = d.Long,
                    address = house.houseAddress,
                    //vehcileNumber = att.vehicleNumber,
                    userMobile = userName.userMobileNumber,
                    type = 1,
                    HouseId = house.ReferanceId,
                    HouseAddress = (house.houseAddress == null ? "" : house.houseAddress.Replace("Unnamed Road, ", "")),
                    HouseOwnerName = house.houseOwner,
                    OwnerMobileNo = house.houseOwnerMobile,
                    WasteType = d.garbageType.ToString(),
                    gpBeforImage = d.gpBeforImage,
                    gpAfterImage = d.gpAfterImage,
                    ZoneList = ListZone(),

                });

            }


            return userLocation;
        }

        public DashBoardVM GetLiquidDashBoardDetails()
        {
            DashBoardVM model = new DashBoardVM();
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {

                    DevSwachhBharatMainEntities dbm = new DevSwachhBharatMainEntities();
                    var appdetails = dbm.AppDetails.Where(c => c.AppId == AppID).FirstOrDefault();
                    List<ComplaintVM> obj = new List<ComplaintVM>();
                    //if (AppID==1)
                    //{
                    //    string json = new WebClient().DownloadString(appdetails.Grampanchayat_Pro + "/api/Get/Complaint?appId=1");
                    //     obj = JsonConvert.DeserializeObject<List<ComplaintVM>>(json).Where(c => Convert.ToDateTime(c.createdDate2).ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")).ToList();
                    //}
                    //if (appdetails.GramPanchyatAppID != null)
                    //{
                    //    string json = new WebClient().DownloadString(appdetails.Grampanchayat_Pro + "/api/Get/Complaint?appId=" + appdetails.GramPanchyatAppID);
                    //    obj = JsonConvert.DeserializeObject<List<ComplaintVM>>(json).Where(c => Convert.ToDateTime(c.createdDate2).ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")).ToList();
                    //}
                    var data = db.SP_LiquidDashboard_Details().First();

                    var date = DateTime.Today;
                    var houseCount = db.SP_TotalHouseCollection_Count(date, AppID).FirstOrDefault();
                    if (data != null)
                    {

                        model.TodayAttandence = data.TodayAttandence;
                        model.TotalAttandence = data.TotalAttandence;
                        model.HouseCollection = data.TotalHouse;
                        model.LiquidCollection = data.TotalLiquid;
                        model.TotalComplaint = obj.Count();
                        model.TotalHouseCount = houseCount.TotalHouseCount;
                        model.MixedCount = houseCount.MixedCount;
                        model.BifurgatedCount = houseCount.BifurgatedCount;
                        model.NotCollected = houseCount.NotCollected;
                        model.NotSpecified = houseCount.NotSpecified;
                        //model.TotalGcWeightCount = houseCount.TotalGcWeightCount;
                        //model.GcWeightCount = Convert.ToDouble(string.Format("{0:0.00}", houseCount.GcWeightCount));
                        //model.DryWeightCount =Convert.ToDouble(string.Format("{0:0.00}", houseCount.DryWeightCount));
                        //model.WetWeightCount =Convert.ToDouble(string.Format("{0:0.00}", houseCount.WetWeightCount));
                        model.GcWeightCount = Convert.ToDouble(houseCount.GcWeightCount);
                        model.DryWeightCount = Convert.ToDouble(houseCount.DryWeightCount);
                        model.WetWeightCount = Convert.ToDouble(houseCount.WetWeightCount);
                        model.TotalGcWeightCount = Convert.ToDouble(houseCount.TotalGcWeightCount);
                        model.TotalDryWeightCount = Convert.ToDouble(houseCount.TotalDryWeightCount);
                        model.TotalWetWeightCount = Convert.ToDouble(houseCount.TotalWetWeightCount);
                        model.DumpYardCount = data.TotalDump;
                        model.TotalLiquidCount = houseCount.TotalLiquidCount;
                        model.TotalStreetCount = houseCount.TotalStreetCount;
                        model.TotalLiquidPropertyCount = houseCount.TotalLiquidPropertyCount;
                        model.TotalStreetPropertyCount = houseCount.TotalStreetPropertyCount;

                        //model.LWGcWeightCount = 4;
                        //model.LWDryWeightCount = 1;
                        //model.LWWetWeightCount = 3;
                        model.LWGcWeightCount = Convert.ToDouble(houseCount.LWGcWeightCount);
                        model.LWDryWeightCount = Convert.ToDouble(houseCount.LWDryWeightCount);
                        model.LWWetWeightCount = Convert.ToDouble(houseCount.LWWetWeightCount);

                        return model;
                    }

                    // String.Format("{0:0.00}", 123.4567); 

                    else
                    {
                        return model;
                    }
                }
            }
            catch (Exception ex)
            {
                return model;
            }
        }

        public DashBoardVM GetStreetDashBoardDetails()
        {
            DashBoardVM model = new DashBoardVM();
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {

                    DevSwachhBharatMainEntities dbm = new DevSwachhBharatMainEntities();
                    var appdetails = dbm.AppDetails.Where(c => c.AppId == AppID).FirstOrDefault();
                    List<ComplaintVM> obj = new List<ComplaintVM>();
                    var data = db.SP_StreetDashboard_Details().First();

                    var date = DateTime.Today;
                    var houseCount = db.SP_TotalHouseCollection_Count(date, AppID).FirstOrDefault();
                    if (data != null)
                    {

                        model.TodayAttandence = data.TodayAttandence;
                        model.TotalAttandence = data.TotalAttandence;
                        model.HouseCollection = data.TotalHouse;
                        model.StreetCollection = data.TotalStreet;
                        model.TotalComplaint = obj.Count();
                        model.TotalHouseCount = houseCount.TotalHouseCount;
                        model.MixedCount = houseCount.MixedCount;
                        model.BifurgatedCount = houseCount.BifurgatedCount;
                        model.NotCollected = houseCount.NotCollected;
                        model.NotSpecified = houseCount.NotSpecified;
                        model.GcWeightCount = Convert.ToDouble(houseCount.GcWeightCount);
                        model.DryWeightCount = Convert.ToDouble(houseCount.DryWeightCount);
                        model.WetWeightCount = Convert.ToDouble(houseCount.WetWeightCount);
                        model.TotalGcWeightCount = Convert.ToDouble(houseCount.TotalGcWeightCount);
                        model.TotalDryWeightCount = Convert.ToDouble(houseCount.TotalDryWeightCount);
                        model.TotalWetWeightCount = Convert.ToDouble(houseCount.TotalWetWeightCount);
                        model.DumpYardCount = data.TotalDump;
                        model.TotalLiquidCount = houseCount.TotalLiquidCount;
                        model.TotalStreetCount = houseCount.TotalStreetCount;
                        model.TotalStreetPropertyCount = houseCount.TotalStreetPropertyCount;
                        model.TotalLiquidPropertyCount = houseCount.TotalLiquidPropertyCount;
                        //model.TotalStreetCount = 1;
                        //model.TotalStreetPropertyCount = 50;
                        //model.SSGcWeightCount = 4;
                        //model.SSDryWeightCount = 1;
                        //model.SSWetWeightCount = 3;
                        model.SSGcWeightCount = Convert.ToDouble(houseCount.SSGcWeightCount);
                        model.SSDryWeightCount = Convert.ToDouble(houseCount.SSDryWeightCount);
                        model.SSWetWeightCount = Convert.ToDouble(houseCount.SSWetWeightCount);




                        return model;
                    }

                    // String.Format("{0:0.00}", 123.4567); 

                    else
                    {
                        return model;
                    }
                }
            }
            catch (Exception)
            {
                return model;
            }
        }

        public string GetLoginidData(string LoginId)
        {

            using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
            {
                var isrecord = db.QrEmployeeMasters.Where(x => x.qrEmpLoginId == LoginId && x.isActive == true).FirstOrDefault();
                var isrecord1 = db.UserMasters.Where(x => x.userLoginId == LoginId && x.isActive == true).FirstOrDefault();
                if (isrecord == null && isrecord1 == null)
                {
                    return "0";
                }
                else
                {
                    return "1";
                }
            }



        }

        public string GetUserName(string userName)
        {

            using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
            {

                var isrecord1 = db.UserMasters.Where(x => x.userName == userName && x.isActive == true).FirstOrDefault();
                if (isrecord1 == null)
                {
                    return "0";
                }
                else
                {
                    return "1";
                }
            }

        }
        public string GetHSUserName(string userName)
        {

            using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
            {

                var isrecord1 = db.QrEmployeeMasters.Where(x => x.qrEmpName == userName && x.isActive == true).FirstOrDefault();
                if (isrecord1 == null)
                {
                    return "0";
                }
                else
                {
                    return "1";
                }
            }

        }
        public List<string> CheckShiftName()
        {
            List<string> lstShifts = new List<string>();
            using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
            {

                lstShifts = db.EmpShifts.Select(x => x.shiftName).ToList();

            }
            return lstShifts;
        }
        public void SaveHouseBunchDetails(HouseQRBunchVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.bunchId > 0)
                    {
                        var model = db.HouseBunches.Where(x => x.bunchId == data.bunchId).FirstOrDefault();
                        if (model != null)
                        {
                            model.bunchId = data.bunchId;
                            model.bunchname = data.bunchName;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var housebunch = FillHouseBunchDataModel(data);
                        if (housebunch.bunchname != null)
                        {
                            db.HouseBunches.Add(housebunch);
                            db.SaveChanges();
                        }

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public MasterQRDetailsVM GetMasterQRBunchDetails(int teamId)
        {
            try
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();

                var db = new DevChildSwachhBharatNagpurEntities(AppID);
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();


                string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.UserProfile + "/";

                MasterQRDetailsVM master = new MasterQRDetailsVM();


                var Details = db.MasterQRBunches.Where(x => x.MasterId == teamId).FirstOrDefault();
                //if (houseId > 0)
                //{
                //    Details = db.MasterQRBunches.Where(x => x.MasterId == teamId & x.HouseBunchId == houseId).FirstOrDefault();

                //}
                if (Details != null)
                {
                    master = FillMasterQRBunchDetailsViewModel(Details);

                    var ReQRList = db.Remaining_QRList(teamId).FirstOrDefault();

                    string r = ReQRList.ToString();
                    string[] values_new = Array.ConvertAll(r.Split(','), p => p.Trim());

                    var excluding = values_new;

                    // master.CheckHlist = db.HouseLists.Where(x => x.IsActive == true).OrderBy(x => x.ReferanceId).ToList<HouseList>();
                    master.CheckHlist = db.HouseLists.Where(x => x.IsActive == true && !excluding.Contains(x.ReferanceId)).OrderBy(x => x.ReferanceId).ToList<HouseList>();



                    // master.CheckAppDs = (List<HouseMaster>)db.HouseMasters.Where(x => x.ReferanceId != null).Select(x => new { x.ReferanceId, x.houseId });

                    if (master.QRList != null)
                    {
                        string s = master.QRList;
                        string[] values = s.Split(',');

                        var including = values;

                        master.SelectedHouseList = db.HouseLists.Where(x => including.Contains(x.ReferanceId)).ToList<HouseList>();



                        for (int i = 0; i < values.Length; i++)
                        {
                            char[] MyChar = { 'H', 'P', 'S', 'B', 'A' };
                            values[i] = values[i].Trim(MyChar);


                            int u = 0;
                            if (values[i] != "")
                            {
                                u = Convert.ToInt32(values[i]);
                                int number = 1000;
                                u = u - number;
                            }
                            string state1 = "";
                            foreach (var v in master.CheckHlist)
                            {
                                if (v.HouseId == u)
                                {

                                    v.IsCheked = true;



                                }
                            }
                        }

                    }
                    master.BunchList = ListBunch(teamId);
                    return master;
                }
                else
                {

                    var areaid = db.HouseLists.FirstOrDefault();
                    if (areaid == null)
                    {
                        var AreaId = 0;
                        var including = "";

                        var ReQRList = db.Remaining_QRList(0).FirstOrDefault();

                        string r = ReQRList.ToString();
                        string[] values_new = Array.ConvertAll(r.Split(','), p => p.Trim());

                        var excluding = values_new;

                        //master.CheckHlist = db.HouseLists.Where(x => x.IsActive == true & x.AreaId > 0).OrderBy(x => x.HouseId).ToList<HouseList>();
                        master.CheckHlist = db.HouseLists.Where(x => x.IsActive == true && !excluding.Contains(x.ReferanceId)).OrderBy(x => x.ReferanceId).ToList<HouseList>();
                        master.SelectedHouseList = db.HouseLists.Where(x => including.Contains(x.ReferanceId)).ToList<HouseList>();
                        // master.CheckAppDs = (List<HouseMaster>)db.HouseMasters.Where(x => x.ReferanceId != null).Select(x => new { x.ReferanceId, x.houseId });
                        master.BunchList = ListBunch(teamId);
                        return master;
                    }
                    else
                    {
                        var AreaId = areaid.AreaId;
                        var including = "";

                        var ReQRList = db.Remaining_QRList(0).FirstOrDefault();

                        string r = ReQRList.ToString();
                        //string[] values_new = r.Split(',');

                        string[] values_new = Array.ConvertAll(r.Split(','), p => p.Trim());

                        var excluding = values_new;
                        // master.CheckHlist = db.HouseLists.Where(x => x.IsActive == true & x.AreaId == AreaId).OrderBy(x => x.HouseId).ToList<HouseList>();
                        master.CheckHlist = db.HouseLists.Where(x => x.IsActive == true && !excluding.Contains(x.ReferanceId)).OrderBy(x => x.ReferanceId).ToList<HouseList>();
                        master.SelectedHouseList = db.HouseLists.Where(x => including.Contains(x.ReferanceId)).ToList<HouseList>();
                        // master.CheckAppDs = (List<HouseMaster>)db.HouseMasters.Where(x => x.ReferanceId != null).Select(x => new { x.ReferanceId, x.houseId });
                        master.BunchList = ListBunch(teamId);
                        return master;
                    }

                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void SaveMasterQRBunchDetails(MasterQRDetailsVM data)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    if (data.masterId > 0)
                    {
                        var model = db.MasterQRBunches.Where(x => x.MasterId == data.masterId).FirstOrDefault();
                        if (model != null)
                        {

                            model.MasterId = data.masterId;
                            model.HouseBunchId = data.HouseBunchId;
                            model.ISActive = data.isActive;
                            string state1 = "";
                            foreach (var s in data.CheckHlist)
                            {
                                if (s.IsCheked == true)
                                {

                                    state1 += s.ReferanceId + ",";

                                }
                            }
                            model.QRList = state1;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var type = FillMasterQRBunchDataModel(data);

                        //arr[CheckAppD] myArray = data.CheckAppDs.ToArray();

                        string state1 = "";
                        foreach (var s in data.CheckHlist)
                        {
                            if (s.IsCheked == true)
                            {

                                state1 += s.ReferanceId + ",";

                            }
                        }

                        type.QRList = state1;

                        db.MasterQRBunches.Add(type);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }


        }
        private MasterQRBunch FillMasterQRBunchDataModel(MasterQRDetailsVM data)
        {
            MasterQRBunch model = new MasterQRBunch();
            model.MasterId = data.masterId;
            model.HouseBunchId = data.HouseBunchId;
            model.ISActive = data.isActive;
            return model;
        }

        public List<SelectListItem> ListBunch(int teamId)
        {
            var House = new List<SelectListItem>();
            SelectListItem itemAdd = new SelectListItem() { Text = "--Select Bunch--", Value = "0" };

            try
            {

                if (teamId > 0)
                {

                    House = db.HouseBunches.ToList()
                       .Select(x => new SelectListItem
                       {
                           Text = x.bunchname,
                           Value = x.bunchId.ToString()
                       }).OrderBy(t => t.Text).ToList();

                    House.Insert(0, itemAdd);
                }
                else
                {
                    House = db.HouseBunches.Where(p => db.MasterQRBunches.All(x => x.HouseBunchId != p.bunchId)).ToList()
                       .Select(x => new SelectListItem
                       {
                           Text = x.bunchname,
                           Value = x.bunchId.ToString()
                       }).OrderBy(t => t.Text).ToList();

                    House.Insert(0, itemAdd);
                }

            }
            catch (Exception ex) { throw ex; }

            return House;
        }

        private MasterQRDetailsVM FillMasterQRBunchDetailsViewModel(MasterQRBunch data)
        {

            MasterQRDetailsVM model = new MasterQRDetailsVM();
            model.masterId = data.MasterId;
            model.HouseBunchId = data.HouseBunchId;
            model.QRList = data.QRList;
            model.isActive = data.ISActive;


            return model;
        }

        private HouseBunch FillHouseBunchDataModel(HouseQRBunchVM data)
        {

            HouseBunch model = new HouseBunch();

            var modelt = db.HouseBunches.Where(x => x.bunchname == data.bunchName).FirstOrDefault();

            if (modelt == null)
            {
                model.bunchId = data.bunchId;
                model.bunchname = data.bunchName;

                return model;
            }

            return model;
        }

        public HouseQRBunchVM GetHouseBunch(int teamId)
        {
            try
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                {
                    var Details = db.HouseBunches.Where(x => x.bunchId == teamId).FirstOrDefault();
                    if (Details != null)
                    {
                        HouseQRBunchVM housebunch = FillHouseBunchViewModel(Details);

                        return housebunch;
                    }
                    else
                    {
                        HouseQRBunchVM housebunch = new HouseQRBunchVM();
                        return housebunch;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private HouseQRBunchVM FillHouseBunchViewModel(HouseBunch data)
        {
            HouseQRBunchVM model = new HouseQRBunchVM();
            model.bunchId = data.bunchId;
            model.bunchName = data.bunchname;
            return model;
        }
    }
}
