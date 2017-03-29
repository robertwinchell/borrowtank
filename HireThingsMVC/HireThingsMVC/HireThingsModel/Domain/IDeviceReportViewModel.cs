using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface IDeviceReportViewModel:IBaseModel
    {
        List<ISelectListItem> SystemList { get; set; }
        List<ISelectListItem> OrganizationList { get; set; }
        List<ISelectListItem> WorkstationList { get; set; }
        List<ISelectListItem> DeviceList { get; set; }

        Int64 SystemId { get; set; }
        Int64 OrganizationId { get; set; }
        Int64 WorkstationId { get; set; }
        Int64 DeviceId { get; set; }

        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
    }
}
