using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GramPanchayat.CMS.Bll.ViewModels.ChildModel.Grid
{
    public class GPContactUsMemberGridRow
    {
        public int memberId { get; set; }
        public string memberName { get; set; }
        public string jobTitle { get; set; }
        public string memberDescription { get; set; }
        public string profileImageUrl { get; set; }
        public string phoneNumber { get; set; }
        public int contactId { get; set; }
     
        public string modifiedBy { get; set; }
        public byte[] modifiedOn { get; set; }
        public string language { get; set; }
        public Nullable<int> languageId { get; set; }
    }
}
