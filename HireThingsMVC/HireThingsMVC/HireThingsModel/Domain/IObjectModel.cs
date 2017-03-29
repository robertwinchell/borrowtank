using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface IObjectModel:IBaseModel    {
        long? ObjectId { get; set; }
        long ModuleId { get; set; }
        string ModuleName { get; set; }
        string Name { get; set; }
        string ParentName { get; set; }
        string Description { get; set; }
        string Caption { get; set; }
        string URL { get; set; }
        long ParentObjectId { get; set; }
        bool AllowWrite { get; set; }
        bool AllowDelete { get; set; }
        Int32 ObjectLevel { get; set; }
        byte? ObjectOrder { get; set; }
        string ObjectImage { get; set; }
        bool IsActive { get; set; }
        DateTime AddDate { get; set; }
        long AddUserId { get; set; }
        DateTime UpdateDate { get; set; }
        long UpdateUserId { get; set; }
        bool IsWithoutSearch { get; set; }
        List<ISelectListItem> ModuleList { get; set; }
        List<ISelectListItem> ParentObjectList { get; set; }
        List<ISelectListItem> ObjectLevelList { get; set; }



    }
}
