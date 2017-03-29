using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASOL.HireThings.Model
{
    public class LocationModel : BaseModel, ILocationModel
    {
        public long? LocationId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Country " + ErrorMessage.RequiredMessage)]
        public long CountryId { get; set; }
        public string CountryName { get; set; }
        
        [Required(ErrorMessage = "Location Name " + ErrorMessage.RequiredMessage)]
        public string LocationName { get; set; }

        [Required(ErrorMessage = "Zip Code " + ErrorMessage.RequiredMessage)]
        [RegularExpression(RegularExpression.ZipCodeRegularExpression, ErrorMessage = "Zip Code is " + ErrorMessage.ZipCodeMessage)]
        public string ZipCode { get; set; }

        public bool IsActive { get; set; }
        public string AddDate { get; set; }
        public long AddUserId { get; set; }
        public string UpdateDate { get; set; }
        public long UpdateUserId { get; set; }

        public List<ISelectListItem> LocationList { get; set; }
        public List<ISelectListItem> CountryList { get; set; }
    }
}
