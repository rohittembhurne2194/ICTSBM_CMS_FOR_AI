using SwachBharat.CMS.Bll.ViewModels;
using SwachBharat.CMS.Bll.ViewModels.Grid;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachBharat.CMS.Bll.ViewModels.MainModel;
using SwachBharat.CMS.Bll.ViewModels.SS2020Reports;
using SwachBharat.CMS.Dal.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data;

namespace SwachBharat.CMS.Bll.Repository.ChildRepository
{
    public interface IChildRepository
    {
     
        DashBoardVM GetDashBoardDetails();

        DashBoardVM GetLiquidDashBoardDetails();

        DashBoardVM GetStreetDashBoardDetails();
        string Address(string location);
        AreaVM GetArea(int teamId,string name);
        void DeletArea(int teamId);
        void SaveArea(AreaVM area);

        void LiquidSaveArea(AreaVM area);

        void StreetSaveArea(AreaVM area);


        VehicleTypeVM GetVehicleType(int teamId);
        VehicleRegVM GetVehicleReg(int teamId);

        void SaveVehicleReg(VehicleRegVM type);
        void DeletVehicleType(int teamId);
        void SaveVehicleType(VehicleTypeVM type);


        WardNumberVM GetWardNumber(int teamId,string name);
        void DeleteWardNumber(int teamId);
        void SaveWardNumber(WardNumberVM type);

        void SaveMasterQrDetails(MasterQRDetailsVM employee);

        void LiquidSaveWardNumber(WardNumberVM type);

        void StreetSaveWardNumber(WardNumberVM type);


        HouseDetailsVM GetHouseById(int teamId);
        MasterQRDetailsVM GetMasterQRById(int teamId , string houseId);
        VehicalRegDetailsVM GetVehicalRegById(int teamId);

        SBALUserLocationMapView GetHouseByIdforMap(int teamId,int daId);
        SBALUserLocationMapView GetDumpByIdforMap(int teamId, int daId, string EmpType);
        SBALUserLocationMapView GetLiquidByIdforMap(int teamId, int daId,string EmpType);
        HouseDetailsVM SaveHouse(HouseDetailsVM data);
        VehicalRegDetailsVM SaveVehicalReg(VehicalRegDetailsVM data);

        void SaveEmpBeatMap(EmpBeatMapVM data);
        EmpBeatMapVM GetEmpBeatMap(int ebmId);
        List<SelectListItem> ListUserBeatMap(string Emptype);
        bool IsPointInPolygon(int ebmId,coordinates p);

        void DeletHouse(int teamId);


        SBALUserLocationMapView GetLocation(int teamId,string Emptype);
        List<SBALUserLocationMapView> GetAllUserLocation(string date,string Emptype);
        // for admin
        List<SBALUserLocationMapView> GetAdminLocation();
        List<SBALUserLocationMapView> GetUserWiseLocation(int id,string date,string Emptype);
        List<SBALUserLocationMapView> GetUserAttenLocation(int id);
        List<SBALUserLocationMapView> GetUserAttenRoute(int id);

        //Added By Saurabh (11 July 2019)
        List<SBALUserLocationMapView> GetHouseAttenRoute(int id,int areaid);
        List<SBALUserLocationMapView> GetLiquidAttenRoute(int id, int areaid);
        List<SBALUserLocationMapView> GetStreetAttenRoute(int id, int areaid);
        List<SBALUserLocationMapView> GetDumpAttenRoute(int id);
        EmpBeatMapCountVM GetbeatMapCount(int daId,int areaid, int polyId);
        List<EmployeeHouseCollectionInnerOuter> getEmployeeHouseCollectionInnerOuter();
        HouseAttenRouteVM GetBeatHouseAttenRoute(int daId, int areaid,int polyId);

        GarbagePointDetailsVM GetGarbagePointById(int teamId);
        GarbagePointDetailsVM SaveGarbagePoint(GarbagePointDetailsVM data);
        void DeletGarbagePoint(int teamId);

        EmployeeDetailsVM GetEmployeeById(int teamId);
        EmpShiftVM GetEmpShiftById(int teamId);

        //EmployeeDetailsVM GetLiquidEmployeeById(int teamId);

        SBAAttendenceSettingsGridRow GetAttendenceEmployeeById(int teamId);
        void DeleteEmployee(int teamId);
        void SaveEmployee(EmployeeDetailsVM employee,string Emptype);

        void SaveEmpShift(EmpShiftVM empShift);
        void SaveAttendenceSettingsDetail(SBAAttendenceSettingsGridRow atten);

        ComplaintVM GetComplaint(int teamId);
        void SaveComplaintStatus(ComplaintVM comp);

        ZoneVM GetZone(int teamId);

        ZoneVM StreetGetZone(int teamId);

        void SaveZone(ZoneVM type);

        void StreetSaveZone(ZoneVM type);
        ZoneVM GetValidZone(string name,int zoneId);

        //Added By Saurabh

        DumpYardDetailsVM GetDumpYardById(int teamId);

        StreetSweepVM GetStreetSweepId(int teamId);

        LiquidWasteVM GetLiquidWasteId(int teamId);

        DumpYardDetailsVM SaveDumpYard(DumpYardDetailsVM data,string Emptype);

     

