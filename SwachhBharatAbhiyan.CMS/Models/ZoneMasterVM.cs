using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwachhBharatAbhiyan.CMS.Models
{
    public class ZoneMasterVM
    {
        public int zoneId { get; set; }
     //   [Required(ErrorMessage = "Name is required.")]
       // [Remote("CheckZoneDetails", "MainMaster", HttpMethod = "POST", ErrorMessage = "Name already exists!", AdditionalFields = "zoneId")]
        public string name { get; set; }

        public int LWzoneId { get; set; }
        //   [Required(ErrorMessage = "Name is required.")]
       // [Remote("CheckZoneDetails", "LiquidMainMaster", HttpMethod = "POST", ErrorMessage = "Name already exists!", AdditionalFields = "LWzoneId")]
        public string LWname { get; set; }


        public int SSzoneId { get; set; }
        //   [Required(ErrorMessage = "Name is required.")]
      //  [Remote("CheckZoneDetails", "StreetMainMaster", HttpMethod = "POST", ErrorMessage = "Name already exists!", AdditionalFields = "SSzoneId")]
        public string SSname { get; set; }
    }
}