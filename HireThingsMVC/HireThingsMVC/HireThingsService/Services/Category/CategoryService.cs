using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using System;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Service
{
    public class CategoryService : BaseService<ICategoryModel>, ICategoryService
    {
        CategoryDAL _dal = null;
        DropDownListDAL _ddlDAL = null;
        long userId = Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public CategoryService()
        {
            _dal = new CategoryDAL();
            _ddlDAL = new DropDownListDAL();
        }

        #region
        public async override Task<ICategoryModel> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            ICategoryModel model = new CategoryModel();
            model = await PopulateInitialValuesAsync(model, cancellationToken);
            return model;
        }

        public async Task<ICategoryModel> IndexAsync(HttpContext context, string CategoryId, CancellationToken cancellationToken)
        {
            ICategoryModel model = await _dal.SelectByIDAsync(Convert.ToInt64(CategoryId), userId, cancellationToken);
            model = await PopulateInitialValuesAsync(model, cancellationToken);
            return model;
        }

        public async Task<ICategoryModel> SaveAsync(HttpContext context, ICategoryModel model, CancellationToken cancellationToken)
        {
            model.UserId = userId;

            if (model.CategoryId > 0)
            {
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(await _dal.UpdateAsync(model, cancellationToken));
                context.Items[Constant.FormTitle] = "EDIT Category";
            }
            else
            {
                model.CategoryId = await _dal.SaveAsync(model, cancellationToken);
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(model.CategoryId);
                context.Items[Constant.FormTitle] = "ADD Category";
            }

            model = await PopulateInitialValuesAsync(model, cancellationToken);
            return model;
        }

        private async Task<ICategoryModel> PopulateInitialValuesAsync(ICategoryModel model, CancellationToken cancellationToken)
        {
           model.ThemeList = await _ddlDAL.GetThemeListAsync(userId, cancellationToken, true);
            
            return model;
        }

        #endregion
    }
}
