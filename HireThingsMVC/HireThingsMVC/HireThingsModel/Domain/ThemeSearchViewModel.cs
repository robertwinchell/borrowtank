using System.Collections.Generic;


namespace ASOL.HireThings.Model
{
    public class ThemeSearchViewModel: BaseModel, IThemeSearchViewModel
    {
        public List<IThemeModel> ThemeList { get; set; }
        public long SearchId { get; set; }
        public List<ISelectListItem> DDLSearch { get; set; }
        public string TxtSearch { get; set; }       
    }
}
