using System.Collections.Generic;

namespace ASOL.HireThings.Model
{
    public interface ICountrySearchViewModel : IBaseModel
    {
        List<ICountryModel> CountryList { get; set; }
        long CountryId { get; set; }
        List<ISelectListItem> DDLCountry { get; set; }
        long SearchId { get; set; }
        List<ISelectListItem> DDLSearch { get; set; }
        string TxtSearch { get; set; }          

    }
}
