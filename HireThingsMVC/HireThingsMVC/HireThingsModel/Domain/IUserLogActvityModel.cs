using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface IUserLogActvityModel : IBaseModel
    {
        long ActivityId { get; set; }
        long SystemId { get; set; }
        long OrganizationId { get; set; }
        long TempidDeviceId { get; set; }
        long WSId { get; set; }
        DateTime LoginTime { get; set; }
        DateTime LogoutTime { get; set; }
        Int16 LoginType { get; set; }
        Int16 LogoutType { get; set; }
        Int32 ModuleId { get; set; }
        string ModuleType { get; set; }
        string ActivityForm { get; set; }
        string SessionId { get; set; }
        string IPAddress { get; set; }
    }
}
