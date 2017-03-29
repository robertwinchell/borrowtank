using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public class WebApiUserService : BaseService<IWebApiUser>, IWebApiUserService
    {
        WebApiUserDAL _dal = null;
        DropDownListDAL _ddlDAL = null;
        long _userId;
        long userId = Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());
        
        public WebApiUserService()
        {
            _dal = new WebApiUserDAL();
            _ddlDAL = new DropDownListDAL();
        }     

        
        #region  Async Region
        public async override Task<IWebApiUser> IndexAsync(HttpContext context, CancellationToken cancellationToken)
        {
            IWebApiUser model = new WebApiUser(); //{ WAUserId = Convert.ToString(userId) };
            model = await PopulateInitialValuesAsync(model, cancellationToken);
            return model;
        }

        public async Task<IWebApiUser> IndexAsync(System.Web.HttpContext context, IWebApiUser model, CancellationToken cancellationToken)
        {
            if (model == null)
                model = new WebApiUser();
            model = await PopulateInitialValuesAsync(model, cancellationToken);
            return model;
        }

        public  async Task<IWebApiUser> IndexAsync(HttpContext context, long userId, CancellationToken cancellationToken)
        {
            IWebApiUser model = await _dal.SelectByIDAsync(userId, userId, cancellationToken);
            model = await PopulateInitialValuesAsync(model, cancellationToken);
            return model;
        }

        public async Task<IWebApiUser> SaveAsync(HttpContext context, IWebApiUser model, CancellationToken cancellationToken)
        {
            model.UserId = userId;

            if (model.WAUserId != "0" & model.WAUserId != null)
            {
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(await _dal.UpdateAsync(model, cancellationToken));
                context.Items[Constant.FormTitle] = "EDIT Web API User";
            }
            else
            {
                model.WAUserId = Convert.ToString(await _dal.SaveAsync(model, cancellationToken));
                context.Items[Constant.QuerySuccess] = model.WAUserId == "0" ? false : true; //Convert.ToBoolean(model.WAUserId);
                context.Items[Constant.FormTitle] = "ADD Web API User";
            }

           await PopulateInitialValuesAsync(model, cancellationToken);
            return model;
        }        

        private async Task<IWebApiUser> PopulateInitialValuesAsync(IWebApiUser model, CancellationToken cancellationToken)
        {
            model.DeviceGropList = new ListBoxSettingsModel();
            
            try
            {
                _userId = Convert.ToInt64(model.WAUserId);
            }
            catch (Exception ex)
            {
                _userId = 0;
            }

            model.SystemList = await _ddlDAL.GetSystemListAsync(userId, cancellationToken, true);
            if (model.SystemId > 0)
                model.OrganizationList =  await _ddlDAL.GetOrganizationListAsync(userId, model.SystemId, cancellationToken, true);
            else
                model.OrganizationList = await _ddlDAL.GetOrganizationListAsync(userId, -1, cancellationToken, true);

            model.TimeZoneList = await _ddlDAL.GetTimeZoneListAsync(userId, cancellationToken);            
            model.RoleList = await _ddlDAL.GetWebAPIRoleListAsync(userId, cancellationToken, true);            

            //model.DeviceGropList.RequestedListBoxItem = await _ddlDAL.GetAssignedDeviceGroupListAsync(_userId, cancellationToken, 0, userId);
            //model.DeviceGropList.AvailableListBoxItem = await _ddlDAL.GetUnassignedDeviceGroupListAsync(_userId, cancellationToken, 0, userId);

            model.SystemList[0].Identifier = "ddlSystem";
            model.SystemList[0].SavedValue = model.SystemId;
            model.OrganizationList[0].Identifier = "ddlOrganization";
            model.OrganizationList[0].SavedValue = model.OrganizationId;




            return model;
        }
        
        #endregion
    }
}
