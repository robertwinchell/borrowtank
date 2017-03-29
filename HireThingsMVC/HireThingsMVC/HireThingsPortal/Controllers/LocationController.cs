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
    public class LocationController : AuthorizedController<ILocationService>
    {
        ILocationService _service;

        public LocationController(ILocationService service)
        {
            _service = service;
        }

    
        #region 
        [HttpGet]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            ViewData[Constant.FormTitle] = "ADD Location";
            ILocationModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(LocationModel model, CancellationToken cancellationToken)
        {
            
            if (!ModelState.IsValid)
            {
                ViewData[Constant.CustomSuccessMessage] = Constant.CustomValidationErrorMessage;
                ViewData[Constant.QuerySuccess] = false;
                model = (LocationModel)await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
                return View(model);
            }
            ModelState.Clear();
            model = (LocationModel) await _service.SaveAsync(this.HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            ViewData[Constant.FormTitle] = HttpContext.Items[Constant.FormTitle];

            if (!Convert.ToBoolean(ViewData[Constant.QuerySuccess]))
            {
                if (model.LocationId == 0)
                    model.LocationId = null;
                ViewData[Constant.CustomSuccessMessage] = "Error : Record cannot  be Saved. Make sure the 'Location Name' isn't being duplicated.";
            }
            else
            {
                ViewData[Constant.FormTitle] = "EDIT Location";
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult IndexEdit(CancellationToken cancellationToken)
        {
            return Redirect("~/LocationSearch/Index");
        }

        [HttpPost]
        public async Task<ActionResult> IndexEdit(string hdnLocationId, CancellationToken cancellationToken)
        {
            if (System.Convert.ToInt64(hdnLocationId) > 0)
            {
                ViewData[Constant.FormTitle] = "EDIT Location";
                ILocationModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, hdnLocationId, GetCanellationToken(cancellationToken));
                ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
                return View("Index", model);
            }
            else
            {
                return Redirect("~/LocationSearch/Index");
            }

        }
        #endregion
    }
}