using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using ASOL.HireThings.Portal.Controllers;
using ASOL.HireThings.Service;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASOL.HireThings.Portal
{
    public class CategorySearchController : BaseSearchController<ICategorySearchService>
    {
        ICategorySearchService _service = null;

        public CategorySearchController(ICategorySearchService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            ICategorySearchViewModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(ICategorySearchViewModel model, CancellationToken cancellationToken)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> FilterCategorySearch(SearchModel model, CancellationToken cancellationToken)
        {
            model.UserId = System.Convert.ToInt64(User.Identity.GetUserId());
            ICategorySearchViewModel CategoryModel = await _service.SearchAsync(HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];

            return PartialView("_CategoryGrid", CategoryModel.CategoryList);
        }
    }
}