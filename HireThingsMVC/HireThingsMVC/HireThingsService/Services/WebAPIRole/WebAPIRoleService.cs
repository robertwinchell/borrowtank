using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using ASOL.HireThings.Model;
using ASOL.HireThings.Core;
using Microsoft.AspNet.Identity;

namespace ASOL.HireThings.Service
{
    public class WebApiRoleService : BaseService<IWebApiRoleModel>, IWebApiRoleService
    {
        WebApiRoleDAL _dal = null;
        long userId = Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public WebApiRoleService()
        {
            _dal = new WebApiRoleDAL();
        }

        public async Task<IWebApiRoleModel> IndexAsync(HttpContext context, long roleId, CancellationToken cancellationToken)
        {
            IWebApiRoleModel model = await _dal.SelectByIDAsync(roleId, userId, cancellationToken);
            return model;
        }

        public async Task<IWebApiRoleModel> SaveAsync(HttpContext context, IWebApiRoleModel model, CancellationToken cancellationToken)
        {
            model.UserId = userId;

            if (model.WARoleId > 0)
            {
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(await _dal.UpdateAsync(model, cancellationToken));
                context.Items[Constant.FormTitle] = "EDIT Role";
            }
            else
            {
                model.WARoleId = await _dal.SaveAsync(model, cancellationToken);
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(model.WARoleId);
                context.Items[Constant.FormTitle] = "ADD Role";
            }

            return model;
        }
    }
}
