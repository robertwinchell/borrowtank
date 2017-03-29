using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using ASOL.HireThings.Service;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASOL.HireThings.Portal
{
    public class SecurityQuestionController : AuthorizedController<ISecurityQuestionService>
    {
        ISecurityQuestionService _service = null;

        public SecurityQuestionController(ISecurityQuestionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            ViewData[Constant.FormTitle] = "ADD Security Question";
            ISecurityQuestionModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(SecurityQuestionModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                ViewData[Constant.CustomSuccessMessage] = Constant.CustomValidationErrorMessage;
                ViewData[Constant.QuerySuccess] = false;
                model = (SecurityQuestionModel) await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
                return View(model);
            }
            ModelState.Clear();
            model = (SecurityQuestionModel) await _service.SaveAsync(this.HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            ViewData[Constant.FormTitle] = HttpContext.Items[Constant.FormTitle];
           
            if (System.Convert.ToBoolean(ViewData[Constant.QuerySuccess]))

            {
                ViewData[Constant.FormTitle] = "EDIT Security Question";
            }
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> IndexEdit(CancellationToken cancellationToken)
        {
            return Redirect("~/SecurityQuestionSearch/Index");
        }

        [HttpPost]
        public async Task<ActionResult> IndexEdit(long hdSecurityQuestionId, CancellationToken cancellationToken)
        {
            if (hdSecurityQuestionId > 0)
            {
                ViewData[Constant.FormTitle] = "EDIT Security Question";
                ISecurityQuestionModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, hdSecurityQuestionId, GetCanellationToken(cancellationToken));
                ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
                return View("Index", model);
            }
            else
            {
                return Redirect("~/SecurityQuestionSearch/Index");
            }
        }
    }
}