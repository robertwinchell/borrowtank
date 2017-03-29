using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using ASOL.HireThings.Portal.Controllers;
using ASOL.HireThings.Service;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASOL.HireThings.Portal
{
    // GET: Main
    public class HirePlanaController : PublicBaseController<IHirePlanaService>
    {
        IHirePlanaService _service = null;

        public HirePlanaController(IHirePlanaService service)
        {
            _service = service;
        }

       
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            IHirePlanaModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View("Index",model);
        }

    }
}