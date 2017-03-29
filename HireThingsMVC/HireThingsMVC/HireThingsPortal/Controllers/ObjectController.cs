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
    public class ObjectController : BaseController<IObjectService>
    {
        IObjectService _service = null;

        public ObjectController(IObjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            ViewData[Constant.FormTitle] = "ADD Object";
            IObjectModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(ObjectModel model, int ddlObjectLevel, long ddlParentObject, CancellationToken cancellationToken)
        {

            model.ObjectLevel = ddlObjectLevel;
            model.ParentObjectId = ddlParentObject;
            ModelState.Remove("ObjectLevel");
            ModelState.Add("ObjectLevel", new ModelState());
            ModelState.SetModelValue("ObjectLevel", new ValueProviderResult(model.ObjectLevel, model.ObjectLevel.ToString(), null));
            ModelState.Remove("ParentObjectId");
            ModelState.Add("ParentObjectId", new ModelState());
            ModelState.SetModelValue("ParentObjectId", new ValueProviderResult(model.ParentObjectId, model.ParentObjectId.ToString(), null));

            if (!ModelState.IsValid)
            {
                ViewData[Constant.CustomSuccessMessage] = Constant.CustomValidationErrorMessage;
                ViewData[Constant.QuerySuccess] = false;
                model = (ObjectModel)await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
                return View(model);
            }
            ModelState.Clear();
            model = (ObjectModel)await _service.SaveAsync(HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            ViewData[Constant.FormTitle] = HttpContext.Items[Constant.FormTitle];
            if (System.Convert.ToBoolean(ViewData[Constant.QuerySuccess]))
            {
                ViewData[Constant.FormTitle] = "EDIT Object";
            }
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> IndexEdit(CancellationToken cancellationToken)
        {
            return Redirect("~/ObjectSearch/Index");
        }

        [HttpPost]
        public async Task<ActionResult> IndexEdit(long hdObjectId, CancellationToken cancellationToken)
        {
            if (hdObjectId > 0)
            {
                ViewData[Constant.FormTitle] = "EDIT Object";
                IObjectModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, hdObjectId, GetCanellationToken(cancellationToken));
                ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
                return View("Index", model);
            }
            else
            {
                return Redirect("~/ObjectSearch/Index");
            }
        }


    }
}