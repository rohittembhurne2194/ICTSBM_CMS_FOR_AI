using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
    public class WasteDetailsVM :BaseVM
    {
        public int CategoryID { get; set; }
        public string GarbageCategory { get; set; }
        public int wasteSubCategoryId { get; set; }
        public string wasteSubCategoryName { get; set; }
        public int ID { get; set; }
        public decimal Weight { get; set; }
        public int UserID { get; set; }
        public int SubCategoryID { get; set; }
        public int UnitID { get; set; }
        public int Source { get; set; }
        public decimal SalesWeight { get; set; }
        public decimal Amount { get; set; }
        public string PartyName { get; set; }
        


    }
}   
