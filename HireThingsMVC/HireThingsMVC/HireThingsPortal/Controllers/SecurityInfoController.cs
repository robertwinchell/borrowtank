using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using ASOL.HireThings.Service;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace ASOL.HireThings.Portal
{
    public class SecurityInfoController : AuthorizedController<ISecurityInfoService>
    {
        ISecurityInfoService _service;

        public SecurityInfoController(ISecurityInfoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            ViewData[Constant.FormTitle] = "ADD Security Information";

            var model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(SecurityInfoViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                ViewData[Constant.CustomSuccessMessage] = Constant.CustomValidationErrorMessage;
                ViewData[Constant.QuerySuccess] = false;
                model = (SecurityInfoViewModel) await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
                return View(model);
            }
            ModelState.Clear();
            model = (SecurityInfoViewModel) await _service.UpdateAsync(this.HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));

            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            ViewData[Constant.FormTitle] = HttpContext.Items[Constant.FormTitle];
            if (System.Convert.ToBoolean(ViewData[Constant.QuerySuccess]))

            {
                ViewData[Constant.FormTitle] = "EDIT Security Info";
            }
            if (!System.Convert.ToBoolean(ViewData[Constant.QuerySuccess]))
            {
                ViewData[Constant.CustomSuccessMessage] = "Error : Record cannot be Saved.";
            }
            return View(model);

        }

    }
}