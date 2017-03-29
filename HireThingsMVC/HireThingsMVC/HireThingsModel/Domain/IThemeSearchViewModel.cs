using System.Collections.Generic;

namespace ASOL.HireThings.Model
{
    public interface IThemeSearchViewModel : IBaseModel
    {
        List<IThemeModel> ThemeList { get; set; }
        long SearchId { get; set; }
        List<ISelectListItem> DDLSearch { get; set; }
        string TxtSearch { get; set; }   
    }
}
