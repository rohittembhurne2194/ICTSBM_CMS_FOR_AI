using SwachBharat.CMS.Bll.ViewModels;
using SwachBharat.CMS.Bll.ViewModels.MainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwachBharat.CMS.Dal.DataContexts;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using System.Web.Mvc;
using SwachBharat.CMS.Bll.ViewModels.Grid;

namespace SwachBharat.CMS.Bll.Repository.MainRepository
{
    public interface IMainRepository
    {
        #region State
        AppStateVM GetStateById(int teamId);
        void SaveState(AppStateVM state);
        void DeleteState(int teamId);
        #endregion


        #region District
        AppDistrictVM GetDistrictById(int teamId);


        AEmployeeDetailVM GetDivision();

        AEmployeeDetailVM GetAUREmployeeDetails(int teamId);


        AEmployeeDetailVM GetDistrict(int id);

        void SaveUREmployee(AEmployeeDetailVM employee);
        void SaveDistrict(AppDistrictVM state);
        void DeleteDistrict(int teamId);
        #endregion


        #region Taluka
        AppTalukaVM GetTalukaById(int teamId,string name);
        void SaveTaluka(AppTalukaVM state);
        void DeleteTaluka(int teamId);
        #endregion

        //int GetAppIdForApp(string appName);
        //void SaveApplicationDetails(AppDetailsVM appDetailsVM);
        //IEnumerable<SubscriptionVM> GetSubscriptionId();
        //IEnumerable<VMApplication> GetAppId();
        //bool AddApptoUser(string UserId, int AppId, int SubscriptionId);
        AppDetailsVM GetApplicationDetails(int AppId);

        AppAreaMapVM GetAppAreaMap(int AppId);
        List<SelectListItem> ListAppMap();
        List<SelectListItem> ListAllApp();
        void SaveAppAreaMap(AppAreaMapVM AppAreaObj);

        void SaveAttendance(HSUR_Daily_AttendanceVM _hsuserinfo);

       
        int GetUserAppId(string UserId);

        int GetUserAppIdL(string UserId);

        int GetUserAppIdSS(string UserId);
        string GetDatabaseFromAppID(int AppId);

        SBAHSUREmpLocationMapView GetEmpByIdforMap(int teamId, int daId);

        List<SBAHSUREmpLocationMapView> GetHSUserAttenRoute(int id);
        string GetDataSourceFromAppID(int AppId);

       // Added By Saurabh ( 27 May 2019)
        List<AppDetail> GetAppName();
        List<AppDetail> GetAppList(string utype, string LoginId, string Password);

        List<EmployeeMaster> GetEmployeeList(int teamId, string Emptype);

        List<AppDetail> GetURAppName(string utype, string LoginId, string Password);

        string GetLoginid(string utype);

        EmployeeVM Login(EmployeeVM _userinfo);

        EmployeeVM LoginStreet(EmployeeVM _userinfo);
        EmployeeVM LoginMaster(EmployeeVM _userinfo);

        EmployeeVM LoginUR(EmployeeVM _userinfo);

      
        List<MenuItem> GetMenus();
        List<MenuItemULB> GetULBMenus(string loginId);


        string SGetULBMenus(string loginId, int DivisionId, int DistrictId, int AppId);

        #region Game
        List<GameMaster> GetGameList();
        InfotainmentDetailsVW GetInfotainmentDetailsById(int ID);
        void SaveGameDetails(InfotainmentDetailsVW data);

        #endregion
    }
}
