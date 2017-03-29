using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public class SecurityQuestionModel : BaseModel, ISecurityQuestionModel
    {
        public long? SecurityQuestionId { get; set; }

        [Required(ErrorMessage = "Security Question " + ErrorMessage.RequiredMessage)]
        [RegularExpression(RegularExpression.AlphaNumericAndQuestionMarkRegularExpression, ErrorMessage = ErrorMessage.AlphaNumericMessage)]
        public string Question { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddDate { get; set; }
        public Int64 AddUserID { get; set; }
        public DateTime UpdateDate { get; set; }
        public Int64 UpdateUserID { get; set; }
    }
}
