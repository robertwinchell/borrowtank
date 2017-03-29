using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPS.MEPSTemp.Model
{
    public interface IOrganizationViewModel: IBaseModel
    {
        long OrganizationId { get; set; }
        long CustomerOrganizationId { get; set; }
        long SystemId { get; set; }
        long BusinessTypeId { get; set; }
        string OrganizationRefNo { get; set; }
        string OrganizationName { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string Address { get; set; }
        string City { get; set; }
        string ZipCode { get; set; }
        string State { get; set; }
        string Country { get; set; }
        string Phone { get; set; }
        string Fax { get; set; }
        string Email { get; set; }
        string Website { get; set; }
        long TimeZoneID { get; set; }
        long ParentOrganizationID { get; set; }
        long OrganizationLevelID { get; set; }
        string DEANo { get; set; }
        bool IsActive { get; set; }
        DateTime AddDate { get; set; }
        long AddUserId { get; set; }
        DateTime UpdateDate { get; set; }
        long UpdateUserId { get; set; }
    }
}
