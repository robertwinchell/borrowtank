using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASOL.HireThings.Model
{
    public class SecurityInfoViewModel : BaseModel, ISecurityInfoViewModel
    {
        public string UserId { get; set; }
        public string EmailId { get; set; }
        public long SecurityQuestionId { get; set; }
        public List<ISelectListItem> DDLSecurityQuestion { get; set; }

        [Required(ErrorMessage = "Security Answer " + ErrorMessage.RequiredMessage)]
        [Display(Name = "Answer")]
        public string Answer { get; set; }

        [Required(ErrorMessage = "Confirm Security Answer " + ErrorMessage.RequiredMessage)]
        [Compare("Answer", ErrorMessage = "Answer and Confirm Answer do not match")]
        public string ConfirmAnswer { get; set; }

        //[Required(ErrorMessage = "Password " + ErrorMessage.RequiredMessage)]
        //public string PIN { get; set; }
    }
}
