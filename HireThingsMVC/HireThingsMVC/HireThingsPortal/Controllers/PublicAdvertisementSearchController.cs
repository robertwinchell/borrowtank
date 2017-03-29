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
    public class PublicAdvertisementSearchController : PublicBaseController<IAdvertisementSearchService>
    {
        IAdvertisementSearchService _service = null;

        public PublicAdvertisementSearchController(IAdvertisementSearchService service)
        {
            _service = service;
        }

       
        public async Task<ActionResult> Index(string searchterm, string themeid, string categoryid, CancellationToken cancellationToken)
        {
            IAdvertisementSearchViewModel model = await _service.IndexSearchAsync(searchterm, themeid, categoryid,this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View("Index",model);
        }

        

    }
}