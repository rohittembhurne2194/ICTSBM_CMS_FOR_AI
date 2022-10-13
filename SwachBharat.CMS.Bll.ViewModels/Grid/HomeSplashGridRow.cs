using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GramPanchayat.CMS.Bll.ViewModels.ChildModel.Grid
{
    public class HomeSplashGridRow
    {
        public int splashId { get; set; }
        public string splashImageUrl { get; set; }
        public Nullable<bool> isActive { get; set; }
        public string modifiedBy { get; set; }
        public byte[] modifiedOn { get; set; }
        public Nullable<int> languageId { get; set; }
    }
}
