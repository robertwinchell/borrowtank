using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface IWebApiUserSearchViewModel : IBaseModel
    {
        List<IWebApiUser> UserList { get; set; }
        long SystemId { get; set; }
        List<ISelectListItem> DDLSystem { get; set; }
        long OrganizationId { get; set; }
        List<ISelectListItem> DDLOrganization { get; set; }
        long SearchId { get; set; }
        List<ISelectListItem> DDLSearch { get; set; }
        string TxtSearch { get; set; }
    }
}
