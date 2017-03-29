using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface IRoleObjectModel:IBaseModel
    {
        long RoleObjectId { get; set; }
        long RoleId { get; set; }
        string RoleName { get; set; }
        long ObjectID { get; set; }
        string ObjectName { get; set; }
        bool AllowWrite { get; set; }
        bool AllowDelete { get; set; }
        DateTime AddDate { get; set; }
        long AddUserId { get; set; }
        DateTime UpdateDate { get; set; }
        long UpdateUserId { get; set; }
        bool IsActive { get; set; }
        bool ShowActive { get; set; }
        bool ShowWrite { get; set; }
        bool ShowDelete { get; set; }
        bool IsChange { get; set; }
        long ParentObjectId { get; set; }
        Byte ObjectOrder { get; set; }
        int ObjectLevel { get; set; }
    }
}
