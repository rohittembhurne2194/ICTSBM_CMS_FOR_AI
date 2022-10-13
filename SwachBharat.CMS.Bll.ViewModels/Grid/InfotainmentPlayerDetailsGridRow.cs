using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.Grid
{
    public class InfotainmentPlayerDetailsGridRow
    {
        public int ID { get; set; }
        public string PlayerId { get; set; }
        public string GameId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public Nullable<int> Score { get; set; }
        public string DeviceId { get; set; }
        public string Created { get; set; }
        public string  DisplayDateTime { get; set; }
    }
}
