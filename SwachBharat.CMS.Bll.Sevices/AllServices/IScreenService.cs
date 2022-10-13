using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachBharat.CMS.Dal.DataContexts;
using SwachBharat.CMS.Bll.ViewModels.Grid;
using SwachBharat.CMS.Bll.ViewModels.SS2020Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data;

namespace SwachBharat.CMS.Bll.Services
{
    public interface IScreenService
    {
        DashBoardVM GetDashBoardDetails();

        DashBoardVM GetLiquidDashBoardDetails();

        DashBoardVM GetStreetDashBoardDetails();
        string Address(string location);
        AreaVM GetAreaDetails(int teamId,string Name);
        void DeletAreaDetails(int teamId);
        void SaveAreaDetails(AreaVM area);
        void LiquidSaveAreaDetails(AreaVM area);

        void StreetSaveAreaDetails(AreaVM area);


        VehicleTypeVM GetVehicleTypeDetails(int teamId);

        VehicleRegVM GetVehicleDetails(int teamId);

        void SaveVehicleRegDetails(VehicleRegVM type);
        void DeletVehicleTypeDetails(int teamId);
        void SaveVehicleTypeDetails(VehicleTypeVM type);

         WardNumberVM GetWardNumberDetails(int teamId,string name);
         void SaveWardNumberDetails(WardNumberVM data);
         void SaveMasterQRDetails(MasterQRDetailsVM data);

        void LiquidSaveWardNumberDetails(WardNumberVM data);

        void StreetSaveWardNumberDetails(WardNumberVM data);

        void DeletWardNumberDetails(int teamId);

        HouseDetailsVM GetHouseDetails(int teamId);
        MasterQRDetailsVM GetMasterQRDetails(int teamId , string houseId);
        VehicalRegDetailsVM GetVehicalRegDetails(int teamId);
        SBALUserLocationMapView GetHouseByIdforMap(int teamId,int daId);
        SBALUserLocationMapView GetLiquidByIdforMap(int teamId, int daId,string EmpType);
        SBALUserLocationMapView GetDumpByIdforMap(int teamId, int daId,string EmpType);
        HouseDetailsVM SaveHouseDetails(HouseDetailsVM data);
        VehicalRegDetailsVM SaveVehicalRegDetails(VehicalRegDetailsVM data);

        void SaveEmpBeatMap(EmpBeatMapVM data);
        EmpBeatMapVM GetEmpBeatMap(int ebmId);
        List<SelectListItem> ListUserBeatMap(string Emptype);

        bool IsPointInPolygon(int ebmId, coordinates p);
         void DeletHouseDetails(int teamId);

        SBALUserLocationMapView GetLocationDetails(int teamId,string Emptype);
        List<SBALUserLocationMapView> GetAllUserLocation(string date,string Emptype);

        List<SBALUserLocationMapView> GetAdminLocation();
        List<SBALUserLocationMapView> GetUserWiseLocation(int userId,string date, string Emptype);
        List<SBALUserLocationMapView> GetUserAttenLocation(int userId);
        List<SBALUserLocationMapView> GetUserAttenRoute(int userId);

        //Added By Saurabh(11 July 2019)
        List<SBALUserLocationMapView> GetHouseAttenRoute(int userId,int areaid);
        List<SBALUserLocationMapView> GetDumpAttenRoute(int userId);

        List<SBALUserLocationMapView> GetLiquidAttenRoute(int userId, int areaid);
        List<SBALUserLocationMapView> GetStreetAttenRoute(int userId, int areaid);

        EmpBeatMapCountVM GetbeatMapCount(int daId,int areaid,int polyId);
        List<EmployeeHouseCollectionInnerOuter> getEmployeeHouseCollectionInnerOuter();
        HouseAttenRouteVM GetBeatHouseAttenRoute(int daId, int areaid, int polyId);
        GarbagePointDetailsVM GetGarbagePointDetails(int teamId);
        GarbagePointDetailsVM SaveGarbagePointDetails(GarbagePointDetailsVM data);
        void DeletGarbagePointDetails(int teamId);

