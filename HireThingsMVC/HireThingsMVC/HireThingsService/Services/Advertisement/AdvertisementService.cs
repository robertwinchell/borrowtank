using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public class AdvertisementService : BaseService<IAdvertisementModel>, IAdvertisementService
    {
        AdvertisementDAL _dal = null;
        DropDownListDAL _ddlDAL = null;
        long userId = Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());
        public AdvertisementService()
        {

            _ddlDAL = new DropDownListDAL();

        }
        public override async Task<IAdvertisementModel> IndexAsync(HttpContext context, CancellationToken cancellationToken)
        {
            IAdvertisementModel model = new AdvertisementModel();


            await PoulateDropDownListAsync(model, cancellationToken);

            return model;
        }

        public async Task<IAdvertisementModel> IndexAsync(HttpContext context, CancellationToken cancellationToken, string AdvertisementId)
        {
            _dal = new AdvertisementDAL();
            IAdvertisementModel model = await _dal.SelectByIDAsync(Convert.ToInt64(AdvertisementId), userId, cancellationToken);
            //await PoulateDropDownListAsync(model, cancellationToken);
            return model;
        }

        public async Task<IAdvertisementModel> SaveAsync(HttpContext context, IAdvertisementModel model, CancellationToken cancellationToken)
        {
            _dal = new AdvertisementDAL();

            model.UserId = userId;

            if (model.AdvertisementId > 0)
            {
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(await _dal.UpdateAsync(model, cancellationToken));
                context.Items[Constant.FormTitle] = "EDIT Advertisement";
            }
            else
            {
                model.AdvertisementId = await _dal.SaveAsync(model, cancellationToken);
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(model.AdvertisementId);
                context.Items[Constant.FormTitle] = "ADD Advertisement";
            }
            await PoulateDropDownListAsync(model, cancellationToken);
            return model;
        }
        private async Task<IAdvertisementModel> PoulateDropDownListAsync(IAdvertisementModel model, CancellationToken cancellationToken)
        {


             model.CategoryList = await _ddlDAL.GetCategoryListAsync(userId, cancellationToken, false);

            return model;
        }

    }
}
