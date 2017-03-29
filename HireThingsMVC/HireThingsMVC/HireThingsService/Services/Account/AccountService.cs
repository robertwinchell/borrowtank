using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public class AccountService : BaseService<IAccountViewModel>, IAccountService
    {
        TempUserDAL _dal;

        public AccountService()
        {
            _dal = new TempUserDAL();
        }

        public async override Task<IAccountViewModel> IndexAsync(HttpContext context, CancellationToken cancellationToken)
        {
            IAccountViewModel model = new AccountViewModel();
            return model;
        }

        public bool sendChangePasswordEmail(HttpContext context, string userEmail, string callBackUrl, IEmailServerModel model)
        {
            model.UserName = context.User.Identity.GetUserName();
            IEmailLogModel emailLog = new EmailLogModel { To = userEmail };
            if (this.SendEmail(emailLog, callBackUrl, model, Constant.EmailType.ChangePassword) == 1)
            {
                context.Items[Constant.QuerySuccess] = true;
                return true;
            }
            else
            {
                context.Items[Constant.QuerySuccess] = false;
                return false;
            }

        }

        public int IsValidateUser(string userEmail)
        {
            string question = "";
            Int64 userId = 0;
            int retVal;

            retVal = _dal.IsUserValidPoulateQuestion(userEmail, ref question, ref userId);
            if (retVal == 2)
            {
                //Question = question;
                //UserId = userId;
                //UserEmail = userEmail;
            }

            return retVal;
        }

        /// <summary>
        /// Return True if Security Answer for given user is correct 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        public bool VerifySecurityAnswer(long userId, string answer)
        {
            return _dal.VerifySecurityAnswer(userId, answer);

        }

        public void LogUserActvity(IUserLogActvityModel model)
        {
            AccountDAL _accdal = new AccountDAL();
            _accdal.LogUserActvity(model);
        }        

    }
}
