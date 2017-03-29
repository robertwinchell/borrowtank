using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
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
    public class WebApiUserController : AuthorizedController<IWebApiUserService>
    {
        IWebApiUserService _service = null;

        public WebApiUserController(IWebApiUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            ViewData[Constant.FormTitle] = "ADD WEB API USER";
            IWebApiUser model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(WebApiUser model, long ddlSystem, CancellationToken cancellationToken)
        {
            model.SystemId = ddlSystem;

            ModelState.Remove("SystemId");
            ModelState.Add("SystemId", new ModelState());
            ModelState.SetModelValue("SystemId", new ValueProviderResult(model.SystemId, model.SystemId.ToString(), null));          
            ModelState.Remove("Pin");
            ModelState.Remove("ConfirmPin");

            if (!ModelState.IsValid)
            {
                ViewData[Constant.CustomSuccessMessage] = Constant.CustomValidationErrorMessage;
                ViewData[Constant.QuerySuccess] = false;
                model = (WebApiUser)await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
                return View(model);
            }
            ModelState.Clear();
            model = (WebApiUser)await _service.SaveAsync(HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            ViewData[Constant.FormTitle] = HttpContext.Items[Constant.FormTitle];

            if (Convert.ToBoolean(ViewData[Constant.QuerySuccess]))
            {
                ViewData[Constant.FormTitle] = "EDIT WEB API USER";
            }
            else
            {
                ViewData[Constant.CustomSuccessMessage] = "Error: email is already associated with another user.";
            }

            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> IndexEdit(CancellationToken cancellationToken)
        {
            return Redirect("~/WebApiUserSearch/Index");
        }

        [HttpPost]
        public async Task<ActionResult> IndexEdit(string hdnWAUserId, CancellationToken cancellationToken)
        {
            if (System.Convert.ToInt64(hdnWAUserId) > 0)
            {
                //ViewData[Constant.HeaderTitle] = "User";
                ViewData[Constant.FormTitle] = "EDIT WEB API USER";
                IWebApiUser model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, Convert.ToInt64(hdnWAUserId), GetCanellationToken(cancellationToken));
                ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
                return View("Index", model);
            }
            else
            {
                return Redirect("~/WebApiUserSearch/Index");
            }
        }
        
    }
}