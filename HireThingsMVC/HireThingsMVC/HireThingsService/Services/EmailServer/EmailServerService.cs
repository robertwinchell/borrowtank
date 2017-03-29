using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using System;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Service
{
    public class EmailServerService : BaseService<IEmailServerModel>, IEmailServerService
    {
        EmailServerDAL _dal = null;
        long userId = Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public EmailServerService()
        {
            _dal = new EmailServerDAL();         
        }

        #region
        public async override Task<IEmailServerModel> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            IEmailServerModel model = new EmailServerModel();
            return model;
        }

        public async Task<IEmailServerModel> IndexAsync(HttpContext context, string EmailServerId, CancellationToken cancellationToken)
        {
            IEmailServerModel model = await _dal.SelectByIDAsync(Convert.ToInt64(EmailServerId), userId, cancellationToken);
            return model;
        }

        public async Task<IEmailServerModel> SaveAsync(HttpContext context, IEmailServerModel model, CancellationToken cancellationToken)
        {
            model.UserId = Convert.ToInt64(context.User.Identity.GetUserId());

            if (model.ServerId > 0)
            {
                if (model.UserPswd.Replace("*", "").Trim().Length == 0)
                    model.UserPswd = "";
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(await _dal.UpdateAsync(model, cancellationToken));
                if (model.UserPswd.Length == 0)
                    model.UserPswd = "**********";
                context.Items[Constant.FormTitle] = "EDIT Email Server";
            }
            else
            {
                model.ServerId = await _dal.SaveAsync(model, cancellationToken);
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(model.ServerId);
                context.Items[Constant.FormTitle] = "ADD Email Server";
            }

            return model;
        }
        #endregion


    }
}
