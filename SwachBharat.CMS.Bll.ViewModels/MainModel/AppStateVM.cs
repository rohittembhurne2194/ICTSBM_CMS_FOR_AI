using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.MainModel
{
    public class AppStateVM :BaseVM
    { 
        public int stateId { get; set; }
        public string CountryName { get; set; }
        public string stateName { get; set; }
        public string stateNameMar { get; set; }
       
    }
}
