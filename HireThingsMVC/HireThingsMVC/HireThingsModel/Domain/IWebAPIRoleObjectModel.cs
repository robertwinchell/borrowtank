using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface IWebAPIRoleObjectModel : IBaseModel
    {
        long RoleObjectId { get; set; }
        long RoleId { get; set; }
        string RoleName { get; set; }
        long ObjectID { get; set; }
        string ObjectName { get; set; }
        bool AllowGet { get; set; }
        bool AllowPost { get; set; }
        bool AllowPut { get; set; }
        bool AllowDelete { get; set; }
        DateTime AddDate { get; set; }
        long AddUserId { get; set; }
        DateTime UpdateDate { get; set; }
        long UpdateUserId { get; set; }
        bool IsActive { get; set; }
        bool ShowGet { get; set; }
        bool ShowPost { get; set; }
        bool ShowPut { get; set; }
        bool ShowDelete { get; set; }
        bool IsChange { get; set; }
    }
}
