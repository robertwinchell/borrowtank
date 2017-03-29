using System.Collections.Generic;

namespace ASOL.HireThings.Model
{
    public interface ILocationModel: IBaseModel
    {
        long? LocationId { get; set; }
        long CountryId { get; set; }
        string CountryName { get; set; }
        string LocationName { get; set; }
        string ZipCode { get; set; }
        bool IsActive { get; set; }
        string AddDate { get; set; }
        long AddUserId { get; set; }
        string UpdateDate { get; set; }
        long UpdateUserId { get; set; }

        List<ISelectListItem> LocationList { get; set; }
        List<ISelectListItem> CountryList { get; set; }
        

    }
}
