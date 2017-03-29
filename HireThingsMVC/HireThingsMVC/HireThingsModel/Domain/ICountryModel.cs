

namespace ASOL.HireThings.Model
{
    public interface ICountryModel : IBaseModel
    {
        long? CountryId { get; set; }
        string CountryName { get; set; }
        bool IsActive { get; set; }

    }
}
