using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using ASOL.HireThings.Portal.Controllers;
using ASOL.HireThings.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASOL.HireThings.Portal
{
    public class WebApiRoleSearchController : BaseSearchController<IWebApiRoleSearchService>
    {
        IWebApiRoleSearchService _service = null;

        public WebApiRoleSearchController(IWebApiRoleSearchService service)
        {
            _service = service;
        }

        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            IWebApiRoleSearchViewModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> FilterRoleSearch(SearchModel model, CancellationToken cancellationToken)
        {
            model.UserId = System.Convert.ToInt64(User.Identity.GetUserId());
            IWebApiRoleSearchViewModel roleSerachModel = await _service.SearchAsync(HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            return PartialView("_RoleGrid", roleSerachModel.RoleList);
        }
    }
}