        StreetSweepVM SaveStreetSweep(StreetSweepVM data);

        LiquidWasteVM SaveLiquidWastes(LiquidWasteVM data);

        #region HouseScanify
        // Added By Saurabh (27 May 2019)
        List<QrEmployeeMaster> GetUserList(int AppId, int teamId);

        //Added By Saurabh (03 June 2019)

        HouseScanifyEmployeeDetailsVM GetHSEmployeeById(int teamId);

        UREmployeeDetailsVM GetUREmployeeById(int teamId);

        //Added By saurabh (04 June 2019)
        void SaveHSEmployee(HouseScanifyEmployeeDetailsVM employee);

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


        void SaveUREmployee(UREmployeeDetailsVM employee);

        //Added By saurabh (04 June 2019)
        HSDashBoardVM GetHSDashBoardDetails();

        HSDashBoardVM GetURDashBoardDetails();

        string GetLoginidData(string utype);
        string GetUserName(string uname);
        List<string> CheckShiftName();
        string GetHSUserName(string uname);
        List<SBALHSUserLocationMapView> GetHSUserAttenRoute(int qrEmpDaId);

        List<SBAHSHouseDetailsGrid> GetHSQRCodeImageByDate(int type,int UserId, DateTime fDate, DateTime tDate,string QrStatus);

        DataTable getHousesList(int option, int type);
        #endregion

        //Added By saurabh (04 June 2019)

        List<SBALHouseLocationMapView> GetAllHouseLocation(string date, int userid, int areaid, int wardNo, string SearchString, int? GarbageType, int FilterType,string Emptype);
        List<SBALHouseLocationMapView> GetAllHouseLocationForEmpBitMap(string Emptype,int ebmId);

        //Code Optimization (code)
        //SBALHouseLocationMapView1 GetAllHouseLocation(string date, int userid,int areaid,int wardNo, string SearchString, string start);


        //Added By saurabh ( June 2019)
        HouseScanifyEmployeeDetailsVM GetUser(int teamId, string name);
     
        HouseScanifyEmployeeDetailsVM GetUserName(int teamId, string name);

        //Added By saurabh ( 02 July 2019)
        DashBoardVM GetHouseOnMapDetails();

        DashBoardVM GetLiquidWasteDetails();

        DashBoardVM GetStreetSweepingDetails();
        //Added By Neha ( 12 July 2019)
        List<SBAEmplyeeIdelGrid> GetIdleTimeRoute(int userId, string Date);
        OnePoint4VM GetOnePointFourTotalCount(int ANS_ID);

        OnePoint4VM GetMaxINSERTID();


        void Save1Point4(List<OnePoint4VM> point);

        List<OnePoint4VM> GetQuetions(string ReportName);

        void Save1Point5(List<OnePoint5VM> point);

        List<OnePoint5VM> GetQuetions1pointfive();

        List<OnePoint4VM> GetOnepointfourEditData(int INSERT_ID);

        void SaveTotalCount(OnePoint4VM onepointfour);

        void Save1Point7(List<OnePointSevenVM> point);

        List<OnePoint7QuestionVM> GetOnePointSevenAnswers(int INSERT_ID);

        List<OnePoint7QuestionVM> GetOnePointSevenQuestions();

        void EditOnePointSeven(List<OnePoint7QuestionVM> OnePoint7);

        List<SelectListItem> LoadListWardNo(int ZoneId);

        List<SelectListItem> LoadListArea(int WardNo);

        List<SelectListItem> ListBeatMapArea(int daId,int areaid);

        //InfotainmentDetailsVW GetInfotainmentDetailsById(int ID);
        //void SaveGameDetails(InfotainmentDetailsVW data);

        SauchalayDetailsVM GetSauchalayById(int teamId);

        SauchalayDetailsVM SaveSauchalay(SauchalayDetailsVM data);

        List<SauchalayDetailsVM> GetCTPTLocation();

        #region WasteManagement

        WasteDetailsVM GetWasteById(int teamId);

        //List<WasteDetailsVM> SaveWaste(List<WasteDetailsVM> data);

        string SaveWaste(string data);

        string SaveSalesWaste(string data);
        List<SelectListItem> LoadListSubCategory(int categoryId);
        List<SelectListItem> GetWMUser();
        #endregion

        List<LogVM> GetLogString();

        List<SBAEmplyeeIdelGrid> GetIdelTimeNotification();

        List<SBAEmplyeeIdelGrid> GetLiquidIdelTimeNotification();

        List<SBAEmplyeeIdelGrid> GetStreetIdelTimeNotification();

        List<SBALUserLocationMapView> GetUserTimeWiseRoute(string date = "", DateTime? fTime = null, DateTime? tTime = null, int? userId = null);

        List<SBALUserLocationMapView> GetHouseTimeWiseRoute(string date = "", DateTime? fTime = null, DateTime? tTime = null, int? userId = null);

        StreetSweepVM GetBeat(int teamId);
        StreetSweepVM SaveStreetBeat(StreetSweepVM data);
        HouseQRBunchVM GetHouseBunch(int teamId);
        void SaveHouseBunch(HouseQRBunchVM housebunch);
        MasterQRDetailsVM GetMasterQRBunchById(int teamId);
        void SaveMasterQrBunchDetails(MasterQRDetailsVM employee);

    }
}
