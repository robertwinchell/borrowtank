using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using System;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Service
{
    public class WebAPIRoleObjectService : BaseService<IWebAPIRoleObjectViewModel>, IWebAPIRoleObjectService
    {
        WebAPIRoleObjectDAL _dal = null;
        DropDownListDAL _ddlDAL = null;
        long userId = System.Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public WebAPIRoleObjectService()
        {
            _dal = new WebAPIRoleObjectDAL();
            _ddlDAL = new DropDownListDAL();
           
        }

        
        #region
        public async override Task<IWebAPIRoleObjectViewModel> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            IWebAPIRoleObjectViewModel model = await PopulateInitialValuesAsync(null, cancellationToken);
            model = await SearchDataAsync(-1,model, cancellationToken);
            return model;
        }

        public async Task<IWebAPIRoleObjectViewModel> IndexAsync(System.Web.HttpContext context, long roleId, SearchModel searchModel, CancellationToken cancellationToken)
        {

            IWebAPIRoleObjectViewModel tdmodel = await SearchDataAsync(roleId, null, cancellationToken, searchModel);
            return tdmodel;
        }

        public async Task<IWebAPIRoleObjectViewModel> SaveAsync(System.Web.HttpContext context, IWebAPIRoleObjectViewModel model, CancellationToken cancellationToken)
        {
            model.UserId = userId;

            context.Items[Constant.QuerySuccess] = Convert.ToBoolean(await _dal.SaveAsync(model, cancellationToken));
           await PopulateInitialValuesAsync(model, cancellationToken);
           await SearchDataAsync(model.RoleId, model, cancellationToken);
            return model;
        }

        private async Task<IWebAPIRoleObjectViewModel> SearchDataAsync(long roleId, IWebAPIRoleObjectViewModel model, CancellationToken cancellationToken, SearchModel searchModel = null)
        {
            if (searchModel == null)
            {
                searchModel = new SearchModel();
            }

            if (model == null)
            {
                model = await PopulateInitialValuesAsync(model, cancellationToken);
            }

            model.WebAPIRoleObjectList = await _dal.SelectAllObjectAsync(roleId, cancellationToken, model.GetPageInfo(searchModel.CPage, searchModel.TPage, searchModel.TRows, searchModel.PSize, searchModel.UserId)); ;
            return model;
        }

        private async Task<IWebAPIRoleObjectViewModel> PopulateInitialValuesAsync(IWebAPIRoleObjectViewModel model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                model = new WebAPIRoleObjectViewModel();
            }
            model.DDLRoleList = await _ddlDAL.GetWebAPIRoleListAsync(userId, cancellationToken, true);
            return model;
        }

        #endregion
    }
}
