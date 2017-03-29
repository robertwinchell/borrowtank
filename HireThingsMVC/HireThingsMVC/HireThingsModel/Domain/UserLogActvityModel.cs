using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public class UserLogActvityModel : BaseModel, IUserLogActvityModel
    {
        public long ActivityId { get; set; }
        public long SystemId { get; set; }
        public long OrganizationId { get; set; }
        public long TempidDeviceId { get; set; }
        public long WSId { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LogoutTime { get; set; }
        public Int16 LoginType { get; set; }
        public Int16 LogoutType { get; set; }
        public Int32 ModuleId { get; set; }
        public string ModuleType { get; set; }
        public string ActivityForm { get; set; }
        public string SessionId { get; set; }
        public string IPAddress { get; set; }

    }
}
