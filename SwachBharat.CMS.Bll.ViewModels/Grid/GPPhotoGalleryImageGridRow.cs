using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GramPanchayat.CMS.Bll.ViewModels.ChildModel.Grid
{
    public class GPPhotoGalleryImageGridRow
    {
        public int galleryImageId { get; set; }
        public string imageUrl { get; set; }
        public string title { get; set; }
        public string subTitle { get; set; }
        public Nullable<bool> isShareAvailable { get; set; }
        public int photoGalleryId { get; set; }
        public string modifiedBy { get; set; }
        public byte[] modifiedOn { get; set; }
        public Nullable<int> languageId { get; set; }

        public string language { get; set; }
    }
}
