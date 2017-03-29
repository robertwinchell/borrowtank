using System.Collections.Generic;

namespace ASOL.HireThings.Model
{
    public class DropdownModel : BaseModel, IDropdownModel
    {
        public long parameter1 { get; set; }
        public List<ISelectListItem> DropdownList { get; set; }
        public long CountryId { get; set; }
        public long LocationId { get; set; }
        public long SystemId { get; set; }
        public long OrganizationId { get; set; }
        public long WSId { get; set; }
        public long DeviceId { get; set; }
        public long ObjectLevel { get; set; }
        public long ParentObjectId { get; set; }
        public long OrganizationLevelId { get; set; }
    }
}
