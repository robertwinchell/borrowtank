using System.Collections.Generic;

namespace ASOL.HireThings.Model
{
    public class LocationSearchViewModel : BaseModel, ILocationSearchViewModel
    {
       public List<ILocationModel> LocationList { get; set; }
       public long CountryId { get; set; }
       public List<ISelectListItem> DDLCountry { get; set; }
       public long LocationId { get; set; }
       public List<ISelectListItem> DDLLocation { get; set; }
       public long SearchId { get; set; }
       public List<ISelectListItem> DDLSearch { get; set; }
       public string TxtSearch { get; set; }
       
    }
}
