using System;
using System.ComponentModel.DataAnnotations;

namespace ASOL.HireThings.Model
{
    public interface ISearchModel :  IBaseModel
    {
        long SystemId { get; set; }
        long CountryId { get; set; }
        

            long LocationId { get; set; }
        long OrganizationId { get; set; }
        long DeviceId { get; set; }
        int SearchField { get; set; }
        string SearchText { get; set; }
        long GroupId { get; set; }
        DateTime From { get; set; }
        DateTime To { get; set; }
    }

    public class SearchModel : BaseModel, ISearchModel
    {
        public long SystemId { get; set; }
        public long CountryId { get; set; }
        public long LocationId { get; set; }
        public long OrganizationId { get; set; }
        public long DeviceId { get; set; }
        public int SearchField { get; set; }
        
        [RegularExpression(RegularExpression.AlphaNumericExceptSpechialCharRegularExpression, ErrorMessage = ErrorMessage.AlphaNumericMessage)]
        public string SearchText { get; set; }
        public long GroupId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
