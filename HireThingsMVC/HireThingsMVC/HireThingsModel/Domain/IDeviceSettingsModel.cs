using System.Collections.Generic;

namespace MEPS.MEPSTemp.Model
{
    public interface IDeviceSettingsModel
    {
        List<ISelectListItem> AvailableDevices { get; set; }
        List<ISelectListItem> RequestedDevices { get; set; }
        long[] AvailableSelected { get; set; }
        long[] RequestedSelected { get; set; }
    }
}
