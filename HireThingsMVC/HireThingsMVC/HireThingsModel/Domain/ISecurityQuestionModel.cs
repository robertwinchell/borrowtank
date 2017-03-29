using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface ISecurityQuestionModel:IBaseModel
    {
        long? SecurityQuestionId { get; set; }
        string Question { get; set; }
        bool IsActive { get; set; }
        DateTime AddDate { get; set; }
        Int64 AddUserID { get; set; }
        DateTime UpdateDate { get; set; }
        Int64 UpdateUserID { get; set; }

    }
}
