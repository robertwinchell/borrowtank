using System;
using System.Collections.Generic;

namespace ASOL.HireThings.Model
{
    public interface IAdvertisementModel:IBaseModel
    {

        long AdvertisementId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int IsMoreThanOne { get; set; }
        int Quantity { get; set; }
        int IsVisibleToPublic { get; set; }
        long ChargingTypeId { get; set; }
        long DefaultTimeChargingId { get; set; }
        decimal MinimumCharges { get; set; }
        decimal BondCharges { get; set; }
        decimal DepositCharges { get; set; }
        decimal FixedRateCharges { get; set; }
        string FixedRateLabel { get; set; }
        string SpecialTerms { get; set; }
        string Email { get; set; }
        string Web { get; set; }
        string PhoneNumber { get; set; }
        string Address { get; set; }

        int VisibilityType { get; set; }
        long LocationId { get; set; }
        long AdvertiserId { get; set; }

        List<ValueId> TimePrices { get; set; }
        string ChargingDetail { get; set; }
        string CategoryName { get; set; }
        string ChargingTypeName { get; set; }

        long[] CategoryIds { get; set; }
        List<ISelectListItem> CategoryList { get; set; }
        DateTime AddDate { get; set; }

    
    }
}
