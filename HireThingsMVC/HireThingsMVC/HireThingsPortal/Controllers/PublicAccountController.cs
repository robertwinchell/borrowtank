using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using ASOL.HireThings.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASOL.HireThings.Portal
{
    // GET: Main
    public class PublicAccountController : PublicBaseController<IUserService>
    {
        IUserService _service = null;

        public PublicAccountController(IUserService service)
        {
            _service = service;
        }

       
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            IHireThingsUser model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(HireThingsUser model, long ddlCountry, long ddlLocation, long[] groupListbox, CancellationToken cancellationToken)
        {

            model.CountryId = ddlCountry;
            model.LocationId = ddlLocation;

            ModelState.Remove("CountryId");
            ModelState.Add("CountryId", new ModelState());
            ModelState.SetModelValue("CountryId", new ValueProviderResult(model.CountryId, model.CountryId.ToString(), null));
            ModelState.Remove("LocationId");
            ModelState.Add("LocationId", new ModelState());
            ModelState.SetModelValue("LocationId", new ValueProviderResult(model.LocationId, model.LocationId.ToString(), null));
            ModelState.Remove("Pin");
            ModelState.Remove("ConfirmPin");
            ModelState.Remove("IsActive");
            ModelState.Remove("RoleId");

            if (!ModelState.IsValid)
            {
                ViewData[Constant.CustomSuccessMessage] = Constant.CustomValidationErrorMessage;
                ViewData[Constant.QuerySuccess] = false;
                model = (HireThingsUser)await _service.IndexPublicAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
                return View(model);
            }
            model.Id = "0";
            ModelState.Clear();
            model = (HireThingsUser)await _service.SaveAsync(HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            ViewData[Constant.FormTitle] = HttpContext.Items[Constant.FormTitle];
            if (Convert.ToBoolean(ViewData[Constant.QuerySuccess]))
            {
                ViewData[Constant.FormTitle] = "Edit User";
            }

            return View(model);
        }

    }    
}