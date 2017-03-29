using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ASOL.HireThings.Model
{
    public class CountryModel : BaseModel, ICountryModel
    {
        public long? CountryId { get; set; }

       
        [Required(ErrorMessage = "Country Name " + ErrorMessage.RequiredMessage)]
        [RegularExpression(RegularExpression.AlphaNumericRegularExpression, ErrorMessage = ErrorMessage.AlphaNumericMessage)]
        public string CountryName { get; set; }

       

        public bool IsActive { get; set; }
       
    }
}
