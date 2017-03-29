using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using ASOL.HireThings.Service;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASOL.HireThings.Portal
{
    // GET: Main
    public class PublicAdvertisementController : BaseController<IAdvertisementService>
    {
        IAdvertisementService _service = null;
        public PublicAdvertisementController(IAdvertisementService service)
        {
            _service = service;
        }
                       
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            IAdvertisementModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View("~/Views/PublicAdvertisement/Index.cshtml",model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> Detail(string advertisementId, CancellationToken cancellationToken)
        {
            IAdvertisementModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken), Convert.ToString(advertisementId));
            return View("~/Views/PublicAdvertisement/Detail.cshtml",model);
        }

    }
}