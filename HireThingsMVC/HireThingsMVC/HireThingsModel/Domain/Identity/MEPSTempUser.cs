using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public class HireThingsUser : BaseModel, IHireThingsUser
    {
        public string Id { get; set; }
        public long CountryId { get; set; }
        public long LocationId { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "Role " + ErrorMessage.RequiredMessage)]
        public long RoleId { get; set; }

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

        [Required(ErrorMessage = "Confirm Email " + ErrorMessage.RequiredMessage)]
        [EmailAddress(ErrorMessage = "Confirm Email " + ErrorMessage.EmailMessage)]
        [Compare("EmailId", ErrorMessage = "Email and Confirm Email do not match.")]
        public string ConfirmEmailId { get; set; }

        public string PhoneNo { get; set; }

        public string Address1 { get; set; }

        [RegularExpression(RegularExpression.LetterRegularExpression, ErrorMessage = "Field " + ErrorMessage.LetterMessage)]
        public string Address2 { get; set; }

        [RegularExpression(RegularExpression.LetterRegularExpression, ErrorMessage = "Field " + ErrorMessage.LetterMessage)]
        public string Address3 { get; set; }

        [Required(ErrorMessage = "Password " + ErrorMessage.RequiredMessage)]
        public string Pin { get; set; }

        [Required(ErrorMessage = "Confirm Password " + ErrorMessage.RequiredMessage)]
        [Compare("Pin", ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPin { get; set; }

        public string PINHash { get; set; }
        public string RFIDPIN { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "Security Question " + ErrorMessage.RequiredMessage)]
        public long SecurityQuestionID { get; set; }
        [Required(ErrorMessage = "Security Answer " + ErrorMessage.RequiredMessage)]
        public string SecurityAnswer { get; set; }
        public bool IsActive { get; set; }
        public bool IsUserLocked { get; set; }
        public DateTime LockedDate { get; set; }
        public string LockedReason { get; set; }
        
        public long AddUserId { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public DateTime EmailConfirmDate { get; set; }
       // [Range(1, long.MaxValue, ErrorMessage = "Gender " + ErrorMessage.RequiredMessage)]
        public long GenderId { get; set; }

        public string Gender { get; set; }

        //[Required(ErrorMessage = "Date of Birth " + ErrorMessage.RequiredMessage)]
        public DateTime DOB { get; set; }

        public string CountryName { get; set; }
        public string LocationName { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
       
        public bool IsSystemGenerated { get; set; }

        public List<ISelectListItem> CountryList { get; set; }
        public List<ISelectListItem> LocationList { get; set; }        
        public List<ISelectListItem> RoleList { get; set; }
        public List<ISelectListItem> GenderList { get; set; }
        public List<ISelectListItem> SecurityQuestionList { get; set; }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<IHireThingsUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }
}