using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public class WebAPIRoleObjectViewModel : BaseModel, IWebAPIRoleObjectViewModel
    {
        public List<ISelectListItem> DDLRoleList { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "Role " + ErrorMessage.RequiredMessage)]
        public long RoleId { get; set; }
        public List<WebAPIRoleObjectModel> WebAPIRoleObjectList { get; set; }
    }
}
