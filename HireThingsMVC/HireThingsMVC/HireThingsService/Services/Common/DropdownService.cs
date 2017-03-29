using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Service
{
    public class DropdownService : BaseService<IDropdownModel>, IDropdownService
    {

        DropDownListDAL _ddlDAL = null;
        long userId = System.Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public DropdownService()
        {
            
            _ddlDAL = new DropDownListDAL();
          
        }

        public async override Task<IDropdownModel> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

         #region 
        public async Task<IDropdownModel> PopulateLocationDropDownListAsync(IDropdownModel model, CancellationToken cancellationToken)
        {
            model.DropdownList = await _ddlDAL.GetLocationListAsync(0, model.CountryId, cancellationToken, true);
            var res = model.DropdownList.Find(item => item.Value == model.LocationId.ToString());
            if (res != null)
                model.DropdownList[0].SavedValue = model.LocationId;
            return model;
        }

  
        public async Task<IDropdownModel> PopulateParentObjectDropDownListAsync(IDropdownModel model, CancellationToken cancellationToken)
        {
            model.DropdownList = await _ddlDAL.GetParentObjectListAsync(userId, cancellationToken, model.ObjectLevel );
            var res = model.DropdownList.Find(item => item.Value == model.ParentObjectId.ToString());
            if (res != null)
                model.DropdownList[0].SavedValue = model.ParentObjectId;
            return model;
        }

        public async Task<IDropdownModel> PopulateParentObjectLevelDropDownListAsync(IDropdownModel model, CancellationToken cancellationToken)
        {
            model.DropdownList = await _ddlDAL.PopulateParentObjectLevelDropDownListAsync(userId, cancellationToken, model.OrganizationLevelId, model.SystemId, true);
            var res = model.DropdownList.Find(item => item.Value == model.ParentObjectId.ToString());
            if (res != null)
                model.DropdownList[0].SavedValue = model.ParentObjectId;
            return model;
        }
        
        #endregion
    }
}
