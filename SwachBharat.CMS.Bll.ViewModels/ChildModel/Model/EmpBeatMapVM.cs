using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
    public class EmpBeatMapVM
    {
        public int ebmId { get; set; }
        public int? userId { get; set; }

        public string userName { get; set; }
        public List<List<coordinates>> ebmLatLong { get; set; }
        public string Type { get; set; }
    }

    public class AppAreaMapVM
    {
        public int AppId { get; set; }
        public string AppName { get; set; }
        public string AppLat { get; set; }
        public string AppLong { get; set; }
        public Nullable<bool> IsAreaActive { get; set; }
        public List<coordinates> AppAreaLatLong { get; set; }

    }
    public class coordinates
    {
        public double? lat { get; set; }
        public double? lng { get; set; }
    }

    public class EmpBeatMapCountVM
    {
        public List<coordinates> poly { get; set; }
        public int innerCount { get; set; }
        public int outerCount { get; set; }

    }
    public class HouseAttenRouteVM
    {
        public List<List<coordinates>> poly { get; set; }
        public List<SBALUserLocationMapView> lstUserLocation { get; set; }
        public int TotalHouseCount { get; set; }
    }

    public class SBALocationMapViewVM
    {
        public List<List<coordinates>> poly { get; set; }
        public List<SBALHouseLocationMapView> lstUserLocation { get; set; }
        public int TotalHouseCount { get; set; }

    }
}
