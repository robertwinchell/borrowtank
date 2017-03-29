using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public class WebApiUser : BaseModel, IWebApiUser
    {
        public string WAUserId { get; set; }
        public long SystemId { get; set; }
        public long OrganizationId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Role " + ErrorMessage.RequiredMessage)]
        public long WARoleId { get; set; }

        [Required(ErrorMessage = "First Name " + ErrorMessage.RequiredMessage)]
        public string FirstName { get; set; }

        [RegularExpression(RegularExpression.LetterRegularExpression, ErrorMessage = "Middle Name " + ErrorMessage.LetterMessage)]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name " + ErrorMessage.RequiredMessage)]
        [RegularExpression(RegularExpression.LetterRegularExpression, ErrorMessage = "Last Name " + ErrorMessage.LetterMessage)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email " + ErrorMessage.RequiredMessage)]
        [EmailAddress(ErrorMessage = "Email " + ErrorMessage.EmailMessage)]
        public string EmailId { get; set; }

        [Compare("EmailId", ErrorMessage = "Confirm Email doesn't match, Type again !")]
        public string ConfirmEmailId { get; set; }

        public string PhoneNo { get; set; }        
        public string Address1 { get; set; }

        [RegularExpression(RegularExpression.LetterRegularExpression, ErrorMessage = "Field " + ErrorMessage.LetterMessage)]
        public string Address2 { get; set; }

        [RegularExpression(RegularExpression.LetterRegularExpression, ErrorMessage = "Field " + ErrorMessage.LetterMessage)]
        public string Address3 { get; set; }

        [Required(ErrorMessage = "Password " + ErrorMessage.RequiredMessage)]
        public string Pin { get; set; }

        [Compare("Pin", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPin { get; set; }

        public string PINHash { get; set; }
        public string RFIDPIN { get; set; }
        public bool IsActive { get; set; }
        public bool IsUserLocked { get; set; }
        public DateTime LockedDate { get; set; }
        public string LockedReason { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Time Zone " + ErrorMessage.RequiredMessage)]
        public long TimeZoneId { get; set; }
        public long AddUserId { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public string SystemName { get; set; }
        public string OrganizationName { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public long GroupId { get; set; }
        public bool IsSystemGenerated { get; set; }

        public List<ISelectListItem> SystemList { get; set; }
        public List<ISelectListItem> OrganizationList { get; set; }
        public List<ISelectListItem> TimeZoneList { get; set; }
        public List<ISelectListItem> RoleList { get; set; }
        public List<ISelectListItem> GenderList { get; set; }
        public ListBoxSettingsModel DeviceGropList { get; set; }
    }
}
