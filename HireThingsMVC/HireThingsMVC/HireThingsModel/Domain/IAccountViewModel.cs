using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface IAccountViewModel : IBaseModel
    {
        string EmailId { get; set; }
        string Pin { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
        bool RememberMe { get; set; }
    }
}
