using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using ASOL.HireThings.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASOL.HireThings.Portal
{
    public class WebAPIRoleObjectController : AuthorizedController<IWebAPIRoleObjectService>
    {
        IWebAPIRoleObjectService _service = null;

        public WebAPIRoleObjectController(IWebAPIRoleObjectService service)
        {
            _service = service;
        }

        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            IWebAPIRoleObjectViewModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            ViewData[Constant.FormTitle] = "Access Rights";
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(WebAPIRoleObjectViewModel smodel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                ViewData[Constant.CustomSuccessMessage] = Constant.CustomValidationErrorMessage;
                ViewData[Constant.QuerySuccess] = false;
                smodel = (WebAPIRoleObjectViewModel)await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
                return View(smodel);
            }
            ModelState.Clear();
            IWebAPIRoleObjectViewModel model = await _service.SaveAsync(this.HttpContext.ApplicationInstance.Context, smodel, GetCanellationToken(cancellationToken));
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            ViewData[Constant.FormTitle] = HttpContext.Items[Constant.FormTitle];
            if (!Convert.ToBoolean(ViewData[Constant.QuerySuccess])) {
                ViewData[Constant.CustomSuccessMessage] = "Error : Record cannot  be Saved. Make sure the 'Role Name' isn't being duplicated.";
            }
            return View(model);
        }

        [HttpPost]     
        public async Task<ActionResult> FilterRoleObject(long RoleId, SearchModel searchModel, CancellationToken cancellationToken)
        {
            IWebAPIRoleObjectViewModel roleObjectModel = await _service.IndexAsync(HttpContext.ApplicationInstance.Context, RoleId, searchModel, GetCanellationToken(cancellationToken));
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            return PartialView("_RoleObjectGrid", roleObjectModel);
        }
    }
}