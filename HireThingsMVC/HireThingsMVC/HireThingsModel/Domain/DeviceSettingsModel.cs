using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPS.MEPSTemp.Model
{
    public class DeviceSettingsModel : IBaseModel, IDeviceSettingsModel
    {
        public List<ISelectListItem> AvailableDevices { get; set; }
        public List<ISelectListItem> RequestedDevices { get; set; }
        public long[] AvailableSelected { get; set; }
        public long[] RequestedSelected { get; set; }
    }
    
}
