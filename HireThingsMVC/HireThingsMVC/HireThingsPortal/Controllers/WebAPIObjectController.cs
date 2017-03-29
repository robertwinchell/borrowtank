using System.Web.Mvc;
using ASOL.HireThings.Service;
using System.Threading.Tasks;
using System.Threading;
using ASOL.HireThings.Core;
using ASOL.HireThings.Model;

namespace ASOL.HireThings.Portal
{
    public class WebApiObjectController : AuthorizedController<IWebApiObjectService>
    {
        private IWebApiObjectService _service;

        public WebApiObjectController(IWebApiObjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            ViewData[Constant.FormTitle] = "ADD Web API Object";
            IWebApiObjectModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(WebApiObjectModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                ViewData[Constant.CustomSuccessMessage] = Constant.CustomValidationErrorMessage;
                ViewData[Constant.QuerySuccess] = false;
                model = (WebApiObjectModel)await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
                return View(model);
            }
            ModelState.Clear();
            model = (WebApiObjectModel)await _service.SaveAsync(HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            ViewData[Constant.FormTitle] = HttpContext.Items[Constant.FormTitle];
            if (System.Convert.ToBoolean(ViewData[Constant.QuerySuccess]))
            {
                ViewData[Constant.FormTitle] = "EDIT Web API Object";
            }
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> IndexEdit(CancellationToken cancellationToken)
        {
            return Redirect("~/WebApiObjectSearch/Index");
        }

        [HttpPost]
        public async Task<ActionResult> IndexEdit(long hdWAObjectId, CancellationToken cancellationToken)
        {
            if (hdWAObjectId > 0)
            {
                ViewData[Constant.FormTitle] = "EDIT Web API Object";
                IWebApiObjectModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, hdWAObjectId, GetCanellationToken(cancellationToken));
                ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
                return View("Index", model);
            }
            else
            {
                return Redirect("~/WebApiObjectSearch/Index");
            }
        }
    }
}