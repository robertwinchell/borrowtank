using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Service
{
    public class MainService : BaseService<IMainModel> , IMainService
    {
        DropDownListDAL _ddlDAL = null;
        long userId = Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());
        public MainService()
        {
           
            _ddlDAL = new DropDownListDAL();

        }
        public async override Task<IMainModel> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            IMainModel model = new MainModel();
            model = await PopulateInitialValuesAsync(model, cancellationToken);
            return model;
        }
        private async Task<IMainModel> PopulateInitialValuesAsync(IMainModel model, CancellationToken cancellationToken)
        {
            AdvertisementDAL _adDAL = new AdvertisementDAL();
            model.categoryList = await _ddlDAL.GetCategoryListForHomePageAsync(userId, cancellationToken, false);
            model.themeList = await _ddlDAL.GetThemeListAsync(userId, cancellationToken, false);
            model.advertisementList= await _adDAL.SelectAllForHomePageAsync(cancellationToken);
            return model;
        }

    }
}
