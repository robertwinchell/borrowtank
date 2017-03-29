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
    public class CountrySearchController : BaseSearchController<ICountrySearchService>
    {
        ICountrySearchService _service = null;

        public CountrySearchController(ICountrySearchService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Index(string CountryId,CancellationToken cancellationToken)
        {
           ICountrySearchViewModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(CountrySearchViewModel model, string CountryId)
        {
            return View(model);
        }
        
        [HttpPost]
        public async Task<ActionResult> FilterCountrySearch(SearchModel model, CancellationToken cancellationToken)
        {
            model.UserId = System.Convert.ToInt64(User.Identity.GetUserId());
            ICountrySearchViewModel CountryModel = await _service.SearchAsync(HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken), model);
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            return PartialView("_CountryGrid", CountryModel.CountryList);
        }
    }
}