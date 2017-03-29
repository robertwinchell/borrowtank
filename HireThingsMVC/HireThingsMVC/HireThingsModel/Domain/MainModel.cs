
using System.Collections.Generic;

namespace ASOL.HireThings.Model
{

    public interface IMainModel : IBaseModel
    {
       List<ISelectListItem> categoryList { get; set; }
       List<ISelectListItem> themeList { get; set; }
       List<IAdvertisementModel> advertisementList { get; set; }
    }

    public class MainModel : BaseModel, IMainModel
    {
        public List<ISelectListItem> categoryList { get; set; }
        public List<ISelectListItem> themeList { get; set; }
        public List<IAdvertisementModel> advertisementList { get; set; }
    }
}
