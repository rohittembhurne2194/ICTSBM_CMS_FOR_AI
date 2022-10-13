
using GramPanchayat.CMS.Bll.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwachBharat.CMS.Bll.ViewModels.MainModel;
using SwachBharat.CMS.Dal.DataContexts;
using System.Web.Mvc;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using System.Web.UI.WebControls;
using SwachBharat.CMS.Bll.ViewModels.Grid;

namespace SwachBharat.CMS.Bll.Services
{
    public class MainService : AppMainService, IMainService
    {


        #region State Service
        public void DeleteStateRecord(int id)
        {
            try
            {
                using (var db = new DevSwachhBharatMainEntities())
                {
                    var stateDetails = db.country_states.Where(x => x.id == id).FirstOrDefault();
                    if (stateDetails != null)
                    {
                        db.country_states.Remove(stateDetails);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public AppStateVM GetStateDetailsById(int id)
        {
            try
            {
                using (var db = new DevSwachhBharatMainEntities())
                {
                    var stateDetails = db.country_states.Where(x => x.id == id).FirstOrDefault();
                    if (stateDetails != null)
                    {
                        AppStateVM details = FillStatesViewModel(stateDetails);
                        return details;
                    }
                    else
                    {
                        return new AppStateVM();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveStateDetail(AppStateVM data)
        {
            try
            {
                using (var db = new DevSwachhBharatMainEntities())
                {
                    if (data.stateId > 0)
                    {
                        var model = db.country_states.Where(x => x.id == data.stateId).FirstOrDefault();
                        if (model != null)
                        {
                            model.country_name = data.CountryName;
                            model.state_name = data.stateName;
                            model.state_name_mar = data.stateNameMar;
                            model.id = data.stateId;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var state = FillStateDataModel(data);
                        db.country_states.Add(state);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        #region District Service

        public AppDistrictVM GetDictrictById(int teamId)
        {
            try
            {
                AppDistrictVM details = new AppDistrictVM();
                details.StateList = ListState();
                using (var db = new DevSwachhBharatMainEntities())
                {
                    var districtDetails = db.state_districts.Where(x => x.id == teamId).FirstOrDefault();
                    if (districtDetails != null)
                    {
                        details = FillDistrictViewModel(districtDetails);
                        details.StateList = ListState();
                        return details;
                    }
                    else
                    {
                        return details;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public AEmployeeDetailVM GetDivision()
        {
            try
            {
                AEmployeeDetailVM details = new AEmployeeDetailVM();
                details.DivisionList = ListDivision();
                //   details.DistrictList = ListSubDivision(0);
                details.ULBList = GetULBMenus();
                using (var db = new DevSwachhBharatMainEntities())
                {
                    var districtDetails = db.state_districts.FirstOrDefault();
                    if (districtDetails != null)
                    {
                        details = FillDivisionViewModel(districtDetails);
                        details.DivisionList = ListDivision();
                        //    details.DistrictList = ListSubDivision(0);
                        details.ULBList = GetULBMenus();

                        return details;
                    }
                    else
                    {
                        return details;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public AEmployeeDetailVM GetAUREmployeeDetails(int teamId)
        {
            try
            {
                AEmployeeDetailVM details = new AEmployeeDetailVM();

                using (var db = new DevSwachhBharatMainEntities())
                {
                    var emp = db.AEmployeeMasters.Where(a => a.EmpId == teamId).FirstOrDefault();
                    if (emp != null)
                    {
                        details = FillAEmployeeViewModel(emp);
                        details.ULBList = GetULBMenus(teamId);

                        return details;
                    }
                    else
                    {
                        details.ULBList = GetULBMenus(teamId);
                        return details;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public List<MenuItemULB> GetULBMenus1(string loginId = "")
        {

            List<int> AppList = new List<int>();
            List<MenuItemULB> menuList = new List<MenuItemULB>();
            using (DevSwachhBharatMainEntities db = new DevSwachhBharatMainEntities())
            {
                var appUser = db.AEmployeeMasters.Where(a => a.LoginId == loginId).FirstOrDefault();
                if (appUser != null)
                {

                    AppList = db.AppDetails.Select(a => a.AppId).ToList();

                }
                else
                {


                }


                var appListDivision = db.AppConnections.Join(db.AppDetails, a => a.AppId, b => b.AppId, (a, b) => new { AppId = a.AppId, AppName = b.AppName, Devision = b.District })
                   .GroupBy(c => c.Devision)
                   .Select(group => group.FirstOrDefault()).ToList();
                if (appListDivision != null && appListDivision.Count > 0)
                {
                    foreach (var appDiv in appListDivision)
                    {

                        var div = db.state_districts.Where(a => a.id == appDiv.Devision)
                            .Select(b => new MenuItemULB { divisionId = b.id, districtId = 0, ULBId = 0, LinkText = b.district_name, ActionName = "", ControllerName = "", returnUrl = "", Type = "W" })
                            .FirstOrDefault();
                        if (div != null)
                        {
                            menuList.Add(div);

                            var appListDistrict = db.AppConnections.Join(db.AppDetails, a => a.AppId, b => b.AppId, (a, b) => new { AppId = a.AppId, AppName = b.AppName, Devision = b.District, District = b.Tehsil })
                            .Where(c => c.Devision == appDiv.Devision)
                            .GroupBy(c => c.District)
                            .Select(group => group.FirstOrDefault()).ToList();

                            if (appListDistrict != null && appListDistrict.Count > 0)
                            {
                                foreach (var appDist in appListDistrict)
                                {
                                    var dist = db.tehsils.Where(a => a.id == appDist.District)
                                        .Select(b => new MenuItemULB { divisionId = appDiv.Devision, districtId = b.id, ULBId = 0, LinkText = b.name, ActionName = "", ControllerName = "", returnUrl = "", Type = "W" })
                                        .FirstOrDefault();
                                    if (dist != null)
                                    {
                                        menuList.Add(dist);

                                        var ulb = db.AppConnections.Join(db.AppDetails, a => a.AppId, b => b.AppId,
                                                (a, b) => new { AppId = a.AppId, AppName = b.AppName, Devision = b.District, District = b.Tehsil })
                                                .Where(c => c.Devision == appDiv.Devision && c.District == appDist.District)
                                                .Join(db.UserInApps, c => c.AppId, d => d.AppId,
                                                (c, d) => new { c, d })
                                                .Join(db.AspNetUsers, e => e.d.UserId, f => f.Id,
                                                (e, f) => new MenuItemULB { divisionId = e.c.Devision, districtId = e.c.District, ULBId = e.c.AppId, LinkText = e.c.AppName, ActionName = "Login", ControllerName = "Account", returnUrl = f.UserName, Type = "W" }).ToList();
                                        if (ulb != null && ulb.Count > 0)
                                        {
                                            menuList.AddRange(ulb);

                                        }


                                    }


                                }
                            }

                        }




                    }
                }



            }


            return menuList.Select(x => new MenuItemULB { divisionId = x.divisionId, districtId = x.districtId, ULBId = x.ULBId, LinkText = x.LinkText, ActionName = x.ActionName, ControllerName = x.ControllerName, returnUrl = x.returnUrl, Type = x.Type, IsChecked = AppList.Contains(x.ULBId ?? 0) }).ToList();
        }

        public List<MenuItemDivison> GetULBMenus2(int teamId = -1)
        {

            List<int> AppList = new List<int>();
            List<MenuItemDivison> menuList = new List<MenuItemDivison>();
            using (DevSwachhBharatMainEntities db = new DevSwachhBharatMainEntities())
            {
                var appUser = db.AEmployeeMasters.Where(a => a.EmpId == teamId).FirstOrDefault();
                if (appUser != null && !string.IsNullOrEmpty(appUser.isActiveULB))
                {
                    AppList = appUser.isActiveULB.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                }
                else
                {


                }


                var appListDivision = db.AppConnections.Join(db.AppDetails, a => a.AppId, b => b.AppId, (a, b) => new { AppId = a.AppId, AppName = b.AppName, Devision = b.District })
                   .GroupBy(c => c.Devision)
                   .Select(group => group.FirstOrDefault()).ToList();
                if (appListDivision != null && appListDivision.Count > 0)
                {
                    foreach (var appDiv in appListDivision)
                    {

                        var div = db.state_districts.Where(a => a.id == appDiv.Devision)
                            .Select(b => new MenuItemDivison { divisionId = b.id, districtId = 0, ULBId = 0, LinkText = b.district_name, ActionName = "", ControllerName = "", returnUrl = "", Type = "W" })
                            .FirstOrDefault();
                        if (div != null)
                        {
                            //menuList.Add(div);

                            var appListDistrict = db.AppConnections.Join(db.AppDetails, a => a.AppId, b => b.AppId, (a, b) => new { AppId = a.AppId, AppName = b.AppName, Devision = b.District, District = b.Tehsil })
                            .Where(c => c.Devision == appDiv.Devision)
                            .GroupBy(c => c.District)
                            .Select(group => group.FirstOrDefault()).ToList();

                            if (appListDistrict != null && appListDistrict.Count > 0)
                            {
                                foreach (var appDist in appListDistrict)
                                {
                                    var dist = db.tehsils.Where(a => a.id == appDist.District)
                                        .Select(b => new MenuItemDistrict { divisionId = appDiv.Devision, districtId = b.id, ULBId = 0, LinkText = b.name, ActionName = "", ControllerName = "", returnUrl = "", Type = "W" })
                                        .FirstOrDefault();
                                    if (dist != null)
                                    {
                                        //menuList.Add(dist);

                                        var ulb = db.AppConnections.Join(db.AppDetails, a => a.AppId, b => b.AppId,
                                                (a, b) => new { AppId = a.AppId, AppName = b.AppName, Devision = b.District, District = b.Tehsil })
                                                .Where(c => c.Devision == appDiv.Devision && c.District == appDist.District)
                                                .Join(db.UserInApps, c => c.AppId, d => d.AppId,
                                                (c, d) => new { c, d })
                                                .Join(db.AspNetUsers, e => e.d.UserId, f => f.Id,
                                                (e, f) => new MenuItemULB { divisionId = e.c.Devision, districtId = e.c.District, ULBId = e.c.AppId, LinkText = e.c.AppName, ActionName = "Login", ControllerName = "Account", returnUrl = f.UserName, Type = "W" }).ToList();
                                        if (ulb != null && ulb.Count > 0)
                                        {
                                            //menuList.AddRange(ulb);
                                            var ULBs = ulb.Select(x => new MenuItemULB { divisionId = x.divisionId, districtId = x.districtId, ULBId = x.ULBId, LinkText = x.LinkText, ActionName = x.ActionName, ControllerName = x.ControllerName, returnUrl = x.returnUrl, Type = x.Type, IsChecked = AppList.Contains(x.ULBId ?? 0) }).ToList();
                                            dist.ULBList.AddRange(ULBs);
                                        }
                                        if (ulb.Count == dist.ULBList.Where(u => u.IsChecked).ToList().Count)
                                        {
                                            dist.IsChecked = true;
                                        }
                                        div.DistrictList.Add(dist);

                                    }


                                }
                            }
                            if (div.DistrictList.Count == div.DistrictList.Where(d => d.IsChecked).ToList().Count)
                            {
                                div.IsChecked = true;
                            }
                            menuList.Add(div);
                        }




                    }
                }



            }

            return menuList;
        }


        public List<MenuItemDivison> GetULBMenus(int teamId = -1)
        {

            List<int> AppList = new List<int>();
            List<MenuItemDivison> menuList = new List<MenuItemDivison>();
            using (DevSwachhBharatMainEntities db = new DevSwachhBharatMainEntities())
            {
                var appUser = db.AEmployeeMasters.Where(a => a.EmpId == teamId).FirstOrDefault();
                if (appUser != null && !string.IsNullOrEmpty(appUser.isActiveULB))
                {
                    AppList = appUser.isActiveULB.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                }
                else
                {


                }


                var appListDivision = db.AppConnections.Join(db.AppDetails, a => a.AppId, b => b.AppId, (a, b) => new { a, b })
                        .Join(db.state_districts, c => c.b.District, d => d.id, (c, d) => new { c, d })
                       .GroupBy(f => f.c.b.District)
                       .Select(group => group.FirstOrDefault())
                       .OrderBy(g => g.d.district_name)
                       .Select(g => new MenuItemDivison { divisionId = g.d.id, districtId = 0, ULBId = 0, LinkText = g.d.district_name, ActionName = "", ControllerName = "", returnUrl = "", Type = "W" })
                       .ToList();
                if (appListDivision != null && appListDivision.Count > 0)
                {
                    foreach (var appDiv in appListDivision)
                    {
                        //menuList.Add(div);

                        var appListDistrict = db.AppConnections.Join(db.AppDetails, a => a.AppId, b => b.AppId, (a, b) => new { a, b })
                        .Join(db.tehsils, c => c.b.Tehsil, d => d.id, (c, d) => new { c, d })
                        .Where(f => f.c.b.District == appDiv.divisionId)
                        .GroupBy(g => g.c.b.Tehsil)
                        .Select(group => group.FirstOrDefault())
                        .OrderBy(g => g.d.name)
                        .Select(g => new MenuItemDistrict { divisionId = appDiv.divisionId, districtId = g.d.id, ULBId = 0, LinkText = g.d.name, ActionName = "", ControllerName = "", returnUrl = "", Type = "W" })
                        .ToList();

                        if (appListDistrict != null && appListDistrict.Count > 0)
                        {
                            foreach (var appDist in appListDistrict)
                            {

                                //menuList.Add(dist);
                                var ulb = db.AppConnections.Join(db.AppDetails, a => a.AppId, b => b.AppId,
                                    (a, b) => new { AppId = a.AppId, AppName = b.AppName, Devision = b.District, District = b.Tehsil })
                                    .Where(c => c.Devision == appDiv.divisionId && c.District == appDist.districtId)
                                    .Join(db.UserInApps, c => c.AppId, d => d.AppId,
                                    (c, d) => new { c, d })
                                    .Join(db.AspNetUsers, e => e.d.UserId, f => f.Id,
                                    (e, f) => new MenuItemULB { divisionId = e.c.Devision, districtId = e.c.District, ULBId = e.c.AppId, LinkText = e.c.AppName, ActionName = "Login", ControllerName = "Account", returnUrl = f.UserName, Type = "W" })
                                    .OrderBy(m => m.LinkText)
                                    .ToList();
                                if (ulb != null && ulb.Count > 0)
                                {
                                    //menuList.AddRange(ulb);
                                    var ULBs = ulb.Select(x => new MenuItemULB { divisionId = x.divisionId, districtId = x.districtId, ULBId = x.ULBId, LinkText = x.LinkText, ActionName = x.ActionName, ControllerName = x.ControllerName, returnUrl = x.returnUrl, Type = x.Type, IsChecked = AppList.Contains(x.ULBId ?? 0) }).ToList();
                                    appDist.ULBList.AddRange(ULBs);
                                }
                                if (ulb.Count == appDist.ULBList.Where(u => u.IsChecked).ToList().Count)
                                {
                                    appDist.IsChecked = true;
                                }
                                appDiv.DistrictList.Add(appDist);


                            }
                        }
                        if (appDiv.DistrictList.Count == appDiv.DistrictList.Where(d => d.IsChecked).ToList().Count)
                        {
                            appDiv.IsChecked = true;
                        }
                        menuList.Add(appDiv);

                    }
                }



            }

            return menuList;
        }





        public AEmployeeDetailVM GetDistrict(int id)
        {
            try
            {
                AEmployeeDetailVM details = new AEmployeeDetailVM();

                details.CheckDist = new List<tehsil>();
                var appid = dbMain.AppDetails.Where(c => c.District == id).GroupBy(c => c.District).ToList();

                foreach (var a in appid)
                {
                    var tes = dbMain.tehsils.Where(c => c.id == a.Key).FirstOrDefault<tehsil>();
                    if (tes != null)
                    {


                        details.CheckDist.Add(new tehsil
                        {
                            id = tes.id,
                            name = tes.name,
                         //   IsCheked = tes.IsCheked
                        });
                    }

                }
                using (var db = new DevSwachhBharatMainEntities())
                {
                    var districtDetails = db.state_districts.FirstOrDefault();
                    if (districtDetails != null)
                    {
                        details = FillDivisionViewModel(districtDetails);
                        details.CheckDist = new List<tehsil>();

                        foreach (var a in appid)
                        {
                            var tes = dbMain.tehsils.Where(c => c.id == a.Key).FirstOrDefault<tehsil>();
                            if (tes != null)
                            {


                                details.CheckDist.Add(new tehsil
                                {
                                    id = tes.id,
                                    name = tes.name,
                                 //   IsCheked = tes.IsCheked
                                });
                            }

                        }
                        return details;
                    }
                    else
                    {
                        return details;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void SaveUREmployeeDetails(AEmployeeDetailVM data)
        {
            try
            {
                using (var db = new DevSwachhBharatMainEntities())
                {
                    if (data.qrEmpId > 0)
                    {
                        var model = db.AEmployeeMasters.Where(x => x.EmpId == data.qrEmpId).FirstOrDefault();
                        if (model != null)
                        {

                            model.EmpId = data.qrEmpId;
                            model.EmpName = data.qrEmpName;
                            model.EmpNameMar = data.qrEmpNameMar;
                            model.LoginId = data.qrEmpLoginId;
                            model.Password = data.qrEmpPassword;
                            model.EmpAddress = data.qrEmpAddress;
                            model.isActive = data.isActive;
                            model.EmpMobileNumber = data.qrEmpMobileNumber;
                            model.lastModifyDateEntry = DateTime.Now;
                            model.DivisionId = data.DivisionId;
                            model.DistictId = data.DistictId;
                            model.type = data.type;
                            model.isActiveULB = GetAciveULB(data.ULBList);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var type = FillUREmployeeDataModel(data);
                        type.isActiveULB = GetAciveULB(data.ULBList);
                        db.AEmployeeMasters.Add(type);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        public string GetAciveULB(List<MenuItemDivison> ULBList)
        {
            List<int> AppList = new List<int>();
            string strActiveULB = string.Empty;
            int AppId;
            foreach (MenuItemDivison div in ULBList)
            {
                foreach (MenuItemDistrict dist in div.DistrictList)
                {
                    foreach (MenuItemULB ulb in dist.ULBList)
                    {
                        if (ulb.IsChecked == true)
                        {
                            AppId = ulb.ULBId ?? 0;
                            if (AppId != 0)
                            {
                                AppList.Add(AppId);
                            }

                        }
                    }

                }


            }
            if (AppList.Count > 0)
            {
                strActiveULB = string.Join(",", AppList);
            }

            return strActiveULB;
        }
        private AEmployeeMaster FillUREmployeeDataModel(AEmployeeDetailVM data)
        {
            AEmployeeMaster model = new AEmployeeMaster();
            model.EmpId = data.qrEmpId;
            model.EmpName = data.qrEmpName;
            model.EmpNameMar = data.qrEmpNameMar;
            model.LoginId = data.qrEmpLoginId;
            model.Password = data.qrEmpPassword;
            model.EmpAddress = data.qrEmpAddress;
            model.isActive = data.isActive;
            model.EmpMobileNumber = data.qrEmpMobileNumber;
            model.lastModifyDateEntry = DateTime.Now;
            model.DivisionId = data.DivisionId;
            model.DistictId = data.DistictId;
            model.type = data.type;
            return model;
        }

        public void SaveDictrictDetails(AppDistrictVM data)
        {
            try
            {
                using (var db = new DevSwachhBharatMainEntities())
                {
                    if (data.districtId > 0)
                    {
                        var model = db.state_districts.Where(x => x.id == data.districtId).FirstOrDefault();
                        if (model != null)
                        {
                            model.district_name = data.districtName;
                            model.district_name_mar = data.districtNameMmar;
                            model.state_id = data.stateId;
                            model.id = data.districtId;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var district = FillDistrictDataModel(data);
                        db.state_districts.Add(district);
                        db.SaveChanges();

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void DeleteDictrictRecord(int teamId)
        {
            try
            {
                using (var db = new DevSwachhBharatMainEntities())
                {
                    var districtDetails = db.state_districts.Where(x => x.id == teamId).FirstOrDefault();
                    if (districtDetails != null)
                    {
                        db.state_districts.Remove(districtDetails);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Taluka Service
        public AppTalukaVM GetTalukaById(int teamId, string name)
        {
            try
            {
                AppTalukaVM details = new AppTalukaVM();

                using (var db = new DevSwachhBharatMainEntities())
                {
                    var talukaDetails = db.tehsils.Where(x => x.id == teamId || x.name == name).FirstOrDefault();
                    if (talukaDetails != null)
                    {
                        details = FillTalukaViewModel(talukaDetails);
                        details.DistrictList = ListDistrict();
                        details.StateList = ListState();
                        return details;
                    }
                    else
                    {
                        details.DistrictList = ListDistrict();
                        details.StateList = ListState();
                        return details;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void SaveTalukaDetails(AppTalukaVM data)
        {
            try
            {
                using (var db = new DevSwachhBharatMainEntities())
                {
                    if (data.talukaId > 0)
                    {
                        var model = db.tehsils.Where(x => x.id == data.talukaId).FirstOrDefault();
                        if (model != null)
                        {
                            model.id = data.talukaId;
                            model.name = data.talukaName;
                            model.name_mar = data.talukaNameMar;
                            model.district = data.districtId;
                            model.state = data.stateId;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var taluka = FillTalukaDataModel(data);
                        db.tehsils.Add(taluka);
                        db.SaveChanges();

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void DeleteTalukaRecord(int teamId)
        {
            try
            {
                using (var db = new DevSwachhBharatMainEntities())
                {
                    var talukaDetails = db.tehsils.Where(x => x.id == teamId).FirstOrDefault();
                    if (talukaDetails != null)
                    {
                        db.tehsils.Remove(talukaDetails);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion


        #region Manage User Custom Chagnes
        public AppDetailsVM GetApplicationDetails(int AppId)
        {
            var appDetails = (dbMain.AppDetails.Where(x => x.AppId == AppId).FirstOrDefault());
            if (appDetails != null)
            {
                return FillAppDetailsVMViewModel(appDetails);
            }
            else
            {
                return null;
            }

        }
        public string GetDatabaseFromAppID(int AppId)
        {
            var DB_Name = dbMain.AppConnections.Where(x => x.AppId == AppId).FirstOrDefault().InitialCatalog;
            return DB_Name.ToString();
        }
        public string GetDataSourceFromAppID(int AppId)
        {
            var DB_Source = dbMain.AppConnections.Where(x => x.AppId == AppId).FirstOrDefault().DataSource;
            return DB_Source.ToString();
        }
public int GetUserAppId(string UserId)
        {
            int AppId = 0;
            AppId = dbMain.UserInApps.Where(x => x.UserId == UserId && x.AppId!=3109 && x.AppId!=3088 && x.AppId!=3108 && x.AppId != 3111 && x.AppId != 3068).Select(x => x.AppId).FirstOrDefault();

            return AppId;
        }

        public int GetUserAppIdL(string UserId)
        {
            int AppId = 0;
            int AppId1 = 0;
            AppId1 = Convert.ToInt32(UserId);
            AppId = dbMain.AD_USER_MST_LIQUID.Where(x => x.APP_ID == AppId1).Select(x => x.APP_ID).FirstOrDefault();

            return AppId;
        }

        public int GetUserAppIdSS(string UserId)
        {
            int AppId = 0;
            int AppId1 = 0;
            AppId1 = Convert.ToInt32(UserId);
            AppId = dbMain.AD_USER_MST_STREET.Where(x => x.APP_ID == AppId1).Select(x => x.APP_ID).FirstOrDefault();

            return AppId;
        }

        public SBAHSUREmpLocationMapView GetEmpByIdforMap(int teamId, int daId)
        {
            try
            {



                SBAHSUREmpLocationMapView house = new SBAHSUREmpLocationMapView();

                HSUR_Daily_Attendance Daily_Attendanceuser = new HSUR_Daily_Attendance();
                Daily_Attendanceuser = dbMain.HSUR_Daily_Attendance.Where(x => x.daID == daId).FirstOrDefault();
                EmployeeMaster user = new EmployeeMaster();
                user = dbMain.EmployeeMasters.Where(x => x.EmpId == Daily_Attendanceuser.userId).FirstOrDefault();
                house.userName = user.EmpName;

                return house;


            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<SBAHSUREmpLocationMapView> GetHSUserAttenRoute(int daId)
        {
            List<SBAHSUREmpLocationMapView> userLocation = new List<SBAHSUREmpLocationMapView>();
            DateTime newdate = DateTime.Now.Date;
            var datt = newdate;
            var att = dbMain.HSUR_Daily_Attendance.Where(c => c.daID == daId).FirstOrDefault();

            var useridnew = dbMain.HSUR_Daily_Attendance.Where(c => c.userId == att.userId && c.daDate == att.daDate).FirstOrDefault();


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
            var data = dbMain.UR_Location.Where(c => c.empId == att.userId & c.datetime >= fdate & c.datetime <= edate).ToList();


            foreach (var x in data)
            {

                string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");
                string tim = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");
                var userName = dbMain.EmployeeMasters.Where(c => c.EmpId == att.userId).FirstOrDefault();

                userLocation.Add(new SBAHSUREmpLocationMapView()
                {
                    userId = userName.EmpId,
                    userName = userName.EmpName,
                    datetime = Convert.ToDateTime(x.datetime).ToString("HH:mm"),
                    date = dat,
                    time = tim,
                    lat = x.lat,
                    log = x.@long,
                    address = x.address,
                    userMobile = userName.EmpMobileNumber,
                    // type = Convert.ToInt32(x.type),

                });

            }

            return userLocation;
        }

        private object checkNull(string address)
        {
            string result = "";
            if (address == null || address == "")
            {
                result = "";
                return result;
            }
            else
            {
                result = address;
                return result;
            }
        }

      
        public List<AppDetail> GetAppName()
        {
            List<AppDetail> appNames = new List<AppDetail>();
            appNames = dbMain.AppDetails.Where(x => x.IsActive == true && x.AppName != "Thane Mahanagar Palika" && x.AppId != 3088 && x.AppId != 3094).OrderBy(x => x.AppName).ToList(); //Live AppID=3088 for Thane ULB  & 3094 For Employee ULB
                                                                                                                                                                                         //appNames = dbMain.AppDetails.ToList();
                                                                                                                                                                                         //var appNames= dbMain.AppDetails.Where(row => row.)
            return appNames.OrderBy(x => x.AppName).ToList();
        }


        public List<AppDetail> GetURAppName(string utype, string LoginId, string Password)
        {
            List<AppDetail> appNames = new List<AppDetail>();
            if (utype == "A")
            {
                appNames = dbMain.AppDetails.Where(x => x.IsActive == true && x.AppName != "Thane Mahanagar Palika").OrderBy(x => x.AppName).ToList();
            }
            else
            {
                var ULBList = dbMain.EmployeeMasters.Where(x => x.LoginId == LoginId && x.Password == Password).FirstOrDefault();
                string s = ULBList.isActiveULB;
                string[] values = s.Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim();
                    int u = 0;
                    if (values[i] != "")
                    {
                        u = Convert.ToInt32(values[i]);
                        var details = dbMain.AppDetails.Where(x => x.IsActive == true && x.AppName != "Thane Mahanagar Palika" && x.AppId == u).OrderBy(x => x.AppName).FirstOrDefault();
                        appNames.Add(details);
                    }


                }



            }
            return appNames.OrderBy(x => x.AppName).ToList();
        }

        public string GetLoginid(string LoginId)
        {
            var isrecord = dbMain.EmployeeMasters.Where(x => x.LoginId == LoginId && x.isActive == true).FirstOrDefault();
            if (isrecord == null)
            {
                return "0";
            }
            else
            {
                return "1";
            }

        }

        #endregion

        #region DataModel
        private AppDetail FillAppDetailsDataModel(AppDetailsVM data)
        {
            AppDetail model = new AppDetail();
            model.AboutAppynity = data.AboutAppynity;
            model.AboutTeamDetail = data.AboutTeamDetail;
            model.AboutThumbnailURL = data.AboutThumbnailURL;
            model.Android_GCM_pushNotification_Key = data.Android_GCM_pushNotification_Key;
            model.AppId = data.AppId;
            model.baseImageUrl = data.baseImageUrl;
            model.baseImageUrlCMS = data.baseImageUrlCMS;
            model.ContactUsTeamMember = data.ContactUsTeamMember;
            model.EmailId = data.EmailId;
            model.website = data.website;
            model.basePath = data.basePath;
            model.Type = data.Type;
            model.Logo = data.Logo;
            model.HousePDF = data.HousePDF;
            model.HouseQRCode = data.HouseQRCode;
            model.PointPDF = data.PointPDF;
            model.PointQRCode = data.PointQRCode;
            return model;
        }
        private country_states FillStateDataModel(AppStateVM data)
        {
            country_states model = new country_states();
            model.country_name = data.CountryName;
            model.state_name = data.stateName;
            model.state_name_mar = data.stateNameMar;
            model.id = data.stateId;

            return model;
        }
        private state_districts FillDistrictDataModel(AppDistrictVM data)
        {
            state_districts model = new state_districts();
            model.district_name = data.districtName;
            model.district_name_mar = data.districtNameMmar;
            model.state_id = data.stateId;
            model.id = data.districtId;
            return model;
        }

        private tehsil FillTalukaDataModel(AppTalukaVM data)
        {
            tehsil model = new tehsil();
            model.id = data.talukaId;
            model.name = data.talukaName;
            model.name_mar = data.talukaNameMar;
            model.district = data.districtId;
            model.state = data.stateId;
            return model;
        }

        #endregion

        #region ViewModel 
        private AppDetailsVM FillAppDetailsVMViewModel(AppDetail data)
        {

            AppDetailsVM model = new AppDetailsVM();
            model.AboutAppynity = data.AboutAppynity;
            model.AboutTeamDetail = data.AboutTeamDetail;
            model.AboutThumbnailURL = data.AboutThumbnailURL;
            model.Android_GCM_pushNotification_Key = data.Android_GCM_pushNotification_Key;
            model.AppId = data.AppId;
            model.AppName = data.AppName;
            model.AppName_mar = data.AppName_mar;
            model.baseImageUrl = data.baseImageUrl;
            model.baseImageUrlCMS = data.baseImageUrlCMS;
            model.ContactUsTeamMember = data.ContactUsTeamMember;
            model.EmailId = data.EmailId;
            model.ContactUs = data.ContactUs;
            model.website = data.website;
            model.basePath = data.basePath;
            model.Type = data.Type;
            model.Logo = data.Logo;
            model.Logitude = data.Logitude;
            model.Latitude = data.Latitude;
            model.Collection = data.Collection;
            model.UserProfile = data.UserProfile;
            model.HousePDF = data.HousePDF;
            model.HouseQRCode = data.HouseQRCode;
            model.PointPDF = data.PointPDF;
            model.PointQRCode = data.PointQRCode;
            model.Grampanchayat_Pro = data.Grampanchayat_Pro;
            model.DumpYardQRCode = data.DumpYardQRCode;
            model.DumpYardPDF = data.DumpYardPDF;
            model.ReportEnable = data.ReportEnable;
            model.YoccClientID = data.YoccClientID;
            model.yoccContact = data.yoccContact;
            model.GramPanchyatAppID = data.GramPanchyatAppID;
            model.YoccFeddbackLink = data.YoccFeddbackLink;
            model.YoccDndLink = data.YoccDndLink;
            model.LiquidQRCode = data.LiquidQRCode;
            model.StreetQRCode = data.StreetQRCode;
            model.VehicalQRCode = data.VehicalQRCode;
            return model;

        }
        private AppStateVM FillStatesViewModel(country_states data)
        {
            AppStateVM model = new AppStateVM();
            model.CountryName = data.country_name;
            model.stateName = data.state_name;
            model.stateNameMar = data.state_name_mar;
            model.stateId = data.id;
            return model;
        }
        private AEmployeeDetailVM FillDivisionViewModel(state_districts data)
        {
            AEmployeeDetailVM model = new AEmployeeDetailVM();
            model.DivisionName = data.district_name;
            model.DivisionId = data.id;

            return model;
        }
        private AEmployeeDetailVM FillAEmployeeViewModel(AEmployeeMaster data)
        {
            AEmployeeDetailVM model = new AEmployeeDetailVM();
            model.qrEmpId = data.EmpId;
            model.qrEmpName = data.EmpName;
            model.qrEmpNameMar = data.EmpNameMar;
            model.qrEmpPassword = data.Password;
            model.qrEmpLoginId = data.LoginId;
            model.qrEmpMobileNumber = data.EmpMobileNumber;
            model.qrEmpAddress = data.EmpAddress;
            model.type = data.type;
            model.isActive = data.isActive;
            model.StateId = data.StateId ?? 0;
            model.DivisionId = data.DivisionId ?? 0;
            model.DistictId = data.DistictId ?? 0;
            model.lastModifyDate = data.lastModifyDateEntry;
            return model;
        }

        private AppDistrictVM FillDistrictViewModel(state_districts data)
        {
            AppDistrictVM model = new AppDistrictVM();
            model.districtName = data.district_name;
            model.districtNameMmar = data.district_name_mar;
            model.districtId = data.id;
            model.stateId = data.state_id;
            return model;
        }

        private AppTalukaVM FillTalukaViewModel(tehsil data)
        {
            AppTalukaVM model = new AppTalukaVM();
            model.talukaId = data.id;
            model.talukaName = data.name;
            model.talukaNameMar = data.name_mar;
            model.districtId = data.district;
            model.stateId = data.state;
            return model;
        }
        #endregion
        //*****************************

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
        public List<SelectListItem> ListState()
        {
            var State = new List<SelectListItem>();
            SelectListItem itemAdd = new SelectListItem() { Text = "--Select State--", Value = "0" };

            try
            {
                State = dbMain.country_states.ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.state_name + '(' + x.state_name_mar + ')',
                        Value = x.id.ToString()
                    }).OrderBy(t => t.Text).ToList();

                State.Insert(0, itemAdd);
            }
            catch (Exception ex) { throw ex; }

            return State;
        }


        public List<SelectListItem> ListDivision()
        {
            var State = new List<SelectListItem>();
            SelectListItem itemAdd = new SelectListItem() { Text = "--Select All--", Value = "0" };

            try
            {

                State = dbMain.state_districts.Join(dbMain.AppDetails, a => a.id, b => b.District, (a, b) => new { id = a.id, district_name = a.district_name, district_name_mar = a.district_name_mar })
                 .GroupBy(c => c.id)
                 .Select(group => group.FirstOrDefault()).ToList()

                    .Select(x => new SelectListItem
                    {
                        Text = x.district_name + '(' + x.district_name_mar + ')',
                        Value = x.id.ToString()
                    }).OrderBy(t => t.Text).ToList();
                State.Insert(0, itemAdd);

            }
            catch (Exception ex) { throw ex; }

            return State;
        }

        //public AEmployeeDetailVM ListSubDivision(int disid)
        //{

        //    AEmployeeDetailVM TypeDetail = new AEmployeeDetailVM();
        //    try
        //    {



        //            .Select(x => new SelectListItem
        //            {
        //                Text = x.name + '(' + x.name_mar + ')',
        //                Value = x.id.ToString()
        //            }).OrderBy(t => t.Text).ToList();

        //    }
        //    else
        //    {
        //        State = dbMain.tehsils.Join(dbMain.AppDetails, a => a.id, b => b.Tehsil, (a, b) => new { id = a.id, name = a.name, name_mar = a.name_mar, Districts = b.District }).GroupBy(c => c.id)
        //        .Select(group => group.FirstOrDefault()).ToList()

        //                                .Select(x => new SelectListItem
        //                                {
        //                                    Text = x.name + '(' + x.name_mar + ')',
        //                                    Value = x.id.ToString()
        //                                }).OrderBy(t => t.Text).ToList();
        //    }
        //    State.Insert(0, itemAdd);
        //}
        //catch (Exception ex) { throw ex; }

        //    return TypeDetail;
        //}
        public List<SelectListItem> ListDistrict()
        {
            var District = new List<SelectListItem>();
            SelectListItem itemAdd = new SelectListItem() { Text = "--Select District--", Value = "0" };

            try
            {
                District = dbMain.state_districts.ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.district_name + '(' + x.district_name_mar + ')',
                        Value = x.id.ToString()
                    }).OrderBy(t => t.Text).ToList();

                District.Insert(0, itemAdd);
            }
            catch (Exception ex) { throw ex; }

            return District;
        }
        public List<SelectListItem> ListTaluka()
        {
            var Taluka = new List<SelectListItem>();
            SelectListItem itemAdd = new SelectListItem() { Text = "--Select Taluka--", Value = "0" };

            try
            {
                Taluka = dbMain.tehsils.ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.name + '(' + x.name_mar + ')',
                        Value = x.id.ToString()
                    }).OrderBy(t => t.Text).ToList();

                Taluka.Insert(0, itemAdd);
            }
            catch (Exception ex) { throw ex; }

            return Taluka;
        }
        #endregion

        //*****************************
        //public int GetAppIdForApp(string appName)
        //{
        //    int appId = 0;
        //    using (var db = new DevGramPanchayatAppyMainEntities())
        //    {
        //        appId = db.AppDetails.Where(x => x.AppName == appName).Select(z => z.AppId).FirstOrDefault();
        //    }
        //    return appId;
        //}
        //public void SaveApplicationDetails(AppDetailsVM appDetailsVM)
        //{
        //    AppDetail data = FillAppDetailsDataModel(appDetailsVM);
        //    using (var db = new DevGramPanchayatAppyMainEntities())
        //    {
        //        db.AppDetails.Add(data);
        //        db.SaveChanges();
        //        //Save AppConnection From Old Information
        //        AppConnection appConnection = db.AppConnections.Where(x => x.AppId == 1).FirstOrDefault();
        //        appConnection.AppId = db.AppDetails.Max(z => z.AppId);
        //        appConnection.InitialCatalog = "Restro" + appDetailsVM.AppName;
        //        db.AppConnections.Add(appConnection);
        //        db.SaveChanges();
        //    }

        //}
        //public IEnumerable<SubscriptionVM> GetSubscriptionId()
        //{
        //    IEnumerable<SubscriptionVM> SubscriptionIds;
        //    using (var context = new DevGramPanchayatAppyMainEntities())
        //    {
        //        SubscriptionIds = context.Subscriptions.Select(x => new SubscriptionVM { name = x.subscriptionName, id = x.subscriptionId }).ToList();
        //    }
        //    return SubscriptionIds;
        //}

        //public IEnumerable<VMApplication> GetAppId()
        //{
        //    IEnumerable<VMApplication> AppIds;
        //    using (var context = new DevGramPanchayatAppyMainEntities())
        //    {
        //        AppIds = context.AppDetails.Select(x => new VMApplication { name = x.AppName, id = x.AppId }).ToList();
        //    }
        //    return AppIds;
        //}
        //public bool AddApptoUser(string UserId, int AppId, int SubscriptionId)
        //{
        //    try
        //    {
        //        using (var context = new DevGramPanchayatAppyMainEntities())
        //        {
        //            UserInApp userInApp = context.UserInApps.Where(x => x.UserId == UserId).FirstOrDefault();
        //            if (userInApp != null)
        //            {
        //                userInApp.AppId = AppId;
        //                userInApp.subscriptionId = SubscriptionId;
        //                context.UserInApps.Attach(userInApp);
        //                context.Entry(userInApp).State = EntityState.Modified;
        //            }
        //            else
        //            {
        //                userInApp = new UserInApp();
        //                userInApp.AppId = AppId;
        //                userInApp.subscriptionId = SubscriptionId;
        //                userInApp.UserId = UserId;
        //                context.UserInApps.Add(userInApp);
        //            }
        //            context.SaveChanges();
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}


        #region Game
        public List<GameMaster> GetGameList()
        {
            List<GameMaster> gameList = new List<GameMaster>();
            gameList = dbMain.GameMasters.ToList();
            //appNames = dbMain.AppDetails.ToList();
            //var appNames= dbMain.AppDetails.Where(row => row.)
            return gameList;
        }

        public List<AppDetail> GetAppList(string utype, string LoginId, string Password)
        {
            List<AppDetail> appList = new List<AppDetail>();
            if (utype == "A" || utype == "SUA")
            {
                appList = dbMain.AppDetails.Where(x => x.IsActive == true && (x.AppName != "Thane Mahanagar Palika" && x.AppName != "Nagpur Mahanagar Palika")).OrderBy(x => x.AppName).ToList();
                //appNames = dbMain.AppDetails.ToList();
                //var appNames= dbMain.AppDetails.Where(row => row.)
                return appList;
            }
            else
            {
                var ULBList = dbMain.EmployeeMasters.Where(x => x.LoginId == LoginId && x.Password == Password).FirstOrDefault();
                string s = ULBList.isActiveULB;
                string[] values = s.Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim();
                    int u = 0;
                    if (values[i] != "")
                    {
                        u = Convert.ToInt32(values[i]);
                        var detail = dbMain.AppDetails.Where(x => x.AppId == u).FirstOrDefault();
                        if (detail.IsActive == true)
                        {
                            var details = dbMain.AppDetails.Where(x => x.IsActive == true && x.AppName != "Thane Mahanagar Palika" && x.AppId == u).OrderBy(x => x.AppName).FirstOrDefault();
                            appList.Add(details);
                        }

                    }


                }

            }
            return appList.OrderBy(x => x.AppName).ToList();
        }

        public List<EmployeeMaster> GetEmployeeDetails(int teamId, string Emptype)
        {
            
           // List<EmployeeMaster> EmpList = new List<EmployeeMaster>();

            var EmpList = new List<EmployeeMaster>();
            try
            {
                EmployeeMaster itemAdd = new EmployeeMaster() { Text = "--Select Employee--", Value = "0" };
                EmpList = dbMain.EmployeeMasters.ToList()
                    .Select(x => new EmployeeMaster
                    {
                        Text = x.EmpName,
                        Value = x.EmpId.ToString(),
                        isActive = x.isActive
                    }).Where(x => x.isActive == true).OrderBy(x => x.Text).ToList();
                EmpList.Insert(0, itemAdd);

            }
            catch (Exception ex) { throw ex; }
            return EmpList;
        }

       
        public InfotainmentDetailsVW GetInfotainmentDetailsById(int ID)
        {
            try
            {
                InfotainmentDetailsVW type = new InfotainmentDetailsVW();
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();

                //using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                //{
                var Details = dbMain.GameDetails.Where(x => x.GameDetailsID == ID).FirstOrDefault();
                if (Details != null)
                {
                    type = FillInfotainmentViewModel(Details);
                    if (type.Image != null && type.Image != "")
                    {
                        type.Image = type.Image.Trim(); //ThumbnaiUrlCMS + type.Image.Trim();
                    }
                    else
                    {
                        type.Image = "/Images/default_not_upload.png";
                    }
                    return type;
                }
                else
                {
                    type.GameMasterList = LoadGameList();
                    type.SloganList = LoadSloganList();
                    type.AnswerTypeList = LoadAnswerTypeList();
                    type.Image = "/Images/add_image_square.png";
                    return type;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        //private GameDetail FillGameDetailsDataModel(InfotainmentDetailsVW data)
        //{
        //    GameDetail model = new GameDetail();
        //    model.GameDetailsID = data.GameDetailsId;
        //    model.ImageUrl = data.Image;
        //    model.RightAnswerID = data.AnswerTypeId;
        //    model.Point = data.Points;
        //    model.GameMasterID = data.GameMasterId;
        //    model.SloganID = data.SloganId;
        //    model.Description = data.Description;
        //    model.Created = DateTime.Now;
        //    return model;
        //}

        private InfotainmentDetailsVW FillInfotainmentViewModel(GameDetail data)
        {
            InfotainmentDetailsVW model = new InfotainmentDetailsVW();
            model.GameDetailsId = data.GameDetailsID;
            model.Image = data.ImageUrl;
            model.AnswerTypeId = Convert.ToInt32(data.RightAnswerID);
            model.Points = Convert.ToInt32(data.Point);
            model.GameMasterId = Convert.ToInt32(data.GameMasterID);
            model.SloganId = Convert.ToInt32(data.SloganID);
            model.Description = data.Description;
            model.GameMasterList = LoadGameList();
            model.SloganList = LoadSloganList();
            model.AnswerTypeList = LoadAnswerTypeList();
            return model;
        }

        public List<SelectListItem> LoadGameList()
        {
            var GameList = new List<SelectListItem>();
            try
            {
                SelectListItem itemAdd = new SelectListItem() { Text = "--Select Game--", Value = "0" };
                GameList = dbMain.GameMasters.ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.GameName,
                        Value = x.GameId.ToString()
                    }).OrderBy(t => t.Text).ToList();
                GameList.Insert(0, itemAdd);

            }
            catch (Exception ex) { throw ex; }
            return GameList;
        }

        public List<SelectListItem> LoadSloganList()
        {
            var SloganList = new List<SelectListItem>();
            try
            {
                SelectListItem itemAdd = new SelectListItem() { Text = "--Select Slogan--", Value = "0" };
                SloganList = dbMain.Game_Slogan.ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.Slogan,
                        Value = x.ID.ToString()
                    }).OrderBy(t => t.Text).ToList();
                SloganList.Insert(0, itemAdd);
            }
            catch (Exception ex) { throw ex; }
            return SloganList;
        }

        public List<SelectListItem> LoadAnswerTypeList()
        {
            var SloganList = new List<SelectListItem>();
            try
            {
                SelectListItem itemAdd = new SelectListItem() { Text = "--Select Type--", Value = "0" };
                SloganList = dbMain.Game_AnswerType.ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.AnswerType,
                        Value = x.AnswerTypeId.ToString()
                    }).OrderBy(t => t.Text).ToList();
                SloganList.Insert(0, itemAdd);
            }
            catch (Exception ex) { throw ex; }
            return SloganList;
        }

        public void SaveGameDetails(InfotainmentDetailsVW data)
        {
            try
            {
                //using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                //{
                if (data.GameDetailsId > 0)
                {
                    var model = dbMain.GameDetails.Where(x => x.GameDetailsID == data.GameDetailsId).FirstOrDefault();
                    if (model != null)
                    {
                        if (data.Image != null)
                        {
                            model.ImageUrl = data.Image;
                        }
                        model.RightAnswerID = data.AnswerTypeId;
                        model.Point = data.Points;
                        model.GameMasterID = data.GameMasterId;
                        model.SloganID = data.SloganId;
                        model.Description = data.Description;
                        model.Created = DateTime.Now;
                        dbMain.SaveChanges();
                    }
                }
                else
                {
                    var type = FillGameDetailsDataModel(data);
                    dbMain.GameDetails.Add(type);
                    dbMain.SaveChanges();
                }
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }

        private GameDetail FillGameDetailsDataModel(InfotainmentDetailsVW data)
        {
            GameDetail model = new GameDetail();
            model.GameDetailsID = data.GameDetailsId;
            model.ImageUrl = data.Image;
            model.RightAnswerID = data.AnswerTypeId;
            model.Point = data.Points;
            model.GameMasterID = data.GameMasterId;
            model.SloganID = data.SloganId;
            model.Description = data.Description;
            model.Created = DateTime.Now;
            return model;
        }


        #endregion
    }
}
