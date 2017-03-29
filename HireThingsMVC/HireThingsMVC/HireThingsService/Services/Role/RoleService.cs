using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using System;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Service
{
    public class RoleService: BaseService<IRoleModel>, IRoleService
    {
        RoleDAL _dal = null;
        DropDownListDAL _ddlDal = null;
        long userId = Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public RoleService()
        {
            _dal = new RoleDAL();
            _ddlDal = new DropDownListDAL();
        }

        #region
        public async override Task<IRoleModel> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            IRoleModel model = new RoleModel();
            return model;
        }

        public async Task<IRoleModel> IndexAsync(System.Web.HttpContext context, long probManfId, CancellationToken cancellationToken)
        {
            IRoleModel model = await _dal.SelectByIDAsync(probManfId, userId, cancellationToken);
            return model;
        }

        public async Task<IRoleModel> SaveAsync(System.Web.HttpContext context, IRoleModel model, CancellationToken cancellationToken)
        {
            model.UserId = userId;

            if (model.RoleId > 0)
            {
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(await _dal.UpdateAsync(model, cancellationToken));
                context.Items[Constant.FormTitle] = "EDIT Role";
            }
            else
            {
                model.RoleId =  await _dal.SaveAsync(model, cancellationToken);
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(model.RoleId);
                context.Items[Constant.FormTitle] = "ADD Role";
            }

            return model;
        }
        #endregion

    }
}
