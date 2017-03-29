using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using System;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Threading;

namespace ASOL.HireThings.Service
{
    public class CountryService : BaseService<ICountryModel>, ICountryService
    {
        CountryDAL _dal = null;
        DropDownListDAL _ddlDal = null;
        long userId = Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        #region async service
        public override async Task<ICountryModel> IndexAsync(HttpContext context, CancellationToken cancellationToken)
        {
            ICountryModel model = new CountryModel();


            await PoulateDropDownListAsync(model, cancellationToken);

            return model;
        }

        public async Task<ICountryModel> IndexAsync(HttpContext context, CancellationToken cancellationToken, string CountryId)
        {
            _dal = new CountryDAL();
            ICountryModel model = await _dal.SelectByIDAsync(Convert.ToInt64(CountryId), userId, cancellationToken);
            await PoulateDropDownListAsync(model, cancellationToken);
            return model;
        }

        public async Task<ICountryModel> SaveAsync(HttpContext context, ICountryModel model, CancellationToken cancellationToken)
        {
            _dal = new CountryDAL();

            model.UserId = userId;

            if (model.CountryId > 0)
            {
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(await _dal.UpdateAsync(model, cancellationToken));
                context.Items[Constant.FormTitle] = "EDIT Country";
            }
            else
            {
                model.CountryId = await _dal.SaveAsync(model, cancellationToken);
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(model.CountryId);
                context.Items[Constant.FormTitle] = "ADD Country";
            }
            await PoulateDropDownListAsync(model, cancellationToken);
            return model;
        }
        private async Task PoulateDropDownListAsync(ICountryModel model, CancellationToken cancellationToken)
        {
            _ddlDal = new DropDownListDAL();
           

        }

        #endregion

    }
}
