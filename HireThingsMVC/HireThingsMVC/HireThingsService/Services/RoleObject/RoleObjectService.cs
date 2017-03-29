using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using System;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Service
{
    public class RoleObjectService : BaseService<IRoleObjectViewModel>, IRoleObjectService
    {
        RoleObjectDAL _dal = null;
        DropDownListDAL _ddlDAL = null;
        long userId = System.Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public RoleObjectService()
        {
            _dal = new RoleObjectDAL();
            _ddlDAL = new DropDownListDAL();
           
        }

        
        #region
        public async override Task<IRoleObjectViewModel> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            IRoleObjectViewModel model = await PopulateInitialValuesAsync(null, cancellationToken);
            model = await SearchDataAsync(-1,model, cancellationToken);
            return model;
        }

        public async Task<IRoleObjectViewModel> IndexAsync(System.Web.HttpContext context, long roleId, SearchModel searchModel, CancellationToken cancellationToken)
        {

            IRoleObjectViewModel tdmodel = await SearchDataAsync(roleId, null, cancellationToken, searchModel);
            return tdmodel;
        }

        public async Task<IRoleObjectViewModel> SaveAsync(System.Web.HttpContext context, IRoleObjectViewModel model, CancellationToken cancellationToken)
        {
            model.UserId = userId;

            context.Items[Constant.QuerySuccess] = Convert.ToBoolean(await _dal.SaveAsync(model, cancellationToken));
           await PopulateInitialValuesAsync(model, cancellationToken);
           await SearchDataAsync(model.RoleId, model, cancellationToken);
            return model;
        }

        private async Task<IRoleObjectViewModel> SearchDataAsync(long roleId, IRoleObjectViewModel model, CancellationToken cancellationToken, SearchModel searchModel = null)
        {
            if (searchModel == null)
            {
                searchModel = new SearchModel();
            }

            if (model == null)
            {
                model = await PopulateInitialValuesAsync(model, cancellationToken);
            }

            model.RoleObjectList = await _dal.SelectAllObjectAsync(roleId, cancellationToken, model.GetPageInfo(searchModel.CPage, searchModel.TPage, searchModel.TRows, searchModel.PSize, searchModel.UserId)); ;
            return model;
        }

        private async Task<IRoleObjectViewModel> PopulateInitialValuesAsync(IRoleObjectViewModel model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                model = new RoleObjectViewModel();
            }
            model.DDLRoleList = await _ddlDAL.GetRoleListAsync(userId, cancellationToken, true);
            return model;
        }

        #endregion
    }
}
