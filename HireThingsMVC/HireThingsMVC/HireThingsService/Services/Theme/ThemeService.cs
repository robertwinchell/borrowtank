using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using System;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Service
{
    public class ThemeService : BaseService<IThemeModel>, IThemeService
    {
        ThemeDAL _dal = null;
        long userId = Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        #region
        public async override Task<IThemeModel> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            IThemeModel model = new ThemeModel();
            return model;
        }

        public async Task<IThemeModel> IndexAsync(HttpContext context, string ThemeId, CancellationToken cancellationToken)
        {
            _dal = new ThemeDAL();
            IThemeModel model = await _dal.SelectByIDAsync(Convert.ToInt64(ThemeId), userId, cancellationToken);
            return model;
        }

        public async Task<IThemeModel> SaveAsync(HttpContext context, IThemeModel model, CancellationToken cancellationToken)
        {
            _dal = new ThemeDAL();

            model.UserId = Convert.ToInt64(context.User.Identity.GetUserId());

            if (model.ThemeId > 0)
            {
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(await _dal.UpdateAsync(model, cancellationToken));
                context.Items[Constant.FormTitle] = "EDIT Probe Type";
            }
            else
            {
                model.ThemeId = await _dal.SaveAsync(model, cancellationToken);
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(model.ThemeId);
                context.Items[Constant.FormTitle] = "ADD Probe Type";
            }

            return model;
        }
        #endregion
    }

}
