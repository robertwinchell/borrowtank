using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public interface IUserService :  IBaseAsyncService<IHireThingsUser>
    {        
        ForgotPasswordViewModel IsValidateUser(ForgotPasswordViewModel model, ref int retVal);
        bool ChangePassword(HttpContext context, ChangePasswordViewModel model);
        bool sendResetPasswordEmail(HttpContext context, ResetPasswordViewModel model);
        bool sendEmail(HttpContext context, string userEmail, string callBackUrl, IEmailServerModel model, Constant.EmailType emailType, long userId = 0, long systemId = 0);

        Task<IHireThingsUser> IndexPublicAsync(HttpContext context, CancellationToken cancellationToken);
        Task<IHireThingsUser> IndexAsync(HttpContext context, long UserId, CancellationToken cancellationToken);
        Task<IHireThingsUser> IndexAsync(HttpContext context, IHireThingsUser model, CancellationToken cancellationToken);
        Task<IHireThingsUser> SaveAsync(HttpContext context, IHireThingsUser model, CancellationToken cancellationToken);
        Task<IHireThingsUser> ProfileUpdateAsync(HttpContext context, IHireThingsUser model, CancellationToken cancellationToken);
        
    }
}