        EmployeeDetailsVM GetEmployeeDetails(int teamId);
        EmpShiftVM GetEmpShiftById(int teamId);



        SBAAttendenceSettingsGridRow GetAttendenceEmployeeById(int teamId);
        void DeleteEmployeeDetails(int teamId);
        void SaveEmployeeDetails(EmployeeDetailsVM employee,string Emptype);
        void SaveEmpShift(EmpShiftVM empShift);
        void SaveAttendenceSettingsDetail(SBAAttendenceSettingsGridRow atten);
        ComplaintVM GetCompalint(int teamId);
        void SaveComplaintStatus(ComplaintVM employee);

        ZoneVM GetZone(int teamId);

        ZoneVM StreetGetZone(int teamId);
        void SaveZone(ZoneVM employee);

        void StreetSaveZone(ZoneVM employee);
        ZoneVM GetValidZone(string name,int zoneId);

        //Added By Saurabh

        DumpYardDetailsVM GetDumpYardtDetails(int teamId);
        //Added By Shubham
        StreetSweepVM GetStreetSweepDetails(int teamId);

        LiquidWasteVM GetLiquidWasteDetails(int teamId);
        DumpYardDetailsVM SaveDumpYardtDetails(DumpYardDetailsVM data,string Emptype);

        StreetSweepVM SaveStreetSweepDetails(StreetSweepVM data);

        LiquidWasteVM SaveLiquidWasteDetails(LiquidWasteVM data);
        void DeletDumpYardtDetails(int teamId);

        //Added By Saurabh (27 May 2019)

        List<QrEmployeeMaster> GetUserList(int AppId, int teamId);

        //Added By Saurabh (03 June 2019)
        HouseScanifyEmployeeDetailsVM GetHSEmployeeDetails(int teamId);

        UREmployeeDetailsVM GetUREmployeeDetails(int teamId);

        //Added By Saurabh (04 June 2019)
        void SaveHSEmployeeDetails(HouseScanifyEmployeeDetailsVM employee);

        void SaveHSQRStatusHouse(int houseId, string QRStatus);
        void SaveQRStatusDump(int dumpId, string QRStatus);
        void SaveQRStatusLiquid(int liquidId, string QRStatus);
        void SaveQRStatusStreet(int streetId, string QRStatus);

        List<int> GetHSHouseDetailsID(DateTime? fromDate, DateTime? toDate, int userId, string searchString, int QRStatus, string sortColumn, string sortOrder);
        List<int> GetHSDumpDetailsID(DateTime? fromDate, DateTime? toDate, int userId, string searchString, int QRStatus, string sortColumn, string sortOrder);
        List<int> GetHSLiquidDetailsID(DateTime? fromDate, DateTime? toDate, int userId, string searchString, int QRStatus, string sortColumn, string sortOrder);
        List<int> GetHSStreetDetailsID(DateTime? fromDate, DateTime? toDate, int userId, string searchString, int QRStatus, string sortColumn, string sortOrder);

        SBAHSHouseDetailsGrid GetHouseDetailsById(int houseId);

        SBAHSDumpyardDetailsGrid GetDumpDetailsById(int dumpId);
        SBAHSLiquidDetailsGrid GetLiquidDetailsById(int liquidId);
        SBAHSStreetDetailsGrid GetStreetDetailsById(int streetId);

        void SaveUREmployeeDetails(UREmployeeDetailsVM employee);

        //Added By Saurabh (04 June 2019)
        HSDashBoardVM GetHSDashBoardDetails();


        HSDashBoardVM GetURDashBoardDetails();
        List<SBALHSUserLocationMapView> GetHSUserAttenRoute(int qrEmpDaId);

        DataTable getHousesList(int option, int type);
        List<SBAHSHouseDetailsGrid> GetHSQRCodeImageByDate(int type,int UserId, DateTime fDate, DateTime tDate,string QrStatus);
        //Added By Saurabh (06 June 2019)
        List<SBALHouseLocationMapView> GetAllHouseLocation(string date, int userid, int areaid, int wardNo, string SearchString, int? GarbageType, int FilterType,string Emptype);
        List<SBALHouseLocationMapView> GetAllHouseLocationForEmpBitMap(string Emptype, int ebmId);

