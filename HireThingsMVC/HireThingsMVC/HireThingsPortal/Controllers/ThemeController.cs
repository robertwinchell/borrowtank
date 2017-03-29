using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using ASOL.HireThings.Service;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASOL.HireThings.Portal
{
    public class ThemeController : AuthorizedController<IThemeService>
    {
        IThemeService _service = null;

        public ThemeController(IThemeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            ViewData[Constant.FormTitle] = "ADD Theme";
            IThemeModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(ThemeModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                ViewData[Constant.CustomSuccessMessage] = Constant.CustomValidationErrorMessage;
                ViewData[Constant.QuerySuccess] = false;
                model = (ThemeModel) await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
                return View(model);
            }
            ModelState.Clear();
            model = (ThemeModel) await _service.SaveAsync(HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            ViewData[Constant.FormTitle] = HttpContext.Items[Constant.FormTitle];

            if (!Convert.ToBoolean(ViewData[Constant.QuerySuccess]))
            {
                ViewData[Constant.CustomSuccessMessage] = "Error : Record cannot be Saved. Make sure the 'Name' isn't being duplicated.";
                if (model.ThemeId == 0)
                    model.ThemeId = null;
            }
            else
            {
                ViewData[Constant.FormTitle] = "EDIT Theme";
            }
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> IndexEdit(CancellationToken cancellationToken)
        {
            return Redirect("~/ThemeSearch/Index");
        }

        [HttpPost]
        public async Task<ActionResult> IndexEdit(string hdnThemeId, CancellationToken cancellationToken)
        {
            if (System.Convert.ToInt64(hdnThemeId) > 0)
            {
                ViewData[Constant.FormTitle] = "EDIT Theme";
                IThemeModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, hdnThemeId, GetCanellationToken(cancellationToken));
                ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
                return View("Index", model);
            }
            else
            {
                return Redirect("~/ThemeSearch/Index");
            }
        }

    }
}