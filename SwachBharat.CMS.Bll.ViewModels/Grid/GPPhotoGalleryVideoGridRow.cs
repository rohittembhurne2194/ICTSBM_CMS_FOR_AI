using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GramPanchayat.CMS.Bll.ViewModels.ChildModel.Grid
{
    public class GPPhotoGalleryVideoGridRow
    {
        public int videoId { get; set; }
        public string videoUrl { get; set; }
        public string title { get; set; }
        public string subTitle { get; set; }
        public int photoGalleryId { get; set; }
        public string modifiedBy { get; set; }
        public byte[] modifiedOn { get; set; }
        public Nullable<int> languageId { get; set; }
        public string language { get; set; }
    }
}
