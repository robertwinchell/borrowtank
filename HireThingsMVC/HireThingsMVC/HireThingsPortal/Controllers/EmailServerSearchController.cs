using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using ASOL.HireThings.Portal.Controllers;
using ASOL.HireThings.Portal.Utils;
using ASOL.HireThings.Service;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASOL.HireThings.Portal
{
    public class EmailServerSearchController : BaseSearchController<IEmailServerSearchService>
    {
        IEmailServerSearchService _service = null;

        public EmailServerSearchController(IEmailServerSearchService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            IEmailServerSearchViewModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(IEmailServerSearchViewModel model, CancellationToken cancellationToken)
        {
            return View(model);
        }
        
        [HttpPost]
        public async Task<ActionResult> FilterEmailServerSearch(SearchModel model, CancellationToken cancellationToken)
        {
            model.UserId = System.Convert.ToInt64(User.Identity.GetUserId());
            IEmailServerSearchViewModel emailServerSearchModel = await _service.SearchAsync(HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            return PartialView("_EmailServerGrid", emailServerSearchModel.EmailServerList);
        }
    }
}