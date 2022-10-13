using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
    public class InfotainmentDetailsVW
    {
        public int GameDetailsId { get; set; }
        public int GameMasterId { get; set; }
        public string GameName { get; set; }
        public string GameNameMar { get; set; }
        public int SloganId { get; set; }
        public string Slogan { get; set; }
        public string Image { get; set; }
        public int AnswerTypeId { get; set; }
        public string RightAnswer { get; set; }
        public int Points { get; set; }
        public string Description { get; set; }
        public List<SelectListItem> GameMasterList { get; set; }
        public List<SelectListItem> SloganList { get; set; }
        public List<SelectListItem> AnswerTypeList { get; set; }
    }
}
