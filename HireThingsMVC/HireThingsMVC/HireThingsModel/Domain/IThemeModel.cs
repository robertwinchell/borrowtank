using System;

namespace ASOL.HireThings.Model
{
    public interface IThemeModel: IBaseModel
    {
        long? ThemeId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Code { get; set; }
        bool IsActive { get; set; }
        long AdduserId { get; set; }
        DateTime AddDate { get; set; }
        long UpdateUserId { get; set; }
        DateTime UpdateDate { get; set; }
    }
}
