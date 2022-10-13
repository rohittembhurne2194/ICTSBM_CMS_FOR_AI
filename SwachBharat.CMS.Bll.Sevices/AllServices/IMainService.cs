using GramPanchayat.CMS.Bll.ViewModels;
using GramPanchayat.CMS.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwachBharat.CMS.Bll.ViewModels.MainModel;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;

namespace SwachBharat.CMS.Bll.Services
{
    public interface IMainService
    {
        
        //void SaveApplicationDetails(AppDetailsVM appDetailsVM);
        //IEnumerable<SubscriptionVM> GetSubscriptionId();
        //IEnumerable<VMApplication> GetAppId();
        //bool AddApptoUser(string UserId, int AppId, int SubscriptionId);
        //int GetUserAppId(string UserId);
        //AppDetailsVM GetApplicationDetails(int AppId);   
        AppStateVM GetStateDetailsById(int AppId);
        void SaveStateDetail(AppStateVM state);
        void DeleteStateRecord(int AppId);


        #region District
        AppDistrictVM GetDictrictById(int teamId);

        AEmployeeDetailVM GetDivision();
        void SaveDictrictDetails(AppDistrictVM state);
        void DeleteDictrictRecord(int teamId);
        #endregion


        #region Taluka
        AppTalukaVM GetTalukaById(int teamId,string name);
        void SaveTalukaDetails(AppTalukaVM state);
        void DeleteTalukaRecord(int teamId);
        #endregion

        #region Game 

        InfotainmentDetailsVW GetInfotainmentDetailsById(int ID);

        void SaveGameDetails(InfotainmentDetailsVW data);

        #endregion

    }
}