        //Code Optimization (code)
        //SBALHouseLocationMapView1 GetAllHouseLocation(string date, int userid,int areaid, int wardNo, string SearchString, string start);

        //Added By Saurabh (1 June 2019)
        HouseScanifyEmployeeDetailsVM GetUserDetails(int teamId, string Name);
     
        HouseScanifyEmployeeDetailsVM GetUserNameDetails(int teamId, string Name);

        //Added By Saurabh (2 July 2019)
        DashBoardVM GetHouseOnMapDetails();
        DashBoardVM GetLiquidWasteDetails();

        DashBoardVM GetStreetSweepingDetails();

        //Added By Neha (12 July 2019)
        List<SBAEmplyeeIdelGrid> GetIdleTimeRoute(int userId, string Date);

        OnePoint4VM GetTotalCountDetails(int ANS_ID);



        OnePoint4VM GetMaxINSERTID();

        void Save1Point4(List<OnePoint4VM> point);

        //OnePoint4VM GetQuetionDetails(int teamId);

        List<OnePoint4VM> GetQuetions(string ReportName);

        void Save1Point5(List<OnePoint5VM> point);

        List<OnePoint5VM> GetQuetions1pointfive();

        void SaveTotalCount(OnePoint4VM onepointfour);

        List<OnePoint4VM> GetOnepointfourEditData(int INSERT_ID);

        void Save1Point7(List<OnePointSevenVM> point);

        List<OnePoint7QuestionVM> GetOnePointSevenQuestions();

        List<OnePoint7QuestionVM> GetOnePointSevenAnswers(int INSERT_ID);

        void EditOnePointSeven(List<OnePoint7QuestionVM> OnePoint7QuestionVM);
        List<SelectListItem> LoadListWardNo(int ZoneId);
        List<SelectListItem> LoadListArea(int WardNo);
        List<SelectListItem> ListBeatMapArea(int daId, int areaid);
        //InfotainmentDetailsVW GetInfotainmentDetailsById(int ID);
        //void SaveGameDetails(InfotainmentDetailsVW data);

        SauchalayDetailsVM GetSauchalayDetails(int teamId);

        SauchalayDetailsVM SaveSauchalayDetails(SauchalayDetailsVM data);

        List<SauchalayDetailsVM> GetCTPTLocation();

        #region WasteManagement
        WasteDetailsVM GetWasteDetails(int teamId);

        //List<WasteDetailsVM> SaveWasteDetails(List<WasteDetailsVM> data);
        string SaveWasteDetails(string data);
        string SaveSalesWasteDetails(string data);
        
        List<SelectListItem> LoadListSubCategory(int categoryId);
        List<SelectListItem> GetWMUserDetails();
        #endregion

        List<LogVM> GetLogString();

        List<SBAEmplyeeIdelGrid> GetIdelTimeNotification();

        List<SBAEmplyeeIdelGrid> GetLiquidIdelTimeNotification();

        List<SBAEmplyeeIdelGrid> GetStreetIdelTimeNotification();
        List<SBALUserLocationMapView> GetUserTimeWiseRoute(string date = "", DateTime? fTime = null, DateTime? tTime = null, int? userId = null);

        List<SBALUserLocationMapView> GetHouseTimeWiseRoute(string date = "", DateTime? fTime = null, DateTime? tTime = null, int? userId = null);

        string GetLoginidData(string LoginId);
        string GetUserName(string userName);
        string GetHSUserName(string userName);
        List<string> CheckShiftName();
        StreetSweepVM GetBeatDetails(int teamId);
        StreetSweepVM SaveStreetBeatDetails(StreetSweepVM data);
        HouseQRBunchVM GetHouseBunch(int teamId);
        void SaveHouseBunchDetails(HouseQRBunchVM housebunch);
        MasterQRDetailsVM GetMasterQRBunchDetails(int teamId);
        void SaveMasterQRBunchDetails(MasterQRDetailsVM data);

    }
}
