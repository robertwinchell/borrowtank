using System.Collections.Generic;

namespace ASOL.HireThings.Model
{
    public interface IListBoxSettingsModel
    {
        List<ISelectListItem> AvailableListBoxItem { get; set; }
        List<ISelectListItem> RequestedListBoxItem { get; set; }
        long[] AvailableSelected { get; set; }
        long[] RequestedSelected { get; set; }
    }
}
