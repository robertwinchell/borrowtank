using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public class SecurityInfoService : BaseService<ISecurityInfoViewModel>, ISecurityInfoService
    {
        SecurityInfoDAL _dal;
        DropDownListDAL _ddlDAL;
        long userId = Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public SecurityInfoService()
        {
            _dal = new SecurityInfoDAL();
            _ddlDAL = new DropDownListDAL();
        }

        #region
        public async override Task<ISecurityInfoViewModel> IndexAsync(HttpContext context, CancellationToken cancellationToken)
        {
            long securityQuestionId = 0;
            ISecurityInfoViewModel model = new SecurityInfoViewModel();
            model.DDLSecurityQuestion = _ddlDAL.GetSecurityQuestionList(userId, ref securityQuestionId, false);
            model.Answer = "";
            model.SecurityQuestionId = securityQuestionId;

            return model;
        }

        public async Task<ISecurityInfoViewModel> UpdateAsync(HttpContext context, SecurityInfoViewModel model, CancellationToken cancellationToken)
        {
            long securityQuestionId = 0;
            model.UserId = context.User.Identity.GetUserId();
            model.EmailId = Convert.ToString(context.Session[Constant.EmailId]);

            context.Items[Constant.QuerySuccess] = Convert.ToBoolean(await _dal.UpdateAsync(model, cancellationToken));
            model.DDLSecurityQuestion = _ddlDAL.GetSecurityQuestionList(Convert.ToInt64(model.UserId), ref securityQuestionId, false);
            return model;
        }
        #endregion
    }
}
