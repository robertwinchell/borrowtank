using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface IWebApiUser : IBaseModel
    {
        string WAUserId { get; set; }
        long SystemId { get; set; }
        long OrganizationId { get; set; }
        long WARoleId { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string EmailId { get; set; }
        string ConfirmEmailId { get; set; }
        string PhoneNo { get; set; }
        string Address1 { get; set; }
        string Address2 { get; set; }
        string Address3 { get; set; }
        string Pin { get; set; }
        string ConfirmPin { get; set; }
        string PINHash { get; set; }
        string RFIDPIN { get; set; }        
        bool IsActive { get; set; }
        bool IsUserLocked { get; set; }
        DateTime LockedDate { get; set; }
        string LockedReason { get; set; }
        long TimeZoneId { get; set; }
        long AddUserId { get; set; }
        DateTime AddDate { get; set; }
        DateTime UpdateDate { get; set; }
        long UpdateUserId { get; set; }       
        string SystemName { get; set; }
        string OrganizationName { get; set; }
        string RoleName { get; set; }
        string UserName { get; set; }
        long GroupId { get; set; }
        bool IsSystemGenerated { get; set; }

        List<ISelectListItem> SystemList { get; set; }
        List<ISelectListItem> OrganizationList { get; set; }
        List<ISelectListItem> TimeZoneList { get; set; }
        List<ISelectListItem> RoleList { get; set; }
        ListBoxSettingsModel DeviceGropList { get; set; }
    }
}
