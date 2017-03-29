using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface ISecurityInfoViewModel : IBaseModel
    {
        string UserId { get; set; }
        string EmailId { get; set; }
        long SecurityQuestionId { get; set; }
        List<ISelectListItem> DDLSecurityQuestion { get; set; }
        string Answer { get; set; }
        string ConfirmAnswer { get; set; }
        //string PIN { get; set; }
    }
}
