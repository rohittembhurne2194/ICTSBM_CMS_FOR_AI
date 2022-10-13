using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.Grid
{
    public class InfotainmentDetailsGridRow
    {
        public int GameDetailsId { get; set; }
        public string GameName { get; set; }
        public string GameNameMar { get; set; }
        public int SloganId { get; set; }
        public string Slogan { get; set; }
        public string Image { get; set; }
        public int AnswerTypeId { get; set; }
        public string RightAnswer { get; set; }
        public int Points { get; set; }


    }
}
