using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using ASOL.HireThings.Portal.Controllers;
using ASOL.HireThings.Portal.Utils;
using ASOL.HireThings.Service;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace ASOL.HireThings.Portal
{
    public class LocationSearchController : BaseSearchController<ILocationSearchService>
    {
        ILocationSearchService _service = null;

        public LocationSearchController(ILocationSearchService service)
        {
            _service = service;
        }
       
        [HttpGet]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            ILocationSearchViewModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View(model);
        }
     
        [HttpPost]
        public async Task<ActionResult> FilterLocationSearch(SearchModel model, CancellationToken cancellationToken)
        {
            model.UserId = System.Convert.ToInt64(User.Identity.GetUserId());
            var cpage=model.CPage;
            var tpage = model.TPage;
            ILocationSearchViewModel orgModel = await _service.SearchAsync(HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            return PartialView("_LocationGrid", orgModel.LocationList);
        }
    }

    public class TestModel {
        public int CPage { get; set; }
        public int TPage { get; set; }
    }
}