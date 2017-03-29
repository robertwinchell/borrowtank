using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public interface IAccountService : IBaseAsyncService<IAccountViewModel>
    {
        List<EmailServerModel> GetEmailServerList();
        bool sendChangePasswordEmail(HttpContext context, string userEmail, string callBackUrl, IEmailServerModel model);
        int IsValidateUser(string userEmail);
        bool VerifySecurityAnswer(long userId, string answer);
        void LogUserActvity(IUserLogActvityModel model);
    }
}
