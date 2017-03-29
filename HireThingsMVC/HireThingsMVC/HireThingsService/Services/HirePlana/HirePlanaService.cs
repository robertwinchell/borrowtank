using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public class HirePlanaService : BaseService<IHirePlanaModel>, IHirePlanaService
    {
        HirePlanaDAL _dal = null;
        DropDownListDAL _ddlDAL = null;
        long userId = Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());
        public HirePlanaService()
        {

            _ddlDAL = new DropDownListDAL();

        }
        public override async Task<IHirePlanaModel> IndexAsync(HttpContext context, CancellationToken cancellationToken)
        {
            IHirePlanaModel model = new HirePlanaModel();


            await PoulateDropDownListAsync(model, cancellationToken);

            return model;
        }

        public async Task<IHirePlanaModel> IndexAsync(HttpContext context, CancellationToken cancellationToken, string HirePlanaId)
        {
            _dal = new HirePlanaDAL();
            IHirePlanaModel model = await _dal.SelectByIDAsync(Convert.ToInt64(HirePlanaId), userId, cancellationToken);
            await PoulateDropDownListAsync(model, cancellationToken);
            return model;
        }

        public async Task<IHirePlanaModel> SaveAsync(HttpContext context, IHirePlanaModel model, CancellationToken cancellationToken)
        {
            _dal = new HirePlanaDAL();

            model.UserId = userId;

            if (model.HirePlanaId > 0)
            {
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(await _dal.UpdateAsync(model, cancellationToken));
                context.Items[Constant.FormTitle] = "EDIT HirePlana";
            }
            else
            {
                model.HirePlanaId = await _dal.SaveAsync(model, cancellationToken);
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(model.HirePlanaId);
                context.Items[Constant.FormTitle] = "ADD HirePlana";
            }
            await PoulateDropDownListAsync(model, cancellationToken);
            return model;
        }
        private async Task<IHirePlanaModel> PoulateDropDownListAsync(IHirePlanaModel model, CancellationToken cancellationToken)
        {


           //  model.CategoryList = await _ddlDAL.GetCategoryListAsync(userId, cancellationToken, false);

            return model;
        }

    }
}
