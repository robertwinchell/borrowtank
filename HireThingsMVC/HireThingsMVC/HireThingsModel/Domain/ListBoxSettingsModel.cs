using System.Collections.Generic;

namespace ASOL.HireThings.Model
{
    public class ListBoxSettingsModel : BaseModel, IListBoxSettingsModel
    {
        public string Identifier { get; set; }
        public List<ISelectListItem> AvailableListBoxItem { get; set; }
        public List<ISelectListItem> RequestedListBoxItem { get; set; }
        public long[] AvailableSelected { get; set; }
        public long[] RequestedSelected { get; set; }
    }
    
}
