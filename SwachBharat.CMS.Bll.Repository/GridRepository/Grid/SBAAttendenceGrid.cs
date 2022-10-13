using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.Grid
{
  public  class SBAAttendenceGrid
    {
        public int daID { get; set; }
        public string userName { get; set; }
        public string daDate { get; set; }
        public string daEndDate { get; set; }
        public string startLat { get; set; }
        public string startLong { get; set; }
        public string endLat { get; set; }
        public string endLong { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }       
        public string vtId { get; set; }      
        public string vehicleNumber { get; set; }
        public int userId { get; set; }
        public Nullable<DateTime> CompareDate { get; set; }
        public string Date { get; set; }
        public string daDateTIme { get; set; }


        public Nullable<int>  month_name { get; set; }

        public string status { get; set; }

        public string day1 { get; set; }
        public string day2 { get; set; }
        public string day3 { get; set; }
        public string day4 { get; set; }
        public string day5 { get; set; }
        public string day6 { get; set; }
        public string day7 { get; set; }
        public string day8 { get; set; }
        public string day9 { get; set; }
        public string day10 { get; set; }

        public string day11 { get; set; }
        public string day12 { get; set; }
        public string day13 { get; set; }
        public string day14 { get; set; }
        public string day15 { get; set; }
        public string day16 { get; set; }
        public string day17 { get; set; }
        public string day18 { get; set; }
        public string day19 { get; set; }
        public string day20 { get; set; }

        public string day21 { get; set; }
        public string day22 { get; set; }
        public string day23 { get; set; }
        public string day24 { get; set; }
        public string day25 { get; set; }
        public string day26 { get; set; }
        public string day27 { get; set; }
        public string day28 { get; set; }
        public string day29 { get; set; }
        public string day30 { get; set; }

        public string day31 { get; set; }

        public string TOTAL_DAYS { get; set; }


        public Nullable<int> YEAR_NAME { get; set; }

        public string ReferanceId { get; set; }

        public  string QRCodeImage { get; set; }
}
}
