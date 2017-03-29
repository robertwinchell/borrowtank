using System.Collections.Generic;

namespace ASOL.HireThings.Model
{
    public class CountrySearchViewModel : BaseModel, ICountrySearchViewModel
    {
        public List<ICountryModel> CountryList { get; set; }
        public long CountryId { get; set; }
        public List<ISelectListItem> DDLCountry { get; set; }
        public long SearchId { get; set; }
        public List<ISelectListItem> DDLSearch { get; set; }
        public string TxtSearch { get; set; }        
    }
}
