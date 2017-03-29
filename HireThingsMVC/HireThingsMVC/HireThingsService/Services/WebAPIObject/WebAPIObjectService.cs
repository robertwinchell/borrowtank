using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using System;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Service
{
    public  class WebApiObjectService : BaseService<IWebApiObjectModel>, IWebApiObjectService
    {
        WebApiObjectDAL _dal = null;
        DropDownListDAL _ddlDal = null;
        long userId = Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public WebApiObjectService()
        {
            _dal = new WebApiObjectDAL();
            _ddlDal = new DropDownListDAL();
        }

        #region
        public async override Task<IWebApiObjectModel> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            IWebApiObjectModel model = new WebApiObjectModel();
            model = await PopulateInitialValuesAsync(model, cancellationToken);
            return model;
        }

        public async Task<IWebApiObjectModel> IndexAsync(System.Web.HttpContext context, long objectId, CancellationToken cancellationToken)
        {
            IWebApiObjectModel model = new WebApiObjectModel();
            model = await _dal.SelectByIDAsync(objectId, userId, cancellationToken);
            await PopulateInitialValuesAsync(model, cancellationToken);
            return model;
        }

        public async Task<IWebApiObjectModel> SaveAsync(System.Web.HttpContext context, IWebApiObjectModel model, CancellationToken cancellationToken)
        {
            model.UserId = Convert.ToInt64(context.User.Identity.GetUserId());

            if (model.WAObjectId > 0)
            {
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(await _dal.UpdateAsync(model, cancellationToken));
                context.Items[Constant.FormTitle] = "EDIT Web API Object";
            }
            else
            {
                model.WAObjectId =  await _dal.SaveAsync(model, cancellationToken);
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(model.WAObjectId);
                context.Items[Constant.FormTitle] = "ADD Web API Object";
            }
            await PopulateInitialValuesAsync(model, cancellationToken);

            return model;
        }

        private async Task<IWebApiObjectModel> PopulateInitialValuesAsync(IWebApiObjectModel model, CancellationToken cancellationToken)
        {
            model.ModuleList = await _ddlDal.GetModuleListAsync(userId, cancellationToken );

            // PopulateParentObjectDropDownList(model);
            //model.ObjectLevelList[0].Identifier = "ddlObjectLevel";
            //model.ObjectLevelList[0].SavedValue = model.ObjectLevel;
            //model.ParentObjectList[0].Identifier = "ddlParentObject";
            //model.ParentObjectList[0].SavedValue = model.ParentObjectId;

            return model;
        }

        #endregion
    }
}
