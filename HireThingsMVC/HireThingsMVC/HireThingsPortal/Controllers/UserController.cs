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
    public class UserController : AuthorizedController<IUserService>
    {
        IUserService _service = null;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
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

            if (!ModelState.IsValid)
            {
                ViewData[Constant.CustomSuccessMessage] = Constant.CustomValidationErrorMessage;
                ViewData[Constant.QuerySuccess] = false;
                model = (HireThingsUser) await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
                return View(model);
            }
            ModelState.Clear();
            model = (HireThingsUser) await _service.SaveAsync(HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            ViewData[Constant.FormTitle] = HttpContext.Items[Constant.FormTitle];
            if (Convert.ToBoolean(ViewData[Constant.QuerySuccess]))
            {
                ViewData[Constant.FormTitle] = "Edit User";
            }

            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> IndexEdit(CancellationToken cancellationToken)
        {
            return Redirect("~/UserSearch/Index");
        }

        [HttpPost]
        public async Task<ActionResult> IndexEdit(string hdnUserId, CancellationToken cancellationToken)
        {
            if (System.Convert.ToInt64(hdnUserId) > 0)
            {
                //ViewData[Constant.HeaderTitle] = "User";
                ViewData[Constant.FormTitle] = "Edit User";
                IHireThingsUser model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context,  Convert.ToInt64(hdnUserId), GetCanellationToken(cancellationToken));
                ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
                return View("Index", model);
            }
            else
            {
                return Redirect("~/UserSearch/Index");
            }
        }

        //
        // GET: /User/ProfileSettings
        [HttpGet]
        public async Task<ActionResult> ProfileSettings(CancellationToken cancellationToken)
        {
            long userId = Convert.ToInt64(User.Identity.GetUserId());
            ViewData[Constant.HeaderTitle] = "Profile Settings";
            IHireThingsUser model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, userId, GetCanellationToken(cancellationToken));
            return View("ProfileSettings", model);
        }

        //
        // POST: /User/ProfileSettings
        [HttpPost]
        public async Task<ActionResult> ProfileSettings(HireThingsUser model, CancellationToken cancellationToken)
        {
           
            TempUserDAL _dal = new TempUserDAL();

            ModelState.Remove("Pin");
            ModelState.Remove("ConfirmPin");
            ModelState.Remove("SecurityQuestionId");
            ModelState.Remove("SecurityAnswer");
            ModelState.Remove("ConfirmEmailId");

            if (ModelState.IsValid)
            {
                model.Id = User.Identity.GetUserId();
                await _service.ProfileUpdateAsync(this.HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
                ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
                return View(model);
            }

            // If ModelState.IsValid = False
            return RedirectToAction("Index?cancellationToken =" + cancellationToken);
        }
    }
}