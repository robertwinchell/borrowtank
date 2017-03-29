using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ASOL.HireThings.Model
{
    public class AccountViewModel : BaseModel, IAccountViewModel
    {
        [EmailAddress(ErrorMessage = ErrorMessage.EmailMessage)]
        public string EmailId { get; set; }
        public string Pin { get; set; }
        public string UserId { get; set; }

        //[Required(ErrorMessage = "Email " + ErrorMessage.RequiredMessage)]
        //[Display(Name = "Email")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password " + ErrorMessage.RequiredMessage)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        //[Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Email " + ErrorMessage.RequiredMessage)]
        [EmailAddress(ErrorMessage = ErrorMessage.EmailMessage)]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "New Password " + ErrorMessage.RequiredMessage)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm New Password " + ErrorMessage.RequiredMessage)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {

        public Int64 UserId { get; set; }
        [Required(ErrorMessage = "Email " + ErrorMessage.RequiredMessage)]
        [EmailAddress(ErrorMessage = "Email "+ ErrorMessage.EmailMessage)]
        public string EmailId { get; set; }
        public string Question { get; set; }
        [Required(ErrorMessage = "Security Answer " + ErrorMessage.RequiredMessage)]
        public string Answer { get; set; }
    }

    public class ChangePasswordViewModel
    {
        public long UserId { get; set; }
        [EmailAddress(ErrorMessage = ErrorMessage.EmailMessage)]
        public string EmailId { get; set; }
        public string UserName { get; set; }
        public string PINHash { get; set; }

        [Required(ErrorMessage = "Old Password " + ErrorMessage.RequiredMessage)]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New Password " + ErrorMessage.RequiredMessage)]
        [Display(Name = "NewPassword")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm New Password " + ErrorMessage.RequiredMessage)]
        [Display(Name = "ConfirmPassword")]
        [Compare("NewPassword", ErrorMessage = "New Password and Confirm New Password do not match")]
        public string ConfirmNewPassword { get; set; }
    }


}
