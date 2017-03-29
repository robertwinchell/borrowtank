using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface IWebApiObjectModel : IBaseModel
    {
        long? WAObjectId { get; set; }
        long ModuleId { get; set; }
        string ModuleName { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string URL { get; set; }
        bool AllowGet { get; set; }
        bool AllowPut { get; set; }
        bool AllowPost { get; set; }
        bool AllowDelete { get; set; }
        bool IsActive { get; set; }
        DateTime AddDate { get; set; }
        long AddUserId { get; set; }
        DateTime UpdateDate { get; set; }
        long UpdateUserId { get; set; }

        List<ISelectListItem> ModuleList { get; set; }        
    }
}
