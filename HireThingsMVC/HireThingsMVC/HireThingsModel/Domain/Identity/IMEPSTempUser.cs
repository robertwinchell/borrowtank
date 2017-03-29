using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface IHireThingsUser : IBaseModel, IUser
    {
        string Id { get; set; }
        long CountryId { get; set; }
        long LocationId { get; set; }
        long RoleId { get; set; }
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
        long SecurityQuestionID { get; set; }
        string SecurityAnswer { get; set; }
        bool IsActive { get; set; }
        bool IsUserLocked { get; set; }
        DateTime LockedDate { get; set; }
        string LockedReason { get; set; }
        
        long AddUserId { get; set; }
        DateTime AddDate { get; set; }
        DateTime UpdateDate { get; set; }
        long UpdateUserId { get; set; }
        bool IsEmailConfirmed { get; set; }
        DateTime EmailConfirmDate { get; set; }
        long GenderId { get; set; }
        string Gender { get; set; }
        DateTime DOB { get; set; }

        string CountryName { get; set; }
        string LocationName { get; set; }
        string RoleName { get; set; }
        string UserName { get; set; }
       
        bool IsSystemGenerated { get; set; }

        List<ISelectListItem> CountryList { get; set; }
        List<ISelectListItem> LocationList { get; set; }
        
        List<ISelectListItem> RoleList { get; set; }
        List<ISelectListItem> GenderList { get; set; }
        List<ISelectListItem> SecurityQuestionList { get; set; }
        
        Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<IHireThingsUser> manager);
    }
}
