using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public class WebApiUserSearchViewModel : BaseModel, IWebApiUserSearchViewModel
    {
        public List<IWebApiUser> UserList { get; set; }
        public long SystemId { get; set; }
        public List<ISelectListItem> DDLSystem { get; set; }
        public long OrganizationId { get; set; }
        public List<ISelectListItem> DDLOrganization { get; set; }
        public long SearchId { get; set; }
        public List<ISelectListItem> DDLSearch { get; set; }
        public string TxtSearch { get; set; }
    }
}
