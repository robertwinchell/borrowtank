using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPS.MEPSTemp.Model
{
    public interface ITempDeviceSearchModel
    {
        List<ITempDeviceModel> TempDeviceList { get; set; }
        long SystemId { get; set; }
        List<ISelectListItem> DDLSystem { get; set; }
        long OrganizationId { get; set; }
        List<ISelectListItem> DDLOrganization { get; set; }
        long SearchId { get; set; }
        List<ISelectListItem> DDLSearch { get; set; }
        string TxtSearch { get; set; }
    }
}
