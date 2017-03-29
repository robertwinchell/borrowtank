using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASOL.HireThings.Model;
using ASOL.HireThings.Service;
using System.Threading.Tasks;
using ASOL.HireThings.Core;
using System.Threading;

namespace ASOL.HireThings.Portal
{
    public class WebApiRoleController : AuthorizedController<IWebApiRoleService>
    {
        IWebApiRoleService _service;

        public WebApiRoleController(IWebApiRoleService service)
        {
            _service = service;
        }        

        [HttpGet]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            ViewData[Constant.FormTitle] = "ADD Web API Role";
            IWebApiRoleModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(WebApiRoleModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                ViewData[Constant.CustomSuccessMessage] = Constant.CustomValidationErrorMessage;
                ViewData[Constant.QuerySuccess] = false;
                model = (WebApiRoleModel)await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
                return View(model);
            }



            ModelState.Clear();
            model = (WebApiRoleModel)await _service.SaveAsync(this.HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            ViewData[Constant.FormTitle] = HttpContext.Items[Constant.FormTitle];

            if (!Convert.ToBoolean(ViewData[Constant.QuerySuccess]))
            {
                ViewData[Constant.CustomSuccessMessage] = "Error : Record cannot be Saved. Make sure the 'Name' isn't being duplicated.";
                if (model.WARoleId == 0)
                    model.WARoleId = null;
            }
            else
            {
                ViewData[Constant.FormTitle] = "EDIT Role";
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> IndexEdit(CancellationToken cancellationToken)
        {
            return Redirect("~/WebApiRoleSearch/Index");
        }

        [HttpPost]
        public async Task<ActionResult> IndexEdit(long hdWARoleId, CancellationToken cancellationToken)
        {
            if (hdWARoleId > 0)
            {
                ViewData[Constant.FormTitle] = "EDIT Web API Role";
                IWebApiRoleModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, hdWARoleId, GetCanellationToken(cancellationToken));
                ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
                return View("Index", model);
            }
            else
            {
                return Redirect("~/WebApiRoleSearch/Index");
            }
        }
    }
}