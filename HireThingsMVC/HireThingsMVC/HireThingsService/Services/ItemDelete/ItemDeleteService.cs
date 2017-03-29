using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using System;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Service
{
    public class ItemDeleteService:BaseService<IItemDeleteModel>,IItemDeleteService
    {       
        ItemDeleteDAL _dal = null;
        long userId = Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public ItemDeleteService()
        {            
            
            _dal = new ItemDeleteDAL();
        }

        #region
        public async override Task<IItemDeleteModel> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            IItemDeleteModel model = new ItemDeleteModel();
            // model = PopulateInitialValues(model);
            return model;
        }

        public async Task<IItemDeleteModel> IndexAsync(HttpContext context, long ItemDeleteId, CancellationToken cancellationToken)
        {
            IItemDeleteModel model = new ItemDeleteModel();
            model.RecordId = ItemDeleteId;
            return model;
        }

        public async Task<IItemDeleteModel> SaveAsync(HttpContext context, IItemDeleteModel model, CancellationToken cancellationToken)
        {
            model.UserId = Convert.ToInt64(context.User.Identity.GetUserId());

            if (model.RecordId > 0)
            {

                context.Items[Constant.QuerySuccess] = Convert.ToBoolean( await _dal.SaveAsync(model, cancellationToken));
                //  context.Items[Constant.FormTitle] = "EDIT Mail List";
            }

            else
            {
                // Nothing to be done
            }
            return model;
        }
        #endregion

    }
}

