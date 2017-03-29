using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using System;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Service
{
    public  class ObjectService : BaseService<IObjectModel>,  IObjectService 
    {
        ObjectDAL _dal = null;
        DropDownListDAL _ddlDal = null;
        long userId = Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public ObjectService()
        {
            _dal = new ObjectDAL();
            _ddlDal = new DropDownListDAL();
        }

        #region
        public async override Task<IObjectModel> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            IObjectModel model = new ObjectModel() { ObjectLevel = -1 };
            model = await PopulateInitialValuesAsync(model, cancellationToken);
            return model;
        }

        public async Task<IObjectModel> IndexAsync(System.Web.HttpContext context, long objectId, CancellationToken cancellationToken)
        {
            IObjectModel model = new ObjectModel();
            model = await _dal.SelectByIDAsync(objectId, userId, cancellationToken);
            await PopulateInitialValuesAsync(model, cancellationToken);
            return model;
        }

        public async Task<IObjectModel> SaveAsync(System.Web.HttpContext context, IObjectModel model, CancellationToken cancellationToken)
        {
            model.UserId = Convert.ToInt64(context.User.Identity.GetUserId());

            if (model.ObjectId > 0)
            {
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(await _dal.UpdateAsync(model, cancellationToken));
                context.Items[Constant.FormTitle] = "EDIT Object";
            }
            else
            {
                model.ObjectId =  await _dal.SaveAsync(model, cancellationToken);
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(model.ObjectId);
                context.Items[Constant.FormTitle] = "ADD Object";
            }
            await PopulateInitialValuesAsync(model, cancellationToken);

            return model;
        }

        private async Task<IObjectModel> PopulateInitialValuesAsync(IObjectModel model, CancellationToken cancellationToken)
        {
            model.ModuleList = await _ddlDal.GetModuleListAsync(userId, cancellationToken );
            model.ObjectLevelList = await _ddlDal.GetObjectLevelListAsync(userId, cancellationToken);
            model.ParentObjectList = await _ddlDal.GetParentObjectListAsync(userId, cancellationToken, -1,  true);

            // PopulateParentObjectDropDownList(model);
            model.ObjectLevelList[0].Identifier = "ddlObjectLevel";
            model.ObjectLevelList[0].SavedValue = model.ObjectLevel;
            model.ParentObjectList[0].Identifier = "ddlParentObject";
            model.ParentObjectList[0].SavedValue = model.ParentObjectId;

            return model;
        }

        #endregion
    }
}
