using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using System;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Service
{
    public class LocationService : BaseService<ILocationModel>, ILocationService
    {
        LocationDAL _dal = null;
        DropDownListDAL _ddlDal = null;
        long userId = Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        #region
        public override async Task<ILocationModel> IndexAsync(HttpContext context, CancellationToken cancellationToken)
        {
            ILocationModel model = new LocationModel();
            await PoulateDropDownListAsync(model, cancellationToken);
            return model;
        }

        public async Task<ILocationModel> IndexAsync(HttpContext context, string orgId, CancellationToken cancellationToken)
        {
            _dal = new LocationDAL();
            ILocationModel model = await _dal.SelectByIDAsync(Convert.ToInt64(orgId), userId, cancellationToken);
            await PoulateDropDownListAsync(model, cancellationToken);
            return model;
        }

        public async Task<ILocationModel> SaveAsync(HttpContext context, ILocationModel model, CancellationToken cancellationToken)
        {
            _dal = new LocationDAL();

            model.UserId = userId;

            if (model.LocationId > 0)
            {
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(await _dal.UpdateAsync(model, cancellationToken));
                context.Items[Constant.FormTitle] = "EDIT Location";
            }
            else
            {
                model.LocationId = await _dal.SaveAsync(model, cancellationToken);

                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(model.LocationId);
                context.Items[Constant.FormTitle] = "ADD Location";
            }

            await PoulateDropDownListAsync(model, cancellationToken);
            return model;
        }

        private async Task PoulateDropDownListAsync(ILocationModel model, CancellationToken cancellationToken)
        {
            _ddlDal = new DropDownListDAL();
            model.CountryList = await _ddlDal.GetCountryListAsync(userId, cancellationToken, true);
            
        }

        #endregion

    }
}
