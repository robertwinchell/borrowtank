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
    public class WebApiUserSearchController : BaseSearchController<IWebApiUserSearchService>
    {
        IWebApiUserSearchService _service = null;

        public WebApiUserSearchController(IWebApiUserSearchService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Index(string UserId, CancellationToken cancellationToken)
        {
            IWebApiUserSearchViewModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> IndexAsync(WebApiUserSearchViewModel model, string UserId, CancellationToken cancellationToken)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> FilterUserSearch(SearchModel model, CancellationToken cancellationToken)
        {
            model.UserId = System.Convert.ToInt64(User.Identity.GetUserId());
            IWebApiUserSearchViewModel userModel = await _service.SearchAsync(HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            return PartialView("_UserGrid", userModel.UserList);
        }
    }
}