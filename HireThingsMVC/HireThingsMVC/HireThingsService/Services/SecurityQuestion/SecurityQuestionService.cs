using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using System;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Service
{
    public class SecurityQuestionService : BaseService<ISecurityQuestionModel>, ISecurityQuestionService
    {
        SecurityQuestionDAL _dal = null;
        DropDownListDAL _ddlDal = null;
        long userId = Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public SecurityQuestionService()
        {
            _dal = new SecurityQuestionDAL();
            _ddlDal = new DropDownListDAL();
        }

        #region
        public async override Task<ISecurityQuestionModel> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            ISecurityQuestionModel model = new SecurityQuestionModel();
            return model;
        }

        public async Task<ISecurityQuestionModel> IndexAsync(System.Web.HttpContext context, long securityQuestionId, CancellationToken cancellationToken)
        {
            ISecurityQuestionModel model = await _dal.SelectByIDAsync(securityQuestionId, userId, cancellationToken);
            return model;
        }

        public async Task<ISecurityQuestionModel> SaveAsync(System.Web.HttpContext context, ISecurityQuestionModel model, CancellationToken cancellationToken)
        {
            model.UserId = Convert.ToInt64(context.User.Identity.GetUserId());

            if (model.SecurityQuestionId > 0)
            {
                context.Items[Constant.QuerySuccess] = await _dal.UpdateAsync(model, cancellationToken);
                context.Items[Constant.FormTitle] = "EDIT Security Question";
            }
            else
            {
                model.SecurityQuestionId = await _dal.SaveAsync(model, cancellationToken);
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(model.SecurityQuestionId);
                context.Items[Constant.FormTitle] = "ADD Security Question";
            }
            return model;
        }
      #endregion
    }
}
