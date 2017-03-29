using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface IWebAPIRoleObjectViewModel : IBaseModel
    {
        List<ISelectListItem> DDLRoleList { get; set; }
        long RoleId { get; set; }
        List<WebAPIRoleObjectModel> WebAPIRoleObjectList { get; set; }
    }
}
