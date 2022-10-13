using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.MainModel
{
    public class AppDetailsVM
    {
        public int AppId { get; set; }
        public string AppName { get; set; }
        public string AppName_mar { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> Tehsil { get; set; }
        public Nullable<int> District { get; set; }
        public string EmailId { get; set; }
        public string website { get; set; }
        public string Android_GCM_pushNotification_Key { get; set; }
        public Nullable<bool> IsProduction { get; set; }
        public string baseImageUrlCMS { get; set; }
        public string baseImageUrl { get; set; }
        public string AboutThumbnailURL { get; set; }
        public string AboutAppynity { get; set; }
        public string AboutTeamDetail { get; set; }
        public string ContactUsTeamMember { get; set; }
        public string HomeSplash { get; set; }
        public string FAQ { get; set; }
        public string ContactUs { get; set; }
        public string basePath { get; set; }
        public string yoccContact { get; set; }
        public string Type { get; set; }
        public string Logo { get; set; }
        public string Latitude { get; set; }
        public string Logitude { get; set; }
        public string UserProfile { get; set; }
        public string Collection { get; set; }
        public string HouseQRCode { get; set; }
        public string PointQRCode { get; set; }
        public string HousePDF { get; set; }
        public string PointPDF { get; set; }
        public string Grampanchayat_Pro { get; set; }
        public string DumpYardQRCode { get; set; }
        public string LiquidQRCode { get; set; }
        public string StreetQRCode { get; set; }
        public string DumpYardPDF { get; set; }
        public Nullable<bool> ReportEnable { get; set; }

        public Nullable<int> GramPanchyatAppID { get; set; }
        public Nullable<int> YoccClientID { get; set; }

        public string YoccFeddbackLink { get; set; }
        public string YoccDndLink { get; set; }

        public string VehicalQRCode { get; set; }
    }
}
