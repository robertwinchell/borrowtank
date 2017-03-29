using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ASOL.HireThings.Model
{
    public class AdvertisementModel : BaseModel, IAdvertisementModel
    {

        public long AdvertisementId { get; set; }

        [Required(ErrorMessage = "Advertisement Name " + ErrorMessage.RequiredMessage)]
        [RegularExpression(RegularExpression.AlphaNumericRegularExpression, ErrorMessage = ErrorMessage.AlphaNumericMessage)]

        public string Name { get; set; }
        [RegularExpression(RegularExpression.AlphaNumericRegularExpression, ErrorMessage = ErrorMessage.AlphaNumericMessage)]

        [Required(ErrorMessage = "Description " + ErrorMessage.RequiredMessage)]
        public string Description { get; set; }
 
        public int IsMoreThanOne { get; set; }
        
        public int Quantity { get; set; }
       
        public int IsVisibleToPublic { get; set; }

        public long ChargingTypeId { get; set; }

        public long DefaultTimeChargingId { get; set; }

        public decimal MinimumCharges { get; set; }
        public decimal BondCharges { get; set; }
        public decimal DepositCharges { get; set; }
        public decimal FixedRateCharges { get; set; }
        public string FixedRateLabel { get; set; }
        [Required(ErrorMessage = "Special Terms " + ErrorMessage.RequiredMessage)]
        [RegularExpression(RegularExpression.AlphaNumericRegularExpression, ErrorMessage = ErrorMessage.AlphaNumericMessage)]

        public string SpecialTerms { get; set; }
        [Required(ErrorMessage = "Email " + ErrorMessage.RequiredMessage)]

        public string Email { get; set; }

       
        public string Web { get; set; }
        [Required(ErrorMessage = "Phone Number " + ErrorMessage.RequiredMessage)]
        [RegularExpression(RegularExpression.NumberRegularExpression, ErrorMessage = ErrorMessage.NumberMessage)]

        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address " + ErrorMessage.RequiredMessage)]
        [RegularExpression(RegularExpression.AlphaNumericRegularExpression, ErrorMessage = ErrorMessage.AlphaNumericMessage)]
        public string Address { get; set; }

        public int VisibilityType { get; set; }
        public long LocationId { get; set; }
        public long AdvertiserId { get; set; }

        public List<ValueId> TimePrices { get; set; }

        public string ChargingDetail { get; set; }
        public string CategoryName { get; set; }
        public string ChargingTypeName { get; set; }
        [Required(ErrorMessage = "Category  " + ErrorMessage.RequiredMessage)]
        public long[] CategoryIds { get; set; }
        public List<ISelectListItem> CategoryList { get; set; }

        public DateTime AddDate { get; set; }


    }
}
