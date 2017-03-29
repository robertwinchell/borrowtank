using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
   public class EmailServerModel: BaseModel, IEmailServerModel
    {
        public long? ServerId { get; set; }

        [Required(ErrorMessage = "Server Name " + ErrorMessage.RequiredMessage)]
        [RegularExpression(RegularExpression.AlphaNumericRegularExpressionWithDash, ErrorMessage = ErrorMessage.AlphaNumericMessage)]
        public string ServerName { get; set; }

        [Required(ErrorMessage = "SMTP Server " + ErrorMessage.RequiredMessage)]
        public string ServerIP { get; set; }

        [Required(ErrorMessage = "Email Port " + ErrorMessage.RequiredMessage)]
        [Range(1,99999999,ErrorMessage="Email Port cannot be less then 1")]
        [RegularExpression(RegularExpression.NumberRegularExpression, ErrorMessage = "Email Port "+ErrorMessage.NumberMessage)]
        public Int32 EmailPort { get; set; }

        [Required(ErrorMessage = "User Name " +  ErrorMessage.RequiredMessage)]
        [EmailAddress(ErrorMessage = "User Name " + ErrorMessage.EmailMessage)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password " + ErrorMessage.RequiredMessage)]
        public string UserPswd { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool EnableSSL { get; set; }
        public bool IsActive { get; set; }
        public DateTime UpdateDate { get; set; }
        public long UpdateUserID { get; set; }
        public DateTime AddDate { get; set; }
        public long AddUserID { get; set; }
        
        [Range(1, 255, ErrorMessage = "Priority can only be in 1-255 range")]
        [RegularExpression(RegularExpression.NumberRegularExpression, ErrorMessage = "Priority "+ErrorMessage.NumberMessage)]
        [Required(ErrorMessage = "Priority " + ErrorMessage.RequiredMessage)]
        public Byte Priority { get; set; }

        public List<EmailServerModel> SysEmailServers { get; set; }
    }
}
