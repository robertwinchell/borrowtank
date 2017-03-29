using System.Collections.Generic;

namespace ASOL.HireThings.Model
{
    public interface IDropdownModel : IBaseModel
    {
        long parameter1 { get; set; }
        List<ISelectListItem> DropdownList { get; set; }
        long CountryId { get; set; }
        long LocationId { get; set; }
        long SystemId { get; set; }
        long OrganizationId { get; set; }
        long WSId { get; set; }
        long DeviceId { get; set; }
        long ObjectLevel { get; set; }
        long ParentObjectId { get; set; }
        long OrganizationLevelId { get; set; }
    }
}